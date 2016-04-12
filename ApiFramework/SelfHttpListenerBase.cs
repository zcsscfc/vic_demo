using System;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ApiFramework
{
    public abstract class SelfHttpListenerBase
    {
        private readonly AutoResetEvent _listenForNextRequest = new AutoResetEvent(false);
        private readonly DateTime _startTime;

        private bool _disposed;
        protected bool IsStarted;
        protected HttpListener Listener;

        private SelfHttpListenerBase()
        {
            _startTime = DateTime.UtcNow;
            Init();
        }

        protected SelfHttpListenerBase(params Assembly[] assembliesWithServices)
            : this()
        {
            Config.RegisterRequestPath(assembliesWithServices);
        }

        public static SelfHttpListenerBase Instance { get; protected set; }

        public ApiConfig Config { get; private set; }

        private bool IsListening
        {
            get { return IsStarted && Listener != null && Listener.IsListening; }
        }

        public void Init()
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("SelfHttpListener.Instance has already been set");
            }
            Instance = this;
            Config = ApiConfig.GetInstance();
        }

        public virtual void Start(string urlBase)
        {
            if (IsStarted)
            {
                return;
            }

            if (Listener == null)
            {
                Listener = new HttpListener();
            }

            Listener.Prefixes.Add(urlBase);
            IsStarted = true;
            Listener.Start();
            ThreadPool.QueueUserWorkItem(Listen);
        }

        private void Listen(object state)
        {
            while (IsListening)
            {
                if (Listener == null) return;

                try
                {
                    Listener.BeginGetContext(ListenerCallBack, Listener);
                    _listenForNextRequest.WaitOne();
                }
                catch (Exception ex)
                {
                    return;
                }
                if (Listener == null) return;
            }
        }

        private void ListenerCallBack(IAsyncResult asyncResult)
        {
            var listener = asyncResult.AsyncState as HttpListener;
            HttpListenerContext context = null;
            if (listener == null) return;

            try
            {
                if (!IsListening)
                {
                    return;
                }
                context = listener.EndGetContext(asyncResult);
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                _listenForNextRequest.Set();
            }

            try
            {
                ProcessRequest(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                var buffer = Encoding.UTF8.GetBytes(ex.Message);
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Flush();
                context.Response.Close();
            }
        }

        public void Stop()
        {
            if (Listener == null) return;
            try
            {
                Listener.Close();
            }
            catch (HttpListenerException ex)
            {
            }
            IsStarted = false;
            Listener = null;
        }

        protected abstract void ProcessRequest(HttpListenerContext context);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            lock (this)
            {
                if (_disposed) return;
                if (disposing)
                {
                    Stop();
                    Instance = null;
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
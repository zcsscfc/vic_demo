﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiFramework
{
    public abstract class SelfHttpListenerBase
    {
        protected HttpListener Listener;
        protected bool IsStarted = false;

        public static SelfHttpListenerBase Instance { get; protected set; }
        private readonly DateTime _startTime;
        private readonly AutoResetEvent _listenForNextRequest = new AutoResetEvent(false);

        private ApiConfig _apiConfig;
        public ApiConfig Config
        {
            get
            {
                return this._apiConfig;
            }
            private set
            {
                _apiConfig = value;
            }
        }
        private SelfHttpListenerBase()
        {
            this._startTime = DateTime.UtcNow;
            Init();
        }

        protected SelfHttpListenerBase(params Assembly[] assembliesWithServices)
            : this()
        {
            _apiConfig.RegisterRequestPath(assembliesWithServices);
        }

        public void Init()
        {
            if (Instance != null)
            {
                throw new InvalidOperationException("SelfHttpListener.Instance has already been set");
            }
            Instance = this;
            _apiConfig = new ApiConfig();
        }

        private bool IsListening
        {
            get { return this.IsStarted && this.Listener != null && this.Listener.IsListening; }
        }
        public virtual void Start(string urlBase)
        {
            if (this.IsStarted)
            {
                return;
            }

            if (this.Listener == null)
            {
                this.Listener = new HttpListener();
            }

            this.Listener.Prefixes.Add(urlBase);
            this.IsStarted = true;
            this.Listener.Start();
            ThreadPool.QueueUserWorkItem(Listen);
        }

        private void Listen(object state)
        {
            while (this.IsListening)
            {
                if (this.Listener == null) return;

                try
                {
                    this.Listener.BeginGetContext(ListenerCallBack, this.Listener);
                    _listenForNextRequest.WaitOne();
                }
                catch (Exception ex)
                {
                    return;
                }
                if (this.Listener == null) return;
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

            if (context == null) return;

            try
            {
                this.ProcessRequest(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;     
                byte[]buffer=Encoding.UTF8.GetBytes(ex.Message);
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
                this.Listener.Close();
            }
            catch (HttpListenerException ex)
            {

            }
            this.IsStarted = false;
            this.Listener = null;
        }
        protected abstract void ProcessRequest(HttpListenerContext context);

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            lock (this)
            {
                if (_disposed) return;
                if (disposing)
                {
                    this.Stop();
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

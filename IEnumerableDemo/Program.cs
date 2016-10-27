using System;
using System.Collections;

namespace IEnumerableDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var charList = new CharList("Hello,world!");

            foreach (var s in charList)
            {
                Console.WriteLine(s);
            }
            Console.ReadKey();
        }
    }

    internal class CharList : IEnumerable
    {
        public string TargetString { get; set; }

        public CharList(string targetString)
        {
            this.TargetString = targetString;
        }

        public IEnumerator GetEnumerator()
        {
            return new CharIterator(TargetString);
        }
    }

    internal class CharIterator : IEnumerator
    {
        public string TargetString { get; set; }
        public int Position { get; set; }

        public CharIterator(string targetString)
        {
            this.TargetString = targetString;
            this.Position = targetString.Length;
        }

        public object Current
        {
            get
            {
                if (this.Position == -1 || this.Position == this.TargetString.Length)
                {
                    throw new InvalidOperationException();
                }
                return this.TargetString[this.Position];
            }
        }

        public bool MoveNext()
        {
            if (this.Position != -1)
            {
                this.Position--;
            }
            return this.Position > -1;
        }

        public void Reset()
        {
            this.Position = this.TargetString.Length;
        }
    }
}
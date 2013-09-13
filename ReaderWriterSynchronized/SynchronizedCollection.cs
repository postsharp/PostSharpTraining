using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using PostSharp.Patterns.Threading;

namespace ReaderWriterSynchronized
{
    [ReaderWriterSynchronized]
    class SynchronizedCollection<T> : IList<T>, INotifyCollectionChanged
    {
        private readonly List<T> list = new List<T>();
        private int accesses;
 
        [ReaderLock]
        public IEnumerator<T> GetEnumerator()
        {
            Interlocked.Increment(ref accesses);
            return new List<T>( this.list ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [WriterLock]
        public void Add(T item)
        {
            this.list.Add(item);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,item, this.list.Count));
        }

        [WriterLock]
        public void Clear()
        {
            this.list.Clear();
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        [ReaderLock]
        public bool Contains(T item)
        {
            return this.list.Contains(item);
        }

        [ReaderLock]
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        [WriterLock]
        public bool Remove(T item)
        {
            int index = this.list.IndexOf(item);
            if (index < 0) return false;

            this.list.RemoveAt(index);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            return true;
        }

        [ReaderLock]
        public int Count { get { return this.list.Count; } }
        public bool IsReadOnly { get { return false; } }

        [ReaderLock]
        public int IndexOf(T item)
        {
            return this.list.IndexOf(item);
        }

        [WriterLock]
        public void Insert(int index, T item)
        {
            this.list.Insert(index, item);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        [WriterLock]
        public void RemoveAt(int index)
        {
            T item = this.list[index];
            this.list.RemoveAt(index);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        }

        public T this[int index]
        {
            [ReaderLock]
            get { return this.list[index]; }

            [WriterLock]
            set
            {
                T oldValue = this.list[index];
                if (!Equals(value, oldValue))
                {
                    this.list[index] = value;
                    this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                                                 NotifyCollectionChangedAction.Replace,value,oldValue,index ));
                }
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        [ObserverLock]
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (this.CollectionChanged != null)
            {
                foreach (NotifyCollectionChangedEventHandler handler in this.CollectionChanged.GetInvocationList())
                {
                    DispatcherObject dispatcherObject = handler.Target as DispatcherObject;
                    if (dispatcherObject != null)
                    {
                        dispatcherObject.Dispatcher.Invoke(handler, handler, args);
                    }
                    else
                    {
                        handler(handler, args);
                    }
                }

            }
        }
    }
}

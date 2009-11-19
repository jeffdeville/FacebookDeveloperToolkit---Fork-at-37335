
namespace Facebook.BindingHelper
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Windows.Threading;
    using System.Windows;
    using System.Threading;

    /// <summary>
    /// Provides the base class for a generic read-only data object collection.
    /// </summary>
    /// <typeparam name="T">Base item type for this collection.</typeparam>
    [DataContract]
    public abstract class UpdatableCollection<T> : IList<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        /// <summary>
        /// The list of objects
        /// </summary>
        [DataMember]
        protected List<T> Items { get; private set; }

        /// <summary>
        /// object used to handle concurrent access to the list
        /// </summary>
        protected object _collectionLock = new object();

        /// <summary>
        /// Initializes a new instance of the DataObjectCollection class.
        /// </summary>
        protected UpdatableCollection()
            : this(null)
        { }

        /// <summary>
        /// Initializes a new instance of the DataObjectCollection class.
        /// </summary>
        /// <param name="dataObjects">Collection of data objects used to initialize this collection.</param>
        protected UpdatableCollection(IEnumerable<T> dataObjects)
        {
            if (dataObjects == null)
            {
                Items = new List<T>();
            }
            else
            {
                Items = new List<T>(dataObjects);
            }
        }

        /// <summary>
        /// Merges data from another collection.
        /// </summary>
        /// <param name="newCollection">The data object collection that contains new data.</param>
        internal virtual void Merge(IEnumerable<T> newCollection)
        {
            lock (_collectionLock)
            {
                // This just replaces the entire collection.
                // Only raises notification about the Count property.
                Items.Clear();
                Items.AddRange(newCollection);
            }

            this.OnPropertyChanged("Count");
            this.OnCollectionChanged();
        }

        /// <summary>
        /// CollectionChanged needs to be raised on the dispatcher thread.
        /// </summary>
        private object OnCollectionChangedWorker(object param)
        {
            lock (_collectionLock) // the only time we enumerate UpdatableCollections is in response to CollectionChanged.
            {
                var handler = CollectionChanged;
                if (handler != null)
                {
                    handler(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
            }

            return null;
        }

        /// <summary>
        /// Raises CollectionChanged event.
        /// </summary>
        protected virtual void OnCollectionChanged()
        {
            // The CollectionChanged event needs to be raised on the dispatcher thread to work with ItemsControl bindings in the UI.
            if (Application.Current != null)
            {
#if !SILVERLIGHT
                if (Thread.CurrentThread == Application.Current.Dispatcher.Thread)
                {
                    OnCollectionChangedWorker(null);
                }
                else
                {
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DispatcherOperationCallback(OnCollectionChangedWorker), null);
                }
#else
                Deployment.Current.Dispatcher.BeginInvoke(() => OnCollectionChangedWorker(null));
#endif
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected abstract void AddCore(T item);

        #region IList<T> Members

        /// <summary>
        /// Overridden operator to return the position of a specific object
        /// </summary>
        public int IndexOf(T item)
        {
            return Items.IndexOf(item);
        }

        /// <summary>
        /// accessor to find an object based on its position
        /// </summary>
        public T this[int index]
        {
            get { return Items[index]; }
            set { Items[index] = value; }
        }

        /// <summary>
        /// insert a new object at a specified position
        /// </summary>
        public void Insert(int index, T item) { throw new NotSupportedException(); }

        /// <summary>
        /// remove an exisiting object from the collection as the specified position.
        /// </summary>
        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
            this.OnPropertyChanged("Count");
            this.OnCollectionChanged();
        }

        #endregion

        #region ICollection<T> Members

        /// <summary>
        /// check if collection contains a specified object
        /// </summary>
        public bool Contains(T item)
        {
            return Items.Contains(item);
        }

        /// <summary>
        /// copy an array into this collection from a specific starting point in the collection
        /// </summary>
        public void CopyTo(T[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// the number of items in the collection
        /// </summary>
        public int Count
        {
            get { return Items.Count; }
        }

        /// <summary>
        /// always true, since we don't allow adding or changing items
        /// </summary>
        public bool IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// not used public.  provided for Serialization scenarios
        /// </summary>
        public void Add(T item)
        {
            // Provided for Serialization scenarios
            AddCore(item);
        }

        /// <summary>
        /// Not Supported
        /// </summary>
        public void Clear() { throw new NotSupportedException(); }
        /// <summary>
        /// Not supported
        /// </summary>
        public bool Remove(T item) { throw new NotSupportedException(); }


        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        /// Accessor for the enumerator for this collection
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        #endregion

        #region INotifyCollectionChanged Members

        /// <summary>
        /// Collection changed notifier
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Property changed notifier
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

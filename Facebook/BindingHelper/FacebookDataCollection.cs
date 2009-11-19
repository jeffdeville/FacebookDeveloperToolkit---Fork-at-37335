using System;
using System.Net;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Threading;
using System.Threading;
using Facebook.Utility;

namespace Facebook.BindingHelper
{
    /// <summary>
    /// Facebook data collection base class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FacebookDataCollection<T> : ObservableCollection<T> where T : IEquatable<T>
    {
        /// <summary>
        /// Event to raise when the asynch operation completes
        /// </summary>
        public event EventHandler<DataRetrievalCompletedEventArgs> DataRetrievalCompletedEvent;

        /// <summary>
        /// Initializes new instance of FacebookDataCollection
        /// </summary>
        protected FacebookDataCollection() : base()
        {
        }

        /// <summary>
        /// Initializes new instance of FacebookDataCollection
        /// </summary>
        /// <param name="dataObjects">Collection whose items are copied</param>
        protected FacebookDataCollection(IEnumerable<T> dataObjects)
#if !SILVERLIGHT
            : base(dataObjects)
#endif
        {
#if SILVERLIGHT
            if (dataObjects != null)
            {
                foreach (T obj in dataObjects)
                {
                    this.Add(obj);
                }
            }
#endif
        }

        /// <summary>
        /// Indicates if notifications needs to be deferred
        /// </summary>
        protected bool DeferNotification
        {
            get;
            set;
        }

        /// <summary>
        /// Invokes the action on dispatcher thread
        /// </summary>
        /// <param name="action"></param>
        protected void InvokeUsingDispatcher(Action action)
        {
#if !SILVERLIGHT
            if (Application.Current != null)
            {
                if (Thread.CurrentThread == Application.Current.Dispatcher.Thread)
                {
                    action();
                }
                else
                {
                    Application.Current.Dispatcher.BeginInvoke(action);
                }
            }
            else
            {
                action();
            }
#else
            if (Deployment.Current.Dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(action);
            }
#endif
        }
        /// <summary>
        /// raises collection changed event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!this.DeferNotification)
            {
                base.OnCollectionChanged(e);
            }
        }

        /// <summary>
        /// Raises proprety change event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (!this.DeferNotification)
            {
                base.OnPropertyChanged(e);

            }
        }

        /// <summary>
        /// Merges data from another collection.
        /// </summary>
        /// <param name="newCollection">The data object collection that contains new data.</param>
        public virtual void Merge(IEnumerable<T> newCollection)
        {
            if (newCollection == null)
                return;

            InvokeUsingDispatcher(() =>
                {
                    if (this.Count == 0)
                    {
                        try
                        {
                            this.DeferNotification = true;
                            this.AddRange(newCollection);
                        }
                        finally
                        {
                            this.DeferNotification = false;
                            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                        }
                    }
                    else
                    {
                        foreach (T item in newCollection)
                        {
                            if (!this.Contains(item))
                            {
                                this.Add(item);
                            }
                            else
                            {
                                int index = this.IndexOf(item);
                                this[index] = item;
                            }
                        }
                    }
                });
        }

        /// <summary>
        /// Inserts a collection to existing collection, removing any entries that would be re-add to existing collection
        /// </summary>
        /// <param name="position"></param>
        /// <param name="newCollection"></param>
        internal virtual void InsertRange(int position, IEnumerable<T> newCollection)
        {
            InvokeUsingDispatcher(() =>
            {
                foreach (T obj in newCollection)
                {
                    if (this.Contains(obj))
                        this.Remove(obj);
                }

                foreach (T obj in newCollection)
                {
                    this.Insert(position, obj);
                    ++position;
                }
            });
        }

        /// <summary>
        /// Adds an item on dispatcher thread
        /// </summary>
        /// <param name="item"></param>
        internal void AddInternal(T item)
        {
            InvokeUsingDispatcher(() => Add(item));
        }

        /// <summary>
        /// Adds an ienumerable collection to existing collection
        /// </summary>
        /// <param name="newCollection"></param>
        internal virtual void AddRange(IEnumerable<T> newCollection)
        {
            InvokeUsingDispatcher(() =>
                {
                    foreach (T obj in newCollection)
                        this.Add(obj);
                });
        }

        /// <summary>
        /// Inserts an item on dispatcher thread
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        internal void InsertInternal(int index, T item)
        {
            InvokeUsingDispatcher(() => Insert(index, item));
        }

        /// <summary>
        /// removes range of items on dispatcher thread
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        internal void RemoveRange(int start, int count)
        {
            InvokeUsingDispatcher(() =>
                {
                    while (count-- != 0)
                    {
                        RemoveAt(start);
                    }
                });
        }


        internal void OnDataRetrievalComplete(FacebookException ex)
        {
            InvokeUsingDispatcher(() =>
                {
                    if (DataRetrievalCompletedEvent != null)
                    {
                        DataRetrievalCompletedEvent(this, new DataRetrievalCompletedEventArgs(ex));
                    }
                });
        }


    }
}

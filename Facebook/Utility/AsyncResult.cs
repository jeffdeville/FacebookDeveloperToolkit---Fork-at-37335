using System;
using System.Threading;

namespace Facebook.Utility
{
    ///<summary>
    ///</summary>
    public class AsyncResult : IAsyncResult
    {
        /// <summary>
        /// the delegate to call when the function completes
        /// </summary>
        readonly AsyncCallback callback;

        /// <summary>
        /// the state passed in by caller
        /// </summary>
        readonly Object state;

        /// <summary>
        /// the state passed in by external caller.
        /// It is convieniently not keep it in the state so we can avoid another cast.
        /// </summary>
        readonly Object externalstate;

        /// <summary>
        /// The sync construct to return when asked for the AsyncWaitHandle
        /// This is lazy-created to prevent a perf hit in cases when
        /// the caller doesn't use this.
        /// </summary>
        private ManualResetEvent waitHandler;

        /// <summary>
        /// to hold the exception if the operation results in an exception
        /// </summary>
        public FacebookException Exception
        {
            get;
            private set;
        }

        /// <summary>
        /// indicates whether an operation is completed.
        /// This is an Int32 and not a bool because Interlocked APIs don't
        /// work with bools.
        /// </summary>
        private Int32 isComplete;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <param name="externalstate"></param>
        public AsyncResult(AsyncCallback callback, Object state, Object externalstate)
        {
            this.callback = callback;
            this.state = state;
            this.externalstate = externalstate;
        }

        /// <summary>
        /// Gets a user-defined object that qualifies or contains information about an 
        /// asynchronous operation
        /// </summary>
        public object AsyncState
        {
            get
            {
                return state;
            }
        }

        /// <summary>
        /// Gets a user-defined object that qualifies or contains information about an 
        /// asynchronous operation
        /// </summary>
        public object AsyncExternalState
        {
            get
            {
                return externalstate;
            }
        }
        /// <summary>
        /// Gets an indication of whether the asynchronous operation completed synchronously.
        /// </summary>
        public bool CompletedSynchronously
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets an indication whether the asynchronous operation has completed.
        /// </summary>
        public bool IsCompleted
        {
            get
            {
                return isComplete == 1;
            }
        }

        /// <summary>
        /// Callback that needs to be called when operation completes
        /// </summary>
        public AsyncCallback AsyncCallback
        {
            get
            {
                return callback;
            }
        }


        /// <summary>
        /// Gets a WaitHandle that is used to wait for an asynchronous operation to complete
        /// </summary>
        public WaitHandle AsyncWaitHandle
        {
            get
            {
                if (waitHandler != null)
                {
                    return waitHandler;
                }

                bool wasCompleted = IsCompleted;
                ManualResetEvent handle = new ManualResetEvent(wasCompleted);
                if (Interlocked.CompareExchange(ref waitHandler, handle, null) != null)
                {
                    // this wasn't null so someone else set it. No need to do anything
                    handle.Close();
                }
                else
                {
                    // we have initialized this.waitHandler. Set it if the task is completed now 
                    // but wasn't before.
                    if (!wasCompleted && IsCompleted)
                    {
                        waitHandler.Set();
                    }
                }

                return waitHandler;
            }
        }

        /// <summary>
        /// The result of operation
        /// </summary>
        public string Result
        {
            get;
            set;
        }

        /// <summary>
        /// The result of operation
        /// </summary>
        public bool JSONFormat
        {
            get;
            set;
        }


        /// <summary>
        /// Mark this operation as completed, set the waitHandle and call the callback delegate
        /// </summary>
        /// <param name="result"></param>
        /// <param name="e">the result of this operation if it was an exception (can be null)</param>
        public virtual void SetComplete(string result, FacebookException e)
        {
            int wasCompleted = Interlocked.Exchange(ref isComplete, 1);
            if (wasCompleted == 1)
            {
                // can't call this if the operation is already set as completed
                throw new InvalidOperationException();
            }

            Result = result;
            Exception = e;

            // set this only if created. No use setting on one that has no threads waiting
            if (waitHandler != null)
            {
                waitHandler.Set();
            }

            // invoke user's callback is provided
            if (callback != null)
            {
                callback(this);
            }
        }


    }
}

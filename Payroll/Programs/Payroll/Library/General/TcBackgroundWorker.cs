using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

// Harshan Nishantha
// 2015-11-18

namespace Payroll.Library
{
    public delegate bool NonArgumentFunction();

    public class TcBackgroundWorker : IDisposable
    {
        // Track whether Dispose has been called. 
        private bool disposed = false;

        private BackgroundWorker    worker;
        private NonArgumentFunction function;

        public bool Loaded { get; set; }
        public bool Succeed { get; set; }
        public bool ReturnValue { get; set; }
        public string Error { get; set; }


        public TcBackgroundWorker()
        {
        }

        private void Init()
        {
            Loaded      = false;
            Succeed     = false;
            ReturnValue = false;
            Error       = String.Empty;

            worker = new BackgroundWorker();

            worker.WorkerSupportsCancellation = false;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        public void Execute(NonArgumentFunction function)
        {
            Init();
            this.function = function;

            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
                WaitForExit();
            }
        }

        private void WaitForExit()
        {
            while (!Loaded)
            {
                Thread.Sleep(100);
                Application.DoEvents();
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ReturnValue = function.Invoke();
                Succeed = true;
            }
            catch (Exception ex)
            {
                Error   = ex.Message;
                Succeed = false;
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Loaded = true;
        }

        #region IDisposable

        // Implement IDisposable. 
        // Do not make this method virtual. 
        // A derived class should not be able to override this method. 
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method. 
            // Therefore, you should call GC.SupressFinalize to 
            // take this object off the finalization queue 
            // and prevent finalization code for this object 
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios. 
        // If disposing equals true, the method has been called directly 
        // or indirectly by a user's code. Managed and unmanaged resources 
        // can be disposed. 
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed. 
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources. 
                if (disposing)
                {
                    // Dispose managed resources.
                    worker.Dispose();
                }

                // Note disposing has been done.
                disposed = true;

            }
        }

        ~TcBackgroundWorker()
        {
            // Do not re-create Dispose clean-up code here. 
            // Calling Dispose(false) is optimal in terms of 
            // readability and maintainability.
            Dispose(false);
        }

        #endregion
    }
}

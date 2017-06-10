using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SGCombo.WallpaperChangeScheduler
{
    public class WallpaperBgWorker : BackgroundWorker
    {
        private Thread workerThread;

        public string Key { get; set; }


        public WallpaperBgWorker(): base()
            {
            Key = Guid.NewGuid().ToString();
        }


        protected override void OnDoWork(DoWorkEventArgs e)
        {
            workerThread = Thread.CurrentThread;
            try
            {
                base.OnDoWork(e);


            }
            catch (ThreadAbortException)
            {
                e.Cancel = true;
                Thread.ResetAbort(); 
            }
        }

        public void Abort()
        {
            if (workerThread != null)
            {
                workerThread.Abort();
                workerThread = null;
            }
        }

        internal void Sleep(int timeout)
        {
            Thread.Sleep(timeout);
        }
    }
}

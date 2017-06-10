////////////////////////////////////////////////////////////////////////////
//	Copyright 2014 : Vladimir Novick    https://www.linkedin.com/in/vladimirnovick/  
//
//    NO WARRANTIES ARE EXTENDED. USE AT YOUR OWN RISK. 
//
//      Available under the BSD and MIT licenses
//
// To contact the author with suggestions or comments, use  :vlad.novick@gmail.com
//
////////////////////////////////////////////////////////////////////////////
using System;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Configuration;

namespace SGCombo.WallpaperChangeScheduler
{
    public class WallpaperChangerStart
    {

        private static string imageDirectory { get; set; }
        private static string style { get; set;  }
        private static int timeOut { get; set; }

        public WallpaperChangerStart()
        {

        }

        public void OnStop()
        {
            JobsWorker.StopBackgowndWorkProcess();
        }

        ManualResetEvent oSignalEvent = new ManualResetEvent(false);

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public void OnStart()
        {

            imageDirectory = ConfigurationManager.AppSettings["ImageDirectory"];
            style = ConfigurationManager.AppSettings["Style"];
       

            IdentifyQueryBackground param = new IdentifyQueryBackground(imageDirectory, style);

            JobsWorker.StartBackgowndWorkProcess(param);
        }

        internal void Wait()
        {
            JobsWorker.mre.WaitOne();
        }
    }
}

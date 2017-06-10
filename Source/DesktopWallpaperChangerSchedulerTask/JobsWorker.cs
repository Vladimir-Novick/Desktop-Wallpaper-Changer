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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;

namespace SGCombo.WallpaperChangeScheduler
{
    public class JobsWorker
    {

        private static WallpaperBgWorker m_BackgroundWorkerAsync = null;

        public static  ManualResetEvent mre = new ManualResetEvent(false);

        private static ConcurrentDictionary<String, WallpaperBgWorker> dictionaryBag = new ConcurrentDictionary<string, WallpaperBgWorker>();

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void StartBackgowndWorkProcess(IdentifyQueryBackground param)
        {
            try
            {
                if (m_BackgroundWorkerAsync != null)
                {
                    m_BackgroundWorkerAsync.CancelAsync();
                    m_BackgroundWorkerAsync.Abort();
                }
                m_BackgroundWorkerAsync = new WallpaperBgWorker();
                m_BackgroundWorkerAsync.WorkerSupportsCancellation = true;
                m_BackgroundWorkerAsync.DoWork += new DoWorkEventHandler(BackgroundWorkerAsyncRequest_DoWork);
                m_BackgroundWorkerAsync.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorkerAsyncRequest_RunWorkerCompleted);

                m_BackgroundWorkerAsync.RunWorkerAsync(param);
                dictionaryBag.TryAdd(m_BackgroundWorkerAsync.Key, m_BackgroundWorkerAsync);

            }
            catch (Exception)
            {

            }
        }

        internal static void StopBackgowndWorkProcess()
        {
            if (m_BackgroundWorkerAsync.IsBusy == true)
            {
                m_BackgroundWorkerAsync.CancelAsync();
                m_BackgroundWorkerAsync.Abort();
                m_BackgroundWorkerAsync.Dispose();
                m_BackgroundWorkerAsync = null;
            }
        }

        public static void getFileList(List<String> files,string path)
        {

            if (File.Exists(path))
            {

                ProcessFile(files,path);
            }
            else if (Directory.Exists(path))
            {

                ProcessDirectory(files,path);
            }

        }

        public static void ProcessDirectory(List<String> files,string targetDirectory)
        {

            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(files,fileName);

            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(files,subdirectory);
        }

        public static void ProcessFile(List<String> files,string filename)
        {
            if (Regex.IsMatch(filename, @".jpg|.jpeg|.png|.gif$"))
                files.Add(filename);
        }

        public static List<String> images = new List<string>();

        private static void BackgroundWorkerAsyncRequest_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WallpaperBgWorker my_BackgroundWorkerAsync = (WallpaperBgWorker)sender;

                IdentifyQueryBackground identifiedQuery = null;

                identifiedQuery = (IdentifyQueryBackground)e.Argument;
                images.Clear();
                getFileList(images, identifiedQuery.directoryPatch);


                    try
                    {
                        Random rnd = new Random();
                        int currentImage = rnd.Next(0, images.Count);

                        String image = images[currentImage];

                        Wallpaper.Set(image, identifiedQuery.style);
                       
                    }
                    catch (Exception ex)
                    {


                    }

                    if (my_BackgroundWorkerAsync.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
            
            }
            catch (Exception ex) { }
        }

        private static void BackgroundWorkerAsyncRequest_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mre.Set();

        }

    }

}

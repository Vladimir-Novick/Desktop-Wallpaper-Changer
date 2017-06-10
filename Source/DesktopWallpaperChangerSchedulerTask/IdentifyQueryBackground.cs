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

namespace SGCombo.WallpaperChangeScheduler
{
    public class IdentifyQueryBackground
    {
        public string directoryPatch { get; set; }

        public string style { get; set; }

      

        public IdentifyQueryBackground(string _dirName,string _style)
        {
            directoryPatch = _dirName;
            style = _style;
         

        }

    }
}

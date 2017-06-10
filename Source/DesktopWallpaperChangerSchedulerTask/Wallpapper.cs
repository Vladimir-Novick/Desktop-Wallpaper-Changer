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
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace SGCombo.WallpaperChangeScheduler
{
    public sealed class Wallpaper
    {
        Wallpaper() { }

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public static Style GetStyleFromString(String strStyle)
        {
            Style ret = Style.Fill;

            switch (strStyle.ToLower())
            {

                case "fill":
                    ret = Style.Fill;
                    break;

                case "fit":
                    ret = Style.Fit;

                    break;

                case "span": // Windows 8 or newer only!
                    ret = Style.Span;

                    break;
                case "stretch":
                    ret = Style.Stretch;
                    break;
                case "tile":
                    ret = Style.Tile;
                    break;
                case "center":
                    ret = Style.Center;
                    break;
            }

            return ret;
        }

        public static void Set(string uri, string _style)
        {
            Style style = GetStyleFromString(_style);
            Set(uri, style);
        }

        public static void Set(string image, Style style)
        {

            try
            {

                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);

                switch (style)
                {

                    case Style.Fill:

                        key.SetValue(@"WallpaperStyle", 10.ToString());
                        key.SetValue(@"TileWallpaper", 0.ToString());
                        break;

                    case Style.Fit:

                        key.SetValue(@"WallpaperStyle", 6.ToString());
                        key.SetValue(@"TileWallpaper", 0.ToString());
                        break;

                    case Style.Span: // Windows 8 or newer only!

                        key.SetValue(@"WallpaperStyle", 22.ToString());
                        key.SetValue(@"TileWallpaper", 0.ToString());
                        break;
                    case Style.Stretch:

                        key.SetValue(@"WallpaperStyle", 2.ToString());
                        key.SetValue(@"TileWallpaper", 0.ToString());
                        break;
                    case Style.Tile:

                        key.SetValue(@"WallpaperStyle", 0.ToString());
                        key.SetValue(@"TileWallpaper", 1.ToString());
                        break;
                    case Style.Center:

                        key.SetValue(@"WallpaperStyle", 0.ToString());
                        key.SetValue(@"TileWallpaper", 0.ToString());
                        break;
                }

                SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0,
                    image,
                    SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            }
            catch (Exception ex) {

            }
        }
    }
}

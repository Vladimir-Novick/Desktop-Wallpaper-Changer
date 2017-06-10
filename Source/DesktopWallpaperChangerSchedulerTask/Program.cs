namespace SGCombo.WallpaperChangeScheduler
{
    class Program
    {
        public const string ParameterNameStyle = "Style";

        public const string ParameterNameImageDirectory = "ImageDirectory";

        static void Main(string[] args)
        {
            WallpaperChangerStart wallpaperServiceStart = new WallpaperChangerStart();
            wallpaperServiceStart.OnStart();
            wallpaperServiceStart.Wait();


        }
    }
}

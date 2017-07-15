using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleDecktopChanger
{
    [RunInstaller(true)]
    public partial class WallpaperServiceInstaller : System.Configuration.Install.Installer
    {
        public WallpaperServiceInstaller()
        {
            InitializeComponent();
        }
    }
}

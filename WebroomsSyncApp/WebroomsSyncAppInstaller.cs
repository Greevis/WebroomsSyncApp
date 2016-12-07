using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;

namespace WebroomsSyncApp
{
    [RunInstaller(true)]
    public partial class WebroomsSyncAppInstaller : System.Configuration.Install.Installer
    {

        public WebroomsSyncAppInstaller()
        {
            InitializeComponent();
        }

    }
}

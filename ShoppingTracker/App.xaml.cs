using ShoppingTracker.Model;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingTracker
{
    public partial class App : Application
    {
        public App()
        {
            // Set culture according to device language
            CultureInfo culture = new CultureInfo(CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            CultureInfo.DefaultThreadCurrentCulture = culture;

            InitializeComponent();

            MainShell appShell = new MainShell();
            MainPage = appShell;
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

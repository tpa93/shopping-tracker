using ShoppingTracker.Model;
using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingTracker
{
    public partial class App : Application
    {
        public App()
        {
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

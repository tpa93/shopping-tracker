using System;
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

            // Sets Colors of NAVBAR and TABBAR
            /*Shell.SetBackgroundColor(appShell, Color.Red);
            Shell.SetTitleColor(appShell, Color.Green);
            Shell.SetNavBarHasShadow(appShell, true);
            Shell.SetNavBarIsVisible(appShell, false);

            // Sets Colors of TABBAR
            Shell.SetTabBarBackgroundColor(appShell, Color.Blue);
            Shell.SetTabBarTitleColor(appShell, Color.Yellow);*/

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

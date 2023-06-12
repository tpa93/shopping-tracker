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
        /*
        private static ShoppingItemList activeShoppingItemList = new ShoppingItemList();
        public static ShoppingItemList ActiveShoppingItemList 
        {
            get 
            {
                return activeShoppingItemList;
            }
            set
            {
                activeShoppingItemList.ShoppingItems = new ObservableCollection<ShoppingItem>(value.ShoppingItems);
                activeShoppingItemList.Name = value.Name;
            }
        }

        public static ShoppingItemList ActiveShoppingItemListTemplate { get; set; }
        */

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

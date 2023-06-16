using ShoppingTracker.Model;
using ShoppingTracker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShoppingTracker.ViewModel
{
    internal class HistoryViewModel
    {
        //ObservableCollection<ShoppingItemList> shoppingHistory;

        public ObservableCollection<ShoppingItemList> ShoppingHistory {get; set;}

        public HistoryViewModel() 
        {
            InitializeShoppingHistory();
        }

        void InitializeShoppingHistory()
        {
            ShoppingHistory = DatabaseHandler.GetTotalShoppingHistory();
            if (ShoppingHistory == null) 
            { 
              Application.Current.MainPage.DisplayAlert("Error", "Loading shopping history failed due to unknown error", "OK");
            }
        }

        public ICommand GetHistoryDetailsCommand => new Command<ShoppingItemList>(GetHistoryDetails);

        void GetHistoryDetails(ShoppingItemList selectedList)
        {
            
        }
    }
}

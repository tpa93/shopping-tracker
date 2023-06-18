using Newtonsoft.Json;
using ShoppingTracker.Model;
using ShoppingTracker.Services;
using ShoppingTracker.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingTracker.ViewModel
{
    internal class HistoryViewModel: INotifyPropertyChanged
    {
        // Current ShoppingHistory
        ObservableCollection<ShoppingItemList> shoppingHistory = new ObservableCollection<ShoppingItemList>();

        public ObservableCollection<ShoppingItemList> ShoppingHistory 
        {
            get { return shoppingHistory; }
            set
            {
                shoppingHistory = value;
                OnPropertyChanged();
            }
        }

        // Count of shoppings depending on items in ShoppingHistory
        int numberOfShoppings;
        public int NumberOfShoppings 
        { 
            get { return numberOfShoppings; } 
            set
            {
                numberOfShoppings = value;
                OnPropertyChanged();
            }
        }

        // Shopping costs of all items in ShoppingHistory
        double totalShoppingCost;
        public double TotalShoppingCost 
        { 
            get { return totalShoppingCost; }
            set
            { 
                totalShoppingCost = value;
                OnPropertyChanged();
            }
        }

        public HistoryViewModel() 
        {
            InitializeShoppingHistory();
        }

        // Get history data
        public void InitializeShoppingHistory()
        {
            ShoppingHistory = DatabaseHandler.GetTotalShoppingHistory();

            if (ShoppingHistory == null) 
            { 
              Application.Current.MainPage.DisplayAlert("Error", "Loading shopping history failed due to unknown error", "OK");
            }

            // Update total values
            CalculateTotalShoppingValues();
        }

        // Execute navigation to details page depending to selected ShoppingItemList
        public ICommand GetHistoryDetailsCommand => new Command<ShoppingItemList>(GetHistoryDetails);

        async void GetHistoryDetails(ShoppingItemList selectedList)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new HistoryDetailView(selectedList));
        }


        // Calculate/Update ListView footer values
        void CalculateTotalShoppingValues()
        {
            NumberOfShoppings = ShoppingHistory.Count;
            TotalShoppingCost = 0;
            ShoppingHistory.ForEach(item => TotalShoppingCost += item.TotalCost);
        }


        // Provide settings menu to user
        public ICommand SettingsMenuCommand => new Command(ProvideSettingsMenu);

        async void ProvideSettingsMenu()
        {
            string action = await Application.Current.MainPage.DisplayActionSheet("Settings", "Cancel", null, "Clear shopping history", "Choose templates to delete");

            // User cancelled action
            if (action == null)
            {
                return;
            }
            // Delete shopping history data
            else if (action == "Clear shopping history")
            {
                
                if (!DatabaseHandler.DeleteShoppingHistory())
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "History could not be cleared due to an unknown error", "Ok");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "History data deleted", "Ok");

                    // Update view
                    ShoppingHistory = null;
                    NumberOfShoppings = 0;
                    TotalShoppingCost = 0;
                }

            }
            // Navigate to DeleteShoppingItemListTemplatesView 
            else if (action == "Choose templates to delete")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new DeleteTemplateView());
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}

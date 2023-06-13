using Newtonsoft.Json;
using ShoppingTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingTracker.ViewModel
{

    [QueryProperty(nameof(JSON), "JSON")]
    internal class ShoppingViewModel:INotifyPropertyChanged
    {
        public ShoppingViewModel()
        {
            ActiveShoppingItemList = new ShoppingItemList();
        }


        // JSON string received by CreateShoppingListTemplateViewModel
        string json;
        public string JSON
        {
            get
            {
                return json;
            }
            // Will be called after the constructor, or directly when routing to view and object from view/viewModel already existing 
            set
            {
                json = Uri.UnescapeDataString(value ?? String.Empty);

                // Trigger setter of ActiveShoppingItemList to update view
                ActiveShoppingItemList = JsonConvert.DeserializeObject<ShoppingItemList>(json);    
            }
        }


        // Currently active ShoppingItemList
        ShoppingItemList activeShoppingItemList = new ShoppingItemList();
        public ShoppingItemList ActiveShoppingItemList
        {
            get
            {
                return activeShoppingItemList;
            }
            set
            {
                activeShoppingItemList = value;
                OnPropertyChanged();
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

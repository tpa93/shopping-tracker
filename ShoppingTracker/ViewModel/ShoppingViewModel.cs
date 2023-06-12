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
    internal class ShoppingViewModel
    {
        public ObservableCollection<ShoppingItem> ShoppingItems { get; set; }
        ShoppingItemList ActiveShoppingItemList { get; set; }

        string json;
        public string JSON
        {
            get
            {
                return json;
            }
            set
            {
                json = Uri.UnescapeDataString(value ?? String.Empty);
                ActiveShoppingItemList = JsonConvert.DeserializeObject<ShoppingItemList>(json);
                //ShoppingItems = ActiveShoppingItemList.ShoppingItems
                
                // Clear necessary, because new data will added to already existing data when routing again 
                ShoppingItems.Clear();
                ActiveShoppingItemList.ShoppingItems.ForEach(item => ShoppingItems.Add(new ShoppingItem(item.Name, item.Count)));
                
            }
        }

        /* Hint and reason for implementation:
         * 
         * When assigning ShoppingItems = ActiveShoppingItemList.ShoppingItems out of constructor,
         * f.e. in the SETTER of the JSON property, 
         * the view didnt get updated. View only gets updated via .Add(new ShoppingItem(x,y)) method, 
         * so this seems to fire an event to send a notification from OC to the view
         * that the OC values changed
         * 
         * INOTIFYCHANGED not necessary when using: Add(new ShoppingItem(x,y)) on OC
         */

        /*
        // Create and fire event when ShoppingItems is set in setter from JSON property to update ListView
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        ObservableCollection<ShoppingItem> shoppingItems;
        public ObservableCollection<ShoppingItem> ShoppingItems 
        { 
            get { return shoppingItems; }
            set 
            { 
                shoppingItems = value;
                OnPropertyChanged();
            }
        }
        */

        public ShoppingViewModel() 
        {
            ShoppingItems = new ObservableCollection<ShoppingItem>();
        }


    }
}

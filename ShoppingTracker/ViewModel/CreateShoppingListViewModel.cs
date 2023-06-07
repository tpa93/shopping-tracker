using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ShoppingTracker.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShoppingTracker.ViewModel
{
    internal class CreateShoppingListViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<ShoppingItem> ShoppingItems { get; set; }
        
        public CreateShoppingListViewModel() 
        { 
            ShoppingItems = new ObservableCollection<ShoppingItem>();
            ShoppingItems.Add(new ShoppingItem("Test 1", "1"));
            ShoppingItems.Add(new ShoppingItem("Test 2", "2"));
            ShoppingItems.Add(new ShoppingItem("Test 3", "3"));
        }


        // Use public Properties to set the fields of the NewShoppingItem
        // If value changed, raise event to notify the view
        private ShoppingItem NewShoppingItem { get; set; } = new ShoppingItem();

        
        public string NewItemName 
        { 
            get { return this.NewShoppingItem.Name; } 
            set { 
                if (NewShoppingItem.Name != value)
                {
                    this.NewShoppingItem.Name = value;
                    OnPropertyChanged();
                } 
            }
        }

        public string NewItemCount
        {
            get { return this.NewShoppingItem.Count; }
            set { 
                if (NewShoppingItem.Count != value)
                {
                    this.NewShoppingItem.Count = value;
                    OnPropertyChanged();
                }
            }
        }

        // Fire event if property value bound to view is changed, to update view
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // Add ShoppingItem with user input via input grid - Command is bound on "+" - button
        public ICommand AddShoppingItemCommand => new Command(AddNewShopingItem);
        
        void AddNewShopingItem()
        {
            if (NewItemCount == "" || NewItemCount == null)
            {
                NewItemCount = "1";
            }
            if (NewItemName != "" && NewItemName != null )
            {
                ShoppingItems.Add(NewShoppingItem);
                
            }

            NewItemName = "";
            NewItemCount = "";
        }


        // Remove ShoppingItem via trash bin image (button) - Command is bound in CreateShoppingListView.xaml on ImageButton
        public ICommand RemoveShoppingItemCommand => new Command(RemoveShoppingItem);

        void RemoveShoppingItem(object o)
        {
            ShoppingItem itemToRemove = o as ShoppingItem;
            ShoppingItems.Remove(itemToRemove);
        }


    }
}

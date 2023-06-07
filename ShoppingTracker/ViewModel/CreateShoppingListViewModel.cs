using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ShoppingTracker.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingTracker.ViewModel
{
    internal class CreateShoppingListViewModel
    {
        public ObservableCollection<ShoppingItem> ShoppingItems { get; set; }
        
        public CreateShoppingListViewModel() 
        { 
            ShoppingItems = new ObservableCollection<ShoppingItem>();
            ShoppingItems.Add(new ShoppingItem("Test 1", "1"));
            ShoppingItems.Add(new ShoppingItem("Test 2", "2"));
            ShoppingItems.Add(new ShoppingItem("Test 3", "3"));
        }


        // Add user input as NewShoppingItem to viewModl and view - NewShoppingItem is bound to .Text-property of user input control elements in view
        public ShoppingItem NewShoppingItem { get; set; } = new ShoppingItem();
        public ICommand AddShoppingItemCommand => new Command(AddNewShopingItem);

        void AddNewShopingItem()
        {
            if (NewShoppingItem.Count == null)
            {
                NewShoppingItem.Count = "1";
            }
            if (NewShoppingItem.Name != null)
            {
                ShoppingItems.Add(new ShoppingItem(NewShoppingItem.Name, NewShoppingItem.Count));
            }

            NewShoppingItem.Name = null;
            NewShoppingItem.Count = null;
            
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

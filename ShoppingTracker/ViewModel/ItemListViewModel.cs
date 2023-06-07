using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ShoppingTracker.Model;
using ShoppingTracker.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingTracker.ViewModel
{
    internal class ItemListViewModel
    {
        public ObservableCollection<ShoppingItem> ShoppingItems { get; set; }
        
        public ItemListViewModel() 
        { 
            ShoppingItems = new ObservableCollection<ShoppingItem>();
            ShoppingItems.Add(new ShoppingItem("Test 1", "1"));
            ShoppingItems.Add(new ShoppingItem("Test 2", "2"));
            ShoppingItems.Add(new ShoppingItem("Test 3", "3"));
        }


        // Fields of this object have to the be bound to the TEXT of the control which the user enters the new item
        public ShoppingItem NewShoppingItem { get; set; } = new ShoppingItem();
        
        //  Create Command
        /*  The following syntax binds this command on XAML File of CreateListView:
            ReturnCommand = {Binding AddShoppingItemCommand}
            Bind this command to the the input control where the user inputs the NewShoppingItem    */
        public ICommand AddShoppingItemCommand => new Command(AddNewShopingItem);

        void AddNewShopingItem()
        {
            if (NewShoppingItem.Count == String.Empty)
            {
                NewShoppingItem.Count = "1";
            }
            else if (NewShoppingItem.Name != String.Empty)
            {
                ShoppingItems.Add(new ShoppingItem(NewShoppingItem.Name, NewShoppingItem.Count));
            }
            
            
        }



        public ICommand RemoveShoppingItemCommand => new Command(RemoveShoppingItem);

        void RemoveShoppingItem(object o)
        {
            ShoppingItem itemToRemove = o as ShoppingItem;
            ShoppingItems.Remove(itemToRemove);
        }


    }
}

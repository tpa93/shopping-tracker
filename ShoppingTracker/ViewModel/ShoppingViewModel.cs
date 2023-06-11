using ShoppingTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ShoppingTracker.ViewModel
{
    internal class ShoppingViewModel
    {
        public ObservableCollection<ShoppingItem> ShoppingItems { get; set; }
        public ShoppingViewModel() 
        { 
            ShoppingItems = new ObservableCollection<ShoppingItem>();

            ShoppingItems.Add(new ShoppingItem("Test 1", "1"));
            ShoppingItems.Add(new ShoppingItem("Test 2", "2"));
            ShoppingItems.Add(new ShoppingItem("Test 3", "3"));

        }

        public ShoppingViewModel(ObservableCollection<ShoppingItem> shoppingItems)
        {
            ShoppingItems = shoppingItems;
        }
    }
}

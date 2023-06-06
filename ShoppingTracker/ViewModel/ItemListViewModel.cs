using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using ShoppingTracker.Model;

namespace ShoppingTracker.ViewModel
{
    internal class ItemListViewModel
    {
        public ObservableCollection<ShoppingItem> ShoppingItems { get; set; }

        public ItemListViewModel() 
        { 
            ShoppingItems = new ObservableCollection<ShoppingItem>();
            ShoppingItems.Add(new ShoppingItem("Test 1", 1));
            ShoppingItems.Add(new ShoppingItem("Test 2", 2));
            ShoppingItems.Add(new ShoppingItem("Test 3", 3));
        }
    }
}

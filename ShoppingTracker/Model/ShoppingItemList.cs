using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ShoppingTracker.Model
{
    public class ShoppingItemList
    {
        public ObservableCollection<ShoppingItem> ShoppingItems { get; set; } = new ObservableCollection<ShoppingItem>();
        public string Name { get; set; }

        public ShoppingItemList() 
        { 

        }

        public ShoppingItemList(ObservableCollection<ShoppingItem> Items)
        {
            this.ShoppingItems = Items;
        }

        public ShoppingItemList(ObservableCollection<ShoppingItem> Items, string name)
        {
            this.ShoppingItems = Items;
            this.Name = name;
        }

    }
}

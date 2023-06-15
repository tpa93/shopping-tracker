using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;

namespace ShoppingTracker.Model
{
    [Table ("shopping_history")]
    public class ShoppingItemList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string TotalCost { get; set; }

        public DateTime ShoppingDate { get; set; }

        public string Location { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<ShoppingItem> ShoppingItems { get; set; } = new ObservableCollection<ShoppingItem>();



        public ShoppingItemList() 
        { 

        }

        public ShoppingItemList(ObservableCollection<ShoppingItem> Items)
        {
            this.ShoppingItems = Items;
        }

        public ShoppingItemList(ObservableCollection<ShoppingItem> shoppingItems, string name)
        {
            this.ShoppingItems = shoppingItems;
            this.Name = name;
        }

        public ShoppingItemList(ShoppingItemList shoppingItemList) 
        { 
            this.Name=shoppingItemList.Name;
            this.TotalCost = shoppingItemList.TotalCost;
            this.ShoppingDate = shoppingItemList.ShoppingDate;
            this.Location = shoppingItemList.Location;
            this.ShoppingItems = shoppingItemList.ShoppingItems;
        
        }

    }
}

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

        // Name of list/ template
        public string Name { get; set; }
        
        // Total shopping cost
        public double TotalCost { get; set; }

        public DateTime ShoppingDate { get; set; }

        // Shopping location
        public string Location { get; set; }
  
        // Shopping items added to list
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<ShoppingItem> ShoppingItems { get; set; } = new ObservableCollection<ShoppingItem>();

        public ShoppingItemList() 
        { 

        }

    }
}

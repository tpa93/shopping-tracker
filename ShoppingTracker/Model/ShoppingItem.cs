using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingTracker.Model
{   
    internal class ShoppingItem
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public bool Checked { get; set; }   
        public ShoppingItem(string name, int count) 
        { 
            this.Name = name;
            this.Count = count;
            this.Checked = false;
        
        }
    }
}

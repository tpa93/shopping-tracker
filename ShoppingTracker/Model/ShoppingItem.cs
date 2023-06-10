using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingTracker.Model
{   
    public class ShoppingItem
    {
        public string Name { get; set; }
        public string Count { get; set; }
        public bool Checked { get; set; }

        public ShoppingItem(string name, string count) 
        { 
            this.Name = name;
            this.Count = count;
            this.Checked = false;
        
        }
        public ShoppingItem() { }
    }
}

using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ShoppingTracker.Model
{
    [Table ("shopping_items")]
    public class ShoppingItem: INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Count { get; set; }

        bool _checked;
        public bool Checked 
        { 
            get { return _checked; }
            set 
            { 
                _checked = value;
                OnPropertyChanged();
            }
        }

        [ForeignKey(typeof(ShoppingItemList))]
        public int ShoppingItemListId { get; set; }

        public ShoppingItem(string name, string count) 
        { 
            this.Name = name;
            this.Count = count;
            this.Checked = false;
        
        }

        public ShoppingItem(string name, string count, bool check)
        {
            this.Name = name;
            this.Count = count;
            this.Checked = check;

        }
        public ShoppingItem() { }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

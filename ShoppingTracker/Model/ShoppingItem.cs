using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ShoppingTracker.Model
{   
    public class ShoppingItem: INotifyPropertyChanged
    {
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace ShoppingTracker.ViewModel
{
    internal class DeleteTemplateViewModel
    {
        public ObservableCollection<string> Templates { get; set; }
        public DeleteTemplateViewModel() 
        {
            Templates = new ObservableCollection<string>();
            Templates.Add("Test");
            Templates.Add("Test");

        }
    }
}

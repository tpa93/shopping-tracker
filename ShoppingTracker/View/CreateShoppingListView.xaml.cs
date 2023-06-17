using ShoppingTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingTracker.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateShoppingListView : ContentPage
    {
        public CreateShoppingListView()
        {
            InitializeComponent();
         
        }

        private void InputItem_Completed(object sender, EventArgs e)
        {
            // Focus next entry
            InputItemCount.Focus();
        }

        private void InputItemCount_Completed(object sender, EventArgs e)
        {
            // Fire command in ViewModel to add entered item
            var vM = BindingContext as CreateShoppingListViewModel;
            vM.AddShoppingItemCommand.Execute(null);
            
        }
    }
}
using ShoppingTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingTracker.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingListScrollView : ContentView
    {
        public ShoppingListScrollView()
        {
            InitializeComponent();
        }


        // CREATE BINDABLE PROPERTY FOR DATA SOURCE
        // Property exposed by the control - to use in XAML file
        public ObservableCollection<ShoppingItem> ShoppingItemsDataSource { get; set; }
        
        
        // Create bindable property that is tracked by the Xamarin.Forms property system
        public static readonly BindableProperty ShoppingItemsDataSourceProperty = BindableProperty.Create(
            nameof(ShoppingItemsDataSource), 
            typeof(ObservableCollection<ShoppingItem>), 
            typeof(ShoppingListScrollView), null, 
            BindingMode.TwoWay, null, 
            ItemsSourcePropertyChanged);

        

        // Event that gets fired when data in the observable collection changes
        private static void ItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object  newValue)
        {
            var control = (ShoppingListScrollView)bindable;
            control.ItemList.ItemsSource = (ObservableCollection<ShoppingItem>)newValue;
        }


        /*
          public ImageButton DeleteShoppingItemButton { get; set; }
          public Command DeleteShoppingItemCommand { get; set; }

          private void createImageButton()
          {
              DeleteShoppingItemButton = new ImageButton
              {
                  Source = "delete_shopping_item.png",
                  BackgroundColor = Color.Transparent,
                  Scale = 0.5,
                  Command = DeleteShoppingItemCommand
              };

          }
      */
    }
}
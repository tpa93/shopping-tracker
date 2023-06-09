using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ShoppingTracker.Model;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;
using ShoppingTracker.Services;

namespace ShoppingTracker.ViewModel
{
    internal class CreateShoppingListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ShoppingItem> ShoppingItems { get; set; }

        public CreateShoppingListViewModel()
        {
            
            ShoppingItems = new ObservableCollection<ShoppingItem>();
            ShoppingItems.Add(new ShoppingItem("Test 1", "1"));
            ShoppingItems.Add(new ShoppingItem("Test 2", "2"));
            ShoppingItems.Add(new ShoppingItem("Test 3", "3"));
            
            
        }


        // Use public Properties to set the fields of the NewShoppingItem
        // If value changed, raise event to notify the view
        private ShoppingItem NewShoppingItem { get; set; } = new ShoppingItem();

        public string NewItemName
        {
            get { return this.NewShoppingItem.Name; }
            set
            {
                if (NewShoppingItem.Name != value)
                {
                    this.NewShoppingItem.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NewItemCount
        {
            get { return this.NewShoppingItem.Count; }
            set
            {
                if (NewShoppingItem.Count != value)
                {
                    this.NewShoppingItem.Count = value;
                    OnPropertyChanged();
                }
            }
        }

        // Fire event if property value bound to view is changed, to update view
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }







        // Add ShoppingItem with user input via input grid - Command is bound on "+" - button
        public ICommand AddShoppingItemCommand => new Command(AddNewShopingItem);

        void AddNewShopingItem()
        {
            if (NewItemCount == "" || NewItemCount == null)
            {
                // Update fiel direct, to not trigger unnecessary event 
                NewShoppingItem.Count = "1";
            }
            if (NewItemName != "" && NewItemName != null)
            {
                ShoppingItems.Add(NewShoppingItem);

            }

            NewItemName = "";
            NewItemCount = "";
        }






        // Remove ShoppingItem via trash bin image (button) - Command is bound in CreateShoppingListView.xaml on ImageButton
        public ICommand RemoveShoppingItemCommand => new Command(RemoveShoppingItem);

        void RemoveShoppingItem(object o)
        {
            ShoppingItem itemToRemove = o as ShoppingItem;
            ShoppingItems.Remove(itemToRemove);
        }






        // Process ShoppingItems according to choosed option on ActionSheet
        public ICommand ProcessShoppingItemsCommand => new Command(ProcessShoppingItems);

        async void ProcessShoppingItems()
        {

            string action = await Application.Current.MainPage.DisplayActionSheet("What do you want to do?", "Cancel", null, "Go shopping", "Save as template", "Save as template & go shopping");

            if (action != "Cancel")
            {
                ShoppingItemList shoppingItemList = new ShoppingItemList(ShoppingItems);

                if (action == "Go shopping")
                {
                    // Transfer current data state of ObservableCollection to "ShoppingView"

                }
                else
                {
                    // Prompt user for template name and validate input
                    shoppingItemList.Name = await PromptUserForTemplateName();

                    if (shoppingItemList.Name == null || shoppingItemList.Name == "")
                    {
                        await Application.Current.MainPage.DisplayAlert("Operation aborted!", null, "Ok");
                    }

                    else if (action == "Save as template")
                    {
                        // Save current data state of ObservableCollection as JSON on local device
                        await ShoppingItemListTemplateHandler.SaveShoppingItemListTemplate(shoppingItemList);

                    }

                    else if (action == "Save as template & go shopping")
                    {
                        // Save current data state of ObservableCollection as JSON on local device

                        // Transfer current data state of ObservableCollection to "ShoppingView"

                    }
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Operation aborted!", null, "Ok");
            }
        }

        async Task<string> PromptUserForTemplateName()
        {
            // Prompt user for template name
            string templateName = string.Empty;
            while (templateName == string.Empty)
            {
                templateName = await Application.Current.MainPage.DisplayPromptAsync("Define template name", "Name:");
            }
            return templateName;
        }
    }
}

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
using PCLStorage;
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


        // Use public Properties to get access to OnPropertyChanged, to clear user input fields after adding an item
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

        // Fire event if property bound to view is changed, to update view
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
                // Update field direct, to not trigger unnecessary event 
                NewShoppingItem.Count = "1";
            }
            if (NewItemName != "" && NewItemName != null)
            {
                // Use temp item, because the values of NewShoppingItem will be overwritten, when clearing properties in the step afterwards
                ShoppingItems.Add(new ShoppingItem(NewItemName, NewItemCount));

            }

            // Clear user input fields with OnPropertyChanged()
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
            if (ShoppingItems.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("No shopping items in template to proceed", null, "Ok");
                return;
            }

            // Prompt user how to proceed with template
            string action = await Application.Current.MainPage.DisplayActionSheet("What do you want to do?", "Cancel", null, "Go shopping", "Save as template", "Save as template & go shopping");

            if (action != "Cancel")
            {
                ShoppingItemList shoppingItemList = new ShoppingItemList(ShoppingItems);

                // Proceed with non-saved template to work with
                if (action == "Go shopping")
                {
                    // Transfer current data state of ObservableCollection to "ShoppingView"

                }

                else
                {
                    // Prompt user for template name
                    shoppingItemList.Name = await PromptUserForTemplateName();

                    // Save list as template
                    if (action == "Save as template" && shoppingItemList.Name != null)
                    {

                        // Save template and check for success
                        bool check = await FileHandler.SaveSILTemplateOnDevice(shoppingItemList);
                        if (!check)
                        {
                            await Application.Current.MainPage.DisplayAlert("Template could not be saved due to unknown error", null, "Cancel");
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Template saved", null, "Ok");
                        }
                        return;
                    }

                    else if (action == "Save as template & go shopping" && shoppingItemList.Name != null)
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

            // Get all saved template names
            List<string> blockedTemplateNames = await FileHandler.GetAllSILTemplateNames();

            // Validate user input
            while (templateName == string.Empty)
            {
                templateName = await Application.Current.MainPage.DisplayPromptAsync("Define template name", "Name:");

                if(templateName == string.Empty)
                {
                    await Application.Current.MainPage.DisplayAlert("Please define a name", null, "Ok");
                }
                if (blockedTemplateNames.Contains(templateName))
                {
                    await Application.Current.MainPage.DisplayAlert("Template name already existing", null, "Ok");
                }
            }
            return templateName;
        }
    }
}

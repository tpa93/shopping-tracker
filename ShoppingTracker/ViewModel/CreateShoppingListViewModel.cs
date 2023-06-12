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
using Newtonsoft.Json;

namespace ShoppingTracker.ViewModel
{
    internal class CreateShoppingListViewModel: INotifyPropertyChanged
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
                ShoppingItemList shoppingItemListTemplate = new ShoppingItemList(ShoppingItems);

                // Proceed with non-saved template to work with
                if (action == "Go shopping")
                {
                    // Transfer current data state of ObservableCollection to "ShoppingView" and navigate to ShoppingView
                    //App.ActiveShoppingItemList = new ShoppingItemList(new ObservableCollection<ShoppingItem>(shoppingItemListTemplate.ShoppingItems), shoppingItemListTemplate.Name);
                    //await Shell.Current.GoToAsync("//goShoppingView");
                    PassDataAndFollowRoute("//goShoppingView", shoppingItemListTemplate);
                }

                else
                {
                    // Prompt user for template name
                    shoppingItemListTemplate.Name = await PromptUserForTemplateName();

                    // Save list as template
                    if (action == "Save as template" && shoppingItemListTemplate.Name != null)
                    {

                        // Save template and check for success
                        await SaveSILTemplate(shoppingItemListTemplate);
                        return;
                    }

                    else if (action == "Save as template & go shopping" && shoppingItemListTemplate.Name != null)
                    {
                        // Save template and check for success
                        if(!await SaveSILTemplate(shoppingItemListTemplate))
                        {
                            return;
                        }

                        // Transfer current data state of ObservableCollection to "ShoppingView"
                        //App.ActiveShoppingItemList = shoppingItemListTemplate;
                        await Shell.Current.GoToAsync("//goShoppingView");
                    }
                }
            }
        }


        async Task<string> PromptUserForTemplateName()
        {
            // Prompt user for template name
            string templateName = string.Empty;

            // Get all saved template names
            List<string> blockedTemplateNames = await TemplateHandler.GetAllSILTemplateNames();

            // Validate user input
            while (templateName == string.Empty)
            {
                templateName = await Application.Current.MainPage.DisplayPromptAsync("Define template name", "Name:");

                if(templateName == string.Empty)
                {
                    await Application.Current.MainPage.DisplayAlert("Please define a name", null, "Ok");
                }
                else if (blockedTemplateNames.Contains(templateName))
                {
                    string action = await Application.Current.MainPage.DisplayActionSheet("Template name already existing. Do you want to overwrite?", "Cancel", null, "Ok");
                    if (action == "Ok")
                    {
                        return templateName;
                    }
                }
            }
            return templateName;
        }


        // Validate saving template and respond to user
        async Task<bool> SaveSILTemplate(ShoppingItemList shoppingItemList)
        {
 
            if (!await TemplateHandler.SaveSILTemplateOnDevice(shoppingItemList))
            {
                await Application.Current.MainPage.DisplayAlert("Template could not be saved due to unknown error", null, "Cancel");
                return false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Template saved", null, "Ok");
                return true;
            }
        }

        async void PassDataAndFollowRoute(string route, ShoppingItemList activeShoppingItemList)
        {
            string json = JsonConvert.SerializeObject(activeShoppingItemList);
            await Shell.Current.GoToAsync($"{route}?JSON={json}");
        }
    }
}

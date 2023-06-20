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
using ShoppingTracker.Services;
using Newtonsoft.Json;

namespace ShoppingTracker.ViewModel
{
    internal class CreateShoppingListViewModel: INotifyPropertyChanged
    {
        // Current ShoppingItemList used and edited by user
        ShoppingItemList activeShoppingItemList = new ShoppingItemList();

        public ShoppingItemList ActiveShoppingItemList 
        { 
            get
            {
                return activeShoppingItemList;
            }
            
            set
            {
                activeShoppingItemList = value;
                OnPropertyChanged();
            }
        }

        public CreateShoppingListViewModel()
        {
            ActiveShoppingItemList = new ShoppingItemList();
        }


        // Store user input for new items
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        // Add ShoppingItem to ActiveShoppingItemList - Command bound on ImageButton
        public ICommand AddShoppingItemCommand => new Command(AddNewShopingItem);

        void AddNewShopingItem()
        {
            if (NewItemCount == "" || NewItemCount == null || !Double.TryParse(NewShoppingItem.Count, out double result))
            {
                // Update field direct, to not trigger unnecessary event 
                NewShoppingItem.Count = "1";
            }
            if (NewItemName != "" && NewItemName != null)
            {
                // Use new item, because the values of NewShoppingItem will be overwritten, when clearing properties in the step afterwards
                ActiveShoppingItemList.ShoppingItems.Add(new ShoppingItem(NewItemName, NewItemCount));

                NewItemName = "";
                NewItemCount = "";
            }
            
        }



        // Remove ShoppingItem selected by user - bound on ImageButton in ListView
        public ICommand RemoveShoppingItemCommand => new Command(RemoveShoppingItem);

        void RemoveShoppingItem(object o)
        {
            ShoppingItem itemToRemove = o as ShoppingItem;
            ActiveShoppingItemList.ShoppingItems.Remove(itemToRemove);
        }


        // Process ShoppingItems according to choosed option
        public ICommand ProcessShoppingItemsCommand => new Command(ProcessShoppingItems);

        async void ProcessShoppingItems()
        {

            // Prompt user how to proceed with template
            string action = await Application.Current.MainPage.DisplayActionSheet("What do you want to do?", "Cancel", null, "Go shopping", "Save as template", "Save as template & go shopping", "Load template");

            if (action != "Cancel")
            {

                // Proceed with non-saved template to work with
                if (action == "Go shopping")
                {
                    // Navigate to ShoppingView with current data state of ShoppingItemList
                    PassDataAndFollowRoute("//goShoppingView", ActiveShoppingItemList);
                }

                // Load ShoppingItemList template selected by user into CreateShoppingListView
                else if (action == "Load template")
                {
                    List<string> userTemplateNames = await SILFileHandler.GetAllSILTemplateNames();

                    string templateName= await Application.Current.MainPage.DisplayActionSheet("Choose template to edit:", "Cancel", null, userTemplateNames.ToArray());

                    if (templateName == "Cancel" || templateName == null)
                    {
                        return;
                    }

                    ActiveShoppingItemList = await SILFileHandler.GetSILTemplateFromDevice(templateName); 

                }
                else
                {
                    // Prompt user for template name
                    ActiveShoppingItemList.Name = await PromptUserForTemplateName();

                    if (ActiveShoppingItemList.Name == null)
                    {
                        return;
                    }

                    // Save list as template
                    if (action == "Save as template" && ActiveShoppingItemList.Name != null)
                    {

                        await SaveSILTemplate(ActiveShoppingItemList);
                        return;
                    }

                    // Save list as template and navigate to ShoppingView with current data state of ShoppingItemList
                    else if (action == "Save as template & go shopping" && ActiveShoppingItemList.Name != null)
                    {
                        if(!await SaveSILTemplate(ActiveShoppingItemList))
                        {
                            return;
                        }

                        PassDataAndFollowRoute("//goShoppingView", ActiveShoppingItemList);
                    }
                }
            }
        }

        async Task<string> PromptUserForTemplateName()
        {

            string templateName = string.Empty;

            // Get all saved template names
            List<string> blockedTemplateNames = await SILFileHandler.GetAllSILTemplateNames();
            if (blockedTemplateNames == null)
            {
                await Application.Current.MainPage.DisplayAlert("Template could not be saved due to unknown error", null, "Ok");
                return null;
            }

            // Validate user input
            while (templateName == string.Empty)
            {

                templateName = await Application.Current.MainPage.DisplayPromptAsync("Define template name", "Name:", initialValue: ActiveShoppingItemList.Name);

                // When prompt was canceled
                if (templateName == null)
                {
                    return null;
                }

                // When user enters empty string
                else if (templateName == string.Empty)
                {
                    await Application.Current.MainPage.DisplayAlert("Please define a name", null, "Ok");
                }

                // When template name already existing
                else if (blockedTemplateNames.Contains(templateName))
                {
                    bool action = await Application.Current.MainPage.DisplayAlert("Template name already existing. Do you want to overwrite?", null, "Ok", "Cancel");
                    if (action == true)
                    {
                        return templateName;
                    }
                    else
                    {
                        templateName = string.Empty;
                    }
                }
            }
            return templateName;
        }


        // Validate saving template and respond to user
        async Task<bool> SaveSILTemplate(ShoppingItemList shoppingItemList)
        {
 
            if (!await SILFileHandler.SaveSILTemplateOnDevice(shoppingItemList))
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


        // Navigate with current data state to specified route
        async void PassDataAndFollowRoute(string route, ShoppingItemList activeShoppingItemList)
        {
            string json = JsonConvert.SerializeObject(activeShoppingItemList);
            await Shell.Current.GoToAsync($"{route}?JSON={json}");
        }
    }
}

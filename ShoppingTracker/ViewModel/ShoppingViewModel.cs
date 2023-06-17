using Newtonsoft.Json;
using ShoppingTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using System.ComponentModel.Design;
using ShoppingTracker.Services;

namespace ShoppingTracker.ViewModel
{

    [QueryProperty(nameof(JSON), "JSON")]
    internal class ShoppingViewModel : INotifyPropertyChanged
    {
        public ShoppingViewModel()
        {
            ActiveShoppingItemList = new ShoppingItemList();
        }


        // JSON string received by CreateShoppingListTemplateViewModel
        string json;
        public string JSON
        {
            get
            {
                return json;
            }
            //  When routing to view
            set
            {
                json = Uri.UnescapeDataString(value ?? String.Empty);

                // Trigger setter of ActiveShoppingItemList to update view
                ActiveShoppingItemList = JsonConvert.DeserializeObject<ShoppingItemList>(json);
            }
        }


        // Currently active ShoppingItemList
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


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // EventToCommandBehavior for TappedItem Event - update tapped ShoppingItem as Checked/Not Checked
        // Property is bound to Image in ShoppingView.xaml
        public ICommand TappedShoppingItemCommand => new Command<ShoppingItem>(SelectedShoppingItem);
        void SelectedShoppingItem(ShoppingItem selectedItem)
        {
            if (selectedItem.Checked == false)
            {
                ActiveShoppingItemList.ShoppingItems[ActiveShoppingItemList.ShoppingItems.IndexOf(selectedItem)].Checked = true;
            }
            else
            {
                ActiveShoppingItemList.ShoppingItems[ActiveShoppingItemList.ShoppingItems.IndexOf(selectedItem)].Checked = false;
            }

        }

        public ICommand FinishShoppingCommand => new Command(FinishShopping);
        async void FinishShopping()
        {
            string action = string.Empty;
            string totalCost = string.Empty;
            string location = string.Empty;

            while (action != "Cancel" && action != "Finish shopping")
            {
                action = await Application.Current.MainPage.DisplayActionSheet("Add extra information to shopping list", "Cancel", null, "Add total cost", "Add location", "Finish shopping");

                // Prompt user for total shopping cost
                if (action == "Add total cost")
                {
                    totalCost = await Application.Current.MainPage.DisplayPromptAsync("Total cost", "Enter total shopping cost:", initialValue: totalCost);
                    ActiveShoppingItemList.TotalCost = Convert.ToDouble(totalCost);
                }

                /*
                // Prompt user for taking or selecting a photo from bill
                else if (action == "Add photo of bill")
                {
                    // Because it is currently not possible to use the Mediapicker on Android 13.0 API 33 according to created GITHUB issue
                    // https://github.com/xamarin/Essentials/issues/2041

                }
                */

                // Prompt user for adding location
                else if (action == "Add location")
                {
                    location = await Application.Current.MainPage.DisplayPromptAsync("Location", "Enter shopping location:", initialValue: location);
                    ActiveShoppingItemList.Location = location;

                }
                // Save ActiveShoppingItemList with eventually added costs
                else if (action == "Finish shopping")
                {
                    if(!await IgnoreMissingItems())
                    {
                        return;
                    }

                    await AddShoppingDateToActiveShoppingList();

                    if(! DatabaseHandler.InsertShoppingItemList(ActiveShoppingItemList)) 
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Shopping list could not be saved to history", "Ok");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Saved", "Shopping list with all information added was saved to history", "Ok");
                    }
                    return;
                }
            }
        }

        // Check ActiveShoppingList for unchecked items and prompt user how to handle
        async Task<bool> IgnoreMissingItems()
        {
            // Check for unchecked items and propmt user to react if he wants to continue to finish shopping, when there are unchecked items
            foreach (var item in ActiveShoppingItemList.ShoppingItems)
            {
                if (item.Checked == false)
                {
                    if (!await Application.Current.MainPage.DisplayAlert("List not complete", "Not every item on the list is checked off. Do you want to continue?", "Yes", "Cancel"))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
            }

            return true;
        }

        // Prompt user how to handle shopping date
        async Task AddShoppingDateToActiveShoppingList()
        {
            if (!await Application.Current.MainPage.DisplayAlert("Shopping date", "Do you want to set the current date and time as shopping date?", "Yes", "No"))
            {
                DateTime dateValue;
                string shoppingDate = await Application.Current.MainPage.DisplayPromptAsync("Enter shopping date:", null);

                while (!DateTime.TryParse(shoppingDate, out dateValue) && shoppingDate == null)
                {
                    shoppingDate = await Application.Current.MainPage.DisplayPromptAsync("Enter shopping date", "Please enter a valid date:");
                }
                activeShoppingItemList.ShoppingDate = dateValue;
            }
            else
            {
                ActiveShoppingItemList.ShoppingDate = DateTime.Now;
            }
        }

        // Load template into ShoppingView
        public ICommand LoadShoppingItemListTemplateCommand => new Command(LoadShoppingItemListTemplate);
        async void LoadShoppingItemListTemplate()
        {

            List<string> userTemplateNames = await SILFileHandler.GetAllSILTemplateNames();

            string templateName = await Application.Current.MainPage.DisplayActionSheet("Choose template for shopping:", "Cancel", null, userTemplateNames.ToArray());

            if (templateName == "Cancel" || templateName == null)
            {
                return;
            }

            ActiveShoppingItemList = await SILFileHandler.GetSILTemplateFromDevice(templateName);
        }

    }
}

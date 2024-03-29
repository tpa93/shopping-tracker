﻿using Newtonsoft.Json;
using ShoppingTracker.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using ShoppingTracker.Services;
using ShoppingTracker.View;


namespace ShoppingTracker.ViewModel
{

    [QueryProperty(nameof(JSON), "JSON")]
    internal class ShoppingViewModel : INotifyPropertyChanged
    {

        // JSON string received when routing to view, where view model is set as binding context
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
                ActiveShoppingItemList = JsonConvert.DeserializeObject<ShoppingItemList>(json);
            }
        }


        // Active ShoppingItemList
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

        public ShoppingViewModel()
        {
            ActiveShoppingItemList = new ShoppingItemList();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // EventToCommandBehavior for TappedItem event - update tapped ShoppingItem as checked off/ not checked off
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

        // Finish shopping command triggered when clicking ImageButton bound to
        public ICommand FinishShoppingCommand => new Command(FinishShopping);
        async void FinishShopping()
        {
            string action = string.Empty;
            string totalCost = string.Empty;
            string location = string.Empty;

            while (action != "Cancel" && action != "Finish shopping")
            {
                // Prompt user for adding additional informations
                action = await Application.Current.MainPage.DisplayActionSheet("Add extra information to shopping list", "Cancel", null, "Add total cost", "Add location", "Finish shopping");

                if (action == "Add total cost")
                {
                    totalCost = await Application.Current.MainPage.DisplayPromptAsync("Total cost", "Enter total shopping cost:", initialValue: Convert.ToString(ActiveShoppingItemList.TotalCost));

                    if (totalCost != null)
                    {
                        try
                        {
                            ActiveShoppingItemList.TotalCost = Convert.ToDouble(totalCost);
                        }
                        catch (Exception ex)
                        {
                           await Application.Current.MainPage.DisplayAlert("Invalid input", "Please enter a valid value.", "Ok");
                        }   
                    }
                    else
                    {
                        ActiveShoppingItemList.TotalCost = 0;
                        totalCost = String.Empty;
                    }
                }

                /*
                // Prompt user for taking or selecting a photo off bill
                else if (action == "Add photo of bill")
                {
                    // Because it is currently not possible to use the Mediapicker on Android 13.0 API 33 according to created GITHUB issue
                    // https://github.com/xamarin/Essentials/issues/2041
                    var photo = await MediaPicker.PickPhotoAsync();

                }
                */


                else if (action == "Add location")
                {
                    location = await Application.Current.MainPage.DisplayPromptAsync("Location", "Enter shopping location:", initialValue: location);
                    ActiveShoppingItemList.Location = location;

                }

                // Save ActiveShoppingItemList
                else if (action == "Finish shopping")
                {
                    if (!await IgnoreMissingItems())
                    {
                        return;
                    }

                    // Prompt user to set a shopping date
                    await AddShoppingDateToActiveShoppingList();


                    // Save data to database
                    if (!DatabaseHandler.InsertShoppingItemList(ActiveShoppingItemList))
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

        // Check ActiveShoppingList for unchecked items and display hint with prompt how to handle
        async Task<bool> IgnoreMissingItems()
        {
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

        // Prompt user how to set shopping date
        async Task AddShoppingDateToActiveShoppingList()
        {
            if (!await Application.Current.MainPage.DisplayAlert("Shopping date", "Do you want to set the current date and time as shopping date?", "Yes", "No"))
            {
                DateTime dateValue;
                string shoppingDate = await Application.Current.MainPage.DisplayPromptAsync("Enter shopping date:", null);

                while (!DateTime.TryParse(shoppingDate, out dateValue) || shoppingDate == null)
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


        // Add photo of bill
        public ICommand AddPhotoCommand => new Command(AddPhoto);
        async void AddPhoto()
        {
            if (ActiveShoppingItemList.PhotoOfBill == null)
            {
                var photoView = new PhotoView();
                photoView.UserActionOnPhoto += OnActionOnPhotoView;
                await Application.Current.MainPage.Navigation.PushAsync(photoView);
            }
            else
            {
                var photoView = new PhotoView(ActiveShoppingItemList.PhotoOfBill, true);
                await Application.Current.MainPage.Navigation.PushAsync(photoView);
            }
        }

        // React to event in view
        void OnActionOnPhotoView(object sender, byte[] image)
        {
            if (image != null)
            {
                ActiveShoppingItemList.PhotoOfBill = image;
                Application.Current.MainPage.DisplayAlert("Photo of bill attached", null, "Ok");
            }
        }
    }
}

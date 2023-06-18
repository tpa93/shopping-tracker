using ShoppingTracker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShoppingTracker.ViewModel
{
    internal class DeleteTemplateViewModel: INotifyPropertyChanged
    {
        // All templates created by user
        ObservableCollection<string> templates = new ObservableCollection<string>();

        public ObservableCollection<string> Templates 
        { 
            get { return templates; }
            set
            {
                templates = value;
                OnPropertyChanged();
            }
        }

        public DeleteTemplateViewModel() 
        {
            InitializeTemplatesCollection();
        }

        // Get all templates created by user
        public async void InitializeTemplatesCollection()
        {
            Templates = new ObservableCollection<string>(await SILFileHandler.GetAllSILTemplateNames());
            if(Templates == null) 
            {
               Application.Current.MainPage.DisplayAlert("Error", "Loading templates data failed due to unknown error", "OK");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // Delete template selected by user - bound on ImageButton in ListView
        public ICommand DeleteTemplateCommand => new Command<string>(DeleteTemplate);

        async void DeleteTemplate(string  templateName) 
        {
            bool action = await Application.Current.MainPage.DisplayAlert($"Are you sure that you want to delete {templateName} ?", null, "Yes", "Cancel");
            if (action == true)
            {
                if (await SILFileHandler.DeleteSILTemplateFromDevice(templateName))
                {
                    templates.Remove(templateName);
                }
            }
            else
            {
                return;
            }
        }
    }
}

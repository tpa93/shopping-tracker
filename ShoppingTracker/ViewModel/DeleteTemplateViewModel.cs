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

        public async void InitializeTemplatesCollection()
        {
            Templates = new ObservableCollection<string>(await SILFileHandler.GetAllSILTemplateNames());
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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

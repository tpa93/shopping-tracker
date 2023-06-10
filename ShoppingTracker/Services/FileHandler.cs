using PCLStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShoppingTracker.Model;

namespace ShoppingTracker.Services
{
    // ShoppingItemListTemplateHandler
    public static class FileHandler
    {
        // Save ShoppingItemListTemplate in templates folder on local device
        public async static Task<bool> SaveSILTemplateOnDevice(ShoppingItemList shoppingItemList)
        {
            // Create file
            IFolder folder = await PCLStorage.FileSystem.Current.LocalStorage.CreateFolderAsync("templates", CreationCollisionOption.OpenIfExists);
            string templateFile = shoppingItemList.Name + ".txt";
            IFile file = await folder.CreateFileAsync(templateFile, CreationCollisionOption.FailIfExists);

            // Convert object to JSON-string and write file
            string json = JsonConvert.SerializeObject(shoppingItemList);
            await file.WriteAllTextAsync(json);
            
            return true;

        }

        // Get ShoppingItemListTemplate from templates folder on local device
        public async static Task<ShoppingItemList> GetSILTemplateFromDevice(string templateName)
        {
            // Get file
            IFolder folder = await PCLStorage.FileSystem.Current.LocalStorage.CreateFolderAsync("templates", CreationCollisionOption.OpenIfExists);
            string fileName = templateName + ".txt";
            IFile file = await folder.GetFileAsync(fileName);

            // Read file and convert to object from JSON-string
            string json = await file.ReadAllTextAsync();
            ShoppingItemList newShoppingItemList = JsonConvert.DeserializeObject<ShoppingItemList>(json);
            newShoppingItemList.Name = templateName;

            return newShoppingItemList;
        }

        // Get all template names from templates folder on local device
        public async static Task<List<string>> GetAllSILTemplateNames()
        {
            // Get alle template file names
            IFolder folder = await PCLStorage.FileSystem.Current.LocalStorage.CreateFolderAsync("templates", CreationCollisionOption.OpenIfExists);
            IList<IFile> files = await folder.GetFilesAsync();

            // Separate file typ ending from string
            List<string> templateNames = new List<string>();

            foreach (IFile file in files) 
            {
                templateNames.Add(file.Name.Replace(".txt", ""));
            }

            return templateNames;

        }
    }
}

using PCLStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShoppingTracker.Model;
using System.Security.Cryptography.X509Certificates;

namespace ShoppingTracker.Services
{
    // ShoppingItemListTemplateHandler
    public static class SILFileHandler
    {

        // Save ShoppingItemListTemplate in templates folder on local device
        public async static Task<bool> SaveSILTemplateOnDevice(ShoppingItemList shoppingItemList)
        {
            
            try
            {   
                // Create file
                IFolder folder = await PCLStorage.FileSystem.Current.LocalStorage.CreateFolderAsync("templates", CreationCollisionOption.OpenIfExists);
                string templateFile = shoppingItemList.Name + ".txt";
                IFile file = await folder.CreateFileAsync(templateFile, CreationCollisionOption.ReplaceExisting);

                // Convert object to JSON-string and write file
                string json = JsonConvert.SerializeObject(shoppingItemList);
                await file.WriteAllTextAsync(json);
                
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
            

        }

        // Get ShoppingItemListTemplate from templates folder on local device
        public async static Task<ShoppingItemList> GetSILTemplateFromDevice(string templateName)
        {
            try
            {
                // Get file
                IFolder folder = await PCLStorage.FileSystem.Current.LocalStorage.CreateFolderAsync("templates", CreationCollisionOption.OpenIfExists);
                string fileName = templateName + ".txt";
                IFile file = await folder.GetFileAsync(fileName);

                // Read file and convert to object from JSON-string
                string json = await file.ReadAllTextAsync();
                ShoppingItemList newShoppingItemList = JsonConvert.DeserializeObject<ShoppingItemList>(json);

                return newShoppingItemList;
            }
            catch (Exception ex) 
            {
                return null;
            }

            
        }

        // Delete ShoppinItemListTemplate from templates folder on local device
        public async static Task<bool> DeleteSILTemplateFromDevice(string templateName)
        {
            try
            {
                IFolder folder = await PCLStorage.FileSystem.Current.LocalStorage.CreateFolderAsync("templates", CreationCollisionOption.OpenIfExists);
                string fileName = templateName + ".txt";
                IFile file = await folder.GetFileAsync(fileName);
                await file.DeleteAsync();
                return true;
            }
            catch(Exception ex) 
            {
                return false;
            } 
        }

        // Get all template names from templates folder on local device
        public async static Task<List<string>> GetAllSILTemplateNames()
        {
            try
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
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

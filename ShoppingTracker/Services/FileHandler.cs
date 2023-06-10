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
        public async static Task<bool> SaveSILTemplateOnDevice(string fileName, ShoppingItemList shoppingItemList)
        {
            IFolder folder = PCLStorage.FileSystem.Current.LocalStorage;
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.FailIfExists);
            string json = JsonConvert.SerializeObject(shoppingItemList);
            await file.WriteAllTextAsync(json);
            return true;

        }

        public async static Task<ShoppingItemList> GetSILTemplateFromDevice(string fileName)
        {
            IFolder folder = PCLStorage.FileSystem.Current.LocalStorage;
            IFile file = await folder.GetFileAsync(fileName);
            string json = await file.ReadAllTextAsync();
            ShoppingItemList newShoppingItemList = new ShoppingItemList();
            newShoppingItemList= JsonConvert.DeserializeObject<ShoppingItemList>(json);
            return newShoppingItemList;
        }

        public async static Task<List<string>> GetAllSILTemplateNames()
        {
            IFolder folder = PCLStorage.FileSystem.Current.LocalStorage;
            IList<IFile> files = await folder.GetFilesAsync();
            List<string> templateNames = new List<string>();

            foreach (IFile file in files) 
            {
                templateNames.Add(file.Name.Replace(".txt", ""));
            }

            return templateNames;

        }
    }
}

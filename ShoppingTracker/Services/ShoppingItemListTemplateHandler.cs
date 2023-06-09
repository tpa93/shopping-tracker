using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Newtonsoft.Json;
using ShoppingTracker.Model;
using System.Threading.Tasks;
using PCLStorage;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShoppingTracker.Services
{
    internal static class ShoppingItemListTemplateHandler
    {

        
        // Save ShoppingItemList (template) to LocalStorage
        public static async Task<bool> SaveShoppingItemListTemplate(ShoppingItemList shoppingItemList)
        {
            string jsonString = JsonConvert.SerializeObject(shoppingItemList);

            IFolder root = PCLStorage.FileSystem.Current.LocalStorage;
            IFile tempFile = await root.CreateFileAsync(shoppingItemList.Name + ".txt", CreationCollisionOption.FailIfExists);
            await tempFile.WriteAllTextAsync(jsonString);

            return true;
        }

        // Get ShoppingItemList (template) from LocalStorage
        public static async Task<ShoppingItemList> GetShoppingItemListTemplate(string templateName) 
        {
            IFolder root = PCLStorage.FileSystem.Current.LocalStorage;
            IFile tempFile = await root.GetFileAsync(templateName + ".txt");
            string jsonString = await tempFile.ReadAllTextAsync();

            ShoppingItemList shoppingItemList = JsonConvert.DeserializeObject<ShoppingItemList>(jsonString);

            return shoppingItemList;
         
        }

    }
}

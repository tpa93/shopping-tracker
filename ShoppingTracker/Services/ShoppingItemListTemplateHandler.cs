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
    internal class ShoppingItemListTemplateHandler
    {
        public ShoppingItemListTemplateHandler() 
        { 
        
        }
        
        public async Task<ShoppingItemList> SaveShoppingItemListTemplate(ShoppingItemList shoppingItemList)
        {
            string json = JsonConvert.SerializeObject(shoppingItemList);

            IFolder root = PCLStorage.FileSystem.Current.LocalStorage;
            IFolder temp_folder = await root.CreateFolderAsync("shopping_items_templates", CreationCollisionOption.OpenIfExists);
        }

        public ShoppingItemList GetShoppingItemListTemplate(string templateName) 
        {
            string jsonString = "";
            ShoppingItemList shoppingItemList = JsonConvert.DeserializeObject<ShoppingItemList>(jsonString);
            return shoppingItemList;
         
        }

    }
}

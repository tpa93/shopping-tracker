using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using ShoppingTracker.Model;
using PCLStorage;
using System.IO;
using SQLiteNetExtensions.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Collections.ObjectModel;

namespace ShoppingTracker.Services
{
    public static class DatabaseHandler
    {
        private static SQLiteConnection db;

        static DatabaseHandler()
        {
            // Establish database connection
            string folder = PCLStorage.FileSystem.Current.LocalStorage.Path;
            string dbName = "shopping_tracker_db_data";
            db = new SQLiteConnection(Path.Combine(folder + dbName));

            db.CreateTable<ShoppingItem>();
            db.CreateTable<ShoppingItemList>();
        }

        // Push ShoppingItemList to database
        public static bool InsertShoppingItemList(ShoppingItemList shoppingItemList)
        {
            db.InsertWithChildren(shoppingItemList);
            return true;
        }

        // Get whole shopping history from database
        public static ObservableCollection<ShoppingItemList> GetTotalShoppingHistory()
        {
            try
            {
                return new ObservableCollection<ShoppingItemList>(db.GetAllWithChildren<ShoppingItemList>());
            }

            catch (Exception ex) 
            {
                return null;
            }
               

           

        }

    }
}

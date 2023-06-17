# ShoppingTracker

### Video Demo:  https://www.youtube.com/watch?v=WDaR99PQzrU

### ShoppingTracker - CS50 final project
ShoppingTracker is an Android app developed with C# and Xamarin, which helps you to organize and track all of your shopping needs.
With ShoppingTracker you can create wonderful shopping lists and templates, to take care that you don't forget anything during your daily or weekly shopping
and log your done shopping to a shopping history.

#### Features:
1. You can create templates, which you can use for shopping. It is possible to edit already created templates by adding or removing. After editing a previously created template, it is possible to overwrite it with the newly generated version.
The created templates are saved to the local device in the applications data folder. It is possible too to create one-time shopping list, which get not saved as a template.

2. Using one-time lists or loading previously created shopping list templates into the shopping view allows you to easily keep track of your shopping progress during your shopping, by checking off already gatheres items.
Checked off items get marked with a green check mark after tapping them in the List.

3. When you got all items you need, you can save your current shopping with additional information like total costs, location and shopping date to the shopping history.
The data saved to the history is stored in a SQLite database file locally on the device. In the shopping history it is possible to open a detailed view for a logged shopping history entry
to see which items were checked of and gathered related to this specific shopping history entry.

4. In the settings it is possible to clear the shopping history completely and delete unused shopping list templates.

#### Planned features:
For further development are several features planned. For example filtering shopping history data, adding geolocations as locations to the shopping history with the Google Places API or
adding photos of bills.

Additional to this features it was planned to setup a iOS version for this application. During development it tourned out that it is necessary to own a Apple Developer License to debug and release iOS applications.
Further information of an implementation of an iOS app will be released here.


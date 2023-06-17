# ShoppingTracker
### Video Demo:  https://www.youtube.com/watch?v=WDaR99PQzrU

### ShoppingTracker - CS50 final project
#### Improve your shopping experience with the help of ShoppingTracker
ShoppingTracker is an Android app developed in C# and with the Xamarin framework which helps you to plan, organize and keep track all of your shopping needs.
With ShoppingTracker you can create wonderful shopping lists and templates, to prepare and plan your future shoppings and take care so that you don't forget anything during your daily or weekly shopping
On top of that the app creates a log of your done shopping data, inserting it into a shopping history, to have a fast overview over past shopping and shopping costs.

#### Features:
##### Create one-time shopping list and shopping list templates for re-use
1. You can create templates, which you can use for shopping. It is possible to edit already created templates by adding or removing. After editing a previously created template, it is possible to overwrite it with the newly generated version.
The created templates are saved to the local device in the applications data folder. It is possible too to create one-time shopping list, which get not saved as a template.

##### Shopping experience
2. Using one-time lists or loading previously created shopping list templates into the shopping view allows you to easily keep track of your shopping progress during your shopping, by checking off already gatheres items.
Checked off items get marked with a green check mark after tapping them in the List.

##### History data
3. When you got all items you need, you can save your current shopping with additional information like total costs, location and shopping date to the shopping history.
The data saved to the history is stored in a SQLite database file locally on the device. In the shopping history it is possible to open a detailed view for a logged shopping history entry
to see which items were checked of and gathered related to this specific shopping history entry.

##### Settings
4. In the settings it is possible to clear the shopping history completely and delete unused shopping list templates.

#### Planned features:
For further development are several features planned. 
1. For example filtering and sorting shopping history data related to shopping location and shopping date, to improve user experience. Currently displayed history data is sorted in descending order by shopping date.
2. Grouping shopping history data by week, month or year.
3. Using geolocations to add the correct location data pinned to the shopping list history according to users shopping location with the Google Places API, so that a user do not need to enter it by hand.
4. Editing item values without removing them completely from the one-time list or template.

Additional to this features it was planned to setup a iOS version for this application. During development it tourned out that it is necessary to own a Apple developer license to debug and release iOS applications.
Further information of an implementation of an iOS app will be released here.



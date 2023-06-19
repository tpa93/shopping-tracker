# ShoppingTracker

### ShoppingTracker (CS50 final project)

#### Improve your shopping experience with the help of ShoppingTracker ;)
ShoppingTracker is an Android app developed with C# and the Xamarin framework which helps you to plan, organize and keep track all of your shopping needs.
With ShoppingTracker you can create wonderful shopping lists and templates, to prepare and plan your future shoppings and take care so that you don't forget anything when you're getting your daily or weekly needs
On top of that the app creates a log of your done shopping data, inserting it into a shopping history, to have a fast overview over past shoppings and shopping costs.

#### Features:
##### Create one-time shopping list and shopping list templates for re-use
1. You can create templates, which you can use for shopping. It is possible to edit already created templates by adding or removing items. After editing a previously created template, it is possible to overwrite it with its newer version.
Created templates are saved to the local device. Unsaved lists can be used in the current app session.

##### Shopping experience
2. Using one-time lists or loading previously created shopping list templates into the shopping view allows you to easily keep track of your shopping progress during your shopping by checking off already gathered items.
Checked off items get marked with a green check mark after tapping them in the list.

##### History data
3. When finishing shopping, you can save your current shopping progress with additional information like total costs, location and shopping date to the shopping history.
The data saved to the history is stored in a SQLite database file locally on the device. The history provides a little detail view where you can see checked off items related to the specific shopping saved.

##### Settings
4. Possible to delete unused shopping list templates and clear history data

#### Features planned
1. Logging location data via Google Places API to shopping history
2. Adding photo of bill to shopping history

An iOS version of the application was planned, but during development it tourned out that it is necessary to own a apple developer license to do so.

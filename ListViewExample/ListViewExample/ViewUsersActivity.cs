using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using ListViewExample.Helpers;

namespace ListViewExample
{
    [Activity(Label = "ViewUsersActivity")]
    public class ViewUsersActivity : ListActivity
    {
        List<Users> usersList = new List<Users>();
        List<User> contactList = Settings.Users;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //add the list to the list adapter
            ListAdapter = new ArrayAdapter<User>(this, Android.Resource.Layout.SimpleListItem1, contactList);
        }
    }
}
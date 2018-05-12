using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ListViewExample
{
    class Users
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        //contructor
        public Users(string username, string password)
        {
            UserName = username;
            Password = password;
        }

        public override string ToString()
        {
            return UserName;
        }
    }
}
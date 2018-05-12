using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using PerpetualEngine.Storage;
using ListViewExample.Helpers;
using Android.Views.InputMethods;
using Android.Views;
using Android.Content;
using Android.Text;

namespace ListViewExample
{
    [Activity(Label = "ListViewExample", MainLauncher = true)]
    public class MainActivity : Activity
    {
        List<User> usersList = new List<User>();

        #region Views
        private EditText textEntryPassword;
        private Button btnAdd;
        #endregion

        #region Fields
        private Regex reg = new Regex("^[ \\t]+|[ \\t]+$+");
        private Regex passwordRegex = new Regex(@"^[a-zA-Z][a-zA-Z0-9]*$");
        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SimpleStorage.SetContext(ApplicationContext);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.addButton);
            textEntryPassword = FindViewById<EditText>(Resource.Id.passwordEntry);
            btnAdd = FindViewById<Button>(Resource.Id.addButton);
          

            //button.Click += Button_Click;
            button.Click += (sender, e) =>
            {
                //get the information from the boxes
                EditText nameBox = FindViewById<EditText>(Resource.Id.usernameEntry);
                string name = nameBox.Text.Trim();

                EditText pwd_entry = FindViewById<EditText>(Resource.Id.passwordEntry);
                string pwd = pwd_entry.Text.Trim();

                var user = new User { Username = name, Password = pwd };

                usersList.Add(user);

                Settings.Users = usersList;

                //create a toast notification to confirm the submission
                //Toast.MakeText(this, "Item Added", ToastLength.Short).Show();

                //show this toast at the center of the screen.
                Toast toast = Toast.MakeText(this, "Item Added", ToastLength.Short);
                toast.SetGravity(GravityFlags.Center, 0, 0);
                toast.Show();

                //clear the boxes of the text
                nameBox.Text = "";
                pwd_entry.Text = "";

                textEntryPassword.ClearFocus();
                DismissKeyboard();
            };

            Button viewContactButton = FindViewById<Button>(Resource.Id.viewUserListButton);
            viewContactButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ViewUsersActivity));
                StartActivity(intent);
            };

            if (textEntryPassword.Text != null)
            {
                textEntryPassword.TextChanged += delegate
                {
                    TextEntryTextChanged();
                };

                textEntryPassword.TextChanged += PasswordEntryText_TextChange;
            }
        }

        //private void Button_Click(object sender, System.EventArgs e)
        //{
        //    //get the information from the boxes
        //    EditText nameBox = FindViewById<EditText>(Resource.Id.usernameEntry);
        //    string name = nameBox.Text.Trim();

        //    EditText pwd_entry = FindViewById<EditText>(Resource.Id.passwordEntry);
        //    string pwd = pwd_entry.Text.Trim();

        //    var user = new User { Username = name, Password = pwd };

        //    usersList.Add(user);

        //    Settings.Users = usersList;

        //    //create a toast notification to confirm the submission
        //    //Toast.MakeText(this, "Item Added", ToastLength.Short).Show();

        //    //show this toast at the center of the screen.
        //    Toast toast = Toast.MakeText(this, "Item Added", ToastLength.Short);
        //    toast.SetGravity(GravityFlags.Center, 0, 0);
        //    toast.Show();

        //    //clear the boxes of the text
        //    nameBox.Text = "";
        //    pwd_entry.Text = "";

        //    textEntryPassword.ClearFocus();
        //    DismissKeyboard();
        //}

        //to hide the keyboard after adding a user.
        private void DismissKeyboard()
        {
            var view = CurrentFocus;
            if (view != null)
            {
                var imm = (InputMethodManager)GetSystemService(InputMethodService);
                imm.HideSoftInputFromWindow(view.WindowToken, 0);
            }
        }

        private void PasswordEntryText_TextChange(object sender, TextChangedEventArgs e)
        {
            Button addButton = FindViewById<Button>(Resource.Id.addButton);
            var passwordEntry = FindViewById<EditText>(Resource.Id.passwordEntry);
            var repeatingRegex = new Regex(@"^(?!.*(.)\1)\w{6,10}$");

            if (!passwordRegex.IsMatch(textEntryPassword.Text))
            {
                addButton.Enabled = false;
                Toast.MakeText(this, "Password can only contain letters and numbers.", ToastLength.Short).Show();
            }
            else if (repeatingRegex.IsMatch(textEntryPassword.Text))
            {
                addButton.Enabled = false;
                Toast.MakeText(this, "Password cannot contain repeating characters.", ToastLength.Short).Show();
            }
            else if (passwordEntry.Length() < 5 || passwordEntry.Length() > 12)
            {
                addButton.Enabled = false;
                Toast.MakeText(this, "Password must be between 5 and 12 characters.", ToastLength.Short).Show();
            }
            else
            {
                addButton.Enabled = true;
            }
        }

        private void TextEntryTextChanged()
        {
            if (!string.IsNullOrEmpty(textEntryPassword?.Text) && textEntryPassword.Text.Length > 5)
            {
                btnAdd.Clickable = true;
            }
            else
            {
                btnAdd.Clickable = false;
            }
        }
    }
}


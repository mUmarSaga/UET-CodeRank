using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UET_CODERANK.BL;
using UET_CODERANK.DL;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UET_CODERANK.UI
{
    public sealed partial class UpdateProfilePage : Page
    {
        public UpdateProfilePage()
        {
            this.InitializeComponent();
            LoadProfile();
        }
        Model.Student student;
        private void LoadProfile()
        {
            student = CurrentSession.Student;
            NameBox.Text = student.Name ?? "";
            EmailBox.Text = student.Email ?? "";
            LeetcodeBox.Text = student.LeetcodeUsername ?? "";
            ProfileNameBox.Text = student.ProfileName ?? "";
        }

        private void SaveProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(NameBox.Text) ||
               string.IsNullOrWhiteSpace(EmailBox.Text))
            {
                ProfileStatusText.Text = "Name and Email cannot be empty.";
                ProfileStatusText.Foreground = new SolidColorBrush(Colors.Red);
                return;
            }
            if(EmailBox.Text == student.Email && LeetcodeBox.Text == student.LeetcodeUsername && NameBox.Text == student.Name && ProfileNameBox.Text == student.ProfileName)
            {
                ProfileStatusText.Text = "No changes detected.";
                ProfileStatusText.Foreground = new SolidColorBrush(Colors.Orange);
                return;
            }
            bool success = UpdateProfileBL.UpdateProfile(
                CurrentSession.Student.Id,
                NameBox.Text.Trim(),
                EmailBox.Text.Trim(),
                LeetcodeBox.Text.Trim(),
                ProfileNameBox.Text.Trim(),
                CurrentSession.Student.ProfilePicPath
            );

            if (success)
            {
                var updated = StudentDL.GetById(CurrentSession.Student.Id);
                CurrentSession.SetStudent(updated);
                ProfileStatusText.Text = "Profile updated successfully.";
                ProfileStatusText.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                ProfileStatusText.Text = "Failed to update. Check your inputs or email may already be taken.";
                ProfileStatusText.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
        private async void btnVerify_Click(object sender, RoutedEventArgs e)
        {
            string username = LeetcodeBox.Text.Trim();
            if (string.IsNullOrEmpty(username)) return;



            txtVerifyStatus.Text = "Checking...";

            var profile = await Task.Run(() => DL.LeetCodeAPI.GetProfile(username));

            if (profile == null)
            {
                txtVerifyStatus.Text = "❌ Username not found";
                txtVerifyStatus.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Red);
    
                return;
            }

            // show profile card
            profileCard.Visibility = Visibility.Visible;
            txtProfileName.Text = profile.Name;
           
            profilePic.ProfilePicture = new BitmapImage(new Uri(profile.Avatar));
            txtVerifyStatus.Text = "";
      
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            profileCard.Visibility = Visibility.Collapsed;
            txtVerifyStatus.Text = "✅ LeetCode verified!";
            txtVerifyStatus.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Green);


        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {

            profileCard.Visibility = Visibility.Collapsed;

            txtVerifyStatus.Text = "";
        }

        private void LeetcodeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(LeetcodeBox.Text == student.LeetcodeUsername) { btnVerify.IsEnabled = false; }
            else { btnVerify.IsEnabled = true; }
        }
    }
}

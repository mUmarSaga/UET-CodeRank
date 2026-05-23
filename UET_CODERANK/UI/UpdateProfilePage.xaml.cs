using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        private void LoadProfile()
        {
            var student = CurrentSession.Student;
            NameBox.Text = student.Name ?? "";
            EmailBox.Text = student.Email ?? "";
            LeetcodeBox.Text = student.LeetcodeUsername ?? "";
            ProfileNameBox.Text = student.ProfileName ?? "";
        }

        private void SaveProfileBtn_Click(object sender, RoutedEventArgs e)
        {
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

        private void SavePasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NewPasswordBox.Password != ConfirmPasswordBox.Password)
            {
                PasswordStatusText.Text = "New passwords do not match.";
                PasswordStatusText.Foreground = new SolidColorBrush(Colors.Red);
                return;
            }

            bool success = UpdateProfileBL.UpdatePassword(
                CurrentSession.Student.Id,
                CurrentPasswordBox.Password,
                NewPasswordBox.Password
            );

            if (success)
            {
                PasswordStatusText.Text = "Password updated successfully.";
                PasswordStatusText.Foreground = new SolidColorBrush(Colors.Green);
                CurrentPasswordBox.Password = "";
                NewPasswordBox.Password = "";
                ConfirmPasswordBox.Password = "";
            }
            else
            {
                PasswordStatusText.Text = "Incorrect current password or new password is too short (min 6 characters).";
                PasswordStatusText.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }
}

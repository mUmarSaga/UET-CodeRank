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
        
            
        }

        private async void SaveProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text) ||
                string.IsNullOrWhiteSpace(EmailBox.Text))
            {
                ProfileStatusText.Text = "Name and Email cannot be empty.";
                ProfileStatusText.Foreground = new SolidColorBrush(Colors.Red);
                return;
            }
            if (EmailBox.Text == student.Email && NameBox.Text == student.Name )
            {
                ProfileStatusText.Text = "No changes detected.";
                ProfileStatusText.Foreground = new SolidColorBrush(Colors.Orange);
                return;
            }
     
           
            ContentDialog dialog = new ContentDialog
            {
                Title = "Save Changes",
                Content = "Are you sure you want to update your profile?",
                PrimaryButtonText = "Save",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = this.XamlRoot
            };

            var result = await dialog.ShowAsync();
            if (result != ContentDialogResult.Primary) return;
           

            


            bool success = UpdateProfileBL.UpdateProfile(
                CurrentSession.Student.Id,
                NameBox.Text.Trim(),
                EmailBox.Text.Trim()
               
            );

            if (success)
            {
                var updated = StudentDL.GetById(CurrentSession.Student.Id);
                CurrentSession.SetStudent(updated);
                CurrentSession.NotifyProfileUpdated();
                LeetCodeStatBL.UpdateLeetCodeStat(CurrentSession.Student);
                ProfileStatusText.Text = "Profile updated successfully.";
                ProfileStatusText.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                ProfileStatusText.Text = "Failed to update. Check your inputs or email may already be taken.";
                ProfileStatusText.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
        
        
    }
}

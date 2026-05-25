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
using Windows.Foundation;
using Windows.Foundation.Collections;
using UET_CODERANK.BL;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UET_CODERANK.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LeetCodeSettingsPage : Page
    {
        private bool isVerified = false;
        private string verifiedUsername = "";
        private string verifiedAvatarUrl = "";
        private string verifiedName = "";

        public LeetCodeSettingsPage()
        {
            this.InitializeComponent();
            LoadCurrentAccount();
        }

        private void LoadCurrentAccount()
        {
            if (!string.IsNullOrEmpty(CurrentSession.Student.LeetcodeUsername))
            {
                hasAccountPanel.Visibility = Visibility.Visible;
                noAccountPanel.Visibility = Visibility.Collapsed;
                txtCurrentUsername.Text = $"@{CurrentSession.Student.LeetcodeUsername}";
                txtCurrentName.Text = CurrentSession.Student.Name;

                if (!string.IsNullOrEmpty(CurrentSession.Student.ProfilePicPath))
                    currentPic.ProfilePicture = new BitmapImage(new Uri(CurrentSession.Student.ProfilePicPath));
            }
            else
            {
                hasAccountPanel.Visibility = Visibility.Collapsed;
                noAccountPanel.Visibility = Visibility.Visible;
            }
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Reset verification when username changes
            isVerified = false;
            btnSave.IsEnabled = false;
            profileCard.Visibility = Visibility.Collapsed;
            txtVerifyStatus.Visibility = Visibility.Collapsed;
        }

        private async void btnVerify_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            if (string.IsNullOrEmpty(username)) return;

            // Check not same as current
            if (username == CurrentSession.Student.LeetcodeUsername)
            {
                txtVerifyStatus.Text = "⚠️ This is already your linked account";
                txtVerifyStatus.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 165, 0));
                txtVerifyStatus.Visibility = Visibility.Visible;
                return;
            }

            btnVerify.IsEnabled = false;
            txtVerifyStatus.Text = "Checking...";
            txtVerifyStatus.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136));
            txtVerifyStatus.Visibility = Visibility.Visible;
            profileCard.Visibility = Visibility.Collapsed;

            var profile = await Task.Run(() => DL.LeetCodeAPI.GetProfile(username));

            btnVerify.IsEnabled = true;

            if (profile == null)
            {
                txtVerifyStatus.Text = "❌ Username not found on LeetCode";
                txtVerifyStatus.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Red);
                return;
            }

            // Show profile preview
            txtVerifyStatus.Visibility = Visibility.Collapsed;
            txtPreviewName.Text = profile.Name;
            if (!string.IsNullOrEmpty(profile.Avatar))
                previewPic.ProfilePicture = new BitmapImage(new Uri(profile.Avatar));

            verifiedUsername = username;
            verifiedAvatarUrl = profile.Avatar ?? "";
            verifiedName = profile.Name ?? "";

            profileCard.Visibility = Visibility.Visible;
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            isVerified = true;
            profileCard.Visibility = Visibility.Collapsed;
            txtVerifyStatus.Text = "✅ Account verified!";
            txtVerifyStatus.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 16, 124, 16));
            txtVerifyStatus.Visibility = Visibility.Visible;
            btnSave.IsEnabled = true;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            isVerified = false;
            profileCard.Visibility = Visibility.Collapsed;
            txtUsername.Text = "";
            txtVerifyStatus.Visibility = Visibility.Collapsed;
            btnSave.IsEnabled = false;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!isVerified) return;

            btnSave.IsEnabled = false;
            btnSave.Content = "Saving...";

            string error = StudentBL.UpdateLeetCodeAccount(
                CurrentSession.Student.Id,
                verifiedUsername,
                verifiedAvatarUrl,verifiedName
            );

            if (error == null)
            {
                // Update session
                CurrentSession.Student.LeetcodeUsername = verifiedUsername;
                CurrentSession.Student.ProfilePicPath = verifiedAvatarUrl;

                ContentDialog success = new ContentDialog
                {
                    Title = "Account Updated!",
                    Content = $"Your LeetCode account has been linked to @{verifiedUsername}",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await success.ShowAsync();

                // Refresh current account display
                LoadCurrentAccount();
                txtUsername.Text = "";
                isVerified = false;
                CurrentSession.NotifyProfileUpdated();
                LeetCodeStatBL.UpsertLeetCodeStat(CurrentSession.Student);
            }
            else
            {
                ContentDialog errorDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = error,
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await errorDialog.ShowAsync();
            }

            btnSave.Content = "Save Changes";
            btnSave.IsEnabled = true;
        }
    }
}

using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
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
using UET_CODERANK.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Collections.Specialized.BitVector32;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UET_CODERANK.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainShellPage : Page
    {
        public MainShellPage()
        {
            this.InitializeComponent();
            CurrentSession.ProfileUpdated += RefreshProfile;
            CurrentSession.NavigationRequested += NavigateTo;
            LoadUserInfo();
            contentFrame.Navigate(typeof(HomePage));

        }
        private Button crntButton;


        private void disableButton()
        {
            if (crntButton != null) { 
                crntButton.Background = new SolidColorBrush(Colors.Transparent);
            }
        }
        private void activeButton(object sender)
        {
            if (sender != null)
            {
                disableButton();
                crntButton = (Button)sender;
                crntButton.Background = Application.Current.Resources["ButtonClickedBrush"] as SolidColorBrush;
            }
        }
        private void LoadUserInfo()
        {
            txtUsername.Text = CurrentSession.Student.Name;
            if(!string.IsNullOrEmpty(CurrentSession.Student.LeetcodeUsername))
            {
                profilePic.ProfilePicture = new BitmapImage(new Uri(CurrentSession.Student.ProfilePicPath));
            }
            txtLeetcode.Text = CurrentSession.Student.LeetcodeUsername;
            activeButton(btnHome);
            RefreshNotificationBadge();
        }
        private void RefreshProfile()
        {
            DispatcherQueue.TryEnqueue(() =>
            {
                txtUsername.Text = CurrentSession.Student.Name;
                txtLeetcode.Text = CurrentSession.Student.LeetcodeUsername;
                if (!string.IsNullOrEmpty(CurrentSession.Student.LeetcodeUsername))
                {
                    profilePic.ProfilePicture = new BitmapImage(new Uri(CurrentSession.Student.ProfilePicPath));
                }
            });
        }


        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            activeButton(sender);
            contentFrame.Navigate(typeof(HomePage));
        }
        
        private void btnLeaderboard_Click(object sender, RoutedEventArgs e)
        {
            activeButton(sender);
            contentFrame.Navigate(typeof(LeaderboardPage));
        }

        private void btnJoinClass_Click(object sender, RoutedEventArgs e)
        {
            activeButton((Button)sender);
            contentFrame.Navigate(typeof(JoinClassPage));
        }

        private void NavigateTo(Type pageType)
        {
            contentFrame.Navigate(pageType);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            // flyout handles it automatically, nothing needed here
        }

        private void menuEditProfile_Click(object sender, RoutedEventArgs e)
        {
            activeButton(btnSettings);
            contentFrame.Navigate(typeof(UpdateProfilePage));
        }

        private void menuChangePassword_Click(object sender, RoutedEventArgs e)
        {
            activeButton(btnSettings);
            contentFrame.Navigate(typeof(ChangePasswordPage));
        }

        private void menuSync_Click(object sender, RoutedEventArgs e)
        {
            // call your sync BL method
            // BL.SyncBL.SyncStudent(CurrentSession.Student.Id);
            if (!string.IsNullOrEmpty(CurrentSession.Student.LeetcodeUsername)) {
                LeetCodeStatBL.UpsertLeetCodeStat(CurrentSession.Student);
                contentFrame.Navigate(typeof(HomePage));
            }
            
      
        }
       

        private void menuLogout_Click(object sender, RoutedEventArgs e)
        {
            // clear session and navigate to login
            CurrentSession.SetStudent(null);
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values.Remove("RememberMeUserId");
            App.MainWindowFrame?.Navigate(typeof(BlankPage1));
        }

        private void Profile_Button_Click(object sender, RoutedEventArgs e)
        {
            activeButton((Button)sender);
            contentFrame.Navigate(typeof(ProfilePage));
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            activeButton(btnSettings);
            contentFrame.Navigate(typeof(LeetCodeSettingsPage));
        }

        private void btnNotifications_Click(object sender, RoutedEventArgs e)
        {
            activeButton(sender);
            contentFrame.Navigate(typeof(NotificationsPage));
            notifBadge.Visibility = Visibility.Collapsed;
        }

        public void RefreshNotificationBadge()
        {
            int count = DL.NotificationDL.GetUnreadCount(CurrentSession.Student.Id);
            if (count > 0)
            {
                txtNotifCount.Text = count > 99 ? "99+" : count.ToString();
                notifBadge.Visibility = Visibility.Visible;
            }
            else
            {
                notifBadge.Visibility = Visibility.Collapsed;
            }
        }
    }
}

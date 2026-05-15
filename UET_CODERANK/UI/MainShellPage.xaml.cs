using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UET_CODERANK.BL;
using Windows.Foundation;
using Windows.Foundation.Collections;

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
            LoadUserInfo();
        
        }

        private void LoadUserInfo()
        {
            txtUsername.Text = CurrentSession.Student.Name;
            profilePic.ProfilePicture = new BitmapImage(new Uri(CurrentSession.Student.ProfilePicPath));
            txtLeetcode.Text = CurrentSession.Student.LeetcodeUsername;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            //contentFrame.Navigate(typeof(DashboardPage));
        }

        private void btnLeaderboard_Click(object sender, RoutedEventArgs e)
        {
            //contentFrame.Navigate(typeof(LeaderboardPage));
        }

        private void btnJoinClass_Click(object sender, RoutedEventArgs e)
        {
            //contentFrame.Navigate(typeof(JoinClassPage));
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            //contentFrame.Navigate(typeof(SettingsPage));
        }
    }
}

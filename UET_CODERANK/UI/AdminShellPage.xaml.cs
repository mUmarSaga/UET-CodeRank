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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UET_CODERANK.UI
{
    public sealed partial class AdminShellPage : Page
    {
        private Button currentButton;

        public AdminShellPage()
        {
            this.InitializeComponent();
            txtAdminName.Text = CurrentSession.Admin?.Username ?? "Admin";
            SetActive(btnDashboard);
            adminContentFrame.Navigate(typeof(AdminDashboardPage));
        }

        private void SetActive(Button btn)
        {
            if (currentButton != null)
                currentButton.Background = new SolidColorBrush(Colors.Transparent);
            currentButton = btn;
            currentButton.Background = Application.Current.Resources["ButtonClickedBrush"] as SolidColorBrush;
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            SetActive(btnDashboard);
            adminContentFrame.Navigate(typeof(AdminDashboardPage));
        }

        private void btnEnrollments_Click(object sender, RoutedEventArgs e)
        {
            SetActive(btnEnrollments);
            adminContentFrame.Navigate(typeof(EnrollmentRequestsPage));
        }

        private void btnBatches_Click(object sender, RoutedEventArgs e)
        {
            SetActive(btnBatches);
            adminContentFrame.Navigate(typeof(ManageBatchesPage));
        }

        private void btnSync_Click(object sender, RoutedEventArgs e)
        {
            SetActive(btnSync);
            adminContentFrame.Navigate(typeof(SyncStatsPage));
        }

        private void btnBadges_Click(object sender, RoutedEventArgs e)
        {
            SetActive(btnBadges);
            adminContentFrame.Navigate(typeof(AwardBadgesPage));
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            SetActive(btnReports);
            adminContentFrame.Navigate(typeof(ReportsPage));
        }

        private void btnErrorLogs_Click(object sender, RoutedEventArgs e)
        {
            SetActive(btnErrorLogs);
            adminContentFrame.Navigate(typeof(ErrorLogsPage));
        }

        private void btnManageAdmins_Click(object sender, RoutedEventArgs e)
        {
            SetActive(btnManageAdmins);
            adminContentFrame.Navigate(typeof(ManageAdminsPage));
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            CurrentSession.Clear();
            App.MainWindowFrame?.Navigate(typeof(BlankPage1));
        }

        public void NavigateTo(string page)
        {
            switch (page)
            {
                case "Enrollments":
                    SetActive(btnEnrollments);
                    adminContentFrame.Navigate(typeof(EnrollmentRequestsPage));
                    break;
                case "Batches":
                    SetActive(btnBatches);
                    adminContentFrame.Navigate(typeof(ManageBatchesPage));
                    break;
                case "Sync":
                    SetActive(btnSync);
                    adminContentFrame.Navigate(typeof(SyncStatsPage));
                    break;
            }
        }
    }
}
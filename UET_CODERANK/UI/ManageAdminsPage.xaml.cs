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
    public sealed partial class ManageAdminsPage : Page
    {
        public ManageAdminsPage()
        {
            this.InitializeComponent();
            LoadAdmins();
        }

        private void LoadAdmins()
        {
            var admins = AdminDL.GetAllAdmins();
            var items = admins.Select(a => new AdminListItem
            {
                Username = a.Username,
                Created_at = a.Created_at.ToString("dd MMM yyyy"),
                IsCurrent = a.Username == CurrentSession.Admin?.Username
                    ? Visibility.Visible : Visibility.Collapsed
            }).ToList();
            AdminList.ItemsSource = items;
        }

        private void btnAddAdmin_Click(object sender, RoutedEventArgs e)
        {
            bool success = AdminBL.AddAdmin(txtNewUsername.Text.Trim(), txtNewPassword.Password);
            if (success)
            {
                txtAddStatus.Text = "Admin added successfully.";
                txtAddStatus.Foreground = new SolidColorBrush(Colors.Green);
                txtNewUsername.Text = "";
                txtNewPassword.Password = "";
                LoadAdmins();
            }
            else
            {
                txtAddStatus.Text = "Failed. Username may already exist or inputs are invalid.";
                txtAddStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void btnDeleteAdmin_Click(object sender, RoutedEventArgs e)
        {
            string username = (sender as Button)?.Tag?.ToString();
            bool success = AdminBL.DeleteAdmin(username);
            if (success)
                LoadAdmins();
            else
            {
                txtAddStatus.Text = "Cannot delete this admin.";
                txtAddStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private class AdminListItem
        {
            public string Username { get; set; }
            public string Created_at { get; set; }
            public Visibility IsCurrent { get; set; }
        }
    }
}
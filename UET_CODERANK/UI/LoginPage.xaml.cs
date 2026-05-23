using Microsoft.UI.Windowing;
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
using System.Threading.Tasks;
using UET_CODERANK.BL;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UET_CODERANK.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            disableLoginButton();
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(email))
            {
                ShowError(EmailErrorMessage, "Email Cannot be empty");
                enableLoginButton();
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                ShowError(PasswordErrorMessage, "Enter Password");
                enableLoginButton();
                return;
            }

            if (rbAdmin.IsChecked == true)
            {
                int adminId = await Task.Run(() => AdminBL.LoginAdmin(email, password));
                if (adminId > 0)
                {
                    var admin = DL.AdminDL.GetByUsername(email);
                    CurrentSession.SetAdmin(admin);
                    App.MainWindowFrame?.Navigate(typeof(AdminShellPage));
                }
                else
                {
                    ShowError(PasswordErrorMessage, "Invalid admin credentials");
                    enableLoginButton();
                }
            }
            else
            {
                int Id = await Task.Run(() => StudentBL.LoginStudent(email, password));
                if (Id > 0)
                {
                    var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    if (RememberMe.IsChecked == true)
                        localSettings.Values["RememberMeUserId"] = Id;
                    else
                        localSettings.Values.Remove("RememberMeUserId");

                    Model.Student student = DL.StudentDL.GetByID(Id);
                    CurrentSession.SetStudent(student);
                    CurrentSession.SetLeetCodeStat(DL.LeetCodeStatDL.GetLeetCodeStatByStudentId(Id));
                    LeetCodeStatBL.UpdateLeetCodeStat(student);
                    App.MainWindowFrame?.Navigate(typeof(MainShellPage));
                }
                else
                {
                    ShowError(PasswordErrorMessage, "Invalid email or password");
                    enableLoginButton();
                }
            }
        }
        private void disableLoginButton()
        {
            btnLogin.IsEnabled = false;
            btnLogin.Content = "Loging In....";
        }

        private void enableLoginButton()
        {
            btnLogin.IsEnabled = true;
            btnLogin.Content = "Login";
        }
        public void ShowError(TextBlock errorLabel, string message)
        {
            errorLabel.Text = message;
            errorLabel.Visibility = Visibility.Visible;
        }
        public void HideError(TextBlock errorLabel)
        {
            errorLabel.Visibility = Visibility.Collapsed;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // navigate to register page
            if (rbStudent.IsChecked == true)
            {
                App.MainWindowFrame?.Navigate(typeof(StudenrRegisterPage));
       
                
            }
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            HideError(EmailErrorMessage);
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            HideError(PasswordErrorMessage);
        }
    }
}

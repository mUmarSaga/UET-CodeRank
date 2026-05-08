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
using Windows.Foundation;
using Windows.Foundation.Collections;

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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            if (string.IsNullOrEmpty(email)) { 
                
                txtEmail.Header = "Email Cannot be empty";
                return;
            }
            if (string.IsNullOrEmpty(password)) { 
                txtPassword.Header = "Password Cannot be empty";
                return;
            }
            if(BL.StudentBL.IsValidEmailFormat(email))
            {
                
            }
            else
            {
                
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // navigate to register page
            Frame.Navigate(typeof(StudenrRegisterPage));
        }
       
    }
}

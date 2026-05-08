using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UET_CODERANK.UI;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class StudenrRegisterPage : Page
{
    public StudenrRegisterPage()
    {
        InitializeComponent();
    }
    private void btnVerify_Click(object sender, RoutedEventArgs e)
    {
        // call LeetCode API and update txtVerifyStatus
    }

    private void btnRegister_Click(object sender, RoutedEventArgs e)
    {
        string username = txtName.Text;
        string Reg_No = txtRegNo.Text;
        string Email = txtEmail.Text;
        string Password;
        string Leetcode_Username = txtLeetcode.Text;
        if (string.IsNullOrEmpty(username))
        {
            txtName.Header = "Name cannot be empty";
            return;
        }
        if (string.IsNullOrEmpty(Reg_No))
        {
            txtRegNo.Header = "Registration Number cannot be empty";
            return;
        }
        if (string.IsNullOrEmpty(Email))
        {
            txtEmail.Header = "Email cannot be empty";
            return;
        }
        if (string.IsNullOrEmpty(txtPassword.Password))
        {
            txtPassword.Header = "Password cannot be empty";
            return;
        }
        if (string.IsNullOrEmpty(txtConfirmPassword.Password))
        {
            txtConfirmPassword.Header = "Please Confirm Password";
            return;
        }
        if(txtConfirmPassword.Password != txtPassword.Password)
        {
            txtConfirmPassword.Header = "Passwords do not match";
            return;
        }
        if(BL.StudentBL.IsValidEmailFormat(Email) == false) 
        {
            txtEmail.Header = "Invalid Email Format";
            return;
        }
        if(BL.StudentBL.IsRegNoFormatValid(Reg_No))
        {
            if(BL.StudentBL.IsRegNoExist(Reg_No))
            {
                txtRegNo.Header = "Registration Number already exists";
                return;
            }
        }
        else
        {
            txtRegNo.Header = "Invalid Registration Number Format";
            return;
        }
        if(BL.StudentBL.IsEmailAlreadyExist(Email))
        {
            txtEmail.Header = "Email already exists";
            return;
        }
        if(BL.StudentBL.RegisterStudent(Reg_No, username, Email, txtPassword.Password, Leetcode_Username))
        {
            Frame.Navigate(typeof(BlankPage1));
        }
         else
        {
            
        }

    }

    private void btnLogin_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(BlankPage1));
    }


}

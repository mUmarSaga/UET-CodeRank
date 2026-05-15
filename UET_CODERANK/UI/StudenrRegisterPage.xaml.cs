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
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    

    private async void btnRegister_Click(object sender, RoutedEventArgs e)
    {
        string name = txtName.Text;
        string Reg_No = txtRegNo.Text;
        string Email = txtEmail.Text;
        string Password;
        string Leetcode_Username = null;
        if (isLeetcodeVerified)
        {
            Leetcode_Username = username;
        }
        
        if (string.IsNullOrEmpty(name))
        {
            ShowError(NameErrorMessage, "Name is required");
            return;
        }
        if (string.IsNullOrEmpty(Reg_No))
        {
            ShowError(RegNoErrorMessage, "Registration Number is required");
            return;
        }
        if (string.IsNullOrEmpty(Email))
        {
            ShowError(EmailErrorMessage, "Email is required");
            return;
        }
        if (string.IsNullOrEmpty(txtPassword.Password))
        {
            ShowError(PasswordErrorMessage, "Please set a password");
            return;
        }
        
        if(txtConfirmPassword.Password != txtPassword.Password)
        {
            ShowError(ConfirmPasswordErrorMessage, "Password Mismatch");
            return;
        }
        if(BL.StudentBL.IsValidEmailFormat(Email) == false) 
        {
            ShowError(EmailErrorMessage, "Invalid Email Format");
            return;
        }
        if(BL.StudentBL.IsRegNoFormatValid(Reg_No))
        {
            if(BL.StudentBL.IsRegNoExist(Reg_No))
            {
                ShowError(RegNoErrorMessage, "Registration Number already exists");
                return;
            }
        }
        else
        {
            ShowError(RegNoErrorMessage, "Invalid Registration Number Format");
            return;
        }
        if(BL.StudentBL.IsEmailAlreadyExist(Email))
        {
            ShowError(EmailErrorMessage, "Email already exists");
            return;
        }
        string passwordText = txtPassword.Password;
        bool done = await Task.Run(() => BL.StudentBL.RegisterStudent(Reg_No, name, Email, passwordText, Leetcode_Username, profileAvatarURL, profileName));
        if (done)
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
    public void ShowError(TextBlock errorLabel, string message)
    {
        errorLabel.Text = message;
        errorLabel.Visibility = Visibility.Visible;
    }
    public void HideError(TextBlock errorLabel)
    {
        errorLabel.Visibility = Visibility.Collapsed;
    }

    private void txtRegNo_TextChanged(object sender, TextChangedEventArgs e)
    {
        HideError(RegNoErrorMessage);
    }

    private void txtName_TextChanged(object sender, TextChangedEventArgs e)
    {
        HideError(NameErrorMessage);
    }

    private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
    {
        HideError(EmailErrorMessage);
    }

    private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
    {
        HideError(PasswordErrorMessage);
    }

    private void txtConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
    {
        HideError(ConfirmPasswordErrorMessage);
    }
    private bool isLeetcodeVerified = false;
    private string username;
    private string profileName;
    private string profileAvatarURL;

    private async void btnVerify_Click(object sender, RoutedEventArgs e)
    {
        if(isLeetcodeVerified && txtLeetcode.Text.Trim() == username)
        {
            return;
        }
        username = txtLeetcode.Text.Trim();
        if (string.IsNullOrEmpty(username)) return;
        

        btnVerify.IsEnabled = false;
        txtVerifyStatus.Text = "Checking...";
        
        var profile = await Task.Run(() => DL.LeetCodeAPI.GetProfile(username));

        if (profile == null)
        {
            txtVerifyStatus.Text = "❌ Username not found";
            txtVerifyStatus.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Red);
            btnVerify.IsEnabled = true;
            return;
        }

        // show profile card
        profileCard.Visibility = Visibility.Visible;
        txtProfileName.Text = profile.Name;
        profileName = profile.Name;
        profileAvatarURL = profile.Avatar;
        profilePic.ProfilePicture = new BitmapImage(new Uri(profile.Avatar));
        txtVerifyStatus.Text = "";
        btnVerify.IsEnabled = true;
    }

    private void btnYes_Click(object sender, RoutedEventArgs e)
    {
        isLeetcodeVerified = true;
        profileCard.Visibility = Visibility.Collapsed;
        txtVerifyStatus.Text = "✅ LeetCode verified!";
        txtVerifyStatus.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Green);

    }

    private void btnNo_Click(object sender, RoutedEventArgs e)
    {
        isLeetcodeVerified = false;
        profileCard.Visibility = Visibility.Collapsed;
        txtLeetcode.Text = "";
        txtVerifyStatus.Text = "";
    }
}

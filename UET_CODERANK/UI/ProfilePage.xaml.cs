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
using UET_CODERANK.BL;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UET_CODERANK.UI
{
    public sealed partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            this.InitializeComponent();
            LoadProfile();
        }

        private void LoadProfile()
        {
            var student = CurrentSession.Student;

            txtName.Text = student.Name ?? "";
            txtRegNo.Text = student.RegNo ?? "";
            txtEmail.Text = student.Email ?? "";
            txtLeetcode.Text = student.LeetcodeUsername ?? "Not set";
            txtSection.Text = student.SectionId != 0 ? student.SectionId.ToString() : "Not enrolled";

            if (!string.IsNullOrEmpty(student.ProfilePicPath))
            {
                try { ProfilePic.ProfilePicture = new BitmapImage(new Uri(student.ProfilePicPath)); }
                catch { }
            }

            var stat = CurrentSession.leetCodeStat;
            if (stat != null)
            {
                txtTotal.Text = stat.Total_solved.ToString();
                txtEasy.Text = stat.Easy_solved.ToString();
                txtMedium.Text = stat.Medium_solved.ToString();
                txtHard.Text = stat.Hard_solved.ToString();
                txtRating.Text = stat.Global_rank.ToString();
                txtScore.Text = (stat.Hard_solved * 5 + stat.Medium_solved * 3 + stat.Easy_solved * 1).ToString();
                txtLastSync.Text = stat.Last_updated.ToString("dd MMM yyyy, hh:mm tt");
            }
        }

      
    }
}
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using UET_CODERANK.BL;
using UET_CODERANK.DL;

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
            txtSection.Text = CurrentSession.SectionName;
            txtBatch.Text = CurrentSession.SessionName;

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

            LoadBadges();
        }

        private void LoadBadges()
        {
            badgesPanel.Children.Clear();
            var badges = BadgeDL.GetBadgesByStudentId(CurrentSession.Student.Id);

            if (badges.Count == 0)
            {
                badgesPanel.Children.Add(new TextBlock
                {
                    Text = "No badges earned yet. Keep solving!",
                    Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                    FontSize = 13
                });
                return;
            }

            var wrap = new ItemsWrapGrid { Orientation = Orientation.Horizontal };
            foreach (var badge in badges)
            {
                var card = new Border
                {
                    Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 30, 33, 36)),
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(16, 12, 16, 12),
                    Margin = new Thickness(0, 0, 12, 12),
                    MinWidth = 160
                };
                var stack = new StackPanel { Spacing = 4 };
                stack.Children.Add(new TextBlock
                {
                    Text = $"⭐ {badge.name}",
                    Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 192, 30)),
                    FontSize = 14,
                    FontWeight = Microsoft.UI.Text.FontWeights.SemiBold
                });
                stack.Children.Add(new TextBlock
                {
                    Text = badge.description,
                    Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                    FontSize = 11,
                    TextWrapping = TextWrapping.Wrap
                });
                stack.Children.Add(new TextBlock
                {
                    Text = badge.criteria,
                    Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 100, 100, 100)),
                    FontSize = 10,
                    TextWrapping = TextWrapping.Wrap
                });
                card.Child = stack;
                badgesPanel.Children.Add(card);
            }
        }

        private void btnUpdateProfile_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UpdateProfilePage));
        }
    }
}
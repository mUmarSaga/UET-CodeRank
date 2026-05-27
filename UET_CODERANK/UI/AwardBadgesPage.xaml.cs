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
using UET_CODERANK.DL;
using UET_CODERANK.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UET_CODERANK.UI
{
    public sealed partial class AwardBadgesPage : Page
    {
        public AwardBadgesPage()
        {
            this.InitializeComponent();
            LoadBadges();
            LoadStudents();
        }

        private void LoadBadges()
        {
            badgeRows.Children.Clear();
            cmbBadges.Items.Clear();
            var badges = BadgeDL.GetAllBadges();
            if (badges.Count == 0)
            {
                badgeRows.Children.Add(new TextBlock
                {
                    Text = "No badges created yet.",
                    Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                    Padding = new Thickness(16, 12, 0, 12)
                });
                return;
            }
            foreach (var badge in badges)
            {
                cmbBadges.Items.Add(new ComboBoxItem { Content = badge.name, Tag = badge });
                badgeRows.Children.Add(CreateBadgeRow(badge));
            }
        }

        private void LoadStudents()
        {
            cmbStudents.Items.Clear();
            var students = StudentDL.GetAllStudents();
            foreach (var student in students)
                cmbStudents.Items.Add(new ComboBoxItem { Content = $"{student.Name} ({student.RegNo})", Tag = student });
        }

        private Border CreateBadgeRow(Badges badge)
        {
            var row = new Border
            {
                Padding = new Thickness(16, 12, 16, 12),
                BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 42, 42, 42)),
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });

            var nameStack = new StackPanel { Spacing = 2 };
            nameStack.Children.Add(new TextBlock
            {
                Text = $"⭐ {badge.name}",
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 13,
                FontWeight = Microsoft.UI.Text.FontWeights.SemiBold
            });
            nameStack.Children.Add(new TextBlock
            {
                Text = badge.criteria,
                Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                FontSize = 11
            });
            Grid.SetColumn(nameStack, 0);
            grid.Children.Add(nameStack);

            var deleteBtn = new Button
            {
                Content = "Delete",
                Padding = new Thickness(10, 4, 10, 4),
                CornerRadius = new CornerRadius(6),
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 192, 57, 43)),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Center,
                Tag = badge.id
            };
            deleteBtn.Click += DeleteBadge_Click;
            Grid.SetColumn(deleteBtn, 1);
            grid.Children.Add(deleteBtn);

            row.Child = grid;
            return row;
        }

        private void btnCreateBadge_Click(object sender, RoutedEventArgs e)
        {
            string name = txtBadgeName.Text.Trim();
            string desc = txtBadgeDesc.Text.Trim();
            string criteria = txtBadgeCriteria.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(desc))
            {
                SetStatus(txtBadgeStatus, "Name and description are required.", false);
                return;
            }
            try
            {
                BadgeDL.AddBadge(new Badges(0, name, desc, criteria, ""));
                txtBadgeName.Text = "";
                txtBadgeDesc.Text = "";
                txtBadgeCriteria.Text = "";
                SetStatus(txtBadgeStatus, "Badge created successfully.", true);
                LoadBadges();
            }
            catch
            {
                SetStatus(txtBadgeStatus, "Failed to create badge.", false);
            }
        }

        private void DeleteBadge_Click(object sender, RoutedEventArgs e)
        {
            int badgeId = (int)((Button)sender).Tag;
            try
            {
                BadgeDL.DeleteBadge(badgeId);
                SetStatus(txtBadgeStatus, "Badge deleted.", true);
                LoadBadges();
            }
            catch
            {
                SetStatus(txtBadgeStatus, "Failed to delete badge.", false);
            }
        }

        private void cmbStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            studentBadgeRows.Children.Clear();
            if (cmbStudents.SelectedItem is ComboBoxItem item && item.Tag is Student student)
            {
                var badges = BadgeDL.GetBadgesByStudentId(student.Id);
                if (badges.Count == 0)
                {
                    studentBadgeRows.Children.Add(new TextBlock
                    {
                        Text = "No badges awarded yet.",
                        Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                        FontSize = 13
                    });
                    return;
                }
                foreach (var badge in badges)
                {
                    studentBadgeRows.Children.Add(new TextBlock
                    {
                        Text = $"⭐ {badge.name} — {badge.criteria}",
                        Foreground = new SolidColorBrush(Colors.White),
                        FontSize = 13,
                        Margin = new Thickness(0, 4, 0, 4)
                    });
                }
            }
        }

        private void btnAwardBadge_Click(object sender, RoutedEventArgs e)
        {
            if (cmbStudents.SelectedItem is not ComboBoxItem studentItem || studentItem.Tag is not Student student)
            {
                SetStatus(txtAwardStatus, "Please select a student.", false);
                return;
            }
            if (cmbBadges.SelectedItem is not ComboBoxItem badgeItem || badgeItem.Tag is not Badges badge)
            {
                SetStatus(txtAwardStatus, "Please select a badge.", false);
                return;
            }
            try
            {
                BadgeDL.AwardBadge(student.Id, badge.id);
                NotificationDL.SendNotification(student.Id, $"🏅 You've been awarded the '{badge.name}' badge! {badge.description}");
                SetStatus(txtAwardStatus, $"Badge '{badge.name}' awarded to {student.Name}.", true);
                cmbStudents_SelectionChanged(null, null);
            }
            catch
            {
                SetStatus(txtAwardStatus, "Failed to award badge.", false);
            }
        }

        private void SetStatus(TextBlock tb, string message, bool success)
        {
            tb.Text = message;
            tb.Foreground = new SolidColorBrush(success ? Colors.Green : Colors.Red);
        }
    }
}
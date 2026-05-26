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
using System.Threading.Tasks;
using UET_CODERANK.DL;
using UET_CODERANK.BL;
using UET_CODERANK.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UET_CODERANK.UI
{
    public sealed partial class SyncStatsPage : Page
    {
        private List<Student> students = new List<Student>();

        public SyncStatsPage()
        {
            this.InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            studentRows.Children.Clear();
            students = StudentDL.GetAllStudents().Where(s => s.IsApproved).ToList();
            if (students.Count == 0)
            {
                studentRows.Children.Add(new TextBlock
                {
                    Text = "No approved students found.",
                    Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                    Padding = new Thickness(20, 16, 0, 16)
                });
                return;
            }
            foreach (var student in students)
                studentRows.Children.Add(CreateRow(student));
        }

        private Border CreateRow(Student student)
        {
            var stat = LeetCodeStatDL.GetLeetCodeStatByStudentId(student.Id);

            var row = new Border
            {
                Padding = new Thickness(20, 12, 20, 12),
                BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 42, 42, 42)),
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(160) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });

            var nameStack = new StackPanel();
            nameStack.Children.Add(new TextBlock
            {
                Text = student.Name,
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 13,
                FontWeight = Microsoft.UI.Text.FontWeights.SemiBold
            });
            nameStack.Children.Add(new TextBlock
            {
                Text = student.RegNo,
                Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                FontSize = 11
            });
            Grid.SetColumn(nameStack, 0);
            grid.Children.Add(nameStack);

            AddText(grid, 1, student.LeetcodeUsername ?? "—", "#888888");
            AddText(grid, 2, stat?.Total_solved.ToString() ?? "—", "#FFFFFF");

            int score = stat != null ? (stat.Easy_solved * 1) + (stat.Medium_solved * 3) + (stat.Hard_solved * 5) : 0;
            AddText(grid, 3, stat != null ? score.ToString() : "—", "#FFFFFF");
            AddText(grid, 4, stat?.Last_updated.ToString("dd MMM yyyy, hh:mm tt") ?? "Never", "#888888");

            var syncBtn = new Button
            {
                Content = "Sync",
                Padding = new Thickness(12, 6, 12, 6),
                CornerRadius = new CornerRadius(6),
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Center,
                Tag = student
            };
            syncBtn.Click += SyncStudent_Click;
            Grid.SetColumn(syncBtn, 5);
            grid.Children.Add(syncBtn);

            row.Child = grid;
            return row;
        }

        private async void SyncStudent_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var student = (Student)btn.Tag;
            btn.IsEnabled = false;
            btn.Content = "Syncing...";

            await Task.Run(() => LeetCodeStatBL.UpsertLeetCodeStat(student));

            btn.Content = "Done";
            LoadStudents();
        }

        private async void btnSyncAll_Click(object sender, RoutedEventArgs e)
        {
            btnSyncAll.IsEnabled = false;
            syncProgress.Visibility = Visibility.Visible;
            syncProgress.IsIndeterminate = true;
            txtSyncStatus.Text = "Syncing...";

            int count = 0;
            foreach (var student in students)
            {
                await Task.Run(() => LeetCodeStatBL.UpsertLeetCodeStat(student));
                count++;
                txtSyncStatus.Text = $"Synced {count} of {students.Count}...";
            }

            syncProgress.IsIndeterminate = false;
            syncProgress.Visibility = Visibility.Collapsed;
            txtSyncStatus.Text = $"All {students.Count} students synced successfully.";
            btnSyncAll.IsEnabled = true;
            LoadStudents();
        }

        private void AddText(Grid grid, int col, string text, string hex)
        {
            var tb = new TextBlock
            {
                Text = text,
                Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255,
                    Convert.ToByte(hex.Substring(1, 2), 16),
                    Convert.ToByte(hex.Substring(3, 2), 16),
                    Convert.ToByte(hex.Substring(5, 2), 16))),
                FontSize = 13,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(tb, col);
            grid.Children.Add(tb);
        }
    }
}
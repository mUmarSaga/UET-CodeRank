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
    public sealed partial class AdminDashboardPage : Page
    {
        public AdminDashboardPage()
        {
            this.InitializeComponent();
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            txtWelcome.Text = $"Welcome, {CurrentSession.Admin?.Username ?? "Admin"} 👋";

            var students = StudentDL.GetAllStudents();
            txtTotalStudents.Text = students.Count.ToString();

            var requests = EnrollmentDL.GetAllRequests();
            txtPendingRequests.Text = requests.Count(r => r.Status == Model.EnrollementStatus.PENDING).ToString();

            var batches = BatchDL.GetAllBatches();
            txtTotalBatches.Text = batches.Count.ToString();

            var sections = SectionDL.GetAllSections();
            txtTotalSections.Text = sections.Count.ToString();

            var recent = requests.OrderByDescending(r => r.RequestedAt).Take(5).ToList();
            foreach (var req in recent)
            {
                recentEnrollmentRows.Children.Add(CreateEnrollmentRow(req));
            }
        }

        private Border CreateEnrollmentRow(Model.EnrollementRequest req)
        {
            var row = new Border
            {
                Padding = new Thickness(20, 12, 20, 12),
                BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 42, 42, 42)),
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });

            var student = StudentDL.GetById(req.StudentId);
            AddText(grid, 0, student?.Name ?? "Unknown", "#FFFFFF");
            AddText(grid, 1, req.SectionId.ToString(), "#888888");
            AddText(grid, 2, req.RequestedAt.ToString("dd MMM yyyy"), "#888888");

            string statusColor = req.Status == Model.EnrollementStatus.PENDING ? "#FFC01E" :
                     req.Status == Model.EnrollementStatus.APPROVED ? "#00b8a3" : "#ef4743";
            AddText(grid, 3, req.Status.ToString(), statusColor);

            row.Child = grid;
            return row;
        }

        private void AddText(Grid grid, int col, string text, string hex)
        {
            Windows.UI.Color color;
            if (hex == "White")
                color = Microsoft.UI.Colors.White;
            else
                color = Windows.UI.Color.FromArgb(255,
                    Convert.ToByte(hex.Substring(1, 2), 16),
                    Convert.ToByte(hex.Substring(3, 2), 16),
                    Convert.ToByte(hex.Substring(5, 2), 16));

            var tb = new TextBlock
            {
                Text = text,
                Foreground = new SolidColorBrush(color),
                FontSize = 13,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(tb, col);
            grid.Children.Add(tb);
        }

        private void btnGoEnrollments_Click(object sender, RoutedEventArgs e)
        {
            var shell = this.Parent as Frame;
            (shell?.Parent as AdminShellPage)?.NavigateTo("Enrollments");
        }

        private void btnGoBatches_Click(object sender, RoutedEventArgs e)
        {
            var shell = this.Parent as Frame;
            (shell?.Parent as AdminShellPage)?.NavigateTo("Batches");
        }

        private void btnGoSync_Click(object sender, RoutedEventArgs e)
        {
            var shell = this.Parent as Frame;
            (shell?.Parent as AdminShellPage)?.NavigateTo("Sync");
        }
    }
}
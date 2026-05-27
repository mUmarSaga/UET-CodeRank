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
    public sealed partial class EnrollmentRequestsPage : Page
    {
        private List<EnrollementRequest> allRequests = new List<EnrollementRequest>();
        private string currentFilter = "All";

        public EnrollmentRequestsPage()
        {
            this.InitializeComponent();
            LoadRequests();
        }

        private void LoadRequests()
        {
            allRequests = EnrollmentDL.GetAllRequests();
            txtSubtitle.Text = $"{allRequests.Count} total requests — {allRequests.Count(r => r.Status == EnrollementStatus.PENDING)} pending";
            RenderRows(allRequests);
        }

        private void RenderRows(List<EnrollementRequest> requests)
        {
            requestRows.Children.Clear();
            if (requests.Count == 0)
            {
                requestRows.Children.Add(new TextBlock
                {
                    Text = "No requests found.",
                    Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                    Padding = new Thickness(20, 16, 0, 16)
                });
                return;
            }
            foreach (var req in requests)
                requestRows.Children.Add(CreateRow(req));
        }

        private Border CreateRow(EnrollementRequest req)
        {
            var row = new Border
            {
                Padding = new Thickness(20, 12, 20, 12),
                BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 42, 42, 42)),
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // 0 name
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });                  // 1 batch
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });                  // 2 section
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(140) });                  // 3 leetcode
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });                  // 4 date
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });                  // 5 status
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(180) });                  // 6 actions

            var student = StudentDL.GetById(req.StudentId);
            var section = SectionDL.GetSectionById(req.SectionId);
            var batch = section != null ? BatchDL.GetBatchById(section.Batch_id) : null;

            // Col 0 — Name + RegNo
            var nameStack = new StackPanel();
            nameStack.Children.Add(new TextBlock
            {
                Text = student?.Name ?? "Unknown",
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 13,
                FontWeight = Microsoft.UI.Text.FontWeights.SemiBold
            });
            nameStack.Children.Add(new TextBlock
            {
                Text = student?.RegNo ?? "",
                Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                FontSize = 11
            });
            Grid.SetColumn(nameStack, 0);
            grid.Children.Add(nameStack);

            // Col 1 — Batch
            AddText(grid, 1, batch?.Name ?? "—", "#888888");

            // Col 2 — Section
            AddText(grid, 2, section?.Name ?? "—", "#888888");

            // Col 3 — LeetCode username + verified badge
            // Col 3 — LeetCode username
            AddText(grid, 3, student?.LeetcodeUsername ?? "—", "#00b8a3");

            // Col 4 — Date
            AddText(grid, 4, req.RequestedAt.ToString("dd MMM yyyy"), "#888888");

            // Col 5 — Status
            string statusColor = req.Status == EnrollementStatus.PENDING ? "#FFC01E" :
                                 req.Status == EnrollementStatus.APPROVED ? "#00b8a3" : "#ef4743";
            AddText(grid, 5, req.Status.ToString(), statusColor);

            // Col 6 — Actions
            if (req.Status == EnrollementStatus.PENDING)
            {
                var actionStack = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8 };

                var approveBtn = new Button
                {
                    Content = "Approve",
                    Padding = new Thickness(12, 6, 12, 6),
                    CornerRadius = new CornerRadius(6),
                    Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 184, 163)),
                    Foreground = new SolidColorBrush(Colors.White),
                    Tag = req
                };
                approveBtn.Click += ApproveBtn_Click;

                var rejectBtn = new Button
                {
                    Content = "Reject",
                    Padding = new Thickness(12, 6, 12, 6),
                    CornerRadius = new CornerRadius(6),
                    Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 192, 57, 43)),
                    Foreground = new SolidColorBrush(Colors.White),
                    Tag = req
                };
                rejectBtn.Click += RejectBtn_Click;

                actionStack.Children.Add(approveBtn);
                actionStack.Children.Add(rejectBtn);
                Grid.SetColumn(actionStack, 6);
                grid.Children.Add(actionStack);
            }

            row.Child = grid;
            return row;
        }

        private void ApproveBtn_Click(object sender, RoutedEventArgs e)
        {
            var req = (EnrollementRequest)((Button)sender).Tag;
            bool success = EnrollmentDL.ApproveRequest(req.Id, req.StudentId, req.SectionId);
            if (success)
            {
                var section = SectionDL.GetSectionById(req.SectionId);
                string sectionName = section?.Name ?? "your section";
                NotificationDL.SendNotification(req.StudentId, $"Your enrollment request for Section {sectionName} has been approved. Welcome!");
                LoadRequests();
            }
        }

        private void RejectBtn_Click(object sender, RoutedEventArgs e)
        {
            var req = (EnrollementRequest)((Button)sender).Tag;
            bool success = EnrollmentDL.RejectRequest(req.Id);
            if (success)
            {
                var section = SectionDL.GetSectionById(req.SectionId);
                string sectionName = section?.Name ?? "your section";
                NotificationDL.SendNotification(req.StudentId, $"Your enrollment request for Section {sectionName} has been rejected.");
                LoadRequests();
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            currentFilter = ((Button)sender).Content.ToString();
            var filtered = currentFilter switch
            {
                "Pending" => allRequests.Where(r => r.Status == EnrollementStatus.PENDING).ToList(),
                "Approved" => allRequests.Where(r => r.Status == EnrollementStatus.APPROVED).ToList(),
                "Rejected" => allRequests.Where(r => r.Status == EnrollementStatus.REJECTED).ToList(),
                _ => allRequests
            };
            RenderRows(filtered);
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
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(tb, col);
            grid.Children.Add(tb);
        }
    }
}
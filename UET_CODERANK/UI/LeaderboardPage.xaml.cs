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
using UET_CODERANK.BL;
using UET_CODERANK.DL;
using UET_CODERANK.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UET_CODERANK.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LeaderboardPage : Page
    {
        private List<LeaderboardEntry> allEntries = new List<LeaderboardEntry>();

        public LeaderboardPage()
        {
            this.InitializeComponent();
            LoadFilters();
            LoadLeaderboard();
        }

        private void LoadFilters()
        {
            var batches = BatchDL.GetAllBatches();
            foreach (var batch in batches)
            {
                cmbBatch.Items.Add(new ComboBoxItem { Content = batch.Name, Tag = batch.Id });
            }
        }

        private void LoadLeaderboard()
        {
            allEntries = LeaderboardDL.GetLeaderboard();
            txtStudentCount.Text = $"{allEntries.Count} students ranked";
            RenderLeaderboard(allEntries);
        }

        private void RenderLeaderboard(List<LeaderboardEntry> entries)
        {   if(leaderboardRows != null)
            leaderboardRows.Children.Clear();

            if (entries.Count == 0) return;

            // Top 3 podium
            if (entries.Count >= 1) SetPodium(txt1stName, txt1stReg, txt1stSolved, entries[0]);
            if (entries.Count >= 2) SetPodium(txt2ndName, txt2ndReg, txt2ndSolved, entries[1]);
            if (entries.Count >= 3) SetPodium(txt3rdName, txt3rdReg, txt3rdSolved, entries[2]);

            // Table rows
            for (int i = 0; i < entries.Count; i++)
            {
                leaderboardRows.Children.Add(CreateRow(i + 1, entries[i]));
            }
        }

        private void SetPodium(TextBlock name, TextBlock reg, TextBlock solved, LeaderboardEntry entry)
        {
            name.Text = entry.Name;
            reg.Text = entry.RegNo;
            solved.Text = entry.TotalSolved.ToString();
        }

        private Border CreateRow(int rank, LeaderboardEntry entry)
        {
            bool isCurrentUser = entry.StudentId == CurrentSession.Student.Id;

            Border row = new Border
            {
                Padding = new Thickness(20, 12, 20, 12),
                Background = isCurrentUser
                    ? new SolidColorBrush(Windows.UI.Color.FromArgb(30, 16, 124, 16))
                    : new SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0)),
                BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 42, 42, 42)),
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });

            // Rank
            string rankText = rank == 1 ? "🥇" : rank == 2 ? "🥈" : rank == 3 ? "🥉" : $"#{rank}";
            AddCell(grid, 0, rankText, rank <= 3 ? 16 : 13, rank <= 3 ? "#FFD700" : "#888888");

            // Name + reg
            StackPanel nameStack = new StackPanel();
            nameStack.Children.Add(new TextBlock
            {
                Text = entry.Name,
                Foreground = new SolidColorBrush(Microsoft.UI.Colors.White),
                FontSize = 14,
                FontWeight = Microsoft.UI.Text.FontWeights.SemiBold
            });
            nameStack.Children.Add(new TextBlock
            {
                Text = entry.RegNo,
                Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                FontSize = 11
            });
            Grid.SetColumn(nameStack, 1);
            grid.Children.Add(nameStack);

            // Stats
            AddCell(grid, 2, entry.EasySolved.ToString(), 13, "#00B8A3", true);
            AddCell(grid, 3, entry.MediumSolved.ToString(), 13, "#FFC01E", true);
            AddCell(grid, 4, entry.HardSolved.ToString(), 13, "#FF375F", true);
            AddCell(grid, 5, entry.TotalSolved.ToString(), 14, "#FFFFFF", true, true);
            AddCell(grid, 6, entry.ContestRating.ToString("F0"), 13, "#888888", true);

            row.Child = grid;
            return row;
        }

        private void AddCell(Grid grid, int col, string text, double size, string color, bool center = false, bool bold = false)
        {
            var tb = new TextBlock
            {
                Text = text,
                Foreground = new SolidColorBrush(ColorHelper.FromArgb(255,
                    Convert.ToByte(color.Substring(1, 2), 16),
                    Convert.ToByte(color.Substring(3, 2), 16),
                    Convert.ToByte(color.Substring(5, 2), 16))),
                FontSize = size,
                HorizontalAlignment = center ? HorizontalAlignment.Center : HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = bold ? Microsoft.UI.Text.FontWeights.Bold : Microsoft.UI.Text.FontWeights.Normal
            };
            Grid.SetColumn(tb, col);
            grid.Children.Add(tb);
        }

        private void cmbBatch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void cmbSection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void cmbSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var filtered = new List<LeaderboardEntry>(allEntries);

            if (cmbBatch.SelectedItem is ComboBoxItem batch && batch.Tag != null)
            {
                int batchId = (int)batch.Tag;
                filtered = filtered.Where(e => e.BatchId == batchId).ToList();

                // Load sections for selected batch
                cmbSection.Items.Clear();
                var sections = SectionDL.GetSectionsByBatchId(batchId);
                foreach (var s in sections)
                    cmbSection.Items.Add(new ComboBoxItem { Content = s.Name, Tag = s.Id });
            }

            if (cmbSection.SelectedItem is ComboBoxItem section && section.Tag != null)
            {
                int sectionId = (int)section.Tag;
                filtered = filtered.Where(e => e.SectionId == sectionId).ToList();
            }

            if (cmbSortBy.SelectedItem is ComboBoxItem sort)
            {
                filtered = sort.Content.ToString() switch
                {
                    "Hard Solved" => filtered.OrderByDescending(e => e.HardSolved).ToList(),
                    "Contest Rating" => filtered.OrderByDescending(e => e.ContestRating).ToList(),
                    "Global Rank" => filtered.OrderBy(e => e.GlobalRanking).ToList(),
                    _ => filtered.OrderByDescending(e => e.TotalSolved).ToList()
                };
            }

            RenderLeaderboard(filtered);
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cmbBatch.SelectedIndex = -1;
            cmbSection.SelectedIndex = -1;
            cmbSortBy.SelectedIndex = 0;
            RenderLeaderboard(allEntries);
        }
    }
}

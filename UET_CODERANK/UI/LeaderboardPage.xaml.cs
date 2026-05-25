using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using UET_CODERANK.BL;
using UET_CODERANK.DL;
using UET_CODERANK.Model;

namespace UET_CODERANK.UI
{
    public sealed partial class LeaderboardPage : Page
    {
        private List<LeaderboardEntry> _allEntries = new List<LeaderboardEntry>();
        private bool _isUpdatingFilters = false;

        public LeaderboardPage()
        {
            this.InitializeComponent();
            LoadFilters();
            LoadLeaderboard();
        }

        private void LoadFilters()
        {
            _isUpdatingFilters = true;

            cmbBatch.Items.Clear();
            cmbBatch.Items.Add(new ComboBoxItem { Content = "All Batches" });
            var batches = BatchDL.GetAllBatches();
            foreach (var batch in batches)
                cmbBatch.Items.Add(new ComboBoxItem { Content = batch.Name, Tag = batch.Id });
            cmbBatch.SelectedIndex = 0;

            cmbSection.Items.Clear();
            cmbSection.Items.Add(new ComboBoxItem { Content = "All Sections" });
            cmbSection.SelectedIndex = 0;

            _isUpdatingFilters = false;
        }

        private void LoadLeaderboard()
        {
            _allEntries = LeaderboardDL.GetLeaderboard();
            txtStudentCount.Text = $"{_allEntries.Count} students ranked";
            RenderLeaderboard(_allEntries);
        }

        // ── Batch changed — reload sections then apply ────────────────────
        private void cmbBatch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isUpdatingFilters) return;
            _isUpdatingFilters = true;

            cmbSection.Items.Clear();
            cmbSection.Items.Add(new ComboBoxItem { Content = "All Sections" });

            if (cmbBatch.SelectedItem is ComboBoxItem batch && batch.Tag != null)
            {
                var sections = SectionDL.GetSectionsByBatchId((int)batch.Tag);
                foreach (var s in sections)
                    cmbSection.Items.Add(new ComboBoxItem { Content = s.Name, Tag = s.Id });
            }

            cmbSection.SelectedIndex = 0;
            _isUpdatingFilters = false;

            ApplyFilters();
        }

        private void cmbSection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isUpdatingFilters) return;
            ApplyFilters();
        }

        private void cmbSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isUpdatingFilters) return;
            ApplyFilters();
        }

        // ── Core filter logic ─────────────────────────────────────────────
        private void ApplyFilters()
        {
            var filtered = new List<LeaderboardEntry>(_allEntries);

            // Batch filter
            if (cmbBatch.SelectedItem is ComboBoxItem batch && batch.Tag != null)
                filtered = filtered.Where(e => e.BatchId == (int)batch.Tag).ToList();

            // Section filter — independent of batch
            if (cmbSection.SelectedItem is ComboBoxItem section && section.Tag != null)
                filtered = filtered.Where(e => e.SectionId == (int)section.Tag).ToList();

            // Sort
            if (cmbSortBy.SelectedItem is ComboBoxItem sort)
            {
                filtered = sort.Content.ToString() switch
                {
                    "Hard Solved" => filtered.OrderByDescending(e => e.HardSolved).ToList(),
                    "Contest Rating" => filtered.OrderByDescending(e => e.ContestRating).ToList(),
                    "Global Rank" => filtered.OrderBy(e => e.GlobalRanking).ToList(),
                    _ => filtered.OrderByDescending(e => e.Score).ToList()
                };
            }

            RenderLeaderboard(filtered);
        }

        // ── Render ────────────────────────────────────────────────────────
        private void RenderLeaderboard(List<LeaderboardEntry> entries)
        {
            if (leaderboardRows != null)
                leaderboardRows.Children.Clear();

            // Podium
            if (entries.Count >= 1) SetPodium(txt1stName, txt1stReg, txt1stSolved, entries[0]);
            if (entries.Count >= 2) SetPodium(txt2ndName, txt2ndReg, txt2ndSolved, entries[1]);
            if (entries.Count >= 3) SetPodium(txt3rdName, txt3rdReg, txt3rdSolved, entries[2]);

            // Table rows
            for (int i = 0; i < entries.Count; i++)
                leaderboardRows.Children.Add(CreateRow(i + 1, entries[i]));
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
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });

            string rankText = rank == 1 ? "🥇" : rank == 2 ? "🥈" : rank == 3 ? "🥉" : $"#{rank}";
            AddCell(grid, 0, rankText, rank <= 3 ? 16 : 13, rank <= 3 ? "#FFD700" : "#888888");

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

            AddCell(grid, 2, entry.EasySolved.ToString(), 13, "#00B8A3", true);
            AddCell(grid, 3, entry.MediumSolved.ToString(), 13, "#FFC01E", true);
            AddCell(grid, 4, entry.HardSolved.ToString(), 13, "#FF375F", true);
            AddCell(grid, 5, entry.TotalSolved.ToString(), 14, "#FFFFFF", true, true);
            AddCell(grid, 6, entry.ContestRating.ToString("F0"), 13, "#888888", true);
            AddCell(grid, 7, entry.Score.ToString("F0"), 13, "#5B6EF5", true, true);

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

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            _isUpdatingFilters = true;
            cmbBatch.SelectedIndex = 0;
            cmbSection.SelectedIndex = 0;
            cmbSortBy.SelectedIndex = 0;
            _isUpdatingFilters = false;
            RenderLeaderboard(_allEntries);
        }
    }
}
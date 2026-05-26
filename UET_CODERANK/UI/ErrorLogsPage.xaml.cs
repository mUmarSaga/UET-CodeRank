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
    public sealed partial class ErrorLogsPage : Page
    {
        public ErrorLogsPage()
        {
            this.InitializeComponent();
            LoadLogs();
        }

        private void LoadLogs()
        {
            logRows.Children.Clear();
            var logs = ErrorLog.GetAll();
            txtSubtitle.Text = $"{logs.Count} error{(logs.Count == 1 ? "" : "s")} recorded";

            if (logs.Count == 0)
            {
                logRows.Children.Add(new TextBlock
                {
                    Text = "No errors logged. All systems running smoothly.",
                    Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                    Padding = new Thickness(20, 16, 0, 16)
                });
                return;
            }

            foreach (var entry in logs)
                logRows.Children.Add(CreateRow(entry));
        }

        private Border CreateRow(ErrorEntry entry)
        {
            var row = new Border
            {
                Padding = new Thickness(20, 12, 20, 12),
                BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 42, 42, 42)),
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(160) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(160) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });

            AddText(grid, 0, entry.OccuredAt.ToString("dd MMM yy, HH:mm"), "#888888");
            AddText(grid, 1, string.IsNullOrEmpty(entry.Source) ? "—" : entry.Source, "#FFC01E");

            var msgTb = new TextBlock
            {
                Text = entry.ErrorMessage,
                Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 239, 71, 67)),
                FontSize = 13,
                VerticalAlignment = VerticalAlignment.Center,
                TextWrapping = TextWrapping.NoWrap,
                TextTrimming = TextTrimming.CharacterEllipsis
            };
            Grid.SetColumn(msgTb, 2);
            grid.Children.Add(msgTb);

            var detailsBtn = new Button
            {
                Content = "View",
                Padding = new Thickness(10, 4, 10, 4),
                CornerRadius = new CornerRadius(6),
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Center,
                Tag = entry
            };
            detailsBtn.Click += ViewDetails_Click;
            Grid.SetColumn(detailsBtn, 3);
            grid.Children.Add(detailsBtn);

            row.Child = grid;
            return row;
        }

        private async void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            var entry = (ErrorEntry)((Button)sender).Tag;
            var dialog = new ContentDialog
            {
                Title = "Error Details",
                Content = new ScrollViewer
                {
                    MaxHeight = 400,
                    Content = new StackPanel
                    {
                        Spacing = 12,
                        Children =
                        {
                            new TextBlock { Text = "Source", Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)), FontSize = 12 },
                            new TextBlock { Text = entry.Source, Foreground = new SolidColorBrush(Colors.White), FontSize = 13, TextWrapping = TextWrapping.Wrap },
                            new TextBlock { Text = "Message", Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)), FontSize = 12 },
                            new TextBlock { Text = entry.ErrorMessage, Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 239, 71, 67)), FontSize = 13, TextWrapping = TextWrapping.Wrap },
                            new TextBlock { Text = "Stack Trace", Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)), FontSize = 12 },
                            new TextBlock { Text = entry.StackTrace, Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 180, 180, 180)), FontSize = 11, TextWrapping = TextWrapping.Wrap, FontFamily = new Microsoft.UI.Xaml.Media.FontFamily("Consolas") }
                        }
                    }
                },
                CloseButtonText = "Close",
                XamlRoot = this.XamlRoot
            };
            await dialog.ShowAsync();
        }

        private async void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "Clear All Logs",
                Content = "Are you sure you want to delete all error logs? This cannot be undone.",
                PrimaryButtonText = "Clear All",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                ErrorLog.ClearAll();
                LoadLogs();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadLogs();
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
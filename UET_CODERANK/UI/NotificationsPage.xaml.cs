using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using UET_CODERANK.BL;
using UET_CODERANK.DL;
using UET_CODERANK.Model;
using Notification = UET_CODERANK.Model.Notification;
namespace UET_CODERANK.UI
{
    public sealed partial class NotificationsPage : Page
    {
        public NotificationsPage()
        {
            this.InitializeComponent();
            LoadNotifications();
        }

        private void LoadNotifications()
        {
            notificationRows.Children.Clear();
            var notifications = NotificationDL.GetByStudentId(CurrentSession.Student.Id);
            int unread = 0;
            foreach (var n in notifications)
                if (!n.IsRead) unread++;

            txtSubtitle.Text = $"{notifications.Count} total — {unread} unread";

            if (notifications.Count == 0)
            {
                notificationRows.Children.Add(new TextBlock
                {
                    Text = "No notifications yet.",
                    Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                    Padding = new Thickness(24, 20, 0, 20),
                    FontSize = 13
                });
                return;
            }

            foreach (var notification in notifications)
                notificationRows.Children.Add(CreateRow(notification));
        }

        private Border CreateRow(Notification notification)
        {
            var row = new Border
            {
                Padding = new Thickness(24, 16, 24, 16),
                BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 42, 42, 42)),
                BorderThickness = new Thickness(0, 0, 0, 1),
                Background = notification.IsRead
                    ? new SolidColorBrush(Colors.Transparent)
                    : new SolidColorBrush(Windows.UI.Color.FromArgb(20, 16, 124, 16))
            };

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(140) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });

            var msgStack = new StackPanel { Spacing = 4 };
            if (!notification.IsRead)
            {
                msgStack.Children.Add(new Border
                {
                    Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 16, 124, 16)),
                    CornerRadius = new CornerRadius(4),
                    Padding = new Thickness(6, 2, 6, 2),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Child = new TextBlock
                    {
                        Text = "NEW",
                        Foreground = new SolidColorBrush(Colors.White),
                        FontSize = 10,
                        FontWeight = Microsoft.UI.Text.FontWeights.Bold
                    }
                });
            }
            msgStack.Children.Add(new TextBlock
            {
                Text = notification.Message,
                Foreground = new SolidColorBrush(notification.IsRead
                    ? Windows.UI.Color.FromArgb(255, 180, 180, 180)
                    : Colors.White),
                FontSize = 13,
                TextWrapping = TextWrapping.Wrap
            });
            Grid.SetColumn(msgStack, 0);
            grid.Children.Add(msgStack);

            var timeTb = new TextBlock
            {
                Text = notification.CreatedAt.ToString("dd MMM yyyy\nhh:mm tt"),
                Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                FontSize = 11,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Right
            };
            Grid.SetColumn(timeTb, 1);
            grid.Children.Add(timeTb);

            if (!notification.IsRead)
            {
                var markBtn = new Button
                {
                    Content = "Mark Read",
                    Padding = new Thickness(10, 4, 10, 4),
                    CornerRadius = new CornerRadius(6),
                    FontSize = 11,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Tag = notification.Id
                };
                markBtn.Click += MarkRead_Click;
                Grid.SetColumn(markBtn, 2);
                grid.Children.Add(markBtn);
            }

            row.Child = grid;
            return row;
        }

        private void MarkRead_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;
            NotificationDL.MarkAsRead(id);
            LoadNotifications();
        }

        private void btnMarkAllRead_Click(object sender, RoutedEventArgs e)
        {
            NotificationDL.MarkAllAsRead(CurrentSession.Student.Id);
            LoadNotifications();
        }
    }
}
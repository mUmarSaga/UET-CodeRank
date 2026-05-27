using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Collections.Generic;
using UET_CODERANK.DL;
using UET_CODERANK.Model;

namespace UET_CODERANK.UI
{
    public sealed partial class SendNotificationPage : Page
    {
        public SendNotificationPage()
        {
            this.InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            cmbStudents.Items.Clear();
            var students = StudentDL.GetAllStudents();
            foreach (var student in students)
                cmbStudents.Items.Add(new ComboBoxItem
                {
                    Content = $"{student.Name} ({student.RegNo})",
                    Tag = student
                });
        }

        private void btnSendToStudent_Click(object sender, RoutedEventArgs e)
        {
            if (cmbStudents.SelectedItem is not ComboBoxItem item || item.Tag is not Student student)
            {
                SetStatus(txtStudentStatus, "Please select a student.", false);
                return;
            }

            string message = txtStudentMessage.Text.Trim();
            if (string.IsNullOrWhiteSpace(message))
            {
                SetStatus(txtStudentStatus, "Message cannot be empty.", false);
                return;
            }

            NotificationDL.SendNotification(student.Id, message);
            txtStudentMessage.Text = "";
            SetStatus(txtStudentStatus, $"Notification sent to {student.Name}.", true);
        }

        private void btnBroadcast_Click(object sender, RoutedEventArgs e)
        {
            string message = txtBroadcastMessage.Text.Trim();
            if (string.IsNullOrWhiteSpace(message))
            {
                SetStatus(txtBroadcastStatus, "Message cannot be empty.", false);
                return;
            }

            var students = StudentDL.GetAllStudents();
            foreach (var student in students)
                NotificationDL.SendNotification(student.Id, message);

            txtBroadcastMessage.Text = "";
            SetStatus(txtBroadcastStatus, $"Broadcast sent to {students.Count} students.", true);
        }

        private void SetStatus(TextBlock tb, string message, bool success)
        {
            tb.Text = message;
            tb.Foreground = new SolidColorBrush(success ? Colors.Green : Colors.Red);
        }
    }
}
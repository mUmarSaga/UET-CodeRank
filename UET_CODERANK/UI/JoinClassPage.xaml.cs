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
    public sealed partial class JoinClassPage : Page
    {
        public JoinClassPage()
        {
            this.InitializeComponent();
            CheckEnrollmentStatus();
            LoadBatches();
            LoadDepartments();
        }

        private void CheckEnrollmentStatus()
        {
            var request = EnrollmentDL.GetLatestRequest(CurrentSession.Student.Id);

            if (CurrentSession.Student.IsApproved && CurrentSession.Student.SectionId != 0)
            {
                var section = SectionDL.GetSectionById(CurrentSession.Student.SectionId);
                approvedBanner.Visibility = Visibility.Visible;
                txtApprovedInfo.Text = $"You are enrolled in {section?.Name ?? "a section"}";
                return;
            }

            if (request != null && request.Status == EnrollementStatus.PENDING)
            {
                var section = SectionDL.GetSectionById(request.SectionId);
                pendingBanner.Visibility = Visibility.Visible;
                txtPendingInfo.Text = $"Waiting for approval to join {section?.Name ?? "a section"}";
            }
        }
        private void LoadDepartments()
        {
            var departments = DepartmentDL.GetAll();
            foreach (var dept in departments)
            {
                cmbDepartment.Items.Add(new ComboBoxItem
                {
                    Content = dept.Name,
                    Tag = dept.Id
                });
            }
        }

        private void cmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            batchesPanel.Children.Clear();

            if (cmbDepartment.SelectedItem is ComboBoxItem item && item.Tag != null)
            {
                int deptId = (int)item.Tag;
                var batches = BatchDL.GetBatchesByDepartmentId(deptId);
                foreach (var batch in batches)
                    batchesPanel.Children.Add(CreateBatchCard(batch));
            }
            else
            {
                LoadBatches(); // show all if no filter
            }
        }

        private void LoadBatches()
        {
            batchesPanel.Children.Clear();
            var batches = BatchDL.GetAllBatches();

            foreach (var batch in batches)
            {
                batchesPanel.Children.Add(CreateBatchCard(batch));
            }
        }

        private Expander CreateBatchCard(Model.Batch batch)
        {
            var sections = SectionDL.GetSectionsByBatchId(batch.Id);

            // Section rows inside expander
            StackPanel sectionList = new StackPanel { Spacing = 8 };

            if (sections.Count == 0)
            {
                sectionList.Children.Add(new TextBlock
                {
                    Text = "No sections available",
                    Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                    FontSize = 12,
                    Padding = new Thickness(8, 4, 8, 4)
                });
            }
            else
            {
                foreach (var section in sections)
                {
                    sectionList.Children.Add(CreateSectionRow(section));
                }
            }

            // Expander
            Expander expander = new Expander
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                IsExpanded = false
            };

            // Expander header
            Grid header = new Grid();
            header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            header.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            StackPanel batchInfo = new StackPanel { Spacing = 2 };
            batchInfo.Children.Add(new TextBlock
            {
                Text = batch.Name,
                Foreground = new SolidColorBrush(Microsoft.UI.Colors.White),
                FontSize = 15,
                FontWeight = Microsoft.UI.Text.FontWeights.SemiBold
            });
            batchInfo.Children.Add(new TextBlock
            {
                Text = $"{sections.Count} section{(sections.Count != 1 ? "s" : "")}",
                Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                FontSize = 12
            });

            Grid.SetColumn(batchInfo, 0);
            header.Children.Add(batchInfo);

            expander.Header = header;
            expander.Content = sectionList;

            return expander;
        }

        private Border CreateSectionRow(Model.Section section)
        {
            Border row = new Border
            {
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 26, 26, 26)),
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(16, 12, 16, 12)
            };

            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            // Section info
            StackPanel info = new StackPanel { Spacing = 2 };
            info.Children.Add(new TextBlock
            {
                Text = $"Section {section.Name}",
                Foreground = new SolidColorBrush(Microsoft.UI.Colors.White),
                FontSize = 14,
                FontWeight = Microsoft.UI.Text.FontWeights.SemiBold
            });

            Grid.SetColumn(info, 0);
            grid.Children.Add(info);

            // Join button
            Button btnJoin = new Button
            {
                Content = "Request to Join",
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 16, 124, 16)),
                Foreground = new SolidColorBrush(Microsoft.UI.Colors.White),
                CornerRadius = new CornerRadius(6),
                Padding = new Thickness(16, 8, 16, 8),
                FontSize = 13
            };
            btnJoin.Click += async (s, e) => await JoinSection(section);
            Grid.SetColumn(btnJoin, 1);
            grid.Children.Add(btnJoin);

            row.Child = grid;
            return row;
        }

        private async Task JoinSection(Model.Section section)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Confirm Join Request",
                Content = $"Send a request to join Section {section.Name}?\n\nYour request will be reviewed by the admin.",
                PrimaryButtonText = "Yes, Send Request",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                string error = EnrollmentBL.SendRequest(CurrentSession.Student.Id, section.Id);
                if (error == null)
                {
                    // Show success
                    ContentDialog success = new ContentDialog
                    {
                        Title = "Request Sent!",
                        Content = "Your join request has been sent. Please wait for admin approval.",
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };
                    await success.ShowAsync();
                    CheckEnrollmentStatus();
                }
                else
                {
                    ContentDialog errorDialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = error,
                        CloseButtonText = "OK",
                        XamlRoot = this.XamlRoot
                    };
                    await errorDialog.ShowAsync();
                }
            }
        }
    }
}

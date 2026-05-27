using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using UET_CODERANK.DL;
using UET_CODERANK.Model;

namespace UET_CODERANK.UI
{
    public sealed partial class ManageBatchesPage : Page
    {
        private int selectedBatchId = 0;
        private int selectedSectionId = 0;
        private const int DepartmentId = 1;

        public ManageBatchesPage()
        {
            this.InitializeComponent();
            LoadBatches();
        }

        private void LoadBatches()
        {
            batchRows.Children.Clear();
            var batches = BatchDL.GetAllBatches();
            if (batches.Count == 0)
            {
                batchRows.Children.Add(new TextBlock
                {
                    Text = "No batches found.",
                    Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                    Padding = new Thickness(16, 12, 0, 12)
                });
                return;
            }
            foreach (var batch in batches)
                batchRows.Children.Add(CreateBatchRow(batch));
        }

        private Border CreateBatchRow(Batch batch)
        {
            var sections = SectionDL.GetSectionsByBatchId(batch.Id);
            var row = new Border
            {
                Padding = new Thickness(16, 12, 16, 12),
                BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 42, 42, 42)),
                BorderThickness = new Thickness(0, 0, 0, 1),
                Background = selectedBatchId == batch.Id
                    ? new SolidColorBrush(Windows.UI.Color.FromArgb(30, 16, 124, 16))
                    : new SolidColorBrush(Colors.Transparent)
            };

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });

            var nameBtn = new Button
            {
                Content = batch.Name,
                Background = new SolidColorBrush(Colors.Transparent),
                Foreground = new SolidColorBrush(Colors.White),
                BorderThickness = new Thickness(0),
                Padding = new Thickness(0),
                FontSize = 14,
                Tag = batch
            };
            nameBtn.Click += BatchName_Click;
            Grid.SetColumn(nameBtn, 0);
            grid.Children.Add(nameBtn);

            var countTb = new TextBlock
            {
                Text = sections.Count.ToString(),
                Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                FontSize = 13,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(countTb, 1);
            grid.Children.Add(countTb);

            var deleteBtn = new Button
            {
                Content = "Delete",
                Padding = new Thickness(10, 4, 10, 4),
                CornerRadius = new CornerRadius(6),
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 192, 57, 43)),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Center,
                Tag = batch
            };
            deleteBtn.Click += DeleteBatch_Click;
            Grid.SetColumn(deleteBtn, 2);
            grid.Children.Add(deleteBtn);

            row.Child = grid;
            return row;
        }

        private void LoadSections(int batchId)
        {
            sectionRows.Children.Clear();
            studentRows.Children.Clear();
            txtSelectedSection.Text = "";
            var sections = SectionDL.GetSectionsByBatchId(batchId);
            if (sections.Count == 0)
            {
                sectionRows.Children.Add(new TextBlock
                {
                    Text = "No sections found. Add one above.",
                    Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                    Padding = new Thickness(16, 12, 0, 12)
                });
                return;
            }
            foreach (var section in sections)
                sectionRows.Children.Add(CreateSectionRow(section));
        }

        private Border CreateSectionRow(Section section)
        {
            var students = StudentDL.GetStudentsBySectionId(section.Id);
            var row = new Border
            {
                Padding = new Thickness(16, 12, 16, 12),
                BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 42, 42, 42)),
                BorderThickness = new Thickness(0, 0, 0, 1),
                Background = selectedSectionId == section.Id
                    ? new SolidColorBrush(Windows.UI.Color.FromArgb(30, 16, 124, 16))
                    : new SolidColorBrush(Colors.Transparent)
            };

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });

            var nameBtn = new Button
            {
                Content = section.Name,
                Background = new SolidColorBrush(Colors.Transparent),
                Foreground = new SolidColorBrush(Colors.White),
                BorderThickness = new Thickness(0),
                Padding = new Thickness(0),
                FontSize = 14,
                Tag = section
            };
            nameBtn.Click += SectionName_Click;
            Grid.SetColumn(nameBtn, 0);
            grid.Children.Add(nameBtn);

            var countTb = new TextBlock
            {
                Text = students.Count.ToString(),
                Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                FontSize = 13,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(countTb, 1);
            grid.Children.Add(countTb);

            var deleteBtn = new Button
            {
                Content = "Delete",
                Padding = new Thickness(10, 4, 10, 4),
                CornerRadius = new CornerRadius(6),
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 192, 57, 43)),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Center,
                Tag = section
            };
            deleteBtn.Click += DeleteSection_Click;
            Grid.SetColumn(deleteBtn, 2);
            grid.Children.Add(deleteBtn);

            row.Child = grid;
            return row;
        }

        private void LoadStudents(int sectionId)
        {
            studentRows.Children.Clear();
            var students = StudentDL.GetStudentsBySectionId(sectionId);
            if (students.Count == 0)
            {
                studentRows.Children.Add(new TextBlock
                {
                    Text = "No students in this section.",
                    Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 136, 136, 136)),
                    Padding = new Thickness(16, 12, 0, 12)
                });
                return;
            }
            foreach (var student in students)
                studentRows.Children.Add(CreateStudentRow(student));
        }

        private Border CreateStudentRow(Student student)
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

            var removeBtn = new Button
            {
                Content = "Remove",
                Padding = new Thickness(10, 4, 10, 4),
                CornerRadius = new CornerRadius(6),
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 192, 57, 43)),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Center,
                Tag = student
            };
            removeBtn.Click += RemoveStudent_Click;
            Grid.SetColumn(removeBtn, 1);
            grid.Children.Add(removeBtn);

            row.Child = grid;
            return row;
        }

        private void BatchName_Click(object sender, RoutedEventArgs e)
        {
            var batch = (Batch)((Button)sender).Tag;
            selectedBatchId = batch.Id;
            selectedSectionId = 0;
            txtSelectedBatch.Text = $"— {batch.Name}";
            txtSelectedSection.Text = "";
            LoadBatches();
            LoadSections(batch.Id);
        }

        private void SectionName_Click(object sender, RoutedEventArgs e)
        {
            var section = (Section)((Button)sender).Tag;
            selectedSectionId = section.Id;
            txtSelectedSection.Text = $"— {section.Name}";
            LoadSections(selectedBatchId);
            LoadStudents(section.Id);
        }

        private void btnAddBatch_Click(object sender, RoutedEventArgs e)
        {
            string name = txtBatchName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                SetStatus(txtBatchStatus, "Batch name cannot be empty.", false);
                return;
            }
            try
            {
                BatchDL.AddBatch(new Batch(name, DepartmentId));
                txtBatchName.Text = "";
                SetStatus(txtBatchStatus, "Batch added successfully.", true);
                LoadBatches();
            }
            catch
            {
                SetStatus(txtBatchStatus, "Failed to add batch.", false);
            }
        }

        private void DeleteBatch_Click(object sender, RoutedEventArgs e)
        {
            var batch = (Batch)((Button)sender).Tag;
            try
            {
                BatchDL.DeleteBatch(batch);
                if (selectedBatchId == batch.Id)
                {
                    selectedBatchId = 0;
                    selectedSectionId = 0;
                    txtSelectedBatch.Text = "";
                    txtSelectedSection.Text = "";
                    sectionRows.Children.Clear();
                    studentRows.Children.Clear();
                }
                SetStatus(txtBatchStatus, "Batch deleted.", true);
                LoadBatches();
            }
            catch
            {
                SetStatus(txtBatchStatus, "Cannot delete — batch may have students assigned.", false);
            }
        }

        private void btnAddSection_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBatchId == 0)
            {
                SetStatus(txtSectionStatus, "Select a batch first by clicking its name.", false);
                return;
            }
            string name = txtSectionName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                SetStatus(txtSectionStatus, "Section name cannot be empty.", false);
                return;
            }
            try
            {
                if (SectionDL.IsSectionNameExists(name, selectedBatchId))
                {
                    SetStatus(txtSectionStatus, "Section name already exists in this batch.", false);
                    return;
                }
                SectionDL.AddSection(new Section(name, selectedBatchId));
                txtSectionName.Text = "";
                SetStatus(txtSectionStatus, "Section added successfully.", true);
                LoadSections(selectedBatchId);
                LoadBatches();
            }
            catch
            {
                SetStatus(txtSectionStatus, "Failed to add section.", false);
            }
        }

        private void DeleteSection_Click(object sender, RoutedEventArgs e)
        {
            var section = (Section)((Button)sender).Tag;
            try
            {
                SectionDL.DeleteSection(section);
                if (selectedSectionId == section.Id)
                {
                    selectedSectionId = 0;
                    txtSelectedSection.Text = "";
                    studentRows.Children.Clear();
                }
                SetStatus(txtSectionStatus, "Section deleted.", true);
                LoadSections(selectedBatchId);
                LoadBatches();
            }
            catch
            {
                SetStatus(txtSectionStatus, "Cannot delete — section may have students assigned.", false);
            }
        }

        private void RemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            var student = (Student)((Button)sender).Tag;
            try
            {
                StudentDL.RemoveStudentFromSection(student.Id);
                NotificationDL.SendNotification(student.Id, $"You have been removed from your section by an admin.");
                LoadStudents(selectedSectionId);
                LoadSections(selectedBatchId);
                LoadBatches();
            }
            catch
            {
                SetStatus(txtSectionStatus, "Failed to remove student.", false);
            }
        }

        private void SetStatus(TextBlock tb, string message, bool success)
        {
            tb.Text = message;
            tb.Foreground = new SolidColorBrush(success ? Colors.Green : Colors.Red);
        }
    }
}
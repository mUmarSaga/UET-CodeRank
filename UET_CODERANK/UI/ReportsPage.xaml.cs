using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UET_CODERANK.DL;
using UET_CODERANK.Model;
using Microsoft.UI;

namespace UET_CODERANK.UI
{
    public sealed partial class ReportsPage : Page
    {
        public ReportsPage()
        {
            this.InitializeComponent();
            QuestPDF.Settings.License = LicenseType.Community;
            LoadBatches();
        }

        private void LoadBatches()
        {
            cmbBatch.Items.Clear();
            var batches = BatchDL.GetAllBatches();
            foreach (var batch in batches)
                cmbBatch.Items.Add(new ComboBoxItem { Content = batch.Name, Tag = batch });
        }

        private void btnGenerateOverall_Click(object sender, RoutedEventArgs e)
        {
            btnGenerateOverall.IsEnabled = false;
            btnGenerateOverall.Content = "Generating...";

            try
            {
                var entries = LeaderboardDL.GetLeaderboard();
                string path = GeneratePDF(entries, "UET CodeRank — Overall Department Report", "All Batches");
                SetStatus(txtOverallStatus, $"✅ Report saved to: {path}", true);
            }
            catch (Exception ex)
            {
                SetStatus(txtOverallStatus, $"❌ Failed: {ex.Message}", false);
            }
            finally
            {
                btnGenerateOverall.IsEnabled = true;
                btnGenerateOverall.Content = "Generate Overall Report";
            }
        }

        private void btnGenerateBatch_Click(object sender, RoutedEventArgs e)
        {
            if (cmbBatch.SelectedItem is not ComboBoxItem item || item.Tag is not Batch batch)
            {
                SetStatus(txtBatchStatus, "Please select a batch.", false);
                return;
            }

            btnGenerateBatch.IsEnabled = false;
            btnGenerateBatch.Content = "Generating...";

            try
            {
                var entries = LeaderboardDL.GetLeaderboardByBatch(batch.Id);
                string path = GeneratePDF(entries, $"UET CodeRank — Batch {batch.Name} Report", batch.Name);
                SetStatus(txtBatchStatus, $"✅ Report saved to: {path}", true);
            }
            catch (Exception ex)
            {
                SetStatus(txtBatchStatus, $"❌ Failed: {ex.Message}", false);
            }
            finally
            {
                btnGenerateBatch.IsEnabled = true;
                btnGenerateBatch.Content = "Generate Batch Report";
            }
        }

        private string GeneratePDF(List<LeaderboardEntry> entries, string title, string subtitle)
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = $"UET_CodeRank_Report_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string path = Path.Combine(folder, fileName);

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Header().Column(col =>
                    {
                        col.Item().Text(title).FontSize(18).Bold().FontColor("#111111");
                        col.Item().Text($"Batch: {subtitle}  |  Generated: {DateTime.Now:dd MMM yyyy, hh:mm tt}  |  Total Students: {entries.Count}")
                            .FontSize(9).FontColor("#888888");
                        col.Item().PaddingTop(8).LineHorizontal(1).LineColor("#2A2A2A");
                    });

                    page.Content().PaddingTop(16).Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.ConstantColumn(30);
                            cols.RelativeColumn(3);
                            cols.RelativeColumn(2);
                            cols.RelativeColumn(2);
                            cols.RelativeColumn(2);
                            cols.ConstantColumn(50);
                            cols.ConstantColumn(50);
                            cols.ConstantColumn(50);
                            cols.ConstantColumn(55);
                            cols.ConstantColumn(55);
                        });

                        table.Header(header =>
                        {
                            string[] headers = { "#", "Student", "Batch", "Section", "LeetCode", "Easy", "Medium", "Hard", "Total", "Score" };
                            foreach (var h in headers)
                            {
                                header.Cell().Background("#1e2124").Padding(6)
                                    .Text(h).Bold().FontColor("#888888").FontSize(8);
                            }
                        });

                        for (int i = 0; i < entries.Count; i++)
                        {
                            var entry = entries[i];
                            bool isEven = i % 2 == 0;
                            string bg = isEven ? "#25282c" : "#1e2124";

                            table.Cell().Background(bg).Padding(6).Text((i + 1).ToString()).FontColor("#888888");
                            table.Cell().Background(bg).Padding(6).Column(c =>
                            {
                                c.Item().Text(entry.Name).Bold().FontColor("#FFFFFF");
                                c.Item().Text(entry.RegNo).FontColor("#888888").FontSize(7);
                            });
                            table.Cell().Background(bg).Padding(6).Text(entry.BatchName).FontColor("#888888");
                            table.Cell().Background(bg).Padding(6).Text(entry.SectionName).FontColor("#888888");
                            table.Cell().Background(bg).Padding(6).Text("—").FontColor("#888888");
                            table.Cell().Background(bg).Padding(6).Text(entry.EasySolved.ToString()).FontColor("#00b8a3");
                            table.Cell().Background(bg).Padding(6).Text(entry.MediumSolved.ToString()).FontColor("#ffc01e");
                            table.Cell().Background(bg).Padding(6).Text(entry.HardSolved.ToString()).FontColor("#ef4743");
                            table.Cell().Background(bg).Padding(6).Text(entry.TotalSolved.ToString()).Bold().FontColor("#FFFFFF");
                            table.Cell().Background(bg).Padding(6).Text(entry.Score.ToString()).Bold().FontColor("#5B6EF5");
                        }
                    });

                    page.Footer().AlignCenter()
                    .Text(x =>
                    {
                        x.Span("UET CodeRank — Computer Science Department, UET Lahore  |  Page ").FontSize(8).FontColor("#888888");
                        x.CurrentPageNumber().FontSize(8).FontColor("#888888");
                        x.Span(" of ").FontSize(8).FontColor("#888888");
                        x.TotalPages().FontSize(8).FontColor("#888888");
                    });
                });
            }).GeneratePdf(path);

            return path;
        }

        private void SetStatus(TextBlock tb, string message, bool success)
        {
            tb.Text = message;
            tb.Foreground = new SolidColorBrush(success ? Microsoft.UI.Colors.Green : Microsoft.UI.Colors.Red);
        }
    }
}
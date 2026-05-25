using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UET_CODERANK.BL;
using UET_CODERANK.DL;
using Windows.Foundation;
using Windows.Foundation.Collections;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UET_CODERANK.UI;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class HomePage : Page
{
    public HomePage()
    {
        InitializeComponent();
        if (!string.IsNullOrEmpty(CurrentSession.Student.LeetcodeUsername))
            
        {
            LoadStats();
        }
        
    }
    private void LoadStats()
    {
        Model.LeetCodeStat stats = CurrentSession.leetCodeStat;
        int score = stats.Easy_solved * 1 + stats.Medium_solved * 3 + stats.Hard_solved * 5;
        Model.ContestStats contest = null;
        

        if (stats != null)
        {
            txtWelcome.Text = $"Welcome back, {CurrentSession.Student.Name} 👋";
            txtTotalSolved.Text = stats.Total_solved.ToString();
            txtGlobalRank.Text = $"#{stats.Global_rank}";   
            txtScore.Text = $"{score}";
            txtEasy.Text = $"Easy: {stats.Easy_solved}";
            txtMedium.Text = $"Medium: {stats.Medium_solved}";
            txtHard.Text = $"Hard: {stats.Hard_solved}";
           

            // Donut chart
            donutChart.Series = new ISeries[]
            {
                new PieSeries<double>
                {
                    Values = new double[] { stats.Easy_solved },
                    Name = "Easy",
                    Fill = new SolidColorPaint(SKColor.Parse("#00B8A3")),
                    InnerRadius = 0
                },
                new PieSeries<double>
                {
                    Values = new double[] { stats.Medium_solved },
                    Name = "Medium",
                    Fill = new SolidColorPaint(SKColor.Parse("#FFC01E")),
                    InnerRadius = 0
                },
                new PieSeries<double>
                {
                    Values = new double[] { stats.Hard_solved },
                    Name = "Hard",
                    Fill = new SolidColorPaint(SKColor.Parse("#FF375F")),
                    InnerRadius = 0
                }
            };
        }

        if (contest != null)
        {
            txtContestRating.Text = contest.ContestRating.ToString("F0");
            txtContests.Text = contest.ContestAttended.ToString();
        }

        // Weekly progress chart
        //var snapshots = WeeklySnapshotDL.GetByStudentId(CurrentSession.Student.Id);

        //if (snapshots != null && snapshots.Count > 0)
        //{
        //    weeklyChart.Series = new ISeries[]
        //    {
        //        new ColumnSeries<int>
        //        {
        //            Values = snapshots.Select(s => s.TotalSolved).ToArray(),
        //            Fill = new SolidColorPaint(SKColor.Parse("#107C10"))
        //        }
        //    };
        //}
    }
}

using System;
using System.Windows;
using System.Windows.Threading;

namespace GameOfLife
{
    public partial class MainWindow : Window
    {
        private Grid mainGrid;
        private DispatcherTimer timer;   //  Generation timer
        private int genCounter;
        private AdWindow[] adWindows;

        public MainWindow()
        {
            InitializeComponent();
            mainGrid = new Grid(MainCanvas);

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(200)
            };
            timer.Tick += OnTimer;
        }

        private void StartAd()
        {
            if (adWindows == null)
                adWindows = new AdWindow[2];

            for (int i = 0; i < adWindows.Length; i++)
            {
                if (adWindows[i] == null)
                {
                    adWindows[i] = new AdWindow(this);
                    adWindows[i].Closed += AdWindowOnClosed;
                    adWindows[i].Top = this.Top + (i * 110); 
                    adWindows[i].Left = this.Left + this.Width + 20;
                    adWindows[i].Show();
                }
            }
        }

        private void AdWindowOnClosed(object sender, EventArgs e)
        {
            for (int i = 0; i < adWindows.Length; i++)
            {
                if (adWindows[i] == sender)
                {
                    adWindows[i].Closed -= AdWindowOnClosed;
                    adWindows[i] = null;
                }
            }
        }

        private void Button_OnClick(object sender, EventArgs e)
        {
            if (!timer.IsEnabled)
            {
                timer.Start();
                ButtonStart.Content = "Stop";
                StartAd();
            }
            else
            {
                timer.Stop();
                ButtonStart.Content = "Start";
                CloseAds();
            }
        }

        private void OnTimer(object sender, EventArgs e)
        {
            mainGrid.Update();
            genCounter++;
            lblGenCount.Content = $"Generations: {genCounter}";
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Clear();
        }

        private void CloseAds()
        {
            if (adWindows == null) return;

            for (int i = 0; i < adWindows.Length; i++)
            {
                if (adWindows[i] != null)
                {
                    adWindows[i].Close();
                    adWindows[i] = null;
                }
            }
        }
    }
}

using CyberPuzzle.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Windows.Threading;

namespace CyberPuzzle.ViewModel
{
    public class BreachTimeViewModel : ObservableObject
    {
        private const double Period = 0.01;

        public DispatcherTimer Timer { get; set; }

        public Level GameLevel { get; set; }

        public double RemainingTime { get; set; }

        public double TotalTime { get; set; }

        public double Percent => RemainingTime / TotalTime * 100;

        public BreachTimeViewModel(Level level)
        {
            GameLevel = level;

            Reset(30);

            Timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(Period) };
            Timer.Tick += Tick;
        }

        private DateTime lastTick;
        private void Tick(object sender, EventArgs e)
        {
            RemainingTime -= (DateTime.Now - lastTick).TotalSeconds;
            lastTick = DateTime.Now;
            if (RemainingTime <= 0)
            {
                RemainingTime = 0;
                Timer.Stop();
                GameLevel.ForceGameOver();
            }
        }

        public void Reset(int time = 60)
        {
            RemainingTime = TotalTime = time;
        }

        public void StartTimer(int time = 60)
        {
            Reset(time);
            lastTick = DateTime.Now;
            Timer.Start();
        }

        internal void StopTimer()
        {
            Timer.Stop();
        }
    }
}

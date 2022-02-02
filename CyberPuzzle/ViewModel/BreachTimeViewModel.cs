using CyberPuzzle.Messages;
using CyberPuzzle.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using PropertyChanged;
using System;
using System.Windows.Threading;

namespace CyberPuzzle.ViewModel
{
    public class BreachTimeViewModel : ObservableObject
    {
        private const double Period = 0.01;

        /// <summary>
        /// the dispatch timer used to count down
        /// </summary>
        [DoNotNotify]
        public DispatcherTimer Timer { get; set; }

        [DoNotNotify]
        public Level GameLevel { get; set; } = App.Current.Services.GetService<Level>();

        public double RemainingTime { get; set; }

        public double TotalTime { get; set; }

        [DependsOn(nameof(RemainingTime), nameof(TotalTime))]
        public double Percent => RemainingTime / TotalTime * 100;

        public BreachTimeViewModel()
        {
            Reset(30);

            Timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(Period) };
            Timer.Tick += Tick;

            WeakReferenceMessenger.Default.Register<StopTimerMessage>(this, (rec, mes) =>
            {
                StopTimer();
            });
        }

        private DateTime lastTick;
        /// <summary>
        /// the function the timer do in its every tick
        /// </summary>
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

        /// <summary>
        /// reset the <see cref="RemainingTime"/> and <see cref="TotalTime"/>
        /// </summary>
        /// <param name="time"></param>
        private void Reset(int time = 60)
        {
            RemainingTime = TotalTime = time;
        }

        /// <summary>
        /// when start a new puzzle, restart the timer
        /// </summary>
        /// <param name="time"></param>
        public void StartTimer(int time = 60)
        {
            Reset(time);
            lastTick = DateTime.Now;
            Timer.Start();
        }

        /// <summary>
        /// stop the timer when the game is finished early because no more quiz can be finished
        /// </summary>
        public void StopTimer()
        {
            Timer.Stop();
        }
    }
}

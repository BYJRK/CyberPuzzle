using CyberPuzzle.Helpers;
using CyberPuzzle.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;

namespace CyberPuzzle.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        public Level GameLevel { get; set; }

        #region Constructor

        public MainWindowViewModel()
        {
            GameLevel = new Level { mainVM = this };
            GameLevel.NewPuzzle(2);

            BufferPanelVM = new(GameLevel);
            CodeMatrixVM = new(GameLevel);
            ObjectiveVM = new(GameLevel);
            BreachTimeVM = new(GameLevel);

            NewGameCommand = new RelayCommand<string>(cnt =>
            {
                switch (cnt)
                {
                    case "EASY":
                        GameLevel.NewPuzzle(RandomHelper.NextInteger(1, 2));
                        BreachTimeVM.StartTimer(45);
                        break;
                    case "MEDIUM":
                        GameLevel.NewPuzzle(RandomHelper.NextInteger(3, 4));
                        BreachTimeVM.StartTimer(60);
                        break;
                    case "HARD":
                        GameLevel.NewPuzzle(RandomHelper.NextInteger(5, 6));
                        BreachTimeVM.StartTimer(90);
                        break;
                }
                CodeMatrixVM.UpdateAvailability();
            });
            QuitGameCommand = new RelayCommand(() => Application.Current.MainWindow.Close());

            BreachTimeVM.StartTimer(45);
        }

        #endregion

        #region ViewModels

        public BufferPanelViewModel BufferPanelVM { get; private set; }
        public CodeMatrixViewModel CodeMatrixVM { get; private set; }
        public ObjectivePanelViewModel ObjectiveVM { get; set; }
        public BreachTimeViewModel BreachTimeVM { get; set; }

        /// <summary>
        /// the command to start a new game, given the difficulty
        /// </summary>
        public ICommand NewGameCommand { get; set; }

        /// <summary>
        /// the command to quit the application
        /// </summary>
        public ICommand QuitGameCommand { get; set; }

        #endregion

    }
}

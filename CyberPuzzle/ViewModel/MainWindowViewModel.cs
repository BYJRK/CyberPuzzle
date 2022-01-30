using CyberPuzzle.Helpers;
using CyberPuzzle.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace CyberPuzzle.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        public Level GameLevel { get; set; }

        #region Constructor

        public MainWindowViewModel()
        {
            GameLevel = new Level();
            GameLevel.NewPuzzle(5, 5, 4);

            BufferPanelVM = new(GameLevel);
            CodeMatrixVM = new(GameLevel);
            ObjectiveVM = new(GameLevel);

            NewGameCommand = new RelayCommand<string>(cnt =>
            {
                switch (cnt)
                {
                    case "EASY":
                        GameLevel.NewPuzzle(5, 6, 4, new[] { 2, 3 });
                        break;
                    case "MEDIUM":
                        GameLevel.NewPuzzle(6, 7, 5, new[] { 3, 4, 4 });
                        break;
                    case "HARD":
                        GameLevel.NewPuzzle(7, 8, 5, new[] { 3, 4, 5 });
                        break;
                }
                CodeMatrixVM.UpdateAvailability();
            });
        }

        #endregion

        #region ViewModels

        public BufferPanelViewModel BufferPanelVM { get; private set; }
        public CodeMatrixViewModel CodeMatrixVM { get; private set; }
        public ObjectivePanelViewModel ObjectiveVM { get; set; }

        public ICommand NewGameCommand { get; set; }

        #endregion

    }
}

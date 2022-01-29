using CyberPuzzle.Helpers;
using CyberPuzzle.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace CyberPuzzle.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        public Level GameLevel { get; set; }

        #region Constructor

        public MainWindowViewModel()
        {
            GameLevel = new Level();

            BufferPanelVM = new(GameLevel);
            CodeMatrixVM = new(GameLevel);
            ObjectiveVM = new(GameLevel);
        }

        #endregion

        #region ViewModels

        public BufferPanelViewModel BufferPanelVM { get; private set; }
        public CodeMatrixViewModel CodeMatrixVM { get; private set; }
        public ObjectivePanelViewModel ObjectiveVM { get; set; }

        #endregion

    }
}

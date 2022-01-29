using CyberPuzzle.Helpers;
using CyberPuzzle.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CyberPuzzle.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        public Level GameLevel { get; set; }

        #region Constructor

        public MainWindowViewModel()
        {
            RandomHelper.Init(1335);

            GameLevel = new Level();

            BufferPanelVM = new(GameLevel);
            CodeMatrixVM = new(GameLevel);
        }

        #endregion

        #region ViewModels

        public BufferPanelViewModel BufferPanelVM { get; private set; }
        public CodeMatrixViewModel CodeMatrixVM { get; private set; }

        #endregion

    }
}

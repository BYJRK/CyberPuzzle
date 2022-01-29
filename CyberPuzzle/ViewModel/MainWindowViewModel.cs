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
        #region Constructor

        public MainWindowViewModel()
        {
            RandomHelper.Init(1335);

            BufferPanelVM = new();
            CodeMatrixVM = new(BufferPanelVM);
        }

        #endregion

        #region ViewModels

        public BufferPanelViewModel BufferPanelVM { get; private set; }
        public CodeMatrixViewModel CodeMatrixVM { get; private set; }

        #endregion

    }
}

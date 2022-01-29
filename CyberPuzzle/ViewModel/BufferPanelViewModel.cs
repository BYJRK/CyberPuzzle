﻿using CyberPuzzle.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace CyberPuzzle.ViewModel
{
    public class BufferPanelViewModel : ObservableObject
    {
        public static BufferPanelViewModel Demo
        {
            get
            {
                var level = new Level();
                level.NewPuzzle(6, 7, 5);
                level.Append(level[0, 0]);
                level.Append(level[0, 1]);
                return new BufferPanelViewModel(level);
            }
        }

        public Level GameLevel { get; set; }

        public BufferPanelViewModel(Level level)
        {
            GameLevel = level;
        }
    }
}

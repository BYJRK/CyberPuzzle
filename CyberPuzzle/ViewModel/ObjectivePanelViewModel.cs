using CyberPuzzle.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPuzzle.ViewModel
{
    public class ObjectivePanelViewModel : ObservableObject
    {
        public Level GameLevel { get; set; }

        public static ObjectivePanelViewModel Demo => new ObjectivePanelViewModel(new Level
        {
            Objectives = { new[] { "FF", "BD", "7A" }, new[] { "7A", "55", "FF", "1C" } }
        });

        public ObjectivePanelViewModel(Level level)
        {
            GameLevel = level;
        }
    }
}

using CyberPuzzle.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace CyberPuzzle.ViewModel
{
    public class BufferPanelViewModel : ObservableObject
    {
        public Level GameLevel { get; set; }

        public BufferPanelViewModel(Level level)
        {
            GameLevel = level;
        }
    }
}

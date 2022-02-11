using CyberPuzzle.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace CyberPuzzle.ViewModel
{
    public class BufferPanelViewModel : ObservableObject, IDesignTimeDemo<BufferPanelViewModel>
    {
        public static BufferPanelViewModel Demo
        {
            get
            {
                var level = new Level();
                level.NewPuzzle(6, 7, 5);
                level.Append(level[0, 0]);
                level.Append(level[0, 1]);
                return new BufferPanelViewModel { GameLevel = level };
            }
        }

        public Level GameLevel { get; private set; } = App.Services.GetService<Level>();

        public BufferPanelViewModel()
        {

        }
    }
}

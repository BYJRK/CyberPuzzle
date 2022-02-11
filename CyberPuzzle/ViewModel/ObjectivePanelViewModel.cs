using CyberPuzzle.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace CyberPuzzle.ViewModel
{
    public class ObjectivePanelViewModel : ObservableObject
    {
        public Level GameLevel { get; private set; } = App.Services.GetService<Level>();

        public ObjectivePanelViewModel()
        {
            MouseEnterCommand = new RelayCommand<string>(s =>
            {
                UpdateIsHinted(s);
            });
            MouseLeaveCommand = new RelayCommand(() =>
            {
                UpdateIsHinted(null);
            });
        }

        /// <summary>
        /// update the pieces' IsHinted property based on the word that mouse is currently on
        /// </summary>
        /// <param name="cur"></param>
        private void UpdateIsHinted(string cur)
        {
            if (cur != null)
                foreach (var p in GameLevel.Puzzle)
                {
                    p.IsHinted = p.Word == cur && !p.IsSelected;
                }
            else
                foreach (var p in GameLevel.Puzzle)
                {
                    p.IsHinted = false;
                }
        }

        /// <summary>
        /// the command that handles the mouse enter event, so that the same words will be highlighted in the code matrix
        /// </summary>
        public ICommand MouseEnterCommand { get; set; }

        /// <summary>
        /// the command to cancel the highlight after mouse leaves
        /// </summary>
        public ICommand MouseLeaveCommand { get; set; }
    }
}

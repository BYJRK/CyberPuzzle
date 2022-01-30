using CyberPuzzle.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace CyberPuzzle.ViewModel
{
    public class ObjectivePanelViewModel : ObservableObject
    {
        public Level GameLevel { get; set; }

        public static ObjectivePanelViewModel Demo => new ObjectivePanelViewModel(new Level
        {
            Objectives = new(new[]
            {
                new Objective(new[] { "FF", "BD", "7A" }),
                new Objective(new[] { "7A", "55", "FF", "1C" })
            })
        });

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

        public ObjectivePanelViewModel(Level level)
        {
            GameLevel = level;

            MouseEnterCommand = new RelayCommand<string>(s =>
            {
                UpdateIsHinted(s);
            });
            MouseLeaveCommand = new RelayCommand(() =>
            {
                UpdateIsHinted(null);
            });
        }

        public ICommand MouseEnterCommand { get; set; }

        public ICommand MouseLeaveCommand { get; set; }
    }
}

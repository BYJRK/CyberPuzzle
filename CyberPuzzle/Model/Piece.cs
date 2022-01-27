using Microsoft.Toolkit.Mvvm.ComponentModel;
using PropertyChanged;
using System.Windows.Media;

namespace CyberPuzzle.Model
{
    internal class Piece : ObservableObject
    {
        public string Word { get; set; }

        /// <summary>
        /// whether this button has been clicked and therefore cannot be clicked anymore
        /// </summary>
        public bool IsSelected { get; set; } = false;

        /// <summary>
        /// whether the button is enabled
        /// </summary>
        [DependsOn(nameof(IsSelected), nameof(IsAvailable))]
        public bool IsEnabled => !IsSelected && IsAvailable;

        /// <summary>
        /// the coordinates of the current piece
        /// </summary>
        public (int X, int Y) Position { get; private set; }

        /// <summary>
        /// according to the rule, whether the current piece can be clicked
        /// </summary>
        public bool IsAvailable { get; set; }

        [DependsOn(nameof(IsSelected))]
        public Brush Foreground => IsSelected ? Brushes.Orange : Brushes.Black;

        public Piece(string word, (int, int) pos)
        {
            Word = word;
            Position = pos;
        }
    }
}

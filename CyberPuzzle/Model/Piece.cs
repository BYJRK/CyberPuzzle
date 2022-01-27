using Microsoft.Toolkit.Mvvm.ComponentModel;
using PropertyChanged;
using System.Windows.Media;

namespace CyberPuzzle.Model
{
    internal class Piece : ObservableObject
    {
        private static Brush NormalBrush;
        private static Brush HighlightBrush;
        private static Brush DisabledBrush;

        static Piece()
        {
            NormalBrush = App.Current.TryFindResource("ForegroundYellowBrush") as Brush;
            HighlightBrush = App.Current.TryFindResource("HighlightCyanBrush") as Brush;
            DisabledBrush = App.Current.TryFindResource("DisableBrush") as Brush;
        }

        private string word;
        public string Word
        {
            get
            {
                return IsSelected ? "[ ]" : word;
            }
            private set => word = value;
        }

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
        public Brush Foreground => IsSelected ? DisabledBrush : NormalBrush;

        public Piece(string word, (int, int) pos)
        {
            Word = word;
            Position = pos;
        }
    }
}

using Microsoft.Toolkit.Mvvm.ComponentModel;
using PropertyChanged;
using System.Windows.Media;

namespace CyberPuzzle.Model
{
    public class Piece : ObservableObject
    {
        /// <summary>
        /// the placeholder when this piece is pressed
        /// </summary>
        public const string SelectedEmptyPlaceholder = "[]";

        /// <summary>
        /// the actual word this piece is holding
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// used to display the word or placeholder in the puzzle panel
        /// </summary>
        [DependsOn(nameof(Word))]
        public string DisplayWord => IsSelected ? SelectedEmptyPlaceholder : Word;

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
        /// indicates that this piece will be available after clicking the specific piece
        /// </summary>
        public bool IsHighlighted { get; set; }

        /// <summary>
        /// the coordinates of the current piece
        /// </summary>
        public (int X, int Y) Position { get; private set; }

        /// <summary>
        /// according to the rule, whether the current piece can be clicked
        /// </summary>
        public bool IsAvailable { get; set; }

        public Piece(string word, (int, int) pos)
        {
            Word = word;
            Position = pos;
        }
    }
}

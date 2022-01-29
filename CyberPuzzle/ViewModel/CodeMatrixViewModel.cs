using CyberPuzzle.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace CyberPuzzle.ViewModel
{
    public class CodeMatrixViewModel : ObservableObject
    {
        public static CodeMatrixViewModel Demo => new CodeMatrixViewModel(new Level());

        public Level GameLevel { get; set; }

        public CodeMatrixViewModel(Level level)
        {
            GameLevel = level;
            PieceClickCommand = new RelayCommand<Piece>(piece =>
            {
                piece.IsSelected = true;
                GameLevel.Append(piece);
                GameLevel.CurrentPosition = piece.Position;
                GameLevel.Direction = !GameLevel.Direction;
                UpdateAvailability();
            });
            PieceMouseEnterCommand = new RelayCommand<Piece>(piece =>
            {
                UpdateHighlight(piece);
            });

            GameLevel.NewPuzzle(6, 7, 5);

            UpdateAvailability();
        }

        /// <summary>
        /// the command when the piece is clicked
        /// </summary>
        public ICommand PieceClickCommand { get; private set; }

        /// <summary>
        /// the command when the mouse is on the piece and indicates the pieces on the same row
        /// or column will be available in the next turn
        /// </summary>
        public ICommand PieceMouseEnterCommand { get; private set; }

        /// <summary>
        /// the command when the mouse has left the current piece
        /// </summary>
        public ICommand PieceMouseLeaveCommand { get; private set; }

        /// <summary>
        /// update the availablility of all pieces according to the current position and direction
        /// </summary>
        private void UpdateAvailability()
        {
            foreach (var p in GameLevel.Puzzle)
            {
                if (GameLevel.Direction && p.Position.Y == GameLevel.CurrentPosition.Y)
                    p.IsAvailable = true;
                else if (!GameLevel.Direction && p.Position.X == GameLevel.CurrentPosition.X)
                    p.IsAvailable = true;
                else
                    p.IsAvailable = false;
            }
        }

        /// <summary>
        /// update the highlight property of all pieces according to the current piece the mouse is on and direction
        /// </summary>
        private void UpdateHighlight(Piece piece)
        {
            foreach (var p in GameLevel.Puzzle)
            {
                // the current piece is shown as enabled button style
                if (p == piece)
                    p.IsHighlighted = false;
                else if (!GameLevel.Direction && p.Position.Y == piece.Position.Y)
                    p.IsHighlighted = true;
                else if (GameLevel.Direction && p.Position.X == piece.Position.X)
                    p.IsHighlighted = true;
                else
                    p.IsHighlighted = false;
            }
        }
    }
}

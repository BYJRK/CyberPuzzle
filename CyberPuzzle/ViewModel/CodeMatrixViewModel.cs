using CyberPuzzle.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Linq;
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
                UpdateFinishStatus();
            });
            PieceMouseEnterCommand = new RelayCommand<Piece>(piece =>
            {
                UpdateHighlight(piece);
                UpdateHighlightObjective(piece);
                UpdateBufferHighlight(piece);
            });
            PieceMouseLeaveCommand = new RelayCommand<Piece>(piece =>
            {
                UpdateHighlightObjective(null);
                UpdateBufferHighlight(null);
            });

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
        public void UpdateAvailability()
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

        /// <summary>
        /// update the highlight property of all pieces in the buffer
        /// </summary>
        private void UpdateBufferHighlight(Piece piece)
        {
            // this should never happen because once the game is over, the puzzle board is disabled
            // and hence the mouseover event shall never be triggerred
            if (GameLevel.Index == GameLevel.SelectedWords.Count)
                return;

            if (piece == null)
            {
                GameLevel.SelectedWords[GameLevel.Index].Word = null;
                GameLevel.SelectedWords[GameLevel.Index].IsHighlighted = false;
            }
            else
            {
                GameLevel.SelectedWords[GameLevel.Index].Word = piece.Word;
                GameLevel.SelectedWords[GameLevel.Index].IsHighlighted = true;
            }
        }

        /// <summary>
        /// update the objectives' IsFinished status
        /// </summary>
        private void UpdateFinishStatus()
        {
            int FindMaxOverlayLength(string[] arr1, string[] arr2)
            {
                int idx = 0;
                for (int i = 0; i < arr1.Length; i++)
                {
                    if (arr1[i] == arr2[idx])
                    {
                        idx++;
                        if (idx == arr2.Length)
                            return idx;
                    }
                    else if (arr1[i] == arr2[0])
                    {
                        idx = 1;
                    }
                    else
                    {
                        idx = 0;
                    }
                }
                return idx;
            }

            var current = GameLevel.SelectedWords.Where(p => p.Word != null).Select(p => p.Word).ToArray();
            foreach (var obj in GameLevel.Objectives)
            {
                // skip if this row has already been marked as finished or cannot finish
                if (obj.CannotFinish)
                    continue;
                if (obj.IsFinished)
                    continue;

                // update isfinished property of each piece
                var sequence = obj.Row.Select(p => p.Word).ToArray();
                var overlayLength = FindMaxOverlayLength(current, sequence);
                for (int i = 0; i < obj.Row.Count; i++)
                {
                    obj[i].IsFinished = i < overlayLength;
                }

                if (overlayLength == sequence.Length)
                    obj.IsFinished = true;

                else if (obj.UnfinishedWordLength > GameLevel.SelectedWords.Count - GameLevel.Index)
                    obj.CannotFinish = true;
            }

            // decide whether the game is over
            if (GameLevel.Objectives.Where(obj => obj.IsFinished || obj.CannotFinish).Count() == GameLevel.Objectives.Count)
            {
                GameLevel.ForceGameOver();
            }
        }

        /// <summary>
        /// update the objectives' current highlighted words according to selected words
        /// </summary>
        /// <param name="piece"></param>
        private void UpdateHighlightObjective(Piece piece)
        {
            foreach (var obj in GameLevel.Objectives)
            {
                foreach (var p in obj.Row)
                {
                    p.IsHighlighted = piece != null && piece.Word == p.Word;
                }
            }
        }
    }
}

using CyberPuzzle.Helpers;
using CyberPuzzle.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CyberPuzzle.ViewModel
{
    internal class MainWindowViewModel : ObservableObject
    {
        public static readonly string[] Words = new[]
        {
             "1C", "55", "7A", "BD", "E9", "FF"
        };

        #region Constructor

        public MainWindowViewModel()
        {
            RandomHelper.Init(1335);
            GenerateRandomPuzzle(6, 5);
            PieceClickCommand = new RelayCommand<Piece>(piece =>
            {
                piece.IsSelected = true;
                SelectedWords.Add(piece);
                CurrentPosition = piece.Position;
                Direction = !Direction;
                UpdateAvailability();
            });
            PieceMouseEnterCommand = new RelayCommand<Piece>(piece =>
            {
                UpdateHighlight(piece);
            });
            UpdateAvailability();
        }

        #endregion

        /// <summary>
        /// the size of the puzzle
        /// </summary>
        public int PuzzleSize => (int)Math.Sqrt(Puzzle.Count);

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
        /// the array saving the pieces and will be showing on the left board
        /// </summary>
        public ObservableCollection<Piece> Puzzle { get; private set; } = new();

        /// <summary>
        /// the selected words
        /// </summary>
        public ObservableCollection<Piece> SelectedWords { get; set; } = new();

        /// <summary>
        /// the coordinates of the last clicked piece
        /// </summary>
        [DoNotNotify]
        public (int X, int Y) CurrentPosition { get; set; } = (-1, 0);

        /// <summary>
        /// the current direction, where true stands for horizontal
        /// </summary>
        [DoNotNotify]
        public bool Direction { get; set; } = true;

        /// <summary>
        /// update the availablility of all pieces according to the current position and direction
        /// </summary>
        private void UpdateAvailability()
        {
            foreach (var piece in Puzzle)
            {
                if (Direction && piece.Position.Y == CurrentPosition.Y)
                    piece.IsAvailable = true;
                else if (!Direction && piece.Position.X == CurrentPosition.X)
                    piece.IsAvailable = true;
                else
                    piece.IsAvailable = false;
            }
        }

        /// <summary>
        /// update the highlight property of all pieces according to the current piece the mouse is on and direction
        /// </summary>
        private void UpdateHighlight(Piece piece)
        {
            foreach (var p in Puzzle)
            {
                // the current piece is shown as enabled button style
                if (p == piece)
                    p.IsHighlighted = false;
                else if (!Direction && p.Position.Y == piece.Position.Y)
                    p.IsHighlighted = true;
                else if (Direction && p.Position.X == piece.Position.X)
                    p.IsHighlighted = true;
                else
                    p.IsHighlighted = false;
            }
        }

        /// <summary>
        /// generate a random puzzle with given side length
        /// </summary>
        /// <param name="size"></param>
        /// <param name="seed"></param>
        /// <param name="wordCount"></param>
        private void GenerateRandomPuzzle(int size, int? wordCount = null)
        {
            var count = wordCount ?? Words.Length;
            var words = RandomHelper.Sample(Words, count).ToArray();

            Puzzle.Clear();
            SelectedWords.Clear();

            for (int i = 0; i < size * size; i++)
            {
                Puzzle.Add(new Piece(words[RandomHelper.Rnd.Next(count)], (i % size, i / size)));
            }
        }
    }
}

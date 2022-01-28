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
            PieceClickCommand = new RelayCommand<Piece>(PieceClick);
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

        public ObservableCollection<Piece> Puzzle { get; private set; } = new();

        public ObservableCollection<string> SelectedWords { get; set; } = new();

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

        private void PieceClick(Piece p)
        {
            p.IsSelected = true;
            SelectedWords.Add(p.Word);
            CurrentPosition = p.Position;
            Direction = !Direction;
            UpdateAvailability();
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

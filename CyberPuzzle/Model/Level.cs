using CyberPuzzle.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CyberPuzzle.Model
{
    public class Level : ObservableObject
    {
        /// <summary>
        /// all possible words
        /// </summary>
        public static readonly string[] Words = new[] { "1C", "55", "7A", "BD", "E9", "FF" };

        /// <summary>
        /// the selected words
        /// </summary>
        public ObservableCollection<Piece> SelectedWords { get; set; } = new();

        /// <summary>
        /// the current index in the <see cref="SelectedWords"/> list
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// whether the game is over
        /// </summary>
        public bool IsGameNotFinished => Index < SelectedWords.Count;

        /// <summary>
        /// the array saving the pieces and will be showing on the left board
        /// </summary>
        public ObservableCollection<Piece> Puzzle { get; private set; } = new();

        /// <summary>
        /// the size of the puzzle
        /// </summary>
        public int PuzzleSize => (int)Math.Sqrt(Puzzle.Count);

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
        /// generate a random puzzle with given side length
        /// </summary>
        /// <param name="size"></param>
        /// <param name="seed"></param>
        /// <param name="wordCount"></param>
        private void GenerateRandomPuzzle(int size, int? wordCount = null)
        {
            var count = wordCount ?? Words.Length;
            var words = RandomHelper.Sample(Words, count).ToArray();

            for (int i = 0; i < size * size; i++)
            {
                Puzzle.Add(new Piece(words[RandomHelper.Rnd.Next(count)], (i % size, i / size)));
            }
        }

        /// <summary>
        /// start a new game
        /// </summary>
        public void NewPuzzle(int size, int maxLength)
        {
            if (size != 5 && size != 6)
                throw new NotImplementedException("size other than 5 or 6 is not supported.");

            Reset(maxLength);

            Puzzle.Clear();
            Direction = true;
            CurrentPosition = (-1, 0);
            GenerateRandomPuzzle(size, size - 1);
        }

        /// <summary>
        /// append a new word into the <see cref="SelectedWords"/>
        /// </summary>
        /// <param name="piece"></param>
        public void Append(Piece piece)
        {
            SelectedWords[Index++] = piece;
        }

        public void Reset(int count)
        {
            SelectedWords.Clear();
            for (int i = 0; i < count; i++)
                SelectedWords.Add(null);
            Index = 0;
        }
    }
}

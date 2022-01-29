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

        public ObservableCollection<string[]> Objectives { get; set; } = new();

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

        public (int X, int Y) CurrentCursorPosition { get; set; }

        /// <summary>
        /// the current direction, where true stands for horizontal
        /// </summary>
        [DoNotNotify]
        public bool Direction { get; set; } = true;

        #region Private Methods

        /// <summary>
        /// generate a random puzzle with given side length
        /// </summary>
        /// <param name="boardSize"></param>
        /// <param name="seed"></param>
        /// <param name="wordCount"></param>
        private void GenerateRandomPuzzle(int boardSize, string[] possibleWords)
        {
            Puzzle.Clear();
            for (int i = 0; i < boardSize * boardSize; i++)
            {
                Puzzle.Add(new Piece(RandomHelper.Choice(possibleWords), (i % boardSize, i / boardSize)));
            }
        }

        /// <summary>
        /// generate random quizes
        /// </summary>
        /// <param name="possibleWords"></param>
        /// <param name="quizSizes"></param>
        private void GenerateRandomObjectives(string[] possibleWords, int[] quizSizes)
        {
            Objectives.Clear();
            foreach (var size in quizSizes)
            {
                Objectives.Add(Enumerable.Range(0, size).Select(_ => RandomHelper.Choice(possibleWords)).ToArray());
            }
        }

        #endregion

        /// <summary>
        /// start a new game
        /// </summary>
        public void NewPuzzle(int boardSize, int maxLength, int numOfPossibleWords)
        {
            if (boardSize != 5 && boardSize != 6)
                throw new NotImplementedException("size other than 5 or 6 is not supported.");

            ResetSelectedWords(maxLength);

            Direction = true;
            CurrentPosition = (-1, 0);

            var possibleWords = RandomHelper.Sample(Words, numOfPossibleWords).ToArray();
            GenerateRandomPuzzle(boardSize, possibleWords);
            GenerateRandomObjectives(possibleWords, new[] { 3, 4, 5 });
        }

        /// <summary>
        /// append a new word into the <see cref="SelectedWords"/>
        /// </summary>
        /// <param name="piece"></param>
        public void Append(Piece piece)
        {
            SelectedWords[Index++] = piece;
        }

        public void ResetSelectedWords(int count)
        {
            SelectedWords.Clear();
            for (int i = 0; i < count; i++)
                SelectedWords.Add(null);
            Index = 0;
        }

        public Piece this[int index]
        {
            get { return Puzzle[index]; }
        }

        public Piece this[int x, int y] { get { return Puzzle[y * PuzzleSize + x]; } }
    }
}

using CyberPuzzle.Helpers;
using CyberPuzzle.ViewModel;
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

        #region Exposed Properties for Binding

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

        public ObservableCollection<Objective> Objectives { get; set; } = new();

        /// <summary>
        /// the array saving the pieces and will be showing on the left board
        /// </summary>
        public ObservableCollection<Piece> Puzzle { get; private set; } = new();

        /// <summary>
        /// the size of the puzzle
        /// </summary>
        public int PuzzleSize { get; set; }

        #endregion

        /// <summary>
        /// the coordinates of the last clicked piece
        /// </summary>
        [DoNotNotify]
        public (int X, int Y) CurrentPosition { get; set; } = (-1, 0);

        [DoNotNotify]
        public (int X, int Y) CurrentCursorPosition { get; set; }

        /// <summary>
        /// the current direction, where true stands for horizontal
        /// </summary>
        [DoNotNotify]
        public bool Direction { get; set; } = true;

        public MainWindowViewModel mainVM { get; set; }

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
                Objectives.Add(new Objective(Enumerable.Range(0, size).Select(_ => RandomHelper.Choice(possibleWords))));
            }
        }

        #endregion

        /// <summary>
        /// start a new game
        /// </summary>
        public void NewPuzzle(int boardSize, int maxLength, int numOfPossibleWords, int[] objectiveSizes = null)
        {
            if (boardSize != 5 && boardSize != 6 && boardSize != 7)
                throw new NotImplementedException("size other than 5 or 6 is not supported.");

            ResetSelectedWords(maxLength);

            PuzzleSize = boardSize;
            Direction = true;
            CurrentPosition = (-1, 0);

            if (objectiveSizes == null)
                objectiveSizes = new[] { 3, 4, 5 };

            var possibleWords = RandomHelper.Sample(Words, numOfPossibleWords).ToArray();
            GenerateRandomPuzzle(boardSize, possibleWords);
            GenerateRandomObjectives(possibleWords, objectiveSizes);
        }

        /// <summary>
        /// quickly start a new game with given difficulty
        /// </summary>
        /// <param name="level">the value ranges from 1 to 6</param>
        public void NewPuzzle(int level)
        {
            switch (level)
            {
                case 1:
                    NewPuzzle(5, 4, 4, new[] { 2, 3 });
                    break;
                case 2:
                    NewPuzzle(5, 5, 4, new[] { 2, 3, 3 });
                    break;
                case 3:
                    NewPuzzle(6, 6, 5, new[] { 3, 3 });
                    break;
                case 4:
                    NewPuzzle(6, 7, 5, new[] { 2, 3, 4 });
                    break;
                case 5:
                    NewPuzzle(7, 8, 5, new[] { 3, 4, 4 });
                    break;
                case 6:
                    NewPuzzle(7, 8, 5, new[] { 3, 4, 5 });
                    break;
            }
        }

        /// <summary>
        /// append a new word into the <see cref="SelectedWords"/>
        /// </summary>
        /// <param name="piece"></param>
        public void Append(Piece piece)
        {
            SelectedWords[Index++] = piece;
        }

        /// <summary>
        /// force end the current game
        /// </summary>
        public void ForceGameOver()
        {
            Index = SelectedWords.Count;
            foreach (var obj in Objectives)
            {
                if (!obj.IsFinished)
                    obj.CannotFinish = true;
            }
            mainVM.BreachTimeVM.StopTimer();
        }

        /// <summary>
        /// all the procedures to reset the selected words (but add empty elements) and the corresponding indices
        /// </summary>
        private void ResetSelectedWords(int count)
        {
            SelectedWords.Clear();
            for (int i = 0; i < count; i++)
                SelectedWords.Add(null);
            Index = 0;
        }

        #region Indexers

        public Piece this[int index]
        {
            get { return Puzzle[index]; }
        }

        public Piece this[int x, int y] { get { return Puzzle[y * PuzzleSize + x]; } }

        #endregion
    }
}

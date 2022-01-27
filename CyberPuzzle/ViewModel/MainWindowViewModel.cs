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

        public int PuzzleSize => (int)Math.Sqrt(Puzzle.Count);

        public ICommand PieceClickCommand { get; private set; }

        public ObservableCollection<Piece> Puzzle { get; private set; } = new();

        public ObservableCollection<string> SelectedWords { get; set; } = new();

        [DoNotNotify]
        public (int X, int Y) CurrentPosition { get; set; } = (-1, 0);

        /// <summary>
        /// the current direction, where true stands for horizontal
        /// </summary>
        [DoNotNotify]
        public bool Direction { get; set; } = true;

        public MainWindowViewModel()
        {
            GenerateRandomPuzzle(6, 1334, 5);
            PieceClickCommand = new RelayCommand<Piece>(PieceClick);
            UpdateAvailability();
        }

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

        private void GenerateRandomPuzzle(int size, int seed, int? wordCount = null)
        {
            var rnd = new Random(seed);

            var words = (string[])Words.Clone();
            Shuffle(words, rnd);
            if (wordCount.HasValue)
                words = words.Take(wordCount.Value).ToArray();
            else
                wordCount = Words.Length;
            Puzzle.Clear();
            for (int i = 0; i < size * size; i++)
            {
                Puzzle.Add(new Piece(words[rnd.Next(wordCount.Value)], (i % size, i / size)));
            }
        }

        public static void Shuffle<T>(IList<T> list, Random rnd)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

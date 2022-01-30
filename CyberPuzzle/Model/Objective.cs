using Microsoft.Toolkit.Mvvm.ComponentModel;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace CyberPuzzle.Model
{
    public class Objective : ObservableObject
    {
        public class QuizPiece : ObservableObject
        {
            public string Word { get; set; }
            public bool IsFinished { get; set; }
            public bool IsHighlighted { get; set; }

            public QuizPiece(string word)
            {
                Word = word;
            }
        }

        #region Exposed Properties for Binding

        /// <summary>
        /// whether the current quiz is finished
        /// </summary>
        public bool IsFinished { get; set; }

        /// <summary>
        /// the current quiz cannot be finished due to a lack of remaining steps
        /// </summary>
        public bool CannotFinish { get; set; }

        /// <summary>
        /// the text showing on the cover of this row
        /// </summary>
        public string CoverText => CannotFinish ? "FAILED" : "INSTALLED";

        /// <summary>
        /// the score of this quiz
        /// </summary>
        public int Score => Row.Count * 100;

        /// <summary>
        /// whether the score is shown
        /// </summary>
        public Visibility ScoreVisibility => IsFinished ? Visibility.Visible : Visibility.Hidden;

        /// <summary>
        /// a collection of words in this quiz
        /// </summary>
        public ObservableCollection<QuizPiece> Row { get; set; }

        #endregion

        public Objective(IEnumerable<string> words)
        {
            Row = new ObservableCollection<QuizPiece>();
            foreach (var word in words)
            {
                Row.Add(new QuizPiece(word));
            }
        }

        public QuizPiece this[int index] => Row[index];

        [DoNotNotify]
        public int WordLength => Row.Count;

        [DoNotNotify]
        public int FinishedWordLength => Row.TakeWhile(p => p.IsFinished).Count();

        [DoNotNotify]
        public int UnfinishedWordLength => WordLength - FinishedWordLength;

    }
}

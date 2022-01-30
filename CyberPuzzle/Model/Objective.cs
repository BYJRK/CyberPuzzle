using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        public bool IsFinished { get; set; }

        public ObservableCollection<QuizPiece> Row { get; set; }

        public Objective(IEnumerable<string> words)
        {
            Row = new ObservableCollection<QuizPiece>();
            foreach (var word in words)
            {
                Row.Add(new QuizPiece(word));
            }
        }
    }
}

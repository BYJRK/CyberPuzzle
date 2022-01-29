using CyberPuzzle.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CyberPuzzle.ViewModel
{
    public class BufferPanelViewModel : ObservableObject
    {
        /// <summary>
        /// the selected words
        /// </summary>
        public ObservableCollection<Piece> SelectedWords { get; set; } = new();

        public int Index { get; set; }

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

        public bool IsGameNotFinished => Index < SelectedWords.Count;
    }
}

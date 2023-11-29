using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Microsoft.Win32;
using Textorator1.ViewModel;

namespace Textorator1.Model
{
    public class RichTextEditorModel : INotifyPropertyChanged
    {
        private string _selectedFontFamily;
        private string _selectedFontSize;

        public IEnumerable<FontFamily> FontFamilies { get; } = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
        public IEnumerable<double> FontSizes { get; } = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

        public string SelectedFontFamily
        {
            get { return _selectedFontFamily; }
            set
            {
                _selectedFontFamily = value;
                OnPropertyChanged(nameof(SelectedFontFamily));
            }
        }

        public string SelectedFontSize
        {
            get { return _selectedFontSize; }
            set
            {
                _selectedFontSize = value;
                OnPropertyChanged(nameof(SelectedFontSize));
            }
        }

        private RichTextBox _rtbEditor = new RichTextBox();
        public RichTextBox RtbEditor
        {
            get { return _rtbEditor; }
            set
            {
                _rtbEditor = value;
                OnPropertyChanged(nameof(RtbEditor));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

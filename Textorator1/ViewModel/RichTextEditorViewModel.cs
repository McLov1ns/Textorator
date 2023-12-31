﻿using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using Textorator1.Model;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Textorator1.ViewModel
{
    public class RichTextEditorViewModel : INotifyPropertyChanged
    {
        private RichTextEditorModel _model = new RichTextEditorModel();
        private RichTextBox _docBox = new RichTextBox();
        public RichTextBox docBox {
            get
            {
                return _docBox;
            }
            set
            {
                
                _docBox = value;
                OnPropertyChanged("docBox");
            }
        }
        public RichTextEditorModel Model
        {
            get { return _model; }
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        private RelayCommand _openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return _openCommand ?? (_openCommand = new RelayCommand(param => Open_Executed()));
            }
        }

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(param => Save_Executed()));
            }
        }
        public RelayCommand LeftCommand => new RelayCommand(param => EditingCommands.AlignLeft.Execute(null, docBox));

        public RelayCommand CenterCommand => new RelayCommand(param => EditingCommands.AlignCenter.Execute(null, docBox));

        public RelayCommand RightCommand => new RelayCommand(param => EditingCommands.AlignRight.Execute(null, docBox));

        public RelayCommand JustifyCommand => new RelayCommand(param => EditingCommands.AlignJustify.Execute(null, docBox));

        public RelayCommand ItalicCommand => new RelayCommand(param => EditingCommands.ToggleItalic.Execute(null, docBox));

        public RelayCommand BoldCommand => new RelayCommand(param => EditingCommands.ToggleBold.Execute(null, docBox));

        public RelayCommand UnderlineCommand => new RelayCommand(param => EditingCommands.ToggleUnderline.Execute(null, docBox));

        public RelayCommand GrowCommand => new RelayCommand(param => EditingCommands.IncreaseFontSize.Execute(null, docBox));

        public RelayCommand ShrinkCommand => new RelayCommand(param => EditingCommands.DecreaseFontSize.Execute(null, docBox));

        private void Open_Executed()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "RichText Files (*.rtf)|*.rtf|All files (*.*)|*.*";
            
            if (ofd.ShowDialog() == true)
            {
                TextRange doc = new TextRange(docBox.Document.ContentStart, docBox.Document.ContentEnd);
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
                {
                    if (Path.GetExtension(ofd.FileName).ToLower() == ".rtf")
                        doc.Load(fs, DataFormats.Rtf);
                    else if (Path.GetExtension(ofd.FileName).ToLower() == ".txt")
                        doc.Load(fs, DataFormats.Text);
                    else
                        doc.Load(fs, DataFormats.Xaml);
                }
            }
        }
        
        private void Save_Executed()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt|RichText Files (*.rtf)|*.rtf|XAML Files (*.xaml)|*.xaml|All files (*.*)|*.*";
            if (sfd.ShowDialog() == true)
            {
                TextRange doc = new TextRange(docBox.Document.ContentStart, docBox.Document.ContentEnd);
                using (FileStream fs = File.Create(sfd.FileName))
                {
                    if (Path.GetExtension(sfd.FileName).ToLower() == ".rtf")
                        doc.Save(fs, DataFormats.Rtf);
                    else if (Path.GetExtension(sfd.FileName).ToLower() == ".txt")
                        doc.Save(fs, DataFormats.Text);
                    else
                        doc.Save(fs, DataFormats.Xaml);
                }
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

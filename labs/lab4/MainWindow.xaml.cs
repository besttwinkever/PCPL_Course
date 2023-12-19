using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Diagnostics;

namespace Lab4
{
    public partial class MainWindow : Window
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        List<String> words = new List<String>();
        Stopwatch stopWatch = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();

            openFileDialog.Title = "Выбор списка слов";
            openFileDialog.Filter = "Текстовый файл (.txt)|*.txt";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
        }

        private void LoadFilesFromFile(string path)
        {
            List<String> readWords = new List<String>();
            stopWatch.Reset();
            stopWatch.Start();
            foreach (string word in File.ReadAllText(path).Split('\n'))
                readWords.Add(word);
            stopWatch.Stop();
            LoadTimeLabel.Content = stopWatch.Elapsed.ToString();

            stopWatch.Reset();

            stopWatch.Start();
            foreach (string word in readWords)
            {
                if (!words.Contains(word))
                    words.Add(word);
                
            }
            stopWatch.Stop();
            SaveTimeLabel.Content = stopWatch.Elapsed.ToString();
        }

        private void UpdateListBox()
        {
            stopWatch.Reset();
            stopWatch.Start();
            string filter = FilterTextBox.Text;
            WordListBox.Items.Clear();
            foreach (string word in words)
            {
                if (filter.Length == 0 || word.Contains(filter))
                    WordListBox.Items.Add(word);
            }
            stopWatch.Stop();
            SearchTimeLabel.Content = stopWatch.Elapsed.ToString();
        }

        private void LoadWordsButton_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                LoadFilesFromFile(openFileDialog.FileName);
            }
            UpdateListBox();
        }

        private void FilterTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            UpdateListBox();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using AnimalQuiz.Models;

namespace AnimalQuiz.WPF
{
    public partial class MainWindow : Window
    {
        private string soundPath = "";
        private string correctImagePath = "";
        private string wrongImage1Path = "";
        private string wrongImage2Path = "";

        private List<Question> questions =
            new List<Question>();

        private int selectedQuestionIndex = -1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnChooseSound_Click(
            object sender,
            RoutedEventArgs e)
        {
            OpenFileDialog dialog =
                new OpenFileDialog();

            dialog.Filter =
                "Fișiere audio|*.wav;*.mp3";

            if (dialog.ShowDialog() == true)
            {
                soundPath = dialog.FileName;

                txtSoundPath.Text =
                    soundPath;

                mediaSound.Source =
                    new Uri(soundPath);
            }
        }

        private void BtnPlaySound_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(soundPath))
            {
                MessageBox.Show(
                    "Alege mai întâi un sunet.");

                return;
            }

            mediaSound.Stop();
            mediaSound.Play();
        }

        private void BtnChooseCorrectImage_Click(
            object sender,
            RoutedEventArgs e)
        {
            correctImagePath =
                ChooseImage(
                    imgCorrect,
                    txtCorrectImagePath);
        }

        private void BtnChooseWrongImage1_Click(
            object sender,
            RoutedEventArgs e)
        {
            wrongImage1Path =
                ChooseImage(
                    imgWrong1,
                    txtWrongImage1Path);
        }

        private void BtnChooseWrongImage2_Click(
            object sender,
            RoutedEventArgs e)
        {
            wrongImage2Path =
                ChooseImage(
                    imgWrong2,
                    txtWrongImage2Path);
        }

        private string ChooseImage(
            Image imageControl,
            TextBlock textBlock)
        {
            OpenFileDialog dialog =
                new OpenFileDialog();

            dialog.Filter =
                "Fișiere imagine|*.jpg;*.jpeg;*.png";

            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;

                imageControl.Source =
                    new BitmapImage(
                        new Uri(path));

                textBlock.Text = path;

                return path;
            }

            return "";
        }

        private void Button_Click(
            object sender,
            RoutedEventArgs e)
        {
            ResetLabels();

            bool valid = true;

            if (string.IsNullOrWhiteSpace(soundPath))
            {
                lblSound.Foreground =
                    Brushes.Red;

                valid = false;
            }

            if (cmbAnimal.SelectedItem == null)
            {
                lblAnimal.Foreground =
                    Brushes.Red;

                valid = false;
            }

            if (rbUsor.IsChecked != true &&
                rbMediu.IsChecked != true &&
                rbGreu.IsChecked != true)
            {
                lblDifficulty.Foreground =
                    Brushes.Red;

                valid = false;
            }

            if (string.IsNullOrWhiteSpace(
                correctImagePath))
            {
                lblCorrectImage.Foreground =
                    Brushes.Red;

                valid = false;
            }

            if (string.IsNullOrWhiteSpace(
                wrongImage1Path))
            {
                lblWrongImage1.Foreground =
                    Brushes.Red;

                valid = false;
            }

            if (string.IsNullOrWhiteSpace(
                wrongImage2Path))
            {
                lblWrongImage2.Foreground =
                    Brushes.Red;

                valid = false;
            }

            if (!valid)
            {
                MessageBox.Show(
                    "Completează toate câmpurile marcate cu roșu.",
                    "Date invalide",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return;
            }

            Question q = new Question
            {
                SoundPath = soundPath,

                Animal =
                    (AnimalType)Enum.Parse(
                        typeof(AnimalType),
                        GetComboValue(cmbAnimal)),

                Level =
                    GetDifficultyFromRadioButtons(),

                CreatedDate =
                    DateTime.Now
            };

            q.Answers.Add(
                new Answer
                {
                    ImagePath = correctImagePath,
                    IsCorrect = true
                });

            q.Answers.Add(
                new Answer
                {
                    ImagePath = wrongImage1Path,
                    IsCorrect = false
                });

            q.Answers.Add(
                new Answer
                {
                    ImagePath = wrongImage2Path,
                    IsCorrect = false
                });

            questions.Add(q);

            RefreshQuestionList();

            MessageBox.Show(
                "Întrebarea a fost adăugată cu succes!",
                "Succes",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            ClearForm();
        }

        private void BtnModify_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (selectedQuestionIndex < 0 ||
                selectedQuestionIndex >= questions.Count)
            {
                MessageBox.Show(
                    "Selectează o întrebare.");

                return;
            }

            questions[selectedQuestionIndex].Animal =
                (AnimalType)Enum.Parse(
                    typeof(AnimalType),
                    GetComboValue(cmbAnimal));

            questions[selectedQuestionIndex].Level =
                GetDifficultyFromRadioButtons();

            questions[selectedQuestionIndex].CreatedDate =
                DateTime.Now;

            questions[selectedQuestionIndex].SoundPath =
                soundPath;

            RefreshQuestionList();

            MessageBox.Show(
                "Întrebarea a fost modificată.");
        }

        private void BtnDeleteQuestion_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (selectedQuestionIndex < 0 ||
                selectedQuestionIndex >= questions.Count)
            {
                MessageBox.Show(
                    "Selectează o întrebare.");

                return;
            }

            questions.RemoveAt(selectedQuestionIndex);

            RefreshQuestionList();

            ClearForm();

            MessageBox.Show(
                "Întrebarea a fost ștearsă.");
        }

        private void BtnSearch_Click(
            object sender,
            RoutedEventArgs e)
        {
            lstResults.Items.Clear();

            if (cmbSearchAnimal.SelectedItem == null)
            {
                MessageBox.Show(
                    "Alege animalul pentru căutare.");

                return;
            }

            AnimalType animal =
                (AnimalType)Enum.Parse(
                    typeof(AnimalType),
                    GetComboValue(cmbSearchAnimal));

            foreach (Question q in questions)
            {
                if (q.Animal == animal)
                {
                    lstResults.Items.Add(
                        $"{q.Animal} | {q.Level}");
                }
            }

            if (lstResults.Items.Count == 0)
            {
                lstResults.Items.Add(
                    "Nu există întrebări.");
            }
        }

        private void lstResults_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            selectedQuestionIndex =
                lstResults.SelectedIndex;

            if (selectedQuestionIndex < 0 ||
                selectedQuestionIndex >= questions.Count)
                return;

            Question q =
                questions[selectedQuestionIndex];

            cmbAnimal.Text =
                q.Animal.ToString();

            rbUsor.IsChecked =
                q.Level == Difficulty.Usor;

            rbMediu.IsChecked =
                q.Level == Difficulty.Mediu;

            rbGreu.IsChecked =
                q.Level == Difficulty.Greu;

            soundPath =
                q.SoundPath;

            txtSoundPath.Text =
                q.SoundPath;
        }

        private void RefreshQuestionList()
        {
            lstResults.Items.Clear();

            foreach (Question q in questions)
            {
                lstResults.Items.Add(
                    $"{q.Animal} | {q.Level}");
            }
        }

        private Difficulty GetDifficultyFromRadioButtons()
        {
            if (rbUsor.IsChecked == true)
                return Difficulty.Usor;

            if (rbMediu.IsChecked == true)
                return Difficulty.Mediu;

            return Difficulty.Greu;
        }

        private string GetComboValue(
            ComboBox comboBox)
        {
            ComboBoxItem item =
                comboBox.SelectedItem as ComboBoxItem;

            return item.Content.ToString();
        }

        private void ResetLabels()
        {
            lblSound.Foreground =
                Brushes.Black;

            lblAnimal.Foreground =
                Brushes.Black;

            lblDifficulty.Foreground =
                Brushes.Black;

            lblCorrectImage.Foreground =
                Brushes.Black;

            lblWrongImage1.Foreground =
                Brushes.Black;

            lblWrongImage2.Foreground =
                Brushes.Black;
        }

        private void ClearForm()
        {
            soundPath = "";

            correctImagePath = "";

            wrongImage1Path = "";

            wrongImage2Path = "";

            txtSoundPath.Text =
                "Niciun sunet ales";

            txtCorrectImagePath.Text =
                "Nicio imagine aleasă";

            txtWrongImage1Path.Text =
                "Nicio imagine aleasă";

            txtWrongImage2Path.Text =
                "Nicio imagine aleasă";

            imgCorrect.Source = null;
            imgWrong1.Source = null;
            imgWrong2.Source = null;

            cmbAnimal.SelectedItem = null;

            rbUsor.IsChecked = false;
            rbMediu.IsChecked = false;
            rbGreu.IsChecked = false;

            selectedQuestionIndex = -1;
        }

        private void MenuExit_Click(
            object sender,
            RoutedEventArgs e)
        {
            Close();
        }

        private void MenuAbout_Click(
            object sender,
            RoutedEventArgs e)
        {
            MessageBox.Show(
                "AnimalQuiz - aplicație educativă pentru copii.");
        }

        private void MenuClear_Click(
            object sender,
            RoutedEventArgs e)
        {
            ClearForm();
        }

        private void MenuSearch_Click(
            object sender,
            RoutedEventArgs e)
        {
            MessageBox.Show(
                "Selectează animalul și apasă Caută.");
        }

        private void BtnStartGame_Click(
            object sender,
            RoutedEventArgs e)
        {
            GameWindow game =
                new GameWindow();

            game.Show();
        }
    }
}
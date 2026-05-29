using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AnimalQuiz.WPF
{
    public partial class GameWindow : Window
    {
        private MediaPlayer player = new MediaPlayer();
        private MediaPlayer rewardPlayer = new MediaPlayer();
        private MediaPlayer wrongPlayer = new MediaPlayer();

        private int correctAnswer = 1;

        private int score = 0;
        private int questionNumber = 1;

        public GameWindow()
        {
            InitializeComponent();

            LoadQuestion();
        }

        private void LoadQuestion()
        {
            try
            {
               
                if (questionNumber == 1)
                {
                    player.Open(
                        new Uri(
                            "Sounds/pisica.mp3",
                            UriKind.Relative));

                    imgAnswer1.Source =
                        new BitmapImage(
                            new Uri(
                                "Images/pisica.jpg",
                                UriKind.Relative));

                    imgAnswer2.Source =
                        new BitmapImage(
                            new Uri(
                                "Images/cal.jpg",
                                UriKind.Relative));

                    imgAnswer3.Source =
                        new BitmapImage(
                            new Uri(
                                "Images/porc.jpg",
                                UriKind.Relative));

                    correctAnswer = 1;
                }

               
                else if (questionNumber == 2)
                {
                    player.Open(
                        new Uri(
                            "Sounds/caine.mp3",
                            UriKind.Relative));

                    imgAnswer1.Source =
                        new BitmapImage(
                            new Uri(
                                "Images/vaca.jpg",
                                UriKind.Relative));

                    imgAnswer2.Source =
                        new BitmapImage(
                            new Uri(
                                "Images/caine.jpg",
                                UriKind.Relative));

                    imgAnswer3.Source =
                        new BitmapImage(
                            new Uri(
                                "Images/oaie.jpg",
                                UriKind.Relative));

                    correctAnswer = 2;
                }

              
                else if (questionNumber == 3)
                {
                    player.Open(
                        new Uri(
                            "Sounds/vaca.mp3",
                            UriKind.Relative));

                    imgAnswer1.Source =
                        new BitmapImage(
                            new Uri(
                                "Images/vaca.jpg",
                                UriKind.Relative));

                    imgAnswer2.Source =
                        new BitmapImage(
                            new Uri(
                                "Images/cal.jpg",
                                UriKind.Relative));

                    imgAnswer3.Source =
                        new BitmapImage(
                            new Uri(
                                "Images/porc.jpg",
                                UriKind.Relative));

                    correctAnswer = 1;
                }

                
                else if (questionNumber == 4)
                {
                    player.Open(
                        new Uri(
                            "Sounds/oaie.mp3",
                            UriKind.Relative));

                    imgAnswer1.Source =
                        new BitmapImage(
                            new Uri(
                                "Images/magar.jpg",
                                UriKind.Relative));

                    imgAnswer2.Source =
                        new BitmapImage(
                            new Uri(
                                "Images/oaie.jpg",
                                UriKind.Relative));

                    imgAnswer3.Source =
                        new BitmapImage(
                            new Uri(
                                "Images/cal.jpg",
                                UriKind.Relative));

                    correctAnswer = 2;
                }

               
                else if (questionNumber == 5)
                {
                    player.Open(
                        new Uri(
                            "Sounds/cal.mp3",
                            UriKind.Relative));

                    imgAnswer1.Source =
                        new BitmapImage(
                            new Uri(
                                "Images/porc.jpg",
                                UriKind.Relative));

                    imgAnswer2.Source =
                        new BitmapImage(
                            new Uri(
                                "Images/cal.jpg",
                                UriKind.Relative));

                    imgAnswer3.Source =
                        new BitmapImage(
                            new Uri(
                                "Images/pisica.jpg",
                                UriKind.Relative));

                    correctAnswer = 2;
                }
            }
            catch
            {
                MessageBox.Show(
                    "Verifică dacă toate imaginile și sunetele există.");
            }
        }

        private void BtnPlaySound_Click(
            object sender,
            RoutedEventArgs e)
        {
            player.Stop();
            player.Play();
        }

        private void CheckAnswer(int answer)
        {
            resultBox.Visibility =
                Visibility.Visible;

            imgAnswer1.IsEnabled = false;
            imgAnswer2.IsEnabled = false;
            imgAnswer3.IsEnabled = false;

           
            if (answer == correctAnswer)
            {
                rewardPlayer.Open(
                    new Uri(
                        "Sounds/corect.mp3",
                        UriKind.Relative));

                rewardPlayer.Play();

                txtResult.Text =
                    "🎉 BRAVO!";

                txtResult.Foreground =
                    Brushes.Green;

                score++;

                txtScore.Text =
                    $"Scor: {score}";
            }

            
            else
            {
                wrongPlayer.Open(
                    new Uri(
                        "Sounds/gresit.mp3",
                        UriKind.Relative));

                wrongPlayer.Play();

                txtResult.Text =
                    "❌ MAI ÎNCEARCĂ!";

                txtResult.Foreground =
                    Brushes.Red;
            }
        }

        private void BtnCloseResult_Click(
            object sender,
            RoutedEventArgs e)
        {
            resultBox.Visibility =
                Visibility.Collapsed;

            imgAnswer1.IsEnabled = true;
            imgAnswer2.IsEnabled = true;
            imgAnswer3.IsEnabled = true;

            questionNumber++;

           
            if (questionNumber > 5)
            {
                MessageBox.Show(
                    $"Joc terminat!\nScor final: {score}",
                    "Felicitări!");

                Close();

                return;
            }

            LoadQuestion();
        }

        private void Answer1_Click(
            object sender,
            RoutedEventArgs e)
        {
            CheckAnswer(1);
        }

        private void Answer2_Click(
            object sender,
            RoutedEventArgs e)
        {
            CheckAnswer(2);
        }

        private void Answer3_Click(
            object sender,
            RoutedEventArgs e)
        {
            CheckAnswer(3);
        }

    }

}
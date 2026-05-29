using System.ComponentModel;

namespace AnimalQuiz.Models
{
    public class Reward : INotifyPropertyChanged
    {
        private string name;
        private string rewardPath;
        private int minimumCorrectAnswers;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string RewardPath
        {
            get { return rewardPath; }
            set
            {
                rewardPath = value;
                OnPropertyChanged(nameof(RewardPath));
            }
        }

        public int MinimumCorrectAnswers
        {
            get { return minimumCorrectAnswers; }
            set
            {
                minimumCorrectAnswers = value;
                OnPropertyChanged(nameof(MinimumCorrectAnswers));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
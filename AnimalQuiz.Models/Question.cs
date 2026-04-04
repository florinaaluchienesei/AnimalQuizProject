using System.Collections.Generic;

namespace AnimalQuiz
{
    public class Question
    {
        public string SoundPath { get; set; }
        public List<Answer> Answers { get; set; }

        public AnimalType Animal { get; set; }
        public Difficulty Level { get; set; }

        public Question()
        {
            Answers = new List<Answer>();
        }
    }
}
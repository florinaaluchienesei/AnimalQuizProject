using System.Collections.Generic;
using System.Linq;

namespace AnimalQuiz
{
    public class AdministratorEntitateMemorie
    {
        private List<Question> questions = new List<Question>();

        
        public void Add(Question q)
        {
            questions.Add(q);
        }

        
        public List<Question> GetAll()
        {
            return questions;
        }

        // Căutare LINQ
        public List<Question> GetByAnimal(AnimalType animal)
        {
            return questions
                .Where(q => q.Animal == animal)
                .ToList();
        }
    }
}
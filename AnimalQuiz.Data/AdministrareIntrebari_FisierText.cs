using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnimalQuiz.Models;

namespace AnimalQuiz.Data
{
    public class AdministrareIntrebari_FisierText
    {
        private const char SEPARATOR = ';';
        private readonly string numeFisier;

        public AdministrareIntrebari_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;

            if (!File.Exists(numeFisier))
            {
                File.Create(numeFisier).Close();
            }
        }

        public void AddQuestion(Question intrebare)
        {
            using (StreamWriter writer = new StreamWriter(numeFisier, true))
            {
                string linieFisier =
                    $"{intrebare.SoundPath}{SEPARATOR}" +
                    $"{intrebare.Animal}{SEPARATOR}" +
                    $"{intrebare.Level}";

                writer.WriteLine(linieFisier);
            }
        }

        public List<Question> GetAllQuestions()
        {
            List<Question> intrebari = new List<Question>();

            using (StreamReader reader = new StreamReader(numeFisier))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] parti = line.Split(SEPARATOR);

                    Question q = new Question();

                    q.SoundPath = parti[0];

                    q.Animal =
                        (AnimalType)Enum.Parse(
                            typeof(AnimalType),
                            parti[1]);

                    q.Level =
                        (Difficulty)Enum.Parse(
                            typeof(Difficulty),
                            parti[2]);

                    intrebari.Add(q);
                }
            }

            return intrebari;
        }

        public List<Question> SearchByAnimal(AnimalType animal)
        {
            return GetAllQuestions()
                .Where(q => q.Animal == animal)
                .ToList();
        }

        public bool ModifyQuestion(string soundPathCautat, Question intrebareNoua)
        {
            List<Question> intrebari = GetAllQuestions();

            bool gasit = false;

            for (int i = 0; i < intrebari.Count; i++)
            {
                if (intrebari[i].SoundPath == soundPathCautat)
                {
                    intrebari[i] = intrebareNoua;
                    gasit = true;
                    break;
                }
            }

            if (gasit)
            {
                using (StreamWriter writer =
                    new StreamWriter(numeFisier, false))
                {
                    foreach (Question q in intrebari)
                    {
                        writer.WriteLine(
                            $"{q.SoundPath}{SEPARATOR}" +
                            $"{q.Animal}{SEPARATOR}" +
                            $"{q.Level}");
                    }
                }
            }

            return gasit;
        }
    }
}
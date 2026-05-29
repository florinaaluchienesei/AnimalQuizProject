using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnimalQuiz.Models;

namespace AnimalQuiz.Data
{
    public class AdministrareRecompense_FisierText
    {
        private const char SEPARATOR = ';';
        private readonly string numeFisier;

        public AdministrareRecompense_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;

            if (!File.Exists(numeFisier))
            {
                File.Create(numeFisier).Close();
            }
        }

        public void AddReward(Reward recompensa)
        {
            using (StreamWriter writer = new StreamWriter(numeFisier, true))
            {
                writer.WriteLine(
                    $"{recompensa.Name}{SEPARATOR}" +
                    $"{recompensa.RewardPath}{SEPARATOR}" +
                    $"{recompensa.MinimumCorrectAnswers}");
            }
        }

        public List<Reward> GetAllRewards()
        {
            List<Reward> recompense = new List<Reward>();

            using (StreamReader reader = new StreamReader(numeFisier))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] parti = line.Split(SEPARATOR);

                    Reward r = new Reward
                    {
                        Name = parti[0],
                        RewardPath = parti[1],
                        MinimumCorrectAnswers = int.Parse(parti[2])
                    };

                    recompense.Add(r);
                }
            }

            return recompense;
        }

        public List<Reward> SearchByMinimumScore(int punctajMinim)
        {
            return GetAllRewards()
                .Where(r => r.MinimumCorrectAnswers >= punctajMinim)
                .ToList();
        }

        public bool ModifyReward(string numeCautat, Reward recompensaNoua)
        {
            List<Reward> recompense = GetAllRewards();
            bool gasit = false;

            for (int i = 0; i < recompense.Count; i++)
            {
                if (recompense[i].Name == numeCautat)
                {
                    recompense[i] = recompensaNoua;
                    gasit = true;
                    break;
                }
            }

            if (gasit)
            {
                using (StreamWriter writer = new StreamWriter(numeFisier, false))
                {
                    foreach (Reward r in recompense)
                    {
                        writer.WriteLine(
                            $"{r.Name}{SEPARATOR}" +
                            $"{r.RewardPath}{SEPARATOR}" +
                            $"{r.MinimumCorrectAnswers}");
                    }
                }
            }

            return gasit;
        }
    }
}
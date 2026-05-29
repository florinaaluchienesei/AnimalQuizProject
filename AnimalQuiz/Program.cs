using System;
using System.Collections.Generic;
using AnimalQuiz.Data;
using AnimalQuiz.Models;

namespace AnimalQuiz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AdministrareIntrebari_FisierText adminIntrebari =
                new AdministrareIntrebari_FisierText("intrebari.txt");

            AdministrareRecompense_FisierText adminRecompense =
                new AdministrareRecompense_FisierText("recompense.txt");

            bool ruleaza = true;

            while (ruleaza)
            {
                Console.WriteLine("\n===== MENIU =====");
                Console.WriteLine("1. Adauga intrebare");
                Console.WriteLine("2. Afiseaza intrebari");
                Console.WriteLine("3. Cauta intrebare dupa animal");
                Console.WriteLine("4. Modifica intrebare");
                Console.WriteLine("5. Adauga recompensa");
                Console.WriteLine("6. Afiseaza recompense");
                Console.WriteLine("7. Cauta recompensa dupa punctaj minim");
                Console.WriteLine("8. Modifica recompensa");
                Console.WriteLine("0. Iesire");

                Console.Write("Alege optiunea: ");
                string optiune = Console.ReadLine();

                switch (optiune)
                {
                    case "1":
                        AddQuestion(adminIntrebari);
                        break;
                    case "2":
                        ShowQuestions(adminIntrebari);
                        break;
                    case "3":
                        SearchQuestion(adminIntrebari);
                        break;
                    case "4":
                        ModifyQuestion(adminIntrebari);
                        break;
                    case "5":
                        AddReward(adminRecompense);
                        break;
                    case "6":
                        ShowRewards(adminRecompense);
                        break;
                    case "7":
                        SearchReward(adminRecompense);
                        break;
                    case "8":
                        ModifyReward(adminRecompense);
                        break;
                    case "0":
                        ruleaza = false;
                        break;
                    default:
                        Console.WriteLine("Optiune invalida.");
                        break;
                }
            }
        }

        static void AddQuestion(AdministrareIntrebari_FisierText admin)
        {
            Question q = new Question();

            Console.Write("SoundPath: ");
            q.SoundPath = Console.ReadLine();

            Console.Write("Animal (Pisica, Caine, Leu, Elefant): ");
            q.Animal = (AnimalType)Enum.Parse(typeof(AnimalType), Console.ReadLine());

            Console.Write("Difficulty (Usor, Mediu, Greu): ");
            q.Level = (Difficulty)Enum.Parse(typeof(Difficulty), Console.ReadLine());

            admin.AddQuestion(q);

            Console.WriteLine("Intrebarea a fost salvata in fisier.");
        }

        static void ShowQuestions(AdministrareIntrebari_FisierText admin)
        {
            List<Question> intrebari = admin.GetAllQuestions();

            foreach (var q in intrebari)
            {
                Console.WriteLine($"{q.SoundPath} | {q.Animal} | {q.Level}");
            }
        }

        static void SearchQuestion(AdministrareIntrebari_FisierText admin)
        {
            Console.Write("Animal cautat (Pisica, Caine, Leu, Elefant): ");
            AnimalType animal = (AnimalType)Enum.Parse(typeof(AnimalType), Console.ReadLine());

            List<Question> rezultate = admin.SearchByAnimal(animal);

            if (rezultate.Count == 0)
            {
                Console.WriteLine("Nu exista intrebari pentru acest animal.");
                return;
            }

            foreach (var q in rezultate)
            {
                Console.WriteLine($"{q.SoundPath} | {q.Animal} | {q.Level}");
            }
        }

        static void ModifyQuestion(AdministrareIntrebari_FisierText admin)
        {
            Console.Write("Introdu SoundPath-ul intrebarii de modificat: ");
            string soundCautat = Console.ReadLine();

            Question qNoua = new Question();

            Console.Write("Noul SoundPath: ");
            qNoua.SoundPath = Console.ReadLine();

            Console.Write("Animal nou (Pisica, Caine, Leu, Elefant): ");
            qNoua.Animal = (AnimalType)Enum.Parse(typeof(AnimalType), Console.ReadLine());

            Console.Write("Difficulty nou (Usor, Mediu, Greu): ");
            qNoua.Level = (Difficulty)Enum.Parse(typeof(Difficulty), Console.ReadLine());

            bool modificat = admin.ModifyQuestion(soundCautat, qNoua);

            if (modificat)
                Console.WriteLine("Intrebarea a fost modificata.");
            else
                Console.WriteLine("Intrebarea nu a fost gasita.");
        }

        static void AddReward(AdministrareRecompense_FisierText admin)
        {
            Reward r = new Reward();

            Console.Write("Nume recompensa: ");
            r.Name = Console.ReadLine();

            Console.Write("Cale recompensa: ");
            r.RewardPath = Console.ReadLine();

            Console.Write("Numar minim raspunsuri corecte: ");
            r.MinimumCorrectAnswers = int.Parse(Console.ReadLine());

            admin.AddReward(r);

            Console.WriteLine("Recompensa a fost salvata in fisier.");
        }

        static void ShowRewards(AdministrareRecompense_FisierText admin)
        {
            List<Reward> recompense = admin.GetAllRewards();

            foreach (var r in recompense)
            {
                Console.WriteLine($"{r.Name} | {r.RewardPath} | minim: {r.MinimumCorrectAnswers}");
            }
        }

        static void SearchReward(AdministrareRecompense_FisierText admin)
        {
            Console.Write("Punctaj minim cautat: ");
            int punctaj = int.Parse(Console.ReadLine());

            List<Reward> rezultate = admin.SearchByMinimumScore(punctaj);

            if (rezultate.Count == 0)
            {
                Console.WriteLine("Nu exista recompense gasite.");
                return;
            }

            foreach (var r in rezultate)
            {
                Console.WriteLine($"{r.Name} | {r.RewardPath} | minim: {r.MinimumCorrectAnswers}");
            }
        }

        static void ModifyReward(AdministrareRecompense_FisierText admin)
        {
            Console.Write("Numele recompensei de modificat: ");
            string numeCautat = Console.ReadLine();

            Reward rNoua = new Reward();

            Console.Write("Nume nou recompensa: ");
            rNoua.Name = Console.ReadLine();

            Console.Write("Cale noua recompensa: ");
            rNoua.RewardPath = Console.ReadLine();

            Console.Write("Numar minim raspunsuri corecte nou: ");
            rNoua.MinimumCorrectAnswers = int.Parse(Console.ReadLine());

            bool modificat = admin.ModifyReward(numeCautat, rNoua);

            if (modificat)
                Console.WriteLine("Recompensa a fost modificata.");
            else
                Console.WriteLine("Recompensa nu a fost gasita.");
        }
    }
}
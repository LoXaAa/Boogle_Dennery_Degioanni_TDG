using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boogle_Dennery_Degioanni_TDG
{
    internal class Joueur
    {
        #region Attributs
        public string Nom { get; private set; } // Nom du joueur
        public int Score { get; private set; }  // Score total du joueur
        private List<string> MotsTrouves { get; set; } // Liste des mots trouvés par le joueur
        #endregion

        #region Constructeur
        /// <summary>
        /// Initialise un joueur avec un nom.
        /// </summary>
        /// <param name="nom">Nom du joueur.</param>
        /// <exception cref="ArgumentException">Si le nom est null ou vide.</exception>
        public Joueur(string nom)
        {
            if (string.IsNullOrWhiteSpace(nom))
            {
                throw new ArgumentException("Un joueur doit avoir un nom.");
            }

            Nom = nom;
            Score = 0;
            MotsTrouves = new List<string>();
        }
        #endregion

        #region Gestion des mots trouvés
        /// <summary>
        /// Vérifie si un mot a déjà été trouvé par le joueur.
        /// </summary>
        /// <param name="mot">Mot à vérifier.</param>
        /// <returns>Vrai si le mot a déjà été trouvé, faux sinon.</returns>
        public bool Contain(string mot)
        {
            return MotsTrouves.Contains(mot.ToUpper());
        }

        /// <summary>
        /// Ajoute un mot trouvé par le joueur et met à jour son score.
        /// </summary>
        /// <param name="mot">Mot trouvé.</param>
        public void Add_Mot(string mot)
        {
            mot = mot.ToUpper();

            if (!Contain(mot))
            {
                MotsTrouves.Add(mot);
                int scoreMot = CalculerScoreMot(mot);
                Score += scoreMot;
            }
            else
            {
                Console.WriteLine($"Le mot '{mot}' a déjà été trouvé.");
            }
        }

        #endregion

        #region Calcul du score
        /// <summary>
        /// Calcule le score d'un mot selon les règles du Scrabble.
        /// </summary>
        /// <param name="mot">Mot dont on veut calculer le score.</param>
        /// <returns>Score du mot.</returns>
        private int CalculerScoreMot(string mot)
        {
            Dictionary<char, int> valeursLettres = new Dictionary<char, int>
            {
                {'A', 1}, {'B', 3}, {'C', 3}, {'D', 2}, {'E', 1}, {'F', 4}, {'G', 2}, {'H', 4}, {'I', 1}, {'J', 8},
                {'K', 5}, {'L', 1}, {'M', 3}, {'N', 1}, {'O', 1}, {'P', 3}, {'Q', 10}, {'R', 1}, {'S', 1}, {'T', 1},
                {'U', 1}, {'V', 4}, {'W', 4}, {'X', 8}, {'Y', 4}, {'Z', 10}
            };

            int score = 0;

            foreach (char lettre in mot)
            {
                if (valeursLettres.ContainsKey(lettre))
                {
                    score += valeursLettres[lettre];
                }
                else
                {
                    throw new ArgumentException($"Le mot contient un caractère invalide : {lettre}");
                }
            }

            return score;
        }
        #endregion

        #region Affichage
        /// <summary>
        /// Retourne une description complète du joueur.
        /// </summary>
        /// <returns>Description du joueur.</returns>
        public override string ToString()
        {
            string description = $"Joueur: {Nom}\nScore: {Score}\nMots trouvés:\n";
            foreach (var mot in MotsTrouves)
            {
                description += $"  - {mot}\n";
            }
            return description;
        }
        #endregion

    }
}
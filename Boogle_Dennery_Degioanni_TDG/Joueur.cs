using System;
using System.Collections.Generic;
using System.Linq;

namespace Boogle_Dennery_Degioanni_TDG
{
    public class Joueur
    {
        #region Attributs
        public string Nom { get; private set; } 
        public int Score { get; private set; }  
        private List<string> motsTrouves;       
        private string langue;                 
        #endregion

        #region Constructeur
        public Joueur(string nom, string langue)
        {
            if (string.IsNullOrWhiteSpace(nom))
            {
                throw new ArgumentException("Un joueur doit avoir un nom.");
            }

            if (langue != "FR" && langue != "EN")
            {
                throw new ArgumentException("Langue invalide. Choisissez 'FR' ou 'EN'.");
            }

            Nom = nom;
            this.langue = langue;
            Score = 0;
            motsTrouves = new List<string>();
        }
        #endregion

        #region Gestion des mots trouvés
        /// <summary>
        /// Vérifie si un mot a déjà été trouvé par le joueur
        /// </summary>
        /// <param name="mot">Mot à vérifier</param>
        /// <returns>True si le mot a déjà été trouvé, false sinon</returns>
        public bool Contain(string mot)
        {
            return motsTrouves.Contains(mot.ToUpper());
        }

        /// <summary>
        /// Ajoute un mot trouvé par le joueur et update son score
        /// </summary>
        /// <param name="mot">Mot trouvé</param>
        public void AddMot(string mot)
        {
            mot = mot.ToUpper();

            if (!Contain(mot))
            {
                motsTrouves.Add(mot);
                int scoreMot = CalculerScoreMot(mot, langue);
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
        /// Calcul du score d'un mot basé sur le sccrable
        /// </summary>
        /// <param name="mot">Mot dont on veut calculer le score</param>
        /// <param name="langue">Langue du jeu</param>
        /// <returns>Score du mot</returns>
        public int CalculerScoreMot(string mot, string langue)
        {
            Dictionary<char, int> valeursLettres = langue == "FR" ? new Dictionary<char, int>
            {
                {'A', 1}, {'B', 3}, {'C', 3}, {'D', 2}, {'E', 1}, {'F', 4}, {'G', 2}, {'H', 4}, {'I', 1}, {'J', 8},
                {'K', 10}, {'L', 1}, {'M', 2}, {'N', 1}, {'O', 1}, {'P', 3}, {'Q', 8}, {'R', 1}, {'S', 1}, {'T', 1},
                {'U', 1}, {'V', 4}, {'W', 10}, {'X', 10}, {'Y', 10}, {'Z', 10}
            } : new Dictionary<char, int>
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

        /// <summary>
        /// Retourne la liste des mots trouvés par le joueur
        /// </summary>
        /// <returns>Liste des mots trouvés</returns>
        public IReadOnlyList<string> GetMotsTrouves()
        {
            return motsTrouves.AsReadOnly();
        }

        /// <summary>
        /// Retourne le score du mot trouvé
        /// </summary>
        /// <param name="mot">Mot dont on veut connaître le score</param>
        /// <returns>Score du mot</returns>
        public int GetScoreMot(string mot)
        {
            return CalculerScoreMot(mot, langue);
        }
        #endregion

        #region Affichage
        /// <summary>
        /// Affiche infos joueur
        /// </summary>
        /// <returns>Description du joueur</returns>
        public override string ToString()
        {
            string description = $"Joueur: {Nom}\nScore: {Score}\nMots trouvés:\n";
            foreach (var mot in motsTrouves)
            {
                description += $"  - {mot}\n";
            }
            return description;
        }
        #endregion
    }
}

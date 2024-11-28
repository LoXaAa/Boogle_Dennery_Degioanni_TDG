using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boogle_Dennery_Degioanni_TDG.Plateau;

namespace Boogle_Dennery_Degioanni_TDG
{
    /// <summary>
    /// Représente un dé utilisé dans le jeu Boggle, avec 6 faces contenant des lettres.
    /// </summary>
    internal class De
    {
        private char[] faces;       // Les lettres sur les faces du dé
        private char lettreVisible; // La lettre visible après un lancer

        /// <summary>
        /// Propriété pour obtenir uniquement la lettre visible du dé.
        /// </summary>
        public char LettreVisible
        {
            get { return lettreVisible; }
        }

        /// <summary>
        /// Constructeur par défaut.
        /// Génère un dé avec 6 lettres uniques aléatoires.
        /// </summary>
        /// <param name="random">Instance de Random pour générer des lettres aléatoires.</param>
        public De(Random random)
        {
            // Générer les lettres uniques aléatoires
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            faces = alphabet.OrderBy(x => random.Next()).Take(6).ToArray();

            // Initialiser la lettre visible avec la première face
            lettreVisible = faces[0];
        }

        /// <summary>
        /// Simule un lancer du dé pour obtenir une lettre visible au hasard.
        /// </summary>
        /// <param name="random">Instance de Random pour générer des valeurs aléatoires.</param>
        public void Lance(Random random)
        {
            lettreVisible = faces[random.Next(6)];
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boogle_Dennery_Degioanni_TDG
{
    /// <summary>
    /// Classe De qui génère des dé à 6 faces aléatoires selon les règles d'occurence et renvoie une faces visible aléatoire
    /// </summary>
    internal class De
    {
        #region Attributs de la classe De
        private char[] faces;       // Toutes les faces
        private char lettreVisible; // La face visble
        #endregion
        #region Obtenir la lettre visible
        /// <summary>
        /// Propriété pour obtenir uniquement la lettre visible du dé.
        /// </summary>
        public char LettreVisible
        {
            get { return lettreVisible; }
        }
        #endregion
        #region Génération du dé
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
        #endregion
        #region Lancé du dé 
        /// <summary>
        /// Simule un lancer du dé pour obtenir une lettre visible au hasard.
        /// </summary>
        /// <param name="random">Instance de Random pour générer des valeurs aléatoires.</param>
        public void Lance(Random random)
        {
            lettreVisible = faces[random.Next(6)];
        }
        #endregion
    }
}


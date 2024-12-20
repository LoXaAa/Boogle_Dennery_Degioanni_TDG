using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boogle_Dennery_Degioanni_TDG.Plateau;

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
            get => lettreVisible;
        }
        #endregion
        #region Constructeur qui initialise un dé
        /// <summary>
        /// Constructeur qui initialise un dé aléatoirement en se basant sur la liste des lettres disponibles
        /// </summary>
        public De(List<char> lettresDisponibles)
        {
            Random random = new Random();
            faces = new char[6];
            for (int i = 0; i < 6; i++)
            {
                int index = random.Next(lettresDisponibles.Count);
                faces[i] = lettresDisponibles[index];
                lettresDisponibles.RemoveAt(index); 
            }

            lettreVisible = faces[0];
        }
        #endregion
        #region Méthode qui lance le dé
        /// <summary>
        /// Méthode qui lance le dé aléatoirement (dé non pipé à 6 faces)
        /// </summary>
        public void Lance(Random r)
        {
            int indexAleatoire = r.Next(0, faces.Length);
            lettreVisible = faces[indexAleatoire];
        }
        #endregion
        #region Méthode qui génère et stock les dés lancés
        /// <summary>
        /// Méthode qui génère et stock dans une liste tous les dés lancés
        /// </summary>
        public static List<De> CreerDes(int nombreDe, List<char> lettresDisponibles)
        {
            List<De> des = new List<De>();

            for (int i = 0; i < nombreDe; i++)
            {
                des.Add(new De(lettresDisponibles));
            }

            return des;
        }
        #endregion

    }
}

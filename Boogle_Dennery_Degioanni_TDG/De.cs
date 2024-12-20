using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boogle_Dennery_Degioanni_TDG.Plateau;

namespace Boogle_Dennery_Degioanni_TDG
{
    internal class De
    {
        #region Attributs de la classe De
        private char[] faces;       
        private char lettreVisible;
        #endregion
        #region Obtenir la lettre visible
        public char LettreVisible
        {
            get => lettreVisible;
        }
        #endregion
        #region Constructeur qui initialise un dé

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
        /// <param name="r">une valeur aléatoire.</param>
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
        /// <param name="nombreDe">le nombre de dés à générer</param>
        /// <param name="lettresDisponibles">Banque de lettres utilisée pour générer les faces des dés</param>
        /// <returns name="des"> La liste de tous les dés générés</returns>
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

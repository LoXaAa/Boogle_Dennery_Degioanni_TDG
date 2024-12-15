using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boogle_Dennery_Degioanni_TDG.De;

namespace Boogle_Dennery_Degioanni_TDG
{
    /// <summary>
    /// Génère et affiche le tableau du jeu grâce aux dés générés aléatoirement
    /// </summary>
    internal class Plateau
    {
        #region Attribut classe plateau
        private De[,] matrice; // La matrice 4x4 de dés
        #endregion
        #region Création du plateau
        /// <summary>
        /// Constructeur du plateau.
        /// Génère une matrice 4x4 remplie de dés aléatoires.
        /// </summary>
        /// <param name="random">Instance de Random pour générer des valeurs aléatoires.</param>
        public Plateau(Random random)
        {
            matrice = new De[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    // Générer un nouveau dé pour chaque case
                    matrice[i, j] = new De(random);
                }
            }
        }
        #endregion
        #region Affichage du plateau
        /// <summary>
        /// Récupère les lettres visibles de chaque dé et les affiche sous forme de matrice 4x4.
        /// </summary>
        public void Afficher()
        {
            Console.WriteLine("Plateau 4x4 :");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    // Afficher la lettre visible de chaque dé
                    Console.Write(matrice[i, j].LettreVisible + " ");
                }
                Console.WriteLine();
            }
        }
        #endregion
    }
}

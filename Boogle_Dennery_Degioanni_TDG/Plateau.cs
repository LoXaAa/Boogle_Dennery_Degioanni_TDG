using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boogle_Dennery_Degioanni_TDG.De;

namespace Boogle_Dennery_Degioanni_TDG
{
    public class Plateau
    {
        #region Attributs classe plateau
        private List<De> des; 
        private int taille;   
        #endregion

        #region Constructeur
        public Plateau(int nombreDe, int taillePlateau)
        {
            taille = taillePlateau;
            des = new List<De>();


            string lettresDisponibles = "AAAAAAAAABBCCDDDEEEEEEEEEEFFGGHHIIIIIIIIJKLLLLLMMMNNNNNNOOOOOOPPQRRRRRRSSSSSSTTTTTTUUUUUUVVWXYZ";
            while (lettresDisponibles.Length < nombreDe * 6)
            {
                lettresDisponibles += lettresDisponibles;
            }

            List<char> listeLettres = new List<char>(lettresDisponibles);

            des = De.CreerDes(nombreDe, listeLettres);
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Affiche le plateau
        /// </summary>
        public void AfficherPlateau()
        {
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    int index = i * taille + j;
                    Console.Write(des[index].LettreVisible + " ");
                }
                Console.WriteLine();
            }
        }

        public void RelanceDes(Random r)
        {
            foreach (var De in des)
            {
                De.Lance(r);
            }
        }

        /// <summary>
        /// Vérifie si un mot peut être formé sur le plateau
        /// </summary>
        /// <param name="mot">Le mot à vérifier</param>
        /// <returns>Vrai si le mot est valide, faux sinon</returns>
        public bool VerifierMot(string mot)
        {
            if (string.IsNullOrWhiteSpace(mot) || mot.Length <= 1)
            {
                throw new ArgumentException("Le mot doit contenir au moins deux lettres.");
            }

            mot = mot.ToUpper(); 

            bool[,] visited = new bool[taille, taille]; 
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    if (des[i * taille + j].LettreVisible == mot[0]) 
                    {
                        if (VerifierMotRecursif(mot, 0, i, j, visited))
                        {
                            return true;
                        }
                    }
                }
            }

            return false; 
        }

        /// <summary>
        /// Vérification de la formation du mot à partir d'une position
        /// </summary>
        /// <param name="mot">mot cible</param>
        /// <param name="index">index du mot recherché</param>
        /// <param name="x">position x</param>
        /// <param name="y">position y</param>
        /// <param name="visited">état de la case (parcourue ou non)</param>
        /// <returns>Vrai si le mot est trouvé, false sinon</returns>
        private bool VerifierMotRecursif(string mot, int index, int x, int y, bool[,] visited)
        {
            if (index == mot.Length)
            {
                return true;
            }
            if (x < 0 || x >= taille || y < 0 || y >= taille)
            {
                return false;
            }

            if (visited[x, y] || des[x * taille + y].LettreVisible != mot[index])
            {
                return false;
            }

            visited[x, y] = true;

            int[] directionsX = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] directionsY = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int d = 0; d < 8; d++)
            {
                if (VerifierMotRecursif(mot, index + 1, x + directionsX[d], y + directionsY[d], visited))
                {
                    return true;
                }
            }
            visited[x, y] = false;
            return false;
        }
        #endregion
    }
}

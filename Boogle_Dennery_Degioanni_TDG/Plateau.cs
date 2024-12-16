using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Boogle_Dennery_Degioanni_TDG.De;

namespace Boogle_Dennery_Degioanni_TDG
{
    /// <summary>
    /// Génère et affiche le tableau du jeu grâce aux dés générés aléatoirement.
    /// </summary>
    public class Plateau
    {
        #region Attributs classe plateau
        private List<De> des; // Liste des dés utilisés pour le plateau
        private int taille;   // Taille du plateau (taille x taille)
        #endregion

        #region Constructeur
        /// <summary>
        /// Initialise un plateau de jeu avec les dés nécessaires.
        /// </summary>
        /// <param name="nombreDe">Nombre de dés à créer.</param>
        /// <param name="taillePlateau">Taille du plateau (par exemple, 4 pour un plateau 4x4).</param>
        /// <exception cref="ArgumentException">Si le plateau n'est pas carré ou si les paramètres sont invalides.</exception>
        public Plateau(int nombreDe, int taillePlateau)
        {
            if (taillePlateau * taillePlateau != nombreDe)
            {
                throw new ArgumentException("Le nombre de dés doit correspondre à la taille du plateau au carré.");
            }

            taille = taillePlateau;
            des = new List<De>();

            // Générer les lettres disponibles
            string lettresDisponibles = "AAAAAAAAABBCCDDDEEEEEEEEEEFFGGHHIIIIIIIIJKLLLLLMMMNNNNNNOOOOOOPPQRRRRRRSSSSSSTTTTTTUUUUUUVVWXYZ";
            while (lettresDisponibles.Length < nombreDe * 6)
            {
                lettresDisponibles += lettresDisponibles; // Étend les lettres disponibles si nécessaire
            }

            List<char> listeLettres = new List<char>(lettresDisponibles);

            // Créer les dés via la classe De
            des = De.CreerDes(nombreDe, listeLettres);
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Affiche le plateau de jeu en grille.
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

        /// <summary>
        /// Vérifie si un mot peut être formé sur le plateau.
        /// </summary>
        /// <param name="mot">Le mot à vérifier.</param>
        /// <returns>Vrai si le mot est valide, faux sinon.</returns>
        public bool VerifierMot(string mot)
        {
            if (string.IsNullOrWhiteSpace(mot) || mot.Length <= 1)
            {
                throw new ArgumentException("Le mot doit contenir au moins deux lettres.");
            }

            mot = mot.ToUpper(); // Convertir en majuscules pour correspondre aux lettres du plateau

            bool[,] visited = new bool[taille, taille]; // Marque les cases déjà visitées

            // Parcourt chaque case pour trouver un point de départ
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    if (des[i * taille + j].LettreVisible == mot[0]) // Si la première lettre correspond
                    {
                        if (VerifierMotRecursif(mot, 0, i, j, visited))
                        {
                            return true;
                        }
                    }
                }
            }

            return false; // Aucun chemin valide trouvé
        }

        /// <summary>
        /// Méthode récursive pour vérifier si le mot peut être formé à partir d'une position donnée.
        /// </summary>
        private bool VerifierMotRecursif(string mot, int index, int x, int y, bool[,] visited)
        {
            // Cas de base : si tout le mot est trouvé
            if (index == mot.Length)
            {
                return true;
            }

            // Vérifier les limites de la grille
            if (x < 0 || x >= taille || y < 0 || y >= taille)
            {
                return false;
            }

            // Vérifier si la case est déjà visitée ou si la lettre ne correspond pas
            if (visited[x, y] || des[x * taille + y].LettreVisible != mot[index])
            {
                return false;
            }

            // Marquer la case comme visitée
            visited[x, y] = true;

            // Vérifier les 8 directions autour de la case actuelle
            int[] directionsX = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] directionsY = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int d = 0; d < 8; d++)
            {
                if (VerifierMotRecursif(mot, index + 1, x + directionsX[d], y + directionsY[d], visited))
                {
                    return true;
                }
            }

            // Défaire la marque (backtracking)
            visited[x, y] = false;
            return false;
        }
        #endregion
    }
}

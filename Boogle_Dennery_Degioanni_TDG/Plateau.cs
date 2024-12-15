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
    public class Plateau
    {
        private List<De> des; // Liste des dés utilisés pour le plateau
        private int taille; // Taille du plateau (taille x taille)

        // Constructeur qui initialise le plateau
        public Plateau(int nombreDe, int taillePlateau)
        {
            taille = taillePlateau;
            des = new List<De>();

            // Lettres disponibles pour les dés
            string lettresDisponibles = "AAAAAAAAABBCCDDDEEEEEEEEEEFFGGHHIIIIIIIIJKLLLLLMMMNNNNNNOOOOOOPPQRRRRRRSSSSSSTTTTTTUUUUUUVVWXYZ";
            List<char> listeLettres = new List<char>(lettresDisponibles);

            // Créer les dés via la classe De
            des = De.CreerDes(nombreDe, listeLettres);
        }

        // Méthode pour afficher le plateau de jeu en carré
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
    }

}
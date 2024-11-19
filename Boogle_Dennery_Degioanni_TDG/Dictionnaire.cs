using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Boogle_Dennery_Degioanni_TDG
{
    internal class Dictionnaire
    {
        private string[] mots;  // Tableau des mots valides
        private string langue;  // Langue du dictionnaire ("FR" ou "EN")

        // Constructeur
        public Dictionnaire(string langue)
        {
            if (langue != "FR" && langue != "EN")
            {
                throw new ArgumentException("Langue invalide. Utilisez 'FR' ou 'EN'.");
            }

            this.langue = langue;
            ChargerMots();
        }

        // Méthode pour charger les mots
        private void ChargerMots()
        {
            string fichier = langue == "FR" ? "MotsPossiblesFR.txt" : "MotsPossiblesEN.txt";
            mots = FichierGestion.ChargerEtNormaliser(fichier);

            // Trier les mots pour permettre une recherche dichotomique
            Array.Sort(mots);
        }

        public override string ToString()
        {
            return $"Dictionnaire ({langue}) - Nombre de mots : {mots.Length}";
        }
    }
}

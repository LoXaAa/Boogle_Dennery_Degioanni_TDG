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

        public bool RechDichoRecursif(string mot)
        {
            mot = mot.ToUpper();
            return RechercheDichotomique(mot, 0, mots.Length - 1);
        }

        private bool RechercheDichotomique(string mot, int debut, int fin)
        {
            if (debut > fin) return false;

            int milieu = (debut + fin) / 2;
            int comparaison = string.Compare(mot, mots[milieu], StringComparison.Ordinal);

            if (comparaison == 0) return true;
            else if (comparaison < 0) return RechercheDichotomique(mot, debut, milieu - 1);
            else return RechercheDichotomique(mot, milieu + 1, fin);
        }

        public override string ToString()
        {
            return $"Dictionnaire ({langue}) - Nombre de mots : {mots.Length}";
        }
    }
}

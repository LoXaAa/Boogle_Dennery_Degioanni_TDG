using System;

namespace Boogle_Dennery_Degioanni_TDG
{
    internal class Dictionnaire
    {
        private string[] mots;

        
        private string langue;

        public string[] Mots => mots;

        public Dictionnaire(string langue)
        {
            if (langue != "FR" && langue != "EN")
            {
                throw new ArgumentException("Langue invalide. Choisissez 'FR' ou 'EN'.");
            }

            this.langue = langue;
            ChargerMots();
        }

        /// <summary>
        /// Charge les mots depuis un fichier, puis les trie.
        /// </summary>
        private void ChargerMots()
        {
            string fichier = langue == "FR" ? "MotsPossiblesFR_Valide.txt" : "MotsPossiblesEN_Valide.txt";

            try
            {
                mots = FichierGestion.ChargerEtNormaliser(fichier);

                if (mots.Length == 0)
                {
                    mots = Array.Empty<string>();
                    return;
                }

                mots = TrierFusion(mots);
            }
            catch (FileNotFoundException)
            {
                mots = Array.Empty<string>();
            }
        }

        /// <summary>
        /// Trie un tableau de chaînes en utilisant un tri fusion.
        /// </summary>
        /// <param name="tableau">Le tableau à trier.</param>
        /// <returns>Le tableau trié.</returns>
        private string[] TrierFusion(string[] tableau)
        {
            if (tableau.Length <= 1) return tableau;

            int milieu = tableau.Length / 2;
            string[] gauche = new string[milieu];
            string[] droite = new string[tableau.Length - milieu];

            Array.Copy(tableau, 0, gauche, 0, milieu);
            Array.Copy(tableau, milieu, droite, 0, tableau.Length - milieu);

            gauche = TrierFusion(gauche);
            droite = TrierFusion(droite);

            return Fusionner(gauche, droite);
        }

        /// <summary>
        /// Fusionne deux tableaux triés en un seul tableau trié.
        /// </summary>
        /// <param name="gauche">Le tableau gauche trié.</param>
        /// <param name="droite">Le tableau droit trié.</param>
        /// <returns>Un tableau fusionné et trié.</returns>
        private string[] Fusionner(string[] gauche, string[] droite)
        {
            string[] resultat = new string[gauche.Length + droite.Length];
            int i = 0, j = 0, k = 0;

            while (i < gauche.Length && j < droite.Length)
            {
                if (string.Compare(gauche[i], droite[j], StringComparison.Ordinal) <= 0)
                {
                    resultat[k++] = gauche[i++];
                }
                else
                {
                    resultat[k++] = droite[j++];
                }
            }

            while (i < gauche.Length)
            {
                resultat[k++] = gauche[i++];
            }

            while (j < droite.Length)
            {
                resultat[k++] = droite[j++];
            }

            return resultat;
        }


        /// <summary>
        /// Recherche un mot dans le dictionnaire en utilisant une recherche dichotomique.
        /// </summary>
        /// <param name="mot">Le mot à rechercher.</param>
        /// <returns>True si le mot est trouvé, false sinon.</returns>
        public bool RechDicho(string mot)
        {
            mot = mot.ToUpper();
            return RechercheDichotomique(mot, 0, mots.Length - 1);
        }

        /// <summary>
        /// Recherche dichotomique récursive.
        /// </summary>
        /// <param name="mot">Le mot à rechercher.</param>
        /// <param name="debut">Index de début.</param>
        /// <param name="fin">Index de fin.</param>
        /// <returns>True si le mot est trouvé, false sinon.</returns>
        private bool RechercheDichotomique(string mot, int debut, int fin)
        {
            if (debut > fin) return false;

            int milieu = (debut + fin) / 2;
            int comparaison = string.Compare(mot, mots[milieu], StringComparison.Ordinal);

            if (comparaison == 0) return true;
            else if (comparaison < 0)
                return RechercheDichotomique(mot, debut, milieu - 1);
            else
                return RechercheDichotomique(mot, milieu + 1, fin);
        }

        /// <summary>
        /// Retourne une description de l'état du dictionnaire.
        /// </summary>
        /// <returns>Une chaîne décrivant la langue et le nombre de mots du dictionnaire.</returns>
        public override string ToString()
        {
            return $"Dictionnaire ({langue}) - Nombre de mots : {mots.Length}";
        }
    }
}

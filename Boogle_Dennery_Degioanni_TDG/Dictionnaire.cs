using System;

namespace Boogle_Dennery_Degioanni_TDG
{
    internal class Dictionnaire
    {
        private string[] mots;  // Tableau des mots valides
        private string langue;  // Langue du dictionnaire ("FR" ou "EN")

        /// <summary>
        /// Propriété pour accéder aux mots chargés.
        /// </summary>
        public string[] Mots => mots;

        /// <summary>
        /// Constructeur du dictionnaire.
        /// </summary>
        /// <param name="langue">Langue ("FR" ou "EN")</param>
        public Dictionnaire(string langue)
        {
            if (langue != "FR" && langue != "EN")
            {
                throw new ArgumentException("Langue invalide. Utilisez 'FR' ou 'EN'.");
            }

            this.langue = langue;
            ChargerMots();
        }

        /// <summary>
        /// Charge et trie les mots en fonction de la langue.
        /// </summary>
        private void ChargerMots()
        {
            string fichier = langue == "FR" ? "MotsPossiblesFR_Valide.txt" : "MotsPossiblesEN_Valide.txt";

            try
            {
                mots = FichierGestion.ChargerEtNormaliser(fichier);

                if (mots.Length == 0)
                {
                    Console.WriteLine("Aucun mot n'a été trouvé dans le fichier.");
                }
                else
                {
                    mots = TrierFusion(mots); // Tri fusion pour garantir l'ordre alphabétique
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
                mots = Array.Empty<string>(); // Initialisation d'un tableau vide en cas d'erreur
            }
        }

        /// <summary>
        /// Méthode de tri fusion.
        /// </summary>
        /// <param name="tableau">Tableau à trier</param>
        /// <returns>Tableau trié</returns>
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
        /// Fusionne deux tableaux triés.
        /// </summary>
        /// <param name="gauche">Tableau gauche trié</param>
        /// <param name="droite">Tableau droit trié</param>
        /// <returns>Tableau fusionné et trié</returns>
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
        /// Recherche un mot dans le dictionnaire (recherche dichotomique).
        /// </summary>
        /// <param name="mot">Mot à rechercher</param>
        /// <returns>True si le mot est trouvé, false sinon</returns>
        public bool RechDicho(string mot)
        {
            mot = mot.ToUpper();
            return RechercheDichotomique(mot, 0, mots.Length - 1);
        }

        /// <summary>
        /// Recherche dichotomique récursive.
        /// </summary>
        /// <param name="mot">Mot à rechercher</param>
        /// <param name="debut">Index de début</param>
        /// <param name="fin">Index de fin</param>
        /// <returns>True si le mot est trouvé, false sinon</returns>
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
        /// Méthode ToString pour afficher des informations sur le dictionnaire.
        /// </summary>
        /// <returns>Description du dictionnaire</returns>
        public override string ToString()
        {
            return $"Dictionnaire ({langue}) - Nombre de mots : {mots.Length}";
        }
    }
}

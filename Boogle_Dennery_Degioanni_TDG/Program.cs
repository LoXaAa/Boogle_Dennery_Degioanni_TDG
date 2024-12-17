using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Boogle_Dennery_Degioanni_TDG
{
    internal class Program
    {
        #region SaisirNombre
        /// <summary>
        /// Méthode pour saisir un nombre entier valide.
        /// </summary>
        /// <returns>Un entier saisi par l'utilisateur.</returns>
        public static int SaisirNombre()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            { }
            return result;
        }
        #endregion

        static void Main(string[] args)
        {
            // Affichage de l'en-tête
            Console.WriteLine(@"     _ _____ _   _   ____  _   _   ____   ___   ___   ____ _     _____ 
    | | ____| | | | |  _ \| | | | | __ ) / _ \ / _ \ / ___| |   | ____|
 _  | |  _| | | | | | | | | | | | |  _ \| | | | | | | |  _| |   |  _|  
| |_| | |___| |_| | | |_| | |_| | | |_) | |_| | |_| | |_| | |___| |___ 
 \___/|_____|\___/  |____/ \___/  |____/ \___/ \___/ \____|_____|_____|");

            Thread.Sleep(2000);
            Console.WriteLine();

            Console.WriteLine(@" ___       _ _   _       _ _           _   _             
|_ _|_ __ (_) |_(_) __ _| (_)___  __ _| |_(_) ___  _ __  
 | || '_ \| | __| |/ _` | | / __|/ _` | __| |/ _ \| '_ \ 
 | || | | | | |_| | (_| | | \__ \ (_| | |_| | (_) | | | |
|___|_| |_|_|\__|_|\__,_|_|_|___/\__,_|\__|_|\___/|_| |_|");

            Thread.Sleep(2000);
            Console.WriteLine();

            string langueJeu = "FR";
            Plateau plateau;

            // Sélection de la langue
            ConsoleKeyInfo cki = new ConsoleKeyInfo();

            do
            {
                Console.WriteLine("Selection de la langue\n"
                    + "Option 1 : Français\n"
                    + "Option 2 : Anglais\n"
                    + "\n"
                    + "Selectionnez 1 ou 2 pour la langue");

                int choixLangue = SaisirNombre();

                switch (choixLangue)
                {
                    case 1:
                        Console.WriteLine("Le jeu va être lancé en français");
                        langueJeu = "FR";
                        break;

                    case 2:
                        Console.WriteLine("Le jeu va être lancé en anglais");
                        langueJeu = "EN";
                        break;

                    default:
                        Console.WriteLine("Choix invalide. Veuillez sélectionner 1 ou 2.");
                        continue;
                }

                // Création du plateau
                Random rand = new Random();


                Console.WriteLine("Tapez Escape pour continuer");
                cki = Console.ReadKey();
                Console.WriteLine();

            } while (cki.Key != ConsoleKey.Escape);


            // Instanciation du dictionnaire
            Dictionnaire dico = new Dictionnaire(langueJeu);

            Thread.Sleep(2000);

            Console.WriteLine("Entrez la taille du plateau (par exemple, 4 pour un plateau 4x4) :");
            int taillePlateau;


            while (!int.TryParse(Console.ReadLine(), out taillePlateau) || taillePlateau <= 0)
            {
                Console.WriteLine("Veuillez entrer un nombre entier positif pour la taille du plateau :");
            }
            Thread.Sleep(1000);

            int nombreDe = taillePlateau * taillePlateau;
            Console.WriteLine($"Création d'un plateau de {taillePlateau}x{taillePlateau} avec {nombreDe} dés.");
            plateau = new Plateau(nombreDe, taillePlateau);





            Thread.Sleep(1000);
            Console.WriteLine(@" ____    __ _           _         _             _            
|  _ \  /_/| |__  _   _| |_    __| |_   _      | | ___ _   _ 
| | | |/ _ \ '_ \| | | | __|  / _` | | | |  _  | |/ _ \ | | |
| |_| |  __/ |_) | |_| | |_  | (_| | |_| | | |_| |  __/ |_| |
|____/ \___|_.__/ \__,_|\__|  \__,_|\__,_|  \___/ \___|\__,_|");
            Thread.Sleep(2000);

            Console.WriteLine("Voici le plateau généré :");
            plateau.AfficherPlateau();
            string motUtilisateur;
            // Recherche de mots dans le dictionnaire
            Console.WriteLine("\nEntrez un mot pour vérifier s'il est valide (ou tapez 'exit' pour quitter) :");
            while ((motUtilisateur = Console.ReadLine()) != null && motUtilisateur.ToLower() != "exit")
            {
                if (dico.RechDicho(motUtilisateur) == true && plateau.VerifierMot(motUtilisateur) == true)
                {
                    Console.WriteLine($"Le mot '{motUtilisateur}' est valide.");
                }
                else if (dico.RechDicho(motUtilisateur) == false && plateau.VerifierMot(motUtilisateur) == true)
                {
                    Console.WriteLine($"Le mot '{motUtilisateur}' n'existe pas.");
                }
                else if (dico.RechDicho(motUtilisateur) == true && plateau.VerifierMot(motUtilisateur) == false)
                {
                    Console.WriteLine($"Le mot '{motUtilisateur}' n'est pas sur le plateau.");
                }
                else if (dico.RechDicho(motUtilisateur) == false && plateau.VerifierMot(motUtilisateur) == false)
                {
                    Console.WriteLine($"Le mot '{motUtilisateur}' mot invalide.");
                }

                Console.WriteLine("\nEntrez un autre mot (ou tapez 'exit' pour quitter) :");
            }

            Console.WriteLine("Merci d'avoir joué !");
        }
    }
}

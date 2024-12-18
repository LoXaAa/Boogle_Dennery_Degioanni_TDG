using System;
using System.Diagnostics;
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
| || | || || | | || | || | | |) | || | || | || | || |___ 
 \/||\/  |/ \/  |/ \/ \/ \|||");

            Thread.Sleep(2000);
            Console.WriteLine();

            Console.WriteLine(@" ___       _ _   _       _ _           _   _             
|_ | __ () |() __ _| ()___  __ | |(_) ___  _ __  
 | || '_ \| | _| |/ _` | | / __|/ _` | __| |/ _ \| ' \ 
 | || | | | | || | (| | | \__ \ (| | || | (_) | | | |
||| |||\||\,|||/\,|\||\/|| ||");

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

            // Instanciation du joueur
            Console.WriteLine("\nEntrez votre nom :");
            string nomJoueur = Console.ReadLine();
            Joueur joueur = new Joueur(nomJoueur);

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

            Console.WriteLine("Combien de temps doit durer un tour");
            int tempsTour = Convert.ToInt32(Console.ReadLine()); 
            while (tempsTour < 10)
            {
                Console.WriteLine("Veuillez saisir un temps supérieur a 10 secondes");
                tempsTour = Convert.ToInt32(Console.ReadLine());
            }

            Stopwatch chrono = new Stopwatch();





            Thread.Sleep(1000);
            Console.WriteLine(@" ____    __ _           _         _             _            
|  _ \  //| |_  _   | |    _| |   _      | | ___ _   _ 
| | | |/ _ \ '_ \| | | | __|  / _` | | | |  _  | |/ _ \ | | |
| || |  __/ |) | || | |  | (| | || | | || |  __/ || |
|/ \|./ \,|\|  \,|\,|  \/ \|\,|");
            Thread.Sleep(2000);

            Console.WriteLine("Voici le plateau généré :");
            plateau.AfficherPlateau();
            string motUtilisateur;
            // Recherche de mots dans le dictionnaire
            Console.WriteLine("\nEntrez un mot pour vérifier s'il est valide (ou tapez 'exit' pour quitter) :");

            // Recherche de mots dans le dictionnaire
            while ((motUtilisateur = Console.ReadLine()) != null && motUtilisateur.ToLower() != "exit")
            {
                motUtilisateur = motUtilisateur.ToUpper();

                if (motUtilisateur.Length <= 1)
                {
                    Console.WriteLine("Le mot doit contenir au moins deux lettres.");
                }
                else if (joueur.Contain(motUtilisateur))
                {
                    Console.WriteLine($"Vous avez déjà trouvé le mot '{motUtilisateur}'.");
                }
                else if (dico.RechDicho(motUtilisateur) && plateau.VerifierMot(motUtilisateur))
                {
                    Console.WriteLine($"Le mot '{motUtilisateur}' est valide !");
                    joueur.Add_Mot(motUtilisateur);
                }
                else if (!dico.RechDicho(motUtilisateur) && plateau.VerifierMot(motUtilisateur))
                {
                    Console.WriteLine($"Le mot '{motUtilisateur}' n'existe pas dans le dictionnaire.");
                }
                else if (dico.RechDicho(motUtilisateur) && !plateau.VerifierMot(motUtilisateur))
                {
                    Console.WriteLine($"Le mot '{motUtilisateur}' n'est pas sur le plateau.");
                }
                else
                {
                    Console.WriteLine($"Le mot '{motUtilisateur}' est invalide.");
                }

                // Affichage du score actuel
                Console.WriteLine($"Score actuel de {joueur.Nom} : {joueur.Score}");
                Console.WriteLine("\nEntrez un autre mot (ou tapez 'exit' pour quitter) :");
            }

            // Résumé final
            Console.WriteLine($"\nMerci d'avoir joué, {joueur.Nom} !");
            Console.WriteLine($"Votre score final est de {joueur.Score} points.");
        }
    }
}
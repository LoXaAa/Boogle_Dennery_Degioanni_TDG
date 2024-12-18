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
            Console.WriteLine(@"     _                  _         ____                    _      
    | | ___ _   _    __| |_   _  | __ )  ___   ___   __ _| | ___ 
 _  | |/ _ \ | | |  / _` | | | | |  _ \ / _ \ / _ \ / _` | |/ _ \
| |_| |  __/ |_| | | (_| | |_| | | |_) | (_) | (_) | (_| | |  __/
 \___/ \___|\__,_|  \__,_|\__,_| |____/ \___/ \___/ \__, |_|\___|
                                                    |___/        ");

            Thread.Sleep(2000);
            Console.WriteLine();

            Console.WriteLine(@" ___       _ _   _       _ _           _   _             
|_ _|_ __ (_) |_(_) __ _| (_)___  __ _| |_(_) ___  _ __  
 | || '_ \| | __| |/ _` | | / __|/ _` | __| |/ _ \| '_ \ 
 | || | | | | |_| | (_| | | \__ \ (_| | |_| | (_) | | | |
|___|_| |_|_|\__|_|\__,_|_|_|___/\__,_|\__|_|\___/|_| |_|");

            Thread.Sleep(1000);
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
            
            Console.WriteLine("Combien de joueurs jouent ?");

            int nombreJoueurs = Convert.ToInt32(Console.ReadLine());

            while (nombreJoueurs<1)
            {
                Console.WriteLine("Veuillez entrer un nombre entier positif pour le nombre de joueurs :");
                nombreJoueurs = Convert.ToInt32(Console.ReadLine());
            }

            Joueur[] joueurs = new Joueur[nombreJoueurs];

            for (int i = 0; i < nombreJoueurs; i++)
            {
                Console.WriteLine($"\nEntrez le nom pour Joueur {i + 1} :");
                string nomJoueur = Console.ReadLine();
                joueurs[i] = new Joueur(nomJoueur);
            }


            Console.WriteLine("\nCombien de manches voulez-vous jouer");
            int manches = Convert.ToInt32(Console.ReadLine());
            while (manches < 1)
            {
                Console.WriteLine("Veuillez saisir un nombre de manches supérieur à 1");
                manches = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("\nEntrez la taille du plateau (par exemple, 4 pour un plateau 4x4) :");
            int taillePlateau;


            while (!int.TryParse(Console.ReadLine(), out taillePlateau) || taillePlateau <= 0)
            {
                Console.WriteLine("Veuillez entrer un nombre entier positif pour la taille du plateau :");
            }
            Thread.Sleep(1000);

            int nombreDe = taillePlateau * taillePlateau;
            Console.WriteLine($"\nCréation d'un plateau de {taillePlateau}x{taillePlateau} avec {nombreDe} dés.");
            plateau = new Plateau(nombreDe, taillePlateau);

            Thread.Sleep(1000);

            Console.WriteLine("\nCombien de temps doit durer un tour (en secondes)");
            int tempsTour = Convert.ToInt32(Console.ReadLine());
            while (tempsTour < 10)
            {
                Console.WriteLine("Veuillez saisir un temps supérieur a 10 secondes");
                tempsTour = Convert.ToInt32(Console.ReadLine());
            }

            Stopwatch chrono = new Stopwatch();






            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine(@" ____    __ _           _         _           _            
|  _ \  /_/| |__  _   _| |_    __| |_   _    (_) ___ _   _ 
| | | |/ _ \ '_ \| | | | __|  / _` | | | |   | |/ _ \ | | |
| |_| |  __/ |_) | |_| | |_  | (_| | |_| |   | |  __/ |_| |
|____/ \___|_.__/ \__,_|\__|  \__,_|\__,_|  _/ |\___|\__,_|
                                           |__/            ");
            Thread.Sleep(2000);

            // plateau.AfficherPlateau();
            string motUtilisateur;
            Random r = new Random();

            for (int i = 1; i < manches; i++)
            {
                Console.WriteLine($"\ndébut de la manche {i} / {manches}");

                foreach (var joueur in joueurs)
                {
                    Thread.Sleep(500);
                    Console.WriteLine($"Tour du joueur {joueur.Nom}");
                    plateau.RelanceDes(r);
                    plateau.AfficherPlateau();

                    chrono.Start();

                    while (chrono.Elapsed.TotalSeconds < tempsTour)
                    {
                        Console.WriteLine($"Temps restant : {tempsTour - (int)chrono.Elapsed.TotalSeconds} secondes");
                        Console.WriteLine("Entrez un mot (ou tapez 'fin' pour terminer votre tour) :");

                        motUtilisateur = Console.ReadLine()?.ToUpper();

                        if (motUtilisateur == "FIN")
                        {
                            Console.WriteLine($"{joueur.Nom} a terminé son tour.");
                            break;
                        }

                        if (joueur.Contain(motUtilisateur))
                        {
                            Console.Clear();
                            plateau.AfficherPlateau();
                            Console.WriteLine();
                            Console.WriteLine($"Vous avez déjà trouvé le mot '{motUtilisateur}'.");
                        }
                        else if (dico.RechDicho(motUtilisateur) && plateau.VerifierMot(motUtilisateur))
                        {
                            Console.Clear();
                            plateau.AfficherPlateau();
                            Console.WriteLine();
                            Console.WriteLine($"Bravo, {joueur.Nom} ! Le mot '{motUtilisateur}' est valide !");
                            joueur.Add_Mot(motUtilisateur);
                        }
                        else if (!dico.RechDicho(motUtilisateur) && plateau.VerifierMot(motUtilisateur))
                        {
                            Console.Clear();
                            plateau.AfficherPlateau();
                            Console.WriteLine();
                            Console.WriteLine($"Le mot '{motUtilisateur}' n'existe pas dans le dictionnaire.");
                        }
                        else if (dico.RechDicho(motUtilisateur) && !plateau.VerifierMot(motUtilisateur))
                        {
                            Console.Clear();
                            plateau.AfficherPlateau();
                            Console.WriteLine();
                            Console.WriteLine($"Le mot '{motUtilisateur}' n'est pas présent sur le plateau.");
                        }

                        else if (string.IsNullOrWhiteSpace(motUtilisateur) || motUtilisateur.Length < 2)
                        {
                            Console.Clear();
                            plateau.AfficherPlateau();
                            Console.WriteLine();
                            Console.WriteLine("Le mot doit contenir au moins deux lettres.");
                        }

                        else
                        {
                            Console.Clear();
                            plateau.AfficherPlateau();
                            Console.WriteLine();
                            Console.WriteLine($"Le mot '{motUtilisateur}' est invalide.");
                        }

                        Console.WriteLine($"Score actuel de {joueur.Nom} : {joueur.Score}");

                    }

                    chrono.Stop();
                    chrono.Reset();
                    Console.WriteLine($"Fin du tour de {joueur.Nom}");
                }

                // Résumé final
                Console.WriteLine("\n=== Résultats finaux ===");
                foreach (var joueur in joueurs)
                {
                    Console.WriteLine($"{joueur.Nom} : {joueur.Score} points");
                }
                Console.WriteLine("Merci d'avoir joué !");


            }
        }
    }
}

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;

namespace Boogle_Dennery_Degioanni_TDG
{
    internal class Program
    {
        #region SaisirNombre
        /// <summary>
        /// Méthode pour saisir un nombre entier valide supérieur ou égal à un.
        /// </summary>
        /// <param name="message">Message à afficher pour demander une entrée utilisateur.</param>
        /// <param name="minValue">Valeur minimale autorisée.</param>
        /// <returns>Un entier saisi par l'utilisateur.</returns>
        public static int SaisirNombre(string message, int minValue = 1)
        {
            int result;
            do
            {
                Console.WriteLine(message);
                string input = Console.ReadLine();
                if (!int.TryParse(input, out result) || result < minValue)
                {
                    Console.WriteLine($"Veuillez entrer un nombre entier supérieur ou égal à {minValue}.");
                }
            } while (result < minValue);

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

            Console.WriteLine(@" ___       _ _   _       _ _           _   _             
|_ _|_ __ (_) |_(_) __ _| (_)___  __ _| |_(_) ___  _ __  
 | || '_ \| | __| |/ _` | | / __|/ _` | __| |/ _ \| '_ \ 
 | || | | | | |_| | (_| | | \__ \ (_| | |_| | (_) | | | |
|___|_| |_|_|\__|_|\__,_|_|_|___/\__,_|\__|_|\___/|_| |_|");

            Thread.Sleep(1000);
            Console.WriteLine();

            string langueJeu; // Langue par défaut
            string cheminFichier = "";
            string cheminSauvegarde = "";
            Plateau plateau;

            // Sélection de la langue
            Console.WriteLine("Selection de la langue\n\nOption 1 : Français\nOption 2 : Anglais");
            int choixLangue = SaisirNombre("\nSélectionnez 1 ou 2 pour la langue :");

            if (choixLangue == 1)
            {
                Console.WriteLine("Le jeu se jouera en français");
                langueJeu = "FR";

            }
            else
            {
                Console.WriteLine("Le jeu se jouera en anglais");
                langueJeu = "EN";

            }


            // Instanciation du dictionnaire
            Dictionnaire dico = new Dictionnaire(langueJeu);
            Thread.Sleep(2000);


            // Instanciation des joueurs
            int nombreJoueurs = SaisirNombre("\nCombien de joueurs jouent ?");
            Joueur[] joueurs = new Joueur[nombreJoueurs];

            for (int i = 0; i < nombreJoueurs; i++)
            {
                string nomJoueur;
                do
                {
                    Console.WriteLine($"Entrez le nom pour le joueur {i + 1} :");
                    nomJoueur = Console.ReadLine()?.Trim();

                    if (string.IsNullOrWhiteSpace(nomJoueur))
                    {
                        Console.WriteLine("Le nom ne doit pas être vide");
                    }
                } while (string.IsNullOrWhiteSpace(nomJoueur));

                joueurs[i] = new Joueur(nomJoueur,langueJeu);
            }

            // Configuration des manches
            int manches = SaisirNombre("\nCombien de manches voulez-vous jouer ?");

            int taillePlateau = SaisirNombre("\nEntrez la taille du plateau (par exemple, 4 pour un plateau 4x4) :",4);





            int nombreDe = taillePlateau * taillePlateau;

            Console.WriteLine($"\nCréation d'un plateau de {taillePlateau}x{taillePlateau} avec {nombreDe} dés.");
            plateau = new Plateau(nombreDe, taillePlateau);
            Thread.Sleep(1000);

            // Configuration du temps par tour
            int tempsTour = SaisirNombre("\nCombien de temps doit durer un tour (en secondes) ?", 10);

            Stopwatch chrono = new Stopwatch();

            // Début des manches
            Console.Clear();
            Console.WriteLine(@" ____    __ _           _         _           _            
|  _ \  /_/| |__  _   _| |_    __| |_   _    (_) ___ _   _ 
| | | |/ _ \ '_ \| | | | __|  / _` | | | |   | |/ _ \ | | |
| |_| |  __/ |_) | |_| | |_  | (_| | |_| |   | |  __/ |_| |
|____/ \___|_.__/ \__,_|\__|  \__,_|\__,_|  _/ |\___|\__,_|
                                           |__/            ");
            Thread.Sleep(2000);

            Random r = new Random();

            for (int manche = 0; manche < manches; manche++)
            {
                Console.WriteLine($"\nDébut de la manche {manche + 1} / {manches}");
                Thread.Sleep(1000);

                foreach (var joueur in joueurs)
                {
                    Console.Write($"\nTour du joueur ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(joueur.Nom);
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    Console.WriteLine();
                    plateau.RelanceDes(r);
                    plateau.AfficherPlateau();

                    chrono.Start();
                    string motUtilisateur;

                    while (chrono.Elapsed.TotalSeconds < tempsTour)
                    {
                        Console.WriteLine($"\nTemps restant : {tempsTour - (int)chrono.Elapsed.TotalSeconds} secondes");
                        Console.WriteLine("Entrez un mot (ou tapez 'fin' pour terminer votre tour) :");
                        motUtilisateur = Console.ReadLine()?.ToUpper();

                        if (motUtilisateur == "FIN")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(joueur.Nom);
                            Console.ResetColor();
                            Console.WriteLine(" a terminé son tour");
                            Thread.Sleep(1000);
                            break;
                        }

                        if (string.IsNullOrWhiteSpace(motUtilisateur) || motUtilisateur.Length < 2)
                        {
                            Console.Clear();
                            plateau.AfficherPlateau();
                            Console.WriteLine("\nLe mot doit contenir au moins deux lettres.");
                        }
                        else if (joueur.Contain(motUtilisateur))
                        {
                            Console.Clear();
                            plateau.AfficherPlateau();
                            Console.WriteLine($"\nVous avez déjà trouvé le mot '{motUtilisateur}'.");
                        }

                        else if (dico.RechDicho(motUtilisateur) && !plateau.VerifierMot(motUtilisateur))
                        {
                            Console.Clear();
                            plateau.AfficherPlateau();
                            Console.WriteLine($"\nle mot {motUtilisateur} n'est pas sur le plateau");
                        }
                        else if (dico.RechDicho(motUtilisateur) && plateau.VerifierMot(motUtilisateur))
                        {
                            Console.Clear();
                            plateau.AfficherPlateau();
                            Console.WriteLine($"\nBravo, {joueur.Nom} ! Le mot '{motUtilisateur}' est valide !");
                            joueur.AddMot(motUtilisateur);
                        }
                        else
                        {
                            Console.Clear();
                            plateau.AfficherPlateau();
                            Console.WriteLine($"\nLe mot '{motUtilisateur}' est invalide.");
                        }
                        Console.Write($"Score actuel de ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{joueur.Nom} : ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(joueur.Score);
                        Console.ResetColor();
                    }

                    chrono.Stop();
                    chrono.Reset();
                    Console.Write($"Fin du tour de ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(joueur.Nom);
                    Console.ResetColor ();
                    Console.Clear() ;
                }
            }

            Console.Clear();
            // Résumé final
            Console.WriteLine("\n=== Résultats finaux ===");
            Thread.Sleep(1000);
            foreach (var joueur in joueurs)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{joueur.Nom} : ");
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{joueur.Score} points");
                Thread.Sleep (1000);
            }
            Console.ResetColor();
            Console.WriteLine("Merci d'avoir joué !");

            // Générer le nuage de mots
            Thread.Sleep(2000);
            NuageMots.CreerNuageMots(joueurs);
        }
    }
}

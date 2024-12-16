using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Boogle_Dennery_Degioanni_TDG;


namespace Boogle_Dennery_Degioanni_TDG
{
    internal class Program
    {

        #region SaisirNombre
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

            string cheminFichier = "";
            string cheminSauvegarde = "";
            string[] contenuNettoye;
            string langueJeu = "FR";

            ConsoleKeyInfo cki = new ConsoleKeyInfo(); // Initialisation par défaut

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
                        cheminFichier = "MotsPossiblesFR.txt";
                        cheminSauvegarde = "MotsPossiblesFR_Valide.txt";
                        break;

                    case 2:
                        Console.WriteLine("Le jeu va être lancé en anglais");
                        langueJeu = "EN";
                        cheminFichier = "MotsPossiblesEN.txt";
                        cheminSauvegarde = "MotsPossiblesEN_Valide.txt";
                        break;

                    default:
                        Console.WriteLine("Choix invalide. Veuillez sélectionner 1 ou 2.");
                        continue;
                }

                try
                {
                    contenuNettoye = FichierGestion.ChargerEtNormaliser(cheminFichier);
                    FichierGestion.SauvegarderFichier(cheminSauvegarde, contenuNettoye);

                    string cheminComplet = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, cheminSauvegarde);
                    Console.WriteLine($"Nouveau fichier sauvegardé dans : {cheminComplet}");
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine($"Erreur : {ex.Message}");
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Une erreur inattendue est survenue : {ex.Message}");
                    continue;
                }

                Console.WriteLine("Tapez Escape pour sortir de l'initiatlisation et commencer le jeu");
                cki = Console.ReadKey();

            } while (cki.Key != ConsoleKey.Escape);

            Thread.Sleep(2000);

            Console.WriteLine(@" ____    __  ____  _   _ _____   ____  _   _       _ _____ _   _ 
|  _ \ _/_/_| __ )| | | |_   _| |  _ \| | | |     | | ____| | | |
| | | | ____|  _ \| | | | | |   | | | | | | |  _  | |  _| | | | |
| |_| |  _|_| |_) | |_| | | |   | |_| | |_| | | |_| | |___| |_| |
|____/|_____|____/ \___/  |_|   |____/ \___/   \___/|_____|\___/ ");

            Thread.Sleep(2000);

            Random rand = new Random();

            Console.WriteLine("Entrez la taille du plateau (par exemple, 4 pour un plateau 4x4) :");
            int taillePlateau;

            while (!int.TryParse(Console.ReadLine(), out taillePlateau) || taillePlateau <= 0)
            {
                Console.WriteLine("Veuillez entrer un nombre entier positif pour la taille du plateau :");
            }

            int nombreDe = taillePlateau * taillePlateau;

            Console.WriteLine($"Création d'un plateau de {taillePlateau}x{taillePlateau} avec {nombreDe} dés.");

            Plateau plateau = new Plateau(nombreDe, taillePlateau);

            Console.WriteLine("Voici le plateau généré :");
            plateau.AfficherPlateau();

            Console.ReadKey();
            Console.WriteLine("\nEntrez un mot pour vérifier s'il est valide (ou tapez 'exit' pour quitter) :");
            string mot;
            while ((mot = Console.ReadLine()) != null && mot.ToLower() != "exit")
            {
                try
                {
                    bool estValide = plateau.VerifierMot(mot);
                    Console.WriteLine(estValide ? $"Le mot '{mot}' est valide." : $"Le mot '{mot}' n'est pas valide.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Erreur : {ex.Message}");
                }

                Console.WriteLine("\nEntrez un autre mot (ou tapez 'exit' pour quitter) :");
            }

            Console.WriteLine("Merci d'avoir joué !");
        }

    }

}


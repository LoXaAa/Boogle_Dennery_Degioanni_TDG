namespace Boogle_Dennery_Degioanni_TDG
{
    internal class Program
    {
        static void Main(string[] a)
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

            string cheminFichier = @"C:\Users\hugod\Desktop\ESILV\Cours\Semestre 3\Info\Algo POO\TD\Boogle_Dennery_Degioanni_TDG\MotsPossiblesFR.txt";
            string cheminSauvegarde = @"C:\Users\hugod\Desktop\ESILV\Cours\Semestre 3\Info\Algo POO\TD\Boogle_Dennery_Degioanni_TDG\Boogle_Dennery_Degioanni_TDG\MotsPossiblesFR_Valide.txt";

            string[] contenuNettoye = FichierGestion.ChargerEtNormaliser(cheminFichier);

            FichierGestion.SauvegarderFichier(cheminSauvegarde, contenuNettoye);

            Console.WriteLine($"Nouveau fichier sauvegardé dans : {Path.GetFullPath("comme : MotsPossiblesFR_Valide.txt")}");
            Thread.Sleep(2000);

            Console.WriteLine(@" ____    __  ____  _   _ _____   ____  _   _       _ _____ _   _ 
|  _ \ _/_/_| __ )| | | |_   _| |  _ \| | | |     | | ____| | | |
| | | | ____|  _ \| | | | | |   | | | | | | |  _  | |  _| | | | |
| |_| |  _|_| |_) | |_| | | |   | |_| | |_| | | |_| | |___| |_| |
|____/|_____|____/ \___/  |_|   |____/ \___/   \___/|_____|\___/ ");
            Console.WriteLine();
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

        }
    }
}
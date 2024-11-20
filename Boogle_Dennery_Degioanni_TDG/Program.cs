namespace Boogle_Dennery_Degioanni_TDG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string cheminFichier = @"C:\Users\hugod\Desktop\ESILV\Cours\Semestre 3\Info\Algo POO\TD\Boogle_Dennery_Degioanni_TDG\MotsPossiblesFR.txt";

            if (File.Exists(cheminFichier))
            {
                Console.WriteLine("Fichier trouvé !");

                string[] contenuNettoye = FichierGestion.ChargerEtNormaliser(cheminFichier);
                string cheminSauvegarde = @"C:\Users\hugod\Desktop\ESILV\Cours\Semestre 3\Info\Algo POO\TD\Boogle_Dennery_Degioanni_TDG\Boogle_Dennery_Degioanni_TDG\MotsPossiblesFR_Valide.txt";
                FichierGestion.SauvegarderFichier(cheminSauvegarde, contenuNettoye);

                Console.WriteLine($"Fichier sauvegardé dans : {Path.GetFullPath("MotsPossiblesFR_Normalise.txt")}");
            }
            else
            {
                Console.WriteLine("Fichier introuvable. Vérifiez le chemin.");
            }
        }
    }
}

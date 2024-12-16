using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

internal class FichierGestion
{
    #region Charger et Normaliser le fichier
    /// <summary>
    /// Méthode pour charger et mettre en ordre le fichier (espaces en trop, etc.)
    /// </summary>
    /// <param name="cheminRelatif">Chemin relatif au répertoire de la solution</param>
    /// <returns>Tableau de mots issus du fichier source</returns>
    public static string[] ChargerEtNormaliser(string cheminRelatif)
    {
        // Construire le chemin absolu à partir du chemin relatif
        string cheminAbsolu = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, cheminRelatif);

        if (!File.Exists(cheminAbsolu))
        {
            throw new FileNotFoundException($"Le fichier {cheminAbsolu} est introuvable.");
        }

        Console.WriteLine("Fichier avec la liste des mots valides trouvé !");
        Thread.Sleep(2000);
        Console.Write("\n");

        // Lire le fichier avec StreamReader
        using (StreamReader sr = new StreamReader(cheminAbsolu))
        {
            string contenu = sr.ReadToEnd(); // Lire tout le contenu
            string[] lignes = contenu.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return NormaliserLignes(lignes);
        }
    }
    #endregion

    #region Normaliser les lignes
    /// <summary>
    /// Méthode qui met au propre le fichier texte avec les mots
    /// </summary>
    /// <param name="lignes">Lignes du fichier à nettoyer</param>
    /// <returns>Tableau de mots nettoyés</returns>
    private static string[] NormaliserLignes(string[] lignes)
    {
        int taille = 0;

        foreach (string ligne in lignes)
        {
            string mot = Regex.Replace(ligne.Trim(), @"\s{2,}", " ").Trim();
            if (!string.IsNullOrEmpty(mot)) taille++;
        }

        // Créer un tableau pour les mots valides
        string[] lignesNettoyees = new string[taille];
        int index = 0;

        foreach (string ligne in lignes)
        {
            string mot = Regex.Replace(ligne.Trim(), @"\s{2,}", " ").Trim();
            if (!string.IsNullOrEmpty(mot))
            {
                lignesNettoyees[index] = mot.ToUpper();
                index++;
            }
        }

        return lignesNettoyees;
    }
    #endregion

    #region Sauvegarder un fichier
    /// <summary>
    /// Sauvegarde un tableau de mots dans un fichier texte
    /// </summary>
    /// <param name="cheminRelatif">Chemin relatif pour le fichier de sauvegarde</param>
    /// <param name="contenu">Tableau de mots à sauvegarder</param>
    public static void SauvegarderFichier(string cheminRelatif, string[] contenu)
    {
        // Construire le chemin absolu à partir du chemin relatif
        string cheminAbsolu = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, cheminRelatif);

        // Écrire le fichier avec StreamWriter
        using (StreamWriter sw = new StreamWriter(cheminAbsolu))
        {
            foreach (string ligne in contenu)
            {
                sw.WriteLine(ligne);
            }
        }
    }
    #endregion
}

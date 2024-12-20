using System;
using System.IO;
using System.Text.RegularExpressions;

internal class FichierGestion
{
    /// <summary>
    /// Charge un fichier texte, lit les lignes et retourne un tableau de chaînes normalisé.
    /// </summary>
    /// <param name="cheminRelatif">Chemin relatif du fichier à charger</param>
    /// <returns>Tableau des chaînes normalisées</returns>
    public static string[] ChargerEtNormaliser(string cheminRelatif)
    {
        if (string.IsNullOrWhiteSpace(cheminRelatif))
        {
            throw new ArgumentException("Le chemin relatif ne peut pas être vide ou null.", nameof(cheminRelatif));
        }

        string cheminAbsolu = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, cheminRelatif);

        if (!File.Exists(cheminAbsolu))
        {
            throw new FileNotFoundException($"Le fichier {cheminAbsolu} est introuvable.");
        }

        string[] lignes;

        using (StreamReader sr = new StreamReader(cheminAbsolu))
        {
            lignes = sr.ReadToEnd()
                       .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        return NormaliserLignes(lignes);
    }


 

    /// <summary>
    /// Nettoie les chaînes, supprime les espaces en trop et convertit en majuscules.
    /// </summary>
    /// <param name="lignes">Lignes à normaliser</param>
    /// <returns>Tableau de chaînes normalisées</returns>
    private static string[] NormaliserLignes(string[] lignes)
    {
        if (lignes == null || lignes.Length == 0)
        {
            return Array.Empty<string>();
        }

        string[] lignesNettoyees = new string[lignes.Length];
        int index = 0;

        foreach (string ligne in lignes)
        {
            string mot = ligne.Trim().ToUpper();
            if (!string.IsNullOrEmpty(mot))
            {
                lignesNettoyees[index++] = mot;
            }
        }

        Array.Resize(ref lignesNettoyees, index);
        return lignesNettoyees;
    }

    /// <summary>
    /// Sauvegarde un tableau de chaînes dans un fichier texte.
    /// </summary>
    /// <param name="cheminRelatif">Chemin relatif pour la sauvegarde</param>
    /// <param name="contenu">Tableau de chaînes à sauvegarder</param>
    public static void SauvegarderFichier(string cheminRelatif, string[] contenu)
    {
        if (string.IsNullOrWhiteSpace(cheminRelatif))
        {
            throw new ArgumentException("Le chemin relatif ne peut pas être vide ou null.", nameof(cheminRelatif));
        }

        if (contenu == null || contenu.Length == 0)
        {
            throw new ArgumentException("Le contenu ne peut pas être vide ou null.", nameof(contenu));
        }

        string cheminAbsolu = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, cheminRelatif);

        using (StreamWriter sw = new StreamWriter(cheminAbsolu))
        {
            foreach (string ligne in contenu)
            {
                sw.WriteLine(ligne);
            }
        }


    }
}

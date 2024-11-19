using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

internal class FichierGestion
{
    #region Charger et Normaliser le fichier
    /// <summary>
    /// Méthode pour charger et mettre en ordre le fichier (espaces en trop etc...)
    /// </summary>
    /// <param name="cheminFichier"></param>
    /// <returns>tableau de mots issus du fichier source</returns>
 
    public static string[] ChargerEtNormaliser(string cheminFichier)
    {
        // Vérifier si le fichier existe
        if (!File.Exists(cheminFichier))
        {
            throw new FileNotFoundException($"Le fichier {cheminFichier} est introuvable.");
        }

        // Charger les lignes du fichier
        string[] lignes = File.ReadAllLines(cheminFichier);

        // Nettoyer et normaliser les lignes
        return NormaliserLignes(lignes);
    }

    #endregion

    // Méthode pour normaliser un tableau de lignes
    private static string[] NormaliserLignes(string[] lignes)
    {
        List<string> lignesNettoyees = new List<string>();

        foreach (string ligne in lignes)
        {
            // Supprimer les espaces inutiles
            string mot = ligne.Trim();
            mot = Regex.Replace(mot, @"\s{2,}", " "); // Réduit les espaces multiples à un seul

            // Ajouter uniquement les lignes non vides
            if (!string.IsNullOrEmpty(mot))
            {
                lignesNettoyees.Add(mot.ToUpper()); // Convertir en majuscules
            }
        }

        return lignesNettoyees.ToArray();
    }

    // Méthode pour sauvegarder un tableau dans un fichier
    public static void SauvegarderFichier(string cheminFichier, string[] contenu)
    {
        File.WriteAllLines(cheminFichier, contenu);
    }
}

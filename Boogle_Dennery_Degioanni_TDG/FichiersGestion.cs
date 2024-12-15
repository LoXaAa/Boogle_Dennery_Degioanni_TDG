using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

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

        if (!File.Exists(cheminFichier))
        {
            throw new FileNotFoundException($"Le fichier {cheminFichier} est introuvable.");
        }
        else
        {
            Console.WriteLine("Fichier avec la liste des mots valides trouvé !");
            Thread.Sleep(2000);
            Console.Write("\n");
            string[] lignes = File.ReadAllLines(cheminFichier);
            return NormaliserLignes(lignes);
        }

       


        return NormaliserLignes(lignes);

        return NormaliserLignes(lignes);
    }
    #endregion

    /// <summary>
    /// Méthode qui met au propre le fichier texte avec les mots
    /// </summary>
    /// <param name="lignes"></param>
    /// <returns>Tableau de mots</returns>
    private static string[] NormaliserLignes(string[] lignes)
    {
        int taille = 0;


        foreach (string ligne in lignes)
        {
            string mot = Regex.Replace(ligne.Trim(), @"\s{2,}", " ").Trim();
            if (!string.IsNullOrEmpty(mot)) taille++;
        }

        // Étape 2 : Créer un tableau pour les mots valides
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

    // Méthode pour sauvegarder un tableau dans un fichier
    public static void SauvegarderFichier(string cheminFichier, string[] contenu)
    {
        File.WriteAllLines(cheminFichier, contenu);
    }
}
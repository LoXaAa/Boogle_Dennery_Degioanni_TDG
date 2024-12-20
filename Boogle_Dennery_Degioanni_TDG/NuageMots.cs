using WordCloudSharp;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using Boogle_Dennery_Degioanni_TDG;

internal static class NuageMots
{
    /// <summary>
    /// Crée un nuage de mots basé sur les mots et leurs scores.
    /// </summary>
    /// <param name="joueurs">Tableau des joueurs avec leurs mots trouvés et scores associés.</param>
    /// <param name="width">Largeur de l'image générée.</param>
    /// <param name="height">Hauteur de l'image générée.</param>
    /// <param name="outputPath">Chemin où sauvegarder l'image générée.</param>
    public static void CreerNuageMots(Joueur[] joueurs, int width = 800, int height = 600, string outputPath = "nuage_de_mots.png")
    {
        // Récupérer tous les mots et scores des joueurs
        var motsEtScores = joueurs
            .SelectMany(joueur => joueur.GetMotsTrouves()
                .Select(mot => new { Mot = mot, Score = joueur.GetScoreMot(mot) }))
            .ToList();

        // Validation des données
        if (!motsEtScores.Any())
        {
            Console.WriteLine("Aucun mot trouvé. Impossible de générer le nuage de mots.");
            return;
        }

        var words = motsEtScores.Select(ms => ms.Mot).ToArray();
        var frequencies = motsEtScores.Select(ms => ms.Score).ToArray();

        try
        {
            // Créer un générateur de nuages
            var generator = new WordCloudSharp.WordCloud(width, height);

            // Générer le nuage
            var bitmap = generator.Draw(words, frequencies);

            // Sauvegarder l'image
            bitmap.Save(outputPath);

            // Ouvrir l'image générée
            Process.Start(new ProcessStartInfo
            {
                FileName = outputPath,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la création du nuage de mots : {ex.Message}");
        }
    }
}

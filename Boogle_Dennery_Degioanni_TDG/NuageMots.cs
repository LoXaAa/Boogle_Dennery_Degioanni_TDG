using WordCloudSharp;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using Boogle_Dennery_Degioanni_TDG;

internal static class NuageMots
{

    /// <summary>
    /// Crée un nuage de mots en fonction des mots trouvés
    /// </summary>
    /// <param name="joueurs">Tableau des joueurs participant au jeu</param>
    /// <param name="width">Largeur image</param>
    /// <param name="height">Hauteur image</param>
    /// <param name="outputPath">Chemin fichier de sortie</param>
    public static void CreerNuageMots(Joueur[] joueurs, int width = 800, int height = 600, string outputPath = "nuage_de_mots.png")
    {
        var motsEtScores = joueurs
            .SelectMany(joueur => joueur.GetMotsTrouves()
                .Select(mot => new { Mot = mot, Score = joueur.GetScoreMot(mot) }))
            .ToList();

        
        if (!motsEtScores.Any())
        {
            Console.WriteLine("Aucun mot trouvé. Impossible de générer le nuage de mots.");
            return;
        }

        var words = motsEtScores.Select(ms => ms.Mot).ToArray();
        var frequencies = motsEtScores.Select(ms => ms.Score).ToArray();

        try
        {
            var generator = new WordCloudSharp.WordCloud(width, height);

            var bitmap = generator.Draw(words, frequencies);

            bitmap.Save(outputPath);

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

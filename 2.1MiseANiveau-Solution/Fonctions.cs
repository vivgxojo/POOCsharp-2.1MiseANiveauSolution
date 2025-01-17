using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._1MiseANiveau_Solution
{
    public class Fonctions
    {
        /// <summary>
        /// Lire le fichier csv et retourner les données dans un tableau de tableaux
        /// </summary>
        /// <param name="chemin">Chemin du fichier</param>
        /// <returns>Tableau de tableaux contenant toutes les données</returns>
        public static string[][] LireFichier(string chemin)
        {
            // Initialisation d'un tableau principal avec une taille arbitraire
            string[][] data = new string[10][];
            int rowCount = 0;

            // Déclaration des objets FileStream et StreamReader
            FileStream fs = null;
            StreamReader sr = null;

            try
            {
                // Ouverture du fichier
                fs = new FileStream(chemin, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);

                string line;

                // Lecture ligne par ligne
                while ((line = sr.ReadLine()) != null)
                {
                    // Découper les valeurs de la ligne par les virgules
                    string[] row = line.Split(',');

                    // Vérifier si le tableau principal doit être agrandi
                    if (rowCount >= data.Length)
                    {
                        // Agrandir le tableau principal manuellement
                        string[][] tempData = new string[data.Length * 2][];
                        for (int i = 0; i < data.Length; i++)
                        {
                            tempData[i] = data[i];
                        }
                        data = tempData;
                    }

                    // Ajouter la ligne au tableau principal
                    data[rowCount] = row;
                    rowCount++;
                }

                // Redimensionner le tableau principal pour qu'il corresponde au nombre exact de lignes lues
                Array.Resize(ref data, rowCount);

            }
            finally
            {
                // Nettoyage des ressources
                if (sr != null) sr.Close();
                if (fs != null) fs.Close();
            }

            return data;
        }

        /// <summary>
        /// Ajoute une ligne au fichier rapport.
        /// </summary>
        /// <param name="filePath">Chemin du fichier de sortie.</param>
        /// <param name="line">Ligne à écrire dans le fichier.</param>
        public static void AjouterLigneAuRapport(string filePath, string line)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(filePath, append: true);
                writer.WriteLine(line);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'écriture dans le fichier : {ex.Message}");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close(); // Ferme le StreamWriter explicitement
                    writer.Dispose(); // Libère les ressources associées
                }
            }
        }

        /// <summary>
        /// Calcule le délai de livraison pour chaque commande.
        /// </summary>
        /// <param name="data">Données des commandes (tableau de tableaux).</param>
        /// <returns>Un tableau contenant les délais de livraison pour chaque commande.</returns>
        public static int[] CalculerDelaiLivraison(string[][] data)
        {
            int[] delays = new int[data.Length - 1]; // Ignorer la ligne d'en-tête

            for (int i = 1; i < data.Length; i++)
            {   
                DateTime dateCommande = DateTime.Parse(data[i][0]);
                DateTime dateLivraison = DateTime.Parse(data[i][1]);
                delays[i - 1] = (dateLivraison - dateCommande).Days;
            }

            return delays;
        }

        /// <summary>
        /// Calcule le délai moyen de livraison.
        /// </summary>
        /// <param name="delays">Tableau des délais de livraison.</param>
        /// <returns>Le délai moyen de livraison.</returns>
        public static double CalculerDelaiMoyen(int[] delays)
        {
            if (delays.Length == 0)
                return 0;

            double total = 0;
            foreach (var delay in delays)
            {
                total += delay;
            }

            return total / delays.Length;
        }

        /// <summary>
        /// Calcule le montant total de toutes les commandes.
        /// </summary>
        /// <param name="data">Données des commandes (tableau de tableaux).</param>
        /// <returns>Le montant total de toutes les commandes.</returns>
        public static double CalculerMontantTotal(string[][] data)
        {
            double total = 0;

            for (int i = 1; i < data.Length; i++)
            {
                // Utiliser CultureInfo.InvariantCulture pour le point (.) décimal
                total += double.Parse(data[i][4], CultureInfo.InvariantCulture);
            }

            return total;
        }

        /// <summary>
        /// Calcule le montant moyen des commandes.
        /// </summary>
        /// <param name="data">Données des commandes (tableau de tableaux).</param>
        /// <returns>Le montant moyen des commandes.</returns>
        public static double CalculerMontantMoyen(string[][] data)
        {
            if (data.Length <= 1)
                return 0;

            double total = CalculerMontantTotal(data);
            return total / (data.Length - 1); // Ignorer la ligne d'en-tête
        }

        /// <summary>
        /// Compte le nombre de commandes selon leur statut.
        /// </summary>
        /// <param name="data">Données des commandes (tableau de tableaux).</param>
        /// <param name="status">Statut à compter (ex: "Livrée").</param>
        /// <returns>Le nombre de commandes correspondant au statut donné.</returns>
        public static int CalculerNbCommandesParStatus(string[][] data, string status)
        {
            int count = 0;

            for (int i = 1; i < data.Length; i++)
            {
                if (data[i][5] == status)
                {
                    count++;
                }
            }

            return count;
        }
    }
}

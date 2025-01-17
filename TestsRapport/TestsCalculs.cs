using _2._1MiseANiveau_Solution;

namespace TestsRapport
{
    public class TestsCalculs
    {
        string dataFile = "fichierTest.csv";
        string rapportFile = "rapportTest.txt";

        string[][] sampleData = new string[][]
        {
            new string[] { "date_commande", "date_livraison", "id_commande", "client", "montant_total", "statut" },
            new string[] { "2025-01-01", "2025-01-05", "1001", "Client_A", "150.75", "Livrée" },
            new string[] { "2025-01-02", "2025-01-05", "1002", "Client_B", "245.30", "Livrée" },
            new string[] { "2025-01-03", "2025-01-05", "1003", "Client_C", "89.99", "Annulée" }
        };

        [Fact]
        public void TestLireFichier()
        {
            string[][] expected = new string[][]
            {
                new string[] {"allo", "maman"},
                new string[] {"123", "45"}
            };

            string[][] result = Fonctions.LireFichier(dataFile);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestCalculateDeliveryDelays()
        {
            int[] delays = Fonctions.CalculerDelaiLivraison(sampleData);
            Assert.Equal(new int[] { 4, 3, 2 }, delays);
        }

        [Fact]
        public void TestCalculateAverageDeliveryDelay()
        {
            int[] delays = new int[] { 4, 3, 2 };
            double averageDelay = Fonctions.CalculerDelaiMoyen(delays);
            Assert.Equal(3.0, averageDelay);
        }

        [Fact]
        public void TestCalculateTotalAmount()
        {
            double total = Fonctions.CalculerMontantTotal(sampleData);
            Assert.Equal(486.04, total, 0.01);
        }

        [Fact]
        public void TestCalculateAverageAmount()
        {
            double average = Fonctions.CalculerMontantMoyen(sampleData);
            Assert.Equal(162.01, average, 0.01);
        }

        [Fact]
        public void TestCountOrdersByStatus()
        {
            int countLivree = Fonctions.CalculerNbCommandesParStatus(sampleData, "Livrée");
            Assert.Equal(2, countLivree);

            int countEnTraitement = Fonctions.CalculerNbCommandesParStatus(sampleData, "En traitement");
            Assert.Equal(0, countEnTraitement);

            int countAnnulee = Fonctions.CalculerNbCommandesParStatus(sampleData, "Annulée");
            Assert.Equal(1, countAnnulee);
        }

        [Fact]
        public void TestAjouterLigneRapport()
        {
            string ligne = "allo rapport";
            Fonctions.AjouterLigneAuRapport(rapportFile, ligne);
        }
    }
}
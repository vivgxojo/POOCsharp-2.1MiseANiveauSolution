using _2._1MiseANiveau_Solution;

//Lire les données
string filePath = "data.csv";
string[][] donnees = Fonctions.LireFichier(filePath);

//Calculs
int[] delays = Fonctions.CalculerDelaiLivraison(donnees);
double delaiMoyen = Fonctions.CalculerDelaiMoyen(delays);
double montantTotal = Fonctions.CalculerMontantTotal(donnees);
double montantMoyen = Fonctions.CalculerMontantMoyen(donnees);
int nbCommandesLivrees = Fonctions.CalculerNbCommandesParStatus(donnees, "Livrée");
int nbCommandesEnTraitement = Fonctions.CalculerNbCommandesParStatus(donnees, "En traitement");
int nbCommandesAnnulees = Fonctions.CalculerNbCommandesParStatus(donnees, "Annulée");

//Écriture du rapport
string fichier = "rapport.txt";

// Ajout des lignes au rapport
Fonctions.AjouterLigneAuRapport(fichier, "Délais de livraison :");
for(int i = 0; i < delays.Length; i++)
{
    Fonctions.AjouterLigneAuRapport(fichier, $"- Commande : {donnees[i+1][2]} livrée en {delays[i]} jours");
}

Fonctions.AjouterLigneAuRapport(fichier, $"- Délai de livraison moyen : {delaiMoyen:F2} jours");
Fonctions.AjouterLigneAuRapport(fichier, "");

Fonctions.AjouterLigneAuRapport(fichier, "Montants :");
Fonctions.AjouterLigneAuRapport(fichier, $"- Montant total : {montantTotal:C}");
Fonctions.AjouterLigneAuRapport(fichier, $"- Montant moyen : {montantMoyen:C}");
Fonctions.AjouterLigneAuRapport(fichier, "");

Fonctions.AjouterLigneAuRapport(fichier, "Statistiques des commandes :");
Fonctions.AjouterLigneAuRapport(fichier, $"- Nombre de commandes livrées : {nbCommandesLivrees}");
Fonctions.AjouterLigneAuRapport(fichier, $"- Nombre de commandes en traitement : {nbCommandesEnTraitement}");
Fonctions.AjouterLigneAuRapport(fichier, $"- Nombre de commandes annulées : {nbCommandesAnnulees}");

Console.WriteLine($"Rapport généré dans le fichier : {fichier}");

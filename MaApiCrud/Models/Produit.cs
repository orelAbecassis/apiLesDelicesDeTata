namespace MaApiCrud.Models;


public class Produit
{
    public int id { get; set; }
    public string nom { get; set; }
    public decimal prix { get; set; }
    public string description { get; set; }
    public string image { get; set; }
    public int? id_Categ_id { get; set; }
    
    /*
    public ICollection<Categorie> Categories { get; set; }
    */

    
    public Produit()
    {
        // Initialisez les propriétés non-nullable ici
        nom = string.Empty;
        description = string.Empty;
        image = string.Empty;
    }
}


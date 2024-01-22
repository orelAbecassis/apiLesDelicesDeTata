using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaApiCrud.Bdd;
using MaApiCrud.Models;

namespace MaApiCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitController : ControllerBase
    {
        // Contexte de la base de données injecté via le constructeur
        private readonly ConnectBdd _context;

        // Constructeur qui initialise le contexte de la base de données
        public ProduitController(ConnectBdd context)
        {
            _context = context;
        }

        // Action pour obtenir tous les produits (GET api/produits)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduits()
        {
            // Récupérer tous les produits de la base de données de manière asynchrone
            return await _context.Produits.ToListAsync();
        }

        // Action pour obtenir un produit par son ID (GET api/produits/id)
        [HttpGet("{id}")]
        public async Task<ActionResult<Produit>> GetProduit(int id)
        {
            // Recherche asynchrone du produit par son ID
            var produit = await _context.Produits.FindAsync(id);

            // Vérifier si le produit a été trouvé
            if (produit == null)
            {
                // Retourner une réponse NotFound si le produit n'est pas trouvé
                return NotFound();
            }

            // Retourner le produit trouvé
            return produit;
        }

        // Action pour ajouter un nouveau produit (POST api/produits)
        [HttpPost]
        public async Task<ActionResult<Produit>> PostProduit(Produit produit)
        {
            // Ajouter le produit à la base de données
            _context.Produits.Add(produit);
            
            // Enregistrer les modifications dans la base de données de manière asynchrone
            await _context.SaveChangesAsync();

            // Retourner une réponse CreatedAtAction avec le nouvel objet produit
            return CreatedAtAction("GetProduit", new { id = produit.id }, produit);

        }

        // Action pour mettre à jour un produit existant (PUT api/produits/id)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduit(int id, Produit produit)
        {
            // Vérifier si l'ID du produit correspond à l'ID spécifié dans la requête
            if (id != produit.id)
            {
                // Retourner une réponse BadRequest si les ID ne correspondent pas
                return BadRequest();
            }

            // Marquer l'entité comme modifiée pour la mise à jour
            _context.Entry(produit).State = EntityState.Modified;

            try
            {
                // Enregistrer les modifications dans la base de données de manière asynchrone
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Gérer les conflits de concurrence
                if (!ProduitExists(id))
                {
                    // Retourner une réponse NotFound si le produit n'est pas trouvé
                    return NotFound();
                }
                else
                {
                    // Propager l'exception si elle n'est pas liée à l'existence du produit
                    throw;
                }
            }

            // Retourner une réponse NoContent en cas de succès
            return NoContent();
        }

        // Action pour supprimer un produit par son ID (DELETE api/produits/id)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduit(int id)
        {
            // Recherche asynchrone du produit par son ID
            var produit = await _context.Produits.FindAsync(id);

            // Vérifier si le produit a été trouvé
            if (produit == null)
            {
                // Retourner une réponse NotFound si le produit n'est pas trouvé
                return NotFound();
            }

            // Supprimer le produit de la base de données
            _context.Produits.Remove(produit);
            
            // Enregistrer les modifications dans la base de données de manière asynchrone
            await _context.SaveChangesAsync();

            // Retourner une réponse NoContent en cas de succès
            return NoContent();
        }

        // Méthode utilitaire pour vérifier l'existence d'un produit par son ID
        private bool ProduitExists(int id)
        {
            return _context.Produits.Any(e => e.id == id);
        }
    }
}

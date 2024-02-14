package main

import (
    "github.com/gin-gonic/gin"
)

func setupRouter() *gin.Engine {
    r := gin.Default()

    // Route pour obtenir la liste des produits
    r.GET("product/api/product", func(c *gin.Context) {
        c.JSON(200, gin.H{"message": "Liste des produits"})
    })

    // Route pour obtenir un produit par son ID
    r.GET("product/api/product/:id", func(c *gin.Context) {
        id := c.Param("id")
        c.JSON(200, gin.H{"message": "Informations sur le produit", "id": id})
    })

    // Route pour créer un nouveau produit
    r.POST("product/api/product", func(c *gin.Context) {
        c.JSON(201, gin.H{"message": "Produit créé avec succès"})
    })

    // Route pour mettre à jour un produit existant
    r.PUT("product/api/product/:id", func(c *gin.Context) {
        id := c.Param("id")
        c.JSON(200, gin.H{"message": "Produit mis à jour", "id": id})
    })

    // Route pour supprimer un produit
    r.DELETE("product/api/product/:id", func(c *gin.Context) {
        id := c.Param("id")
        c.JSON(200, gin.H{"message": "Produit supprimé", "id": id})
    })

    return r
}

func main() {
    r := setupRouter()
    
    // Démarrez le serveur sur le port 5002
    r.Run(":50002")
}

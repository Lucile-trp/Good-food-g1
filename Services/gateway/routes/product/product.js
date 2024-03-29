const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');

const PRODUCT_API = process.env.PRODUCT_API;

// Route pour obtenir la liste des produits
router.get('/', async (req, res, next) => {
  try {
    const response = await axios.get(`${PRODUCT_API}/products`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour obtenir un produit par son ID
router.get("/:id", async (req, res, next) => {
  try {
    const response = await axios.get(`${PRODUCT_API}/products/${req.params.id}`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour créer un nouveau produit
router.post('/', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.post(PRODUCT_API + '/products', req.body);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour mettre à jour un produit existant
router.put("/:id", isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.put(`${PRODUCT_API}/products/${req.params.id}`, req.body);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour supprimer un produit
router.delete("/:id", isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.delete(`${PRODUCT_API}/products/${req.params.id}`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

module.exports = router;

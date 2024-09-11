const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');

const DELIVERY_API = process.env.DELIVERY_API;

// Route pour obtenir la liste des commandes
router.get('/', isAuthorized, async (req, res, next) => {
  console.log(DELIVERY_API);

  try {
    const response = await axios.get(DELIVERY_API + '/api/v1/order/');
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour obtenir une commande par son ID
router.get("/:id", async (req, res, next) => {
  try {
    const response = await axios.get(`${DELIVERY_API}/api/v1/order/${req.params.id}`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour créer une nouvelle commande
router.post('/', async (req, res, next) => {
  try {
    const response = await axios.post(DELIVERY_API + '/api/v1/order/', req.body);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour mettre à jour une commande existant
router.put("/:id", async (req, res, next) => {
  try {
    const response = await axios.put(`${DELIVERY_API}/api/v1/order/${req.params.id}`, req.body);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour supprimer une commande
router.delete("/:id", async (req, res, next) => {
  try {
    const response = await axios.delete(`${DELIVERY_API}/api/v1/order/${req.params.id}`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

module.exports = router;

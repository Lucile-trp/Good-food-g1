const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');

const FRANCHISE_API = process.env.FRANCHISE_API;

// Route pour obtenir la liste des franchises
router.get('/', async (req, res, next) => {
  try {
    const response = await axios.get(FRANCHISE_API + '/api/v1/supplier');
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour obtenir un franchise par son ID
router.get("/:id", async (req, res, next) => {
  try {
    const response = await axios.get(`${FRANCHISE_API}/api/v1/supplier/${req.params.id}`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour créer un nouveau franchise
router.post('/', async (req, res, next) => {
  try {
    const response = await axios.post(FRANCHISE_API + '/api/v1/supplier', req.body);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour mettre à jour un franchise existant
router.put("/:id", async (req, res, next) => {
  try {
    const response = await axios.put(`${FRANCHISE_API}/api/v1/supplier/${req.params.id}`, req.body);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour supprimer un franchise
router.delete("/:id", isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.delete(`${FRANCHISE_API}/api/v1/supplier/${req.params.id}`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

module.exports = router;

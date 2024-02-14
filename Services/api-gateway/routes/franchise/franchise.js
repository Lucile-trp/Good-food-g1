const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');
require('dotenv').config();

const FRANCHISE_API = process.env.FRANCHISE_API;

// Route pour obtenir la liste des franchises
router.get('/', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(FRANCHISE_API + '/franchise/api/franchise');
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour obtenir un franchise par son ID
router.get("/:id", isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${FRANCHISE_API}/franchise/api/franchise/${req.params.id}`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour créer un nouveau franchise
router.post('/', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.post(FRANCHISE_API + '/franchise/api/franchise', req.body);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour mettre à jour un franchise existant
router.put("/:id", isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.put(`${FRANCHISE_API}/franchise/api/franchise/${req.params.id}`, req.body);
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
    const response = await axios.delete(`${FRANCHISE_API}/franchise/api/franchise/${req.params.id}`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

module.exports = router;

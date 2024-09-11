const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');

const FRANCHISE_API = process.env.FRANCHISE_API;

// Route pour obtenir la liste des franchises
router.get('/', async (req, res, next) => {
  try {
    const response = await axios.get(FRANCHISE_API + '/api/v1/productType');
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

module.exports = router;

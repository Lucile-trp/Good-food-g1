const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');

const DELIVERY_API = process.env.DELIVERY_API;

// Route pour obtenir la liste des deliverys
router.get('/', isAuthorized, async (req, res, next) => {
  console.log(DELIVERY_API);

  try {
    const response = await axios.get(DELIVERY_API + '/deliveries/');
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour obtenir un delivery par son ID
router.get("/:id", isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${DELIVERY_API}/deliveries/${req.params.id}`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour créer un nouveau delivery
router.post('/', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.post(DELIVERY_API + '/deliveries/', req.body);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour mettre à jour un delivery existant
router.put("/:id", isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.put(`${DELIVERY_API}/deliveries/${req.params.id}`, req.body);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour supprimer un delivery
router.delete("/:id", isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.delete(`${DELIVERY_API}/deliveries/${req.params.id}`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

module.exports = router;

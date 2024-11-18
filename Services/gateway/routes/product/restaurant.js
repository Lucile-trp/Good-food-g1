const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');

const PRODUCT_API = process.env.PRODUCT; // URL de base pour le MS-DELIVERY

// Route pour obtenir la liste de toutes les restaurants
router.get('/', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${PRODUCT_API}/api/v1/restaurants/`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

// Route pour obtenir une restaurant par ID
router.get('/:id', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${PRODUCT_API}/api/v1/restaurant?id=${req.params.id}`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

// Route pour créer un nouvelle restaurant
router.post('/', isAuthorized, async (req, res, next) => {
  const body = req.body;

  try {
    const response = await axios.post(`${PRODUCT_API}/api/v1/restaurant`, body);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);  
    next(err);  
  }
});

module.exports = router;

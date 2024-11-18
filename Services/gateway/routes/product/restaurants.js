const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');

const PRODUCT_API = process.env.PRODUCT_API; // URL de base pour le MS-DELIVERY

// Route pour obtenir la liste de toutes les restaurants
router.get('/', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${PRODUCT_API}/api/v1/restaurants/`);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

module.exports = router;

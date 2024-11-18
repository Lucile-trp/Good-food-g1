const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');

const PRODUCT_API = process.env.PRODUCT; // URL de base pour le MS-DELIVERY

// Route pour obtenir une image par ID
router.get('/', isAuthorized, async (req, res, next) => {
  const { id } = req.query;
  try {
    const response = await axios.get(`${PRODUCT_API}/api/v1/image?id=${id}`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

// Route pour créer un nouvelle image
router.post('/', isAuthorized, async (req, res, next) => {
  const body = req.body;

  try {
    const response = await axios.post(`${PRODUCT_API}/api/v1/image`, body);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);  
    next(err);  
  }
});

module.exports = router;

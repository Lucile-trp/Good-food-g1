const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');

const PRODUCT_API = process.env.PRODUCT_API; 

// // Route pour obtenir un plat par ID
router.get('/', isAuthorized, async (req, res, next) => {
  const { id } = req.query;
  try {
    const response = await axios.get(`${PRODUCT_API}/api/v1/dish?id=${id}`);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

// Route pour créer un nouveu plat
router.post('/', isAuthorized, async (req, res, next) => {
  const body = req.body;

  try {
    const response = await axios.post(`${PRODUCT_API}/api/v1/dish`, body);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);  
    next(err);  
  }
});

module.exports = router;

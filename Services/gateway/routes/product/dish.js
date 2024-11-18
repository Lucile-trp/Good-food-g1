const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');

const PRODUCT_API = process.env.PRODUCT_API; 

// Route pour obtenir la liste de tous les plats
router.get('/', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${PRODUCT_API}/api/v1/dishes/`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

// Route pour obtenir un plat par ID
router.get('/:id', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${PRODUCT_API}/api/v1/dish?id=${req.params.id}`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

// Route pour créer un nouveu plat
router.post('/dish', isAuthorized, async (req, res, next) => {
  const body = req.body;

  try {
    const response = await axios.post(`${PRODUCT_API}/api/v1/dish`, body);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);  
    next(err);  
  }
});

module.exports = router;

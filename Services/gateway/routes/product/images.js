const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');

const PRODUCT_API = process.env.PRODUCT; // URL de base pour le MS-DELIVERY

router.get('/', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${PRODUCT_API}/api/v1/images/`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

module.exports = router;

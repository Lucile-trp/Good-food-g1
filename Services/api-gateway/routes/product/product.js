const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');
require('dotenv').config();

const { PRODUCT_API } = process.env.PRODUCT_API;

router.get('/product', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(PRODUCT_API + req.originalUrl);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    next(err);
  }
});

router.get("/product/:id", isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(PRODUCT_API + req.originalUrl);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    next(err);
  }
});

module.exports = router;

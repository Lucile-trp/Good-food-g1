const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');
require('dotenv').config();

const { DELIVERY_API } = process.env.DELIVERY_API;

router.get('/', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(DELIVERY_API + req.originalUrl);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    next(err);
  }
});

router.get("/:id", isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(DELIVERY_API + req.originalUrl);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    next(err);
  }
});

module.exports = router;

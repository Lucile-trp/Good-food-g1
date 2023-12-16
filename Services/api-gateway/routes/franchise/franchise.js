const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');
require('dotenv').config();

const { FRANCHISE_API } = process.env.FRANCHISE_API;

router.get('/franchise', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(FRANCHISE_API + req.originalUrl);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    next(err);
  }
});

router.get("franchise/:id", isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(FRANCHISE_API + req.originalUrl);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    next(err);
  }
});

module.exports = router;
const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');
require('dotenv').config();

const { MAILING_API } = process.env.MAILING_API;

router.post('/', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.post(MAILING_API + req.originalUrl, req.body);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    next(err);
  }
});

module.exports = router;

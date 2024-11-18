const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');

const DELIVERY_API = process.env.DELIVERY_API;

// Route pour obtenir la liste des deliverys
router.get('/', async (req, res, next) => {
  console.log(DELIVERY_API);

  try {
    const response = await axios.get(`${DELIVERY_API}/api/v1/orderstate/`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

module.exports = router;

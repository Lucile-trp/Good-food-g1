const express = require('express');
const router = express.Router();
const axios = require("axios");
require('dotenv').config();

const { AUTH_API } = process.env.AUTH_API;

// exemple de route pour l'authentification (a modifier si besoins)

router.get('/test', async (req, res, next) => {
  try {
    const response = await axios.get(AUTH_API + req.originalUrl);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    next(err);
  }
});

router.post('/refresh', async (req, res, next) => {
  const axios_instance = axios.create({
    baseURL: AUTH_API,
    timeout: 10000,
    headers: { Authorization: req.headers.authorization }
  });

  try {
    const response = await axios_instance.post("auth/refresh");
    res.set(response.headers);
    res.json(response.data);
  } catch (err) {
    next(err.response.status);
  }
});

router.post('/signin', async (req, res, next) => {
  const axios_instance = axios.create({
    baseURL: AUTH_API,
    timeout: 10000,
    headers: { Authorization: req.headers.authorization }
  });

  try {
    const response = await axios_instance.post("auth/signin", req.body);
    res.set(response.headers);
    res.json(response.data);
  } catch (err) {
    next(err.response.status);
  }
});

router.post('/signup', async (req, res, next) => {
  const axios_instance = axios.create({
    baseURL: AUTH_API,
    timeout: 10000,
    headers: { Authorization: req.headers.authorization }
  });

  try {
    const response = await axios_instance.post("auth/signup", req.body);
    res.set(response.headers);
    res.json(response.data);
  } catch (err) {
    next(err.response.status);
  }
});

module.exports = router;

const express = require('express');
const router = express.Router();
const axios = require("axios");
const isAuthorized = require('../../middlewares/isAuthorized');

const AUTH_API = process.env.AUTH_API;

// exemple de route pour l'authentification (a modifier si besoins)

// Route pour obtenir un delivery par son ID
router.get("/:id", isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${AUTH_API}/users/${req.params.id}`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
    next(err);
  }
});

// Route pour obtenir un delivery par son ID
router.get("/", isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${AUTH_API}/users`);
    res.set(response.headers);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); // Afficher l'erreur dans la console pour le débogage
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
    const response = await axios_instance.post("/auth/api/auth/refresh");
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
    const response = await axios_instance.post("/auth/api/auth/signin", req.body);
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
    const response = await axios_instance.post("/auth/api/auth/signup", req.body);
    res.set(response.headers);
    res.json(response.data);
  } catch (err) {
    next(err.response.status);
  }
});

module.exports = router;

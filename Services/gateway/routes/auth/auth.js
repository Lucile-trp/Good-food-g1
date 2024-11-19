const express = require('express');
const router = express.Router();
const axios = require("axios");
const isAuthorized = require('../../middlewares/isAuthorized');

const AUTH_API = process.env.AUTH_API;


// Route pour obtenir la liste de tous les users
router.get('/', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${AUTH_API}/api/v1/users/`);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

// Route pour obtenir un user par ID
router.get('/:id', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${AUTH_API}/api/v1/users/${req.params.id}`);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

// Route pour obtenir les commandes par adresse de livraison
router.get('/ByMail/:mail', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${AUTH_API}/api/v1/users/ByMail/${req.params.mail}`);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

// Route pour créer une nouvelle commande
router.post('/', isAuthorized, async (req, res, next) => {
  const body = req.body;
  try {
    const response = await axios.post(`${AUTH_API}/api/v1/users`, body);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);  
    next(err);  
  }
});

// Route pour créer une nouvelle commande
router.post('/auth/login', isAuthorized, async (req, res, next) => {
  const body = req.body;

  try {
    const response = await axios.post(`${AUTH_API}/api/v1/users/auth`, body);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);  
    next(err);  
  }
});

// Route pour créer une nouvelle commande
router.post('/auth/register', isAuthorized, async (req, res, next) => {
  const body = req.body;

  try {
    const response = await axios.post(`${AUTH_API}/api/v1/users/register`, body);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);  
    next(err);  
  }
});

// Route pour créer une nouvelle commande
router.post('/auth/verifyAuthorization', isAuthorized, async (req, res, next) => {
  const body = req.body;

  try {
    const response = await axios.post(`${AUTH_API}/api/v1/users/verifyAuthorization`, body);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);  
    next(err);  
  }
});

module.exports = router;

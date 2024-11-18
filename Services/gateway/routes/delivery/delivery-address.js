const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');

const DELIVERY_API = process.env.DELIVERY_API;

// Route pour obtenir la liste des adresses de livraison
router.get('/', async (req, res, next) => {
  try {
    const response = await axios.get(`${DELIVERY_API}/api/v1/deliveryAddress/`);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); 
    next(err);
  }
});

// Route pour obtenir une adresse de livraison par son ID
router.get('/:id', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${DELIVERY_API}/api/v1/deliveryAddress/${req.params.id}`);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); 
    next(err);
  }
});

// Route pour obtenir les adresses de livraison par client
router.get('/ByCustomer/:customerId', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${DELIVERY_API}/api/v1/deliveryAddress/ByCustomer/${req.params.customerId}`);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); 
    next(err);
  }
});

// Route pour obtenir l'adresse de livraison par ID de commande
router.get('/ByOrder/:orderId', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${DELIVERY_API}/api/v1/deliveryAddress/ByOrder/${req.params.orderId}`);
    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); 
    next(err);
  }
});

// Route pour créer une nouvelle adresse de livraison
router.post('/', isAuthorized, async (req, res, next) => {
  const { customerId } = req.query; 
  const body = req.body; 

  if (!customerId) {
    return res.status(400).json({ error: "customerId is required in query string" });
  }

  try {
    const response = await axios.post(`${DELIVERY_API}/api/v1/deliveryAddress?customerId=${customerId}`, body);
    res.status(response.status).json(response.data); 
  } catch (err) {
    console.error(err);  
    next(err);  
  }
});

// Route pour mettre à jour une adresse de livraison existante
router.put('/:id', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.put(`${DELIVERY_API}/api/v1/deliveryAddress/${req.params.id}`, req.body);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); 
    next(err);
  }
});

// Route pour supprimer une adresse de livraison
router.delete("/:id", isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.delete(`${DELIVERY_API}/api/v1/deliveryAddress/${req.params.id}`);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error("DELETE error:", err.response ? err.response.data : err.message); // Log détaillé
    next(err);
  }
});


module.exports = router;

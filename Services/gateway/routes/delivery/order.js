const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');

const DELIVERY_API = process.env.DELIVERY_API; // URL de base pour le MS-DELIVERY

// Route pour obtenir la liste de toutes les commandes
router.get('/', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${DELIVERY_API}/api/v1/order/`);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

// Route pour obtenir une commande par ID
router.get('/:id', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${DELIVERY_API}/api/v1/order/${req.params.id}`);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

// Route pour obtenir les commandes par ID client
router.get('/ByCustomer/:customerId', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${DELIVERY_API}/api/v1/order/ByCustomer/${req.params.customerId}`);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

// Route pour obtenir les commandes par ID livreur
router.get('/byDeliverer/:delivererId', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${DELIVERY_API}/api/v1/order/byDeliverer/${req.params.delivererId}`);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

// Route pour obtenir les commandes par adresse de livraison
router.get('/ByDeliveryAddress/:deliveryAddressId', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${DELIVERY_API}/api/v1/order/ByDeliveryAddress/${req.params.deliveryAddressId}`);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);
    next(err);
  }
});

// Route pour obtenir les commandes par état
router.get('/ByState/:state', isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.get(`${DELIVERY_API}/api/v1/order/ByState/${req.params.state}`);

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
    const response = await axios.post(`${DELIVERY_API}/api/v1/order`, body);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err);  
    next(err);  
  }
});

// Route pour mettre à jour une commande existante
router.put('/:orderId', isAuthorized, async (req, res, next) => {
  const { delivererId, deliveryAddressId } = req.query;
  const body = req.body;

  if (!delivererId || !deliveryAddressId) {
    return res.status(400).json({ error: "delivererId and deliveryAddressId are required in query string" });
  }

  try {
    const response = await axios.put(`${DELIVERY_API}/api/v1/order/${req.params.orderId}`, body);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error(err); 
    next(err);
  }
});

// Route pour supprimer une commande
router.delete("/:orderId", isAuthorized, async (req, res, next) => {
  try {
    const response = await axios.delete(`${DELIVERY_API}/api/v1/order/${req.params.orderId}`);

    res.status(response.status).json(response.data);
  } catch (err) {
    console.error("DELETE error:", err.response ? err.response.data : err.message); // Log détaillé
    next(err);
  }
});

module.exports = router;

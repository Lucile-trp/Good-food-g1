const express = require('express');
const bodyParser = require('body-parser');
const isAuthorized = require('./middlewares/isAuthorized');

const authRoutes = require('./routes/auth/auth');
const productRoutes = require('./routes/product/product');
const deliveryRoutes = require('./routes/delivery/delivery');
const franchiseRoutes = require('./routes/franchise/franchise');
const mailingRoutes = require('./routes/mailing/mailing');

const app = express();
const GATEWAY_PORT = process.env.GATEWAY_PORT;

app.use(bodyParser.json());

// Routes pour AUTH
app.use('/auth/api', authRoutes);

// Routes pour PRODUCT
app.use('/product/api', isAuthorized, productRoutes);

// Routes pour DELIVERY
app.use('/delivery/api', isAuthorized, deliveryRoutes);

// Routes pour FRANCHISE
app.use('/franchise/api', isAuthorized, franchiseRoutes);

// Routes pour MAILING
app.use('/mailing/api', isAuthorized, mailingRoutes);

// Gestion des erreurs
app.use((err, req, res, next) => {
  console.error(err);
  res.status(err || 500).json({ error: err.message || 'Internal Server Error' });
});

// Démarrage du serveur
app.listen(GATEWAY_PORT, () => {
  console.log(`Server is running on http://localhost:${GATEWAY_PORT}`);
});

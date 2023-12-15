const express = require('express');
const bodyParser = require('body-parser');
const isAuthorized = require('./middlewares/isAuthorized');

const authRoutes = require('./routes/auth');
const productRoutes = require('./routes/product');
const deliveryRoutes = require('./routes/delivery');
const franchiseRoutes = require('./routes/franchise');
const mailingRoutes = require('./routes/mailing');

const app = express();
const GATEWAY_PORT = process.env.GATEWAY_PORT;

app.use(bodyParser.json());

// Routes pour AUTH
app.use('/auth', authRoutes);

// Routes pour PRODUCT
app.use('/product', isAuthorized, productRoutes);

// Routes pour DELIVERY
app.use('/delivery', isAuthorized, deliveryRoutes);

// Routes pour FRANCHISE
app.use('/franchise', isAuthorized, franchiseRoutes);

// Routes pour MAILING
app.use('/mailing', isAuthorized, mailingRoutes);

// Gestion des erreurs
app.use((err, req, res, next) => {
  console.error(err);
  res.status(err || 500).json({ error: err.message || 'Internal Server Error' });
});

// Démarrage du serveur
app.listen(GATEWAY_PORT, () => {
  console.log(`Server is running on http://localhost:${GATEWAY_PORT}`);
});

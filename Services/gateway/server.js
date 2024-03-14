const express = require('express');
const bodyParser = require('body-parser');
const isAuthorized = require('./middlewares/isAuthorized');

const authRoutes = require('./routes/auth/auth');
const productRoutes = require('./routes/product/product');
const deliveryRoutes = require('./routes/delivery/delivery');
const franchiseRoutes = require('./routes/franchise/franchise');
const mailingRoutes = require('./routes/mailing/mailing');

const app = express();

app.use(bodyParser.json());

// Routes du MS-AUTH
app.use('/api/auth/', authRoutes);

// Routes du MS-PRODUCT 
app.use('/api/product/', productRoutes);

// Routes du MS-DELIVERY 
app.use('/api/delivery/', deliveryRoutes);

// Routes du MS-FRANCHISE 
app.use('/api/franchise/', franchiseRoutes);

// Routes du MS-MAILING 
app.use('/api/mailing/', mailingRoutes);

// Gestion des erreurs
app.use((err, req, res, next) => {
  console.error(err);
  res.status(err || 500).json({ error: err.message || 'Internal Server Error' });
});

// Démarrage du serveur
app.listen(8080, () => {
  console.log(`Server is running on http://localhost:8080`);
});

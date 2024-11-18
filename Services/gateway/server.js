const express = require('express');
const bodyParser = require('body-parser');

const authRoutes = require('./routes/auth/auth');
const dishRoutes = require('./routes/product/dish');
const restaurantRoutes = require('./routes/product/restaurant');
const imageRoutes = require('./routes/product/image');
const dishesRoutes = require('./routes/product/dishes');
const restaurantsRoutes = require('./routes/product/restaurants');
const imagesRoutes = require('./routes/product/images');
const orderDeliveryRoutes = require('./routes/delivery/order');
const deliveryAddressRoutes = require('./routes/delivery/delivery-address');
const orderStateRoutes = require('./routes/delivery/order-state');
const supplierRoutes = require('./routes/franchise/supplier');
const orderFranchiseRoutes = require('./routes/franchise/order');
const productTypeRoutes = require('./routes/franchise/product-type');
const mailingRoutes = require('./routes/mailing/mailing');

const app = express();

app.use(bodyParser.json());

// Routes du MS-AUTH
app.use('/api/user/', authRoutes);

// Routes du MS-PRODUCT 
app.use('/api/v1/dish', dishRoutes);  
app.use('/api/v1/restaurant', restaurantRoutes);
app.use('/api/v1/image', imageRoutes);

// Routes du MS-PRODUCT 
app.use('/api/v1/dishes', dishesRoutes);  
app.use('/api/v1/restaurants', restaurantsRoutes);
app.use('/api/v1/images', imagesRoutes);

// Routes du MS-DELIVERY 
app.use('/api/v1/order/', orderDeliveryRoutes);
app.use('/api/v1/deliveryAddress/', deliveryAddressRoutes);
app.use('/api/v1/orderState/', orderStateRoutes);

// Routes du MS-FRANCHISE 
app.use('/api/v1/supplier/', supplierRoutes);
app.use('/api/v1/order', orderFranchiseRoutes);
app.use('/api/v1/productType/', productTypeRoutes);

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

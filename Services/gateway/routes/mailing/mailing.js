const express = require('express');
const router = express.Router();
const axios = require('axios');
const isAuthorized = require('../../middlewares/isAuthorized');

const MAILING_API = process.env.MAILING_API;

module.exports = router;

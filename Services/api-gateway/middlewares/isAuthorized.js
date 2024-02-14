const validator = require('validator');
const axios = require('axios');
const { AUTH_API } = process.env;

const isAuthorized = async (req, res, next) => {
    if (
        req.headers.authorization === undefined ||
        req.headers.authorization === null
    ) {
        next(400);
    } else {
        const axios_instance = axios.create({
            baseURL: AUTH_API,
            timeout: 10000,
            headers: { Authorization: req.headers.authorization }
        });

        try {
            const response = await axios_instance.post(AUTH_API + '/auth/api/auth/verify');

            if (response.status === 200) {
                next();
            } else {
                next(400);
            }
        } catch (err) {
            if (err.response && err.response.status) {
                next(err.response.status);
            } else {
                next(500);
            }
        }
    }
};

module.exports = isAuthorized;

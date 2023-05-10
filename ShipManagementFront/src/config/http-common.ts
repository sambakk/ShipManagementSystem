import axios from "axios";
const { REACT_APP_BACKEND_URL } = process.env;

/**
 * Axios helper
 */
export default axios.create({
    baseURL: REACT_APP_BACKEND_URL,
    headers: {
        "Content-type": "application/json"
    }
});
import axios, { AxiosInstance } from "axios";
import router from '../routers';

const _baseURL = import.meta.env.VITE_BASE_API_URL.toString();
const _timeOut = import.meta.env.VITE_BASE_API_TIMEOUT;

const _httpClient: AxiosInstance = axios.create({
    baseURL: _baseURL,
    headers: {
        'Access-Control-Allow-Origin': '*',
        "Content-type": "application/json"
    },
    timeout: _timeOut,
    validateStatus: function (status) {
        return status >= 100 && status < 1000;
    },
});

_httpClient.defaults.withCredentials = false;

// Cấu hình response điều hướng
_httpClient.interceptors.response.use(
    function (response) {
        if (response.status == 401 || response.data.Code == 401) {
            router.push({ name: 'LoginPage' });
            return;
        }
        if (response.status == 403) {
            window.location.href = '/403';
            return;
        }

        return response;
    },
    function (error) {
        return Promise.reject(error)
    });

export default _httpClient;
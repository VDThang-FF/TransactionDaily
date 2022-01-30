import axios, { AxiosInstance } from "axios";
import router from '../routers';
import { useCookies } from "vue3-cookies";

const _baseURL = import.meta.env.VITE_BASE_API_URL.toString();
const _timeOut = import.meta.env.VITE_BASE_API_TIMEOUT;
const { cookies } = useCookies();
const headerCookie = JSON.stringify({
    "SessionID": cookies.get("SessionID"),
    "UserID": cookies.get("UserID"),
    "UserName": cookies.get("UserName"),
    "Email": cookies.get("Email"),
    "DeviceID": cookies.get("DeviceID")
});

const _httpClient: AxiosInstance = axios.create({
    baseURL: _baseURL,
    headers: {
        'Access-Control-Allow-Origin': '*',
        'Content-type': 'application/json',
        VDT: headerCookie,
    },
    timeout: _timeOut,
    validateStatus: function (status) {
        return status >= 100 && status < 1000;
    },
});

// Cấu hình request trước khi gửi
_httpClient.interceptors.request.use(function (config) {
    // Kiểm tra lại cấu hình cookie còn đúng với instance hiện tại không
    const currentCookie = JSON.stringify({
        "SessionID": cookies.get("SessionID"),
        "UserID": cookies.get("UserID"),
        "UserName": cookies.get("UserName"),
        "Email": cookies.get("Email"),
        "DeviceID": cookies.get("DeviceID")
    });

    const requestCookie = config.headers["VDT"];
    if (currentCookie !== requestCookie)
        config.headers["VDT"] = currentCookie;
        
    return config;
}, function (error) {
    return Promise.reject(error);
})

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
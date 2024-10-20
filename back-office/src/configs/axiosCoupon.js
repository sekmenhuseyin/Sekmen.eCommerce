import axios from "axios";
import { getCouponOrigin } from "./origins";

const redirectToLogin = () => (window.location.href = "/login");
const redirectToForbidden = () => (window.location.href = "/forbidden");

export const urlSerializer = (params) => {
  const searchParams = new URLSearchParams();
  for (const key of Object.keys(params)) {
    const param = params[key];
    if (Array.isArray(param)) {
      for (const p of param) {
        searchParams.append(key, p);
      }
    }
    else if (params[key] !== null && params[key] !== undefined) {
      searchParams.append(key, param);
    }
  }
  return searchParams;
}

export const urlDeserializer = (queryString) => {
  const pairs = queryString.slice(1).split('&');

  const result = {};
  pairs.forEach(function(pair) {
    pair = pair.split('=');
    result[pair[0]] = decodeURIComponent(pair[1] || '');
  });

  return JSON.parse(JSON.stringify(result));
}

const client = axios.create({
  baseURL: getCouponOrigin(),
  timeout: 5000,
  headers: {
    "Content-Type": "application/json",
    Accept: "application/json"
  }
});

client.interceptors.request.use(
  async config => {
    try {
      const user = JSON.parse(localStorage.getItem("token"));
      if (user && user.access_token)
        config.headers.Authorization = `Bearer ${user.access_token}`;
    } catch { }
    return config;
  },
  error => {
    Promise.reject(error);
  }
);

client.interceptors.response.use(
  response => response,
  error => {
    if (error.response && error.response.status === 403)
      redirectToForbidden()
    if (error.response && error.response.status === 401)
      redirectToLogin()
    return Promise.reject(error)
  }
);

export default client

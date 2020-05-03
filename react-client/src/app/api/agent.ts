import axios, { AxiosResponse } from "axios";
import { history } from "../..";
import { toast } from "react-toastify";
import {IUser, IUserForm } from "../models/user";

axios.defaults.baseURL = "https://localhost:5001/api";

axios.interceptors.request.use(
  (config) => {
    const token = window.localStorage.getItem("jwt");
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axios.interceptors.response.use(undefined, (error) => {
  if (error.message === "Network Error" && !error.response) {
    console.error("Network error!");
    toast.info("Network error");
  }

  const { status, data, config, headers } = error.response;

  if (status === 404) {
    history.push("/notfound");
  }
  if (status === 401 && headers["www-authenticate"]) {
    window.localStorage.removeItem("jwt");
    history.push("/");
    console.info("Login again");
    toast.info("Login again");
  }

  if (
    status === 400 &&
    config.method === "get" &&
    data.errors.hasOwnProperty("id")
  ) {
    history.push("/notfound");
  }

  if (status === 500) {
    console.error("Server error - check terminal for more info");
    toast.info("Internal server error");
  }

  throw error.response;
});

const responseBody = (response: AxiosResponse) => response.data;

const request = {
  get: (url: string) => axios.get(url).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  del: (url: string) => axios.delete(url).then(responseBody),
};

const User = {
  getCurrentUser: (): Promise<IUser> => request.get("/user"),
  login : (loginForm : IUserForm) : Promise<IUser> => request.post("/user/login", loginForm), 
  register : (userForm : IUserForm) : Promise<IUser> => request.post("/user/register", userForm),
};

export default {
  User,
};

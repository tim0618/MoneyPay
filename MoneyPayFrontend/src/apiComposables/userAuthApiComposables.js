import axios from "axios";

const API_BASE_URL = "http://localhost:5140/api/User";

export const auth = () => {
  const loginApi = async (email, password) => {
    try {
      const response = await axios.post(`${API_BASE_URL}/Login`, {
        email: email,
        password: password,
      });

      return response.data;
    } catch (e) {
      console.error("Login API error:", e);
      throw e;
    }
  };

  const registerApi = async (email, password, name) => {
    try {
      const response = await axios.post(`${API_BASE_URL}/Register`, {
        email: email,
        password: password,
        name: name,
      });

      return response.data;
    } catch (e) {
      console.error("Register API error:", e);
      throw e;
    }
  };

  return {
    loginApi,
    registerApi,
  };
};

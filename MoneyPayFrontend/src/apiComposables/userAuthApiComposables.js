import axios from "axios";

const API_BASE_URL = "http://localhost:5140/api/User";

export const auth = () => {
  const loginApi = async (payload) => {
    try {
      const response = await axios.post(`${API_BASE_URL}/Login`, payload);
      return response.data;
    } catch (e) {
      console.error("Login API error:", e);
      throw e;
    }
  };

  const registerApi = async (payload) => {
    try {
      const response = await axios.post(`${API_BASE_URL}/Register`, payload);
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

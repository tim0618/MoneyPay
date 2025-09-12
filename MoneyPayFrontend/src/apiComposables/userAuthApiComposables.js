import axios from "axios";

const API_BASE_URL = "http://localhost:5140/api/User";

export const auth = () => {
  const loginApi = async (email, password) => {
    try {
      const response = await axios.post(`${API_BASE_URL}/login`, {
        email: email,
        password: password,
      });

      return response.data;
    } catch (e) {
      console.error("Login API error:", e);
      throw e;
    }
  };

  return { loginApi };
};

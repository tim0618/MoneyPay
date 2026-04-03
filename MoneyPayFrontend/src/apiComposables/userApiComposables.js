import axios from "axios";

const API_BASE_URL = "http://localhost:5140/api/User";

export const userApi = () => {
  const getProfileApi = async (token) => {
    try {
      const response = await axios.get(`${API_BASE_URL}/GetProfile`, {
        headers: { Authorization: `Bearer ${token}` },
      });
      return response.data;
    } catch (e) {
      console.error("Get Profile API error:", e);
      throw e;
    }
  };

  const updateProfileApi = async (token, payload) => {
    try {
      const response = await axios.put(`${API_BASE_URL}/UpdateProfile`, payload, {
        headers: { Authorization: `Bearer ${token}` },
      });
      return response.data;
    } catch (e) {
      console.error("Update Profile API error:", e);
      throw e;
    }
  };

  const changePasswordApi = async (token, payload) => {
    try {
      const response = await axios.put(`${API_BASE_URL}/ChangePassword`, payload, {
        headers: { Authorization: `Bearer ${token}` },
      });
      return response.data;
    } catch (e) {
      console.error("Change Password API error:", e);
      throw e;
    }
  };

  return {
    getProfileApi,
    updateProfileApi,
    changePasswordApi,
  };
};

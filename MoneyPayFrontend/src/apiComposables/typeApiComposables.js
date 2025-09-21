import axios from "axios";

const API_BASE_URL = "http://localhost:5140/api/Type";

export const typeApi = () => {
  const getMoneyTypesSumApi = async (token) => {
    try {
      const response = await axios.get(`${API_BASE_URL}/GetMoneyTypesSum`, {
        headers: { Authorization: `Bearer ${token}` },
      });
      return response.data;
    } catch (e) {
      console.error("Get All Types API error:", e);
      throw e;
    }
  };

  return { getMoneyTypesSumApi };
};

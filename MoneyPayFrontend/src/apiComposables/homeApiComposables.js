import axios from "axios";

const API_BASE_URL = "http://localhost:5140/api/Home";

export const homeApi = () => {
  const getHomeSummaryApi = async (token, year, month) => {
    try {
      const response = await axios.get(`${API_BASE_URL}/GetSummary`, {
        params: { year, month },
        headers: { Authorization: `Bearer ${token}` },
      });
      return response.data;
    } catch (e) {
      console.error("Get Home Summary API error:", e);
      throw e;
    }
  };

  return {
    getHomeSummaryApi,
  };
};

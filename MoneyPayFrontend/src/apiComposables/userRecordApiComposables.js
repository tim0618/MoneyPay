import axios from "axios";

const API_BASE_URL = "http://localhost:5140/api/UserRecord";

export const userRecordApi = () => {
  const GetBookRecordsApi = async (token) => {
    try {
      const response = await axios.get(`${API_BASE_URL}/GetBookRecords`, {
        headers: { Authorization: `Bearer ${token}` },
      });
      return response.data;
    } catch (e) {
      console.error("Get BookRecords Api", e);
      throw e;
    }
  };
  return {
    GetBookRecordsApi
  }
};



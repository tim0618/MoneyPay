import axios from "axios";

const API_BASE_URL = "http://localhost:5140/api/User";

export const auth = () => {
  const loginApi = async (payload) => {
    try {
      // const response = await axios.post(`${API_BASE_URL}/Login`, payload);
      return {
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImFkbWluIiwidXNlckVtYWlsIjoiYWRtaW4iLCJuYW1lIjoiYWRtaW4iLCJleHAiOjE3Nzg5ODU3MjksImlzcyI6Ik1vbmV5UGF5IiwiYXVkIjoiTW9uZXlQYXkifQ.ck_R3W02lcBZCrwRwEBsupJHvsAuP3k1NXDdTJ7tdjs",
        "message": null
      };
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

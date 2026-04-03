import axios from "axios";

const API_BASE_URL = "http://localhost:5140/api/MoneyType";

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

  const getMoneyTypesApi = async (token, type) => {
    try {
      const response = await axios.get(`${API_BASE_URL}/GetMoneyTypes`, {
        params: type ? { type } : undefined,
        headers: { Authorization: `Bearer ${token}` },
      });
      return response.data;
    } catch (e) {
      console.error("Get Money Types API error:", e);
      throw e;
    }
  };

  const getTypeRemarkApi = async (token, typeId) => {
    try {
      const response = await axios.get(`${API_BASE_URL}/GetTypeRemark/${typeId}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      return response.data;
    } catch (e) {
      console.error("Get Type Remark API error",e);
      throw e;
    }
  };

  const createMoneyTypeApi = async (token, payload) => {
    try {
      const response = await axios.post(
        `${API_BASE_URL}/CreateMoneyType`,
        payload,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      return response.data;
    } catch (e) {
      console.error("Create Money Type API error", e);
      throw e;
    }
  };

  const createSubMoneyTypeApi = async (token, parentId, payload) => {
    try {
      const response = await axios.post(
        `${API_BASE_URL}/CreateSubMoneyType/${parentId}`,
        payload,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      return response.data;
    } catch (e) {
      console.error("Create Sub Money Type API error", e);
      throw e;
    }
  };

  const updateSubMoneyTypeApi = async (token, subCategoryId, payload) => {
    try {
      const response = await axios.put(
        `${API_BASE_URL}/UpdateSubMoneyType/${subCategoryId}`,
        payload,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      return response.data;
    } catch (e) {
      console.error("Update Sub Money Type API error", e);
      throw e;
    }
  };

  const deleteMoneyTypeApi = async (token, typeId) => {
    try {
      const response = await axios.delete(
        `${API_BASE_URL}/DeleteMoneyType/${typeId}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      return response.data;
    } catch (e) {
      console.error("Delete Money Type API error", e);
      throw e;
    }
  };

  const deleteSubMoneyTypeApi = async (token, subCategoryId) => {
    try {
      const response = await axios.delete(
        `${API_BASE_URL}/DeleteSubMoneyType/${subCategoryId}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      return response.data;
    } catch (e) {
      console.error("Delete Sub Money Type API error", e);
      throw e;
    }
  };

  const getDeletedMoneyTypesApi = async (token, type) => {
    try {
      const response = await axios.get(`${API_BASE_URL}/GetDeletedMoneyTypes`, {
        params: type ? { type } : undefined,
        headers: { Authorization: `Bearer ${token}` },
      });
      return response.data;
    } catch (e) {
      console.error("Get Deleted Money Types API error:", e);
      throw e;
    }
  };

  const restoreMoneyTypeApi = async (token, typeId) => {
    try {
      const response = await axios.put(
        `${API_BASE_URL}/RestoreMoneyType/${typeId}`,
        null,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      return response.data;
    } catch (e) {
      console.error("Restore Money Type API error", e);
      throw e;
    }
  };

  const restoreSubMoneyTypeApi = async (token, subCategoryId) => {
    try {
      const response = await axios.put(
        `${API_BASE_URL}/RestoreSubMoneyType/${subCategoryId}`,
        null,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      return response.data;
    } catch (e) {
      console.error("Restore Sub Money Type API error", e);
      throw e;
    }
  };

  const addTypePay = async (token, payload) => {
    try {
      const response = await axios.post(`${API_BASE_URL}/AddTypePay`, payload, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      return response.data;
    } catch (e) {
      console.error("Add Type Pay API error",e);
      throw e;
    }
  };

  return {
    getMoneyTypesSumApi,
    getMoneyTypesApi,
    getTypeRemarkApi,
    createMoneyTypeApi,
    createSubMoneyTypeApi,
    updateSubMoneyTypeApi,
    deleteMoneyTypeApi,
    deleteSubMoneyTypeApi,
    getDeletedMoneyTypesApi,
    restoreMoneyTypeApi,
    restoreSubMoneyTypeApi,
    addTypePay,
  };
};

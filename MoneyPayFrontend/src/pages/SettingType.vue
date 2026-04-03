<template>
  <div class="settingPage pixel-page">
    <div v-if="loading" class="pixel-card state-card">{{ t("common.loading") }}</div>
    <div v-else class="sectionStack">
      <CategorySection
        :title="t('categories.expenseSection')"
        category-type="Expense"
        :categories="expenseTypes"
        @create-main="openMainDialog"
        @create-subcategory="openSubDialog"
        @edit-subcategory="openEditSubDialog"
        @delete-category="handleDeleteCategory"
        @delete-subcategory="handleDeleteSubcategory"
      />

      <CategorySection
        :title="t('categories.incomeSection')"
        category-type="Income"
        :categories="incomeTypes"
        @create-main="openMainDialog"
        @create-subcategory="openSubDialog"
        @edit-subcategory="openEditSubDialog"
        @delete-category="handleDeleteCategory"
        @delete-subcategory="handleDeleteSubcategory"
      />
    </div>

    <CategoryFormDialog
      v-model="showDialog"
      :mode="dialogMode"
      :category-type="activeCategoryType"
      :parent-category="selectedParent"
      :subcategory="selectedSubcategory"
      @submit="handleSubmit"
    />
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { typeApi } from "../apiComposables/typeApiComposables";
import { useAppPreferences } from "../composables/useAppPreferences";
import CategorySection from "../components/categories/CategorySection.vue";
import CategoryFormDialog from "../components/dialogs/CategoryFormDialog.vue";

const router = useRouter();
const { t } = useAppPreferences();
const {
  getMoneyTypesApi,
  createMoneyTypeApi,
  createSubMoneyTypeApi,
  updateSubMoneyTypeApi,
  deleteMoneyTypeApi,
  deleteSubMoneyTypeApi,
} = typeApi();

const loading = ref(true);
const expenseTypes = ref([]);
const incomeTypes = ref([]);
const showDialog = ref(false);
const dialogMode = ref("main");
const activeCategoryType = ref("Expense");
const selectedParent = ref(null);
const selectedSubcategory = ref(null);

const getMoneyTypes = async () => {
  try {
    loading.value = true;

    const token = localStorage.getItem("token");
    if (!token) {
      throw new Error("No token found");
    }

    const [expenseResult, incomeResult] = await Promise.all([
      getMoneyTypesApi(token, "expense"),
      getMoneyTypesApi(token, "income"),
    ]);

    expenseTypes.value = expenseResult;
    incomeTypes.value = incomeResult;
  } catch (error) {
    if (error.message === "No token found") {
      window.alert(t("common.loginRequired"));
      router.push("/login");
      return;
    }

    console.error("Get Money Types Error", error);
    window.alert(t("categories.loadError"));
  } finally {
    loading.value = false;
  }
};

const openMainDialog = (categoryType) => {
  dialogMode.value = "main";
  activeCategoryType.value = categoryType;
  selectedParent.value = null;
  selectedSubcategory.value = null;
  showDialog.value = true;
};

const openSubDialog = (category) => {
  dialogMode.value = "sub";
  activeCategoryType.value = category.categoryType;
  selectedParent.value = category;
  selectedSubcategory.value = null;
  showDialog.value = true;
};

const openEditSubDialog = ({ category, parentCategory }) => {
  dialogMode.value = "edit-sub";
  activeCategoryType.value = parentCategory.categoryType;
  selectedParent.value = parentCategory;
  selectedSubcategory.value = category;
  showDialog.value = true;
};

const handleSubmit = async (form) => {
  try {
    const token = localStorage.getItem("token");
    if (!token) {
      throw new Error("No token found");
    }

    if (dialogMode.value === "main") {
      await createMoneyTypeApi(token, {
        typeName: form.name,
        icon: form.icon,
        color: form.color,
        categoryType: activeCategoryType.value,
      });
      window.alert(t("categories.createSuccess"));
    } else if (dialogMode.value === "sub") {
      await createSubMoneyTypeApi(token, selectedParent.value.id, {
        typeName: form.name,
      });
      window.alert(t("categories.createSubSuccess"));
    } else {
      await updateSubMoneyTypeApi(token, selectedSubcategory.value.id, {
        typeName: form.name,
      });
      window.alert(t("categories.updateSubSuccess"));
    }

    showDialog.value = false;
    await getMoneyTypes();
  } catch (error) {
    console.error("Save Category Error", error);
    window.alert(error.response?.data || t("categories.loadError"));
  }
};

const handleDeleteCategory = async (category) => {
  const confirmed = window.confirm(
    t("categories.deleteCategoryConfirm", { name: category.name })
  );

  if (!confirmed) {
    return;
  }

  try {
    const token = localStorage.getItem("token");
    if (!token) {
      throw new Error("No token found");
    }

    await deleteMoneyTypeApi(token, category.id);
    window.alert(t("categories.deleteSuccess"));
    await getMoneyTypes();
  } catch (error) {
    console.error("Delete Category Error", error);
    window.alert(error.response?.data || t("categories.loadError"));
  }
};

const handleDeleteSubcategory = async ({ category, parentCategory }) => {
  const confirmed = window.confirm(
    t("categories.deleteSubConfirm", {
      name: category.name,
      parent: parentCategory.name,
    })
  );

  if (!confirmed) {
    return;
  }

  try {
    const token = localStorage.getItem("token");
    if (!token) {
      throw new Error("No token found");
    }

    await deleteSubMoneyTypeApi(token, category.id);
    window.alert(t("categories.deleteSubSuccess"));
    await getMoneyTypes();
  } catch (error) {
    console.error("Delete Subcategory Error", error);
    window.alert(error.response?.data || t("categories.loadError"));
  }
};

onMounted(() => {
  getMoneyTypes();
});
</script>

<style scoped>
.settingPage {
  display: flex;
  flex-direction: column;
  gap: 18px;
}

.sectionStack {
  display: flex;
  flex-direction: column;
  gap: 16px;
}
</style>

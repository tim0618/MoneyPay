<template>
  <div class="settingsPage pixel-page">
    <section class="heroCard pixel-card">
      <div class="heroText">
        <span class="heroEyebrow">{{ t("common.appName") }}</span>
        <h2 class="heroTitle">
          {{ profile.name || t("settings.userFallback") }}
        </h2>
        <p class="heroSubtitle">{{ profile.email }}</p>
      </div>

      <div class="heroStats">
        <div class="heroStat">
          <span class="heroStatLabel">{{ t("settings.summaryStreak") }}</span>
          <strong class="heroStatValue">{{
            recordStats.recordContinuous || 0
          }}</strong>
        </div>
        <div class="heroStat">
          <span class="heroStatLabel">{{ t("settings.summaryRecords") }}</span>
          <strong class="heroStatValue">{{
            recordStats.recordCount || 0
          }}</strong>
        </div>
      </div>
    </section>

    <div v-if="pageError" class="pixel-card state-card">
      <p>{{ pageError }}</p>
      <button
        class="pixel-button secondary retryButton"
        type="button"
        @click="initializePage"
      >
        {{ t("common.retry") }}
      </button>
    </div>

    <template v-else>
      <section class="sectionCard pixel-card">
        <div class="sectionHeader">
          <div>
            <h3 class="sectionTitle">{{ t("settings.summaryTitle") }}</h3>
            <p class="sectionHint">{{ t("settings.summaryHint") }}</p>
          </div>
        </div>

        <div class="summaryGrid">
          <article class="summaryItem pixel-card">
            <span class="summaryLabel">{{ t("settings.summaryStreak") }}</span>
            <strong class="summaryValue">{{
              recordStats.recordContinuous || 0
            }}</strong>
            <span class="summaryHint">{{
              t("settings.summaryContinuousHint")
            }}</span>
          </article>
          <article class="summaryItem pixel-card">
            <span class="summaryLabel">{{ t("settings.summaryDays") }}</span>
            <strong class="summaryValue">{{
              recordStats.recordDaysCount || 0
            }}</strong>
            <span class="summaryHint">{{ t("settings.summaryDaysHint") }}</span>
          </article>
          <article class="summaryItem pixel-card">
            <span class="summaryLabel">{{ t("settings.summaryRecords") }}</span>
            <strong class="summaryValue">{{
              recordStats.recordCount || 0
            }}</strong>
            <span class="summaryHint">{{
              t("settings.summaryRecordsHint")
            }}</span>
          </article>
        </div>
      </section>

      <section class="sectionCard pixel-card">
        <div class="sectionHeader">
          <div>
            <h3 class="sectionTitle">{{ t("settings.appearanceTitle") }}</h3>
            <p class="sectionHint">{{ t("settings.appearanceHint") }}</p>
          </div>
        </div>

        <div class="preferenceGrid">
          <div class="field-stack">
            <span class="field-label">{{ t("settings.themeTitle") }}</span>
            <div class="segmented-group">
              <button
                class="segmented-option"
                :class="{ active: theme === 'light' }"
                type="button"
                @click="setTheme('light')"
              >
                {{ t("settings.themeLight") }}
              </button>
              <button
                class="segmented-option"
                :class="{ active: theme === 'dark' }"
                type="button"
                @click="setTheme('dark')"
              >
                {{ t("settings.themeDark") }}
              </button>
            </div>
          </div>

          <div class="field-stack">
            <span class="field-label">{{ t("settings.languageTitle") }}</span>
            <div class="segmented-group">
              <button
                class="segmented-option"
                :class="{ active: locale === 'zh-TW' }"
                type="button"
                @click="setLocale('zh-TW')"
              >
                {{ t("settings.languageZh") }}
              </button>
              <button
                class="segmented-option"
                :class="{ active: locale === 'en' }"
                type="button"
                @click="setLocale('en')"
              >
                {{ t("settings.languageEn") }}
              </button>
            </div>
          </div>
        </div>
      </section>

      <section class="sectionCard pixel-card">
        <div class="sectionHeader">
          <div>
            <h3 class="sectionTitle">{{ t("settings.profileTitle") }}</h3>
            <p class="sectionHint">{{ t("settings.profileHint") }}</p>
          </div>
        </div>

        <div class="formGrid">
          <label class="field-stack">
            <span class="field-label">{{ t("auth.name") }}</span>
            <input
              v-model="profileForm.name"
              class="pixel-input"
              type="text"
              :placeholder="t('auth.name')"
            />
          </label>

          <label class="field-stack">
            <span class="field-label">{{ t("auth.email") }}</span>
            <input
              :value="profile.email"
              class="pixel-input disabledInput"
              type="text"
              readonly
            />
          </label>
        </div>

        <div class="inlineMeta">
          {{ t("settings.joinedDate", { date: formattedJoinDate }) }}
        </div>

        <div class="actionsRow">
          <button class="pixel-button" type="button" @click="saveProfile">
            {{ t("common.save") }}
          </button>
        </div>
      </section>

      <section class="sectionCard pixel-card">
        <div class="sectionHeader">
          <div>
            <h3 class="sectionTitle">{{ t("settings.passwordTitle") }}</h3>
            <p class="sectionHint">{{ t("settings.passwordHint") }}</p>
          </div>
        </div>

        <div class="formGrid">
          <label class="field-stack">
            <span class="field-label">{{ t("settings.currentPassword") }}</span>
            <input
              v-model="passwordForm.currentPassword"
              class="pixel-input"
              type="password"
              autocomplete="current-password"
            />
          </label>

          <label class="field-stack">
            <span class="field-label">{{ t("settings.newPassword") }}</span>
            <input
              v-model="passwordForm.newPassword"
              class="pixel-input"
              type="password"
              autocomplete="new-password"
            />
          </label>

          <label class="field-stack fieldFull">
            <span class="field-label">{{ t("settings.confirmPassword") }}</span>
            <input
              v-model="passwordForm.confirmPassword"
              class="pixel-input"
              type="password"
              autocomplete="new-password"
            />
          </label>
        </div>

        <div class="actionsRow">
          <button class="pixel-button" type="button" @click="changePassword">
            {{ t("common.update") }}
          </button>
        </div>
      </section>

      <DeletedCategoryRestoreList
        :categories="deletedCategories"
        :loading="deletedLoading"
        :selected-type="deletedType"
        @change-type="changeDeletedType"
        @restore-category="restoreCategory"
        @restore-subcategory="restoreSubcategory"
      />

      <section class="sectionCard pixel-card">
        <div class="sectionHeader">
          <div>
            <h3 class="sectionTitle">{{ t("settings.logoutTitle") }}</h3>
            <p class="sectionHint">{{ t("settings.logoutHint") }}</p>
          </div>
        </div>

        <div class="actionsRow">
          <button class="pixel-button danger" type="button" @click="logout">
            {{ t("common.logout") }}
          </button>
        </div>
      </section>
    </template>
  </div>
</template>

<script setup>
import { computed, onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { typeApi } from "../apiComposables/typeApiComposables";
import { userApi } from "../apiComposables/userApiComposables";
import { userRecordApi } from "../apiComposables/userRecordApiComposables";
import { useAppPreferences } from "../composables/useAppPreferences";
import DeletedCategoryRestoreList from "../components/settings/DeletedCategoryRestoreList.vue";

const router = useRouter();
const { getProfileApi, updateProfileApi, changePasswordApi } = userApi();
const { GetBookRecordsApi } = userRecordApi();
const { getDeletedMoneyTypesApi, restoreMoneyTypeApi, restoreSubMoneyTypeApi } =
  typeApi();
const { t, theme, locale, setTheme, setLocale, formatDate } =
  useAppPreferences();

const pageError = ref("");
const deletedLoading = ref(false);
const deletedType = ref("expense");
const deletedCategories = ref([]);
const profile = ref({
  userId: 0,
  email: "",
  name: "",
  createTime: "",
});
const profileForm = ref({
  name: "",
});
const passwordForm = ref({
  currentPassword: "",
  newPassword: "",
  confirmPassword: "",
});
const recordStats = ref({
  recordContinuous: 0,
  recordDaysCount: 0,
  recordCount: 0,
});

const formattedJoinDate = computed(() => formatDate(profile.value.createTime));

const getToken = () => {
  const token = localStorage.getItem("token");
  if (!token) {
    throw new Error("No token found");
  }
  return token;
};

const handleAuthError = (error) => {
  if (error.message === "No token found" || error.response?.status === 401) {
    window.alert(t("common.loginRequired"));
    router.push("/login");
    return true;
  }

  return false;
};

const loadProfile = async () => {
  const token = getToken();
  const result = await getProfileApi(token);
  profile.value = result;
  profileForm.value.name = result.name;
};

const loadBookRecords = async () => {
  const token = getToken();
  recordStats.value = await GetBookRecordsApi(token);
};

const loadDeletedCategories = async () => {
  try {
    deletedLoading.value = true;
    const token = getToken();
    deletedCategories.value = await getDeletedMoneyTypesApi(
      token,
      deletedType.value,
    );
  } finally {
    deletedLoading.value = false;
  }
};

const initializePage = async () => {
  try {
    pageError.value = "";
    await Promise.all([
      loadProfile(),
      loadBookRecords(),
      loadDeletedCategories(),
    ]);
  } catch (error) {
    console.error(error);
    if (handleAuthError(error)) {
      return;
    }

    pageError.value = t("settings.pageLoadError");
  }
};

const saveProfile = async () => {
  try {
    if (!profileForm.value.name.trim()) {
      window.alert(t("settings.nameRequired"));
      return;
    }

    const token = getToken();
    const result = await updateProfileApi(token, {
      name: profileForm.value.name.trim(),
    });

    profile.value = result;
    profileForm.value.name = result.name;
    localStorage.setItem("name", result.name);
    window.alert(t("settings.saveProfileSuccess"));
  } catch (error) {
    console.error(error);
    if (handleAuthError(error)) {
      return;
    }

    window.alert(error.response?.data || t("settings.pageLoadError"));
  }
};

const changePassword = async () => {
  try {
    if (
      !passwordForm.value.currentPassword ||
      !passwordForm.value.newPassword ||
      !passwordForm.value.confirmPassword
    ) {
      window.alert(t("settings.passwordRequired"));
      return;
    }

    if (passwordForm.value.newPassword !== passwordForm.value.confirmPassword) {
      window.alert(t("settings.passwordMismatch"));
      return;
    }

    const token = getToken();
    await changePasswordApi(token, {
      currentPassword: passwordForm.value.currentPassword,
      newPassword: passwordForm.value.newPassword,
    });

    passwordForm.value = {
      currentPassword: "",
      newPassword: "",
      confirmPassword: "",
    };
    window.alert(t("settings.passwordChanged"));
  } catch (error) {
    console.error(error);
    if (handleAuthError(error)) {
      return;
    }

    window.alert(error.response?.data || t("settings.pageLoadError"));
  }
};

const changeDeletedType = async (type) => {
  deletedType.value = type;
  try {
    await loadDeletedCategories();
  } catch (error) {
    console.error(error);
    if (handleAuthError(error)) {
      return;
    }

    window.alert(t("settings.pageLoadError"));
  }
};

const restoreCategory = async (category) => {
  try {
    const token = getToken();
    await restoreMoneyTypeApi(token, category.id);
    window.alert(t("settings.restoreCategorySuccess", { name: category.name }));
    await loadDeletedCategories();
  } catch (error) {
    console.error(error);
    if (handleAuthError(error)) {
      return;
    }

    window.alert(error.response?.data || t("settings.pageLoadError"));
  }
};

const restoreSubcategory = async (subcategory) => {
  try {
    const token = getToken();
    await restoreSubMoneyTypeApi(token, subcategory.id);
    window.alert(
      t("settings.restoreSubcategorySuccess", { name: subcategory.name }),
    );
    await loadDeletedCategories();
  } catch (error) {
    console.error(error);
    if (handleAuthError(error)) {
      return;
    }

    window.alert(error.response?.data || t("settings.pageLoadError"));
  }
};

const logout = () => {
  localStorage.removeItem("token");
  localStorage.removeItem("name");
  localStorage.removeItem("userEmail");
  router.push("/login");
};

onMounted(() => {
  initializePage();
});
</script>

<style scoped>
.settingsPage {
  display: flex;
  flex-direction: column;
  gap: 18px;
}

.heroCard {
  padding: 18px;
  display: flex;
  justify-content: space-between;
  gap: 16px;
}

.heroText {
  display: flex;
  flex-direction: column;
  gap: 6px;
}

.heroEyebrow,
.heroSubtitle,
.sectionHint,
.inlineMeta,
.summaryHint {
  color: var(--muted);
}

.heroTitle {
  font-size: 1.4rem;
  font-weight: 700;
}

.heroStats {
  min-width: 140px;
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.heroStat,
.summaryItem {
  padding: 14px;
  background: var(--surface-soft);
}

.heroStatLabel,
.summaryLabel {
  display: block;
  color: var(--muted);
  font-size: 0.78rem;
  margin-bottom: 6px;
}

.heroStatValue,
.summaryValue {
  font-size: 1.2rem;
  font-weight: 700;
}

.sectionCard {
  padding: 18px;
}

.sectionHeader {
  margin-bottom: 16px;
}

.sectionTitle {
  font-size: 1rem;
  font-weight: 700;
  margin-bottom: 4px;
}

.preferenceGrid,
.formGrid,
.summaryGrid {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  gap: 14px;
}

.fieldFull {
  grid-column: 1 / -1;
}

.disabledInput {
  opacity: 0.8;
}

.inlineMeta {
  margin-top: 12px;
  font-size: 0.82rem;
}

.actionsRow {
  margin-top: 16px;
  display: flex;
  justify-content: flex-end;
}

.summaryGrid {
  grid-template-columns: repeat(3, minmax(0, 1fr));
}

.retryButton {
  margin-top: 14px;
}

@media (max-width: 520px) {
  .heroCard,
  .preferenceGrid,
  .formGrid,
  .summaryGrid {
    grid-template-columns: 1fr;
  }

  .heroCard {
    flex-direction: column;
  }

  .heroStats {
    min-width: 0;
  }

  .actionsRow {
    justify-content: stretch;
  }

  .actionsRow .pixel-button {
    width: 100%;
  }
}
</style>

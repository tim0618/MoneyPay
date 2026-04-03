import { computed, reactive } from "vue";
import { messages } from "../i18n/messages";

const THEME_KEY = "moneyPayTheme";
const LOCALE_KEY = "moneyPayLocale";
const DEFAULT_LOCALE = "zh-TW";

const storage =
  typeof window !== "undefined"
    ? window.localStorage
    : {
        getItem: () => null,
        setItem: () => undefined,
      };

const state = reactive({
  theme: storage.getItem(THEME_KEY) === "dark" ? "dark" : "light",
  locale: storage.getItem(LOCALE_KEY) === "en" ? "en" : DEFAULT_LOCALE,
});

const resolveMessage = (locale, key) =>
  key.split(".").reduce((current, segment) => current?.[segment], messages[locale]);

const interpolate = (value, params = {}) =>
  value.replace(/\{(\w+)\}/g, (_, key) => params[key] ?? "");

const persistPreference = (key, value) => {
  storage.setItem(key, value);
};

export const useAppPreferences = () => {
  const locale = computed(() => state.locale);
  const theme = computed(() => state.theme);
  const intlLocale = computed(() => (state.locale === "en" ? "en-US" : "zh-TW"));

  const t = (key, params = {}) => {
    const template =
      resolveMessage(state.locale, key) ??
      resolveMessage(DEFAULT_LOCALE, key) ??
      key;

    return typeof template === "string" ? interpolate(template, params) : key;
  };

  const setTheme = (value) => {
    state.theme = value === "dark" ? "dark" : "light";
    persistPreference(THEME_KEY, state.theme);
  };

  const toggleTheme = () => {
    setTheme(state.theme === "dark" ? "light" : "dark");
  };

  const setLocale = (value) => {
    state.locale = value === "en" ? "en" : DEFAULT_LOCALE;
    persistPreference(LOCALE_KEY, state.locale);
  };

  const formatCurrency = (value, options = {}) =>
    new Intl.NumberFormat(intlLocale.value, {
      style: "currency",
      currency: options.currency || "TWD",
      maximumFractionDigits: options.maximumFractionDigits ?? 0,
    }).format(Number(value) || 0);

  const formatDate = (value, options = {}) => {
    if (!value) {
      return "--";
    }

    return new Intl.DateTimeFormat(intlLocale.value, {
      year: "numeric",
      month: "2-digit",
      day: "2-digit",
      ...options,
    }).format(new Date(value));
  };

  const formatMonthSlash = (year, month) =>
    `${year} / ${String(month).padStart(2, "0")}`;

  const formatMonthShort = (year, month) =>
    new Intl.DateTimeFormat(intlLocale.value, {
      month: state.locale === "en" ? "short" : "2-digit",
    }).format(new Date(year, month - 1, 1));

  const formatNumber = (value) =>
    new Intl.NumberFormat(intlLocale.value).format(Number(value) || 0);

  const formatCompactNumber = (value) =>
    new Intl.NumberFormat(intlLocale.value, {
      notation: "compact",
      maximumFractionDigits: 1,
    }).format(Number(value) || 0);

  return {
    locale,
    theme,
    t,
    setTheme,
    toggleTheme,
    setLocale,
    formatCurrency,
    formatDate,
    formatMonthSlash,
    formatMonthShort,
    formatNumber,
    formatCompactNumber,
  };
};

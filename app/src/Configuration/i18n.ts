import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import * as translationDe from "./Translations/de.json";
import * as translationEn from "./Translations/en.json";
import * as translationSlo from "./Translations/slo.json";

i18n.use(initReactI18next).init({
  fallbackLng: "en",
  debug: false,
  interpolation: {
    escapeValue: false,
    formatSeparator: ",",
  },
  resources: {
    en: { common: { ...translationEn } },
    de: { common: { ...translationDe } },
    slo: { common: { ...translationSlo } },
  },

  defaultNS: "common",
});

export default i18n;
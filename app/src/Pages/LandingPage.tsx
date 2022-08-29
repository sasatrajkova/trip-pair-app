import React from "react";
import Layout from "../Components/Layout";
import SearchBar from "../Components/SearchBar";
import { useTranslation } from 'react-i18next';

const LandingPage: React.FC = () => {
  const { t } = useTranslation();
  const preface = t('pages.landing.preface');

  return (
    <Layout>
      <SearchBar />
      <div className="flex justify-center pt-1">{preface}</div>
    </Layout>
  );
};

export default LandingPage;

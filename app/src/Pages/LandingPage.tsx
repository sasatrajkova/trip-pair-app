import React from "react";
import Layout from "../Components/Layout";
import SearchBar from "../Components/SearchBar";

const LandingPage: React.FC = () => {
  return (
    <Layout>
      <SearchBar />
      <div className="flex justify-center pt-1">Your next trip awaits.</div>
    </Layout>
  );
};

export default LandingPage;

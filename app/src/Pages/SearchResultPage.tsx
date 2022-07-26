import React from "react";
import { useParams } from "react-router-dom";
import Layout from "../Components/Layout";
import ResortInventory from "../Components/ResortInventory";
import SearchBar from "../Components/SearchBar";

const SearchResultPage: React.FC = () => {
  const { searchValue } = useParams();

  return (
    <Layout>
      <SearchBar searchValue={searchValue} />
      <ResortInventory searchValue={searchValue} />
    </Layout>
  );
};

export default SearchResultPage;

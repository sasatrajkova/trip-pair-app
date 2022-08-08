import React from "react";
import { useParams } from "react-router-dom";
import Layout from "../Components/Layout";
import SearchBar from "../Components/SearchBar";

const SearchResultPage: React.FC = () => {
    const { searchValue } = useParams()

    return (
      <Layout>
        <SearchBar searchValue={searchValue}/>
      </Layout>
    );
  };

export default SearchResultPage;
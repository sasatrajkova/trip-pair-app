import React from "react";
import SearchBar from "../Components/SearchBar";

const LandingPage: React.FC = () => {
  return (
    <div>
      <SearchBar />
      <div className="flex justify-center">Your next advanture awaits.</div>
    </div>
  );
};

export default LandingPage;

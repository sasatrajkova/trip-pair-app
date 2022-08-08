import React from "react";
import LandingPage from "./Pages/LandingPage";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import "./index.css";
import SearchResultPage from "./Pages/SearchResultPage";

const App: React.FC = () => {
  return (
      <Router>
        <Routes>
          <Route path="/" element={<LandingPage />} />
          <Route path="/:searchValue" element={<SearchResultPage />} />
        </Routes>
      </Router>
  );
};

export default App;

import React from "react";
import LandingPage from "./Pages/LandingPage";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import "./index.css";
import SearchResultPage from "./Pages/SearchResultPage";

const App: React.FC = () => {
  return (
    <React.StrictMode>
      <Router>
        <Routes>
          <Route path="/" element={<LandingPage />} />
          <Route path="/:searchValue" element={<SearchResultPage />} />
        </Routes>
      </Router>
    </React.StrictMode>
  );
};

export default App;

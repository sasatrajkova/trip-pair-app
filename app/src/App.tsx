import React from "react";
import LandingPage from "./Pages/LandingPage";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import "./index.css";

const App: React.FC = () => {
  return (
      <Router>
        <Routes>
          <Route path="/" element={<LandingPage />} />
        </Routes>
      </Router>
  );
};

export default App;

import React from "react";
import { Link } from "react-router-dom";

const Header: React.FC = () => {
  return (
    <div className="flex justify-center px-5 pt-28 pb-12">
      <Link to="/" className="text-7xl font-thin">
        TripPair
      </Link>
    </div>
  );
};

export default Header;

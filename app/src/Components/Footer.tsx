import React from "react";
import { Link } from "react-router-dom";

const Footer: React.FC = () => {
  return (
    <div className="flex justify-center px-8 py-12">
      <Link to="/add">
        <button className="bg-white hover:bg-gray-100 text-gray-800 font-semibold py-2 px-4 border border-gray-400 rounded shadow">Add resort</button>
      </Link>
    </div>
  );
};

export default Footer;
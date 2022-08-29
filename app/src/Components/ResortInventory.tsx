import React from "react";
import ResortCard from "./ResortCard";

const ResortInventory: React.FC = () => {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-8 px-8 pt-8">
      {[1, 2, 3, 4, 5, 6].map((i) => (
        <ResortCard key={i}/>
      ))}
    </div>
  );
};

export default ResortInventory;

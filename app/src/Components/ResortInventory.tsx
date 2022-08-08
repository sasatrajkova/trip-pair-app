import React from "react";
import ResortCard from "./ResortCard";

const ResortInventory: React.FC = () => {
  return (
    <div className="grid grid-cols-3 gap-8 px-8 pt-8">
      {[1, 2, 3, 4, 5, 6].map((i) => (
        <ResortCard />
      ))}
    </div>
  );
};

export default ResortInventory;

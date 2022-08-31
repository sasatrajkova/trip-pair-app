import axios from "axios";
import React, { useEffect, useState } from "react";
import { resortDto } from "../Models/resortDto";
import ResortCard from "./ResortCard";

interface Props {
  searchValue?: string;
}

const ResortInventory: React.FC<Props> = (props) => {
  const { searchValue } = props;
  const [resorts, setResorts] = useState<Array<resortDto>>([]);

  useEffect(() => {
    axios
      .get<Array<resortDto>>(`/resorts/${searchValue}`, {
        baseURL: "https://localhost:7187/api",
      })
      .then((response) => {
        setResorts(response.data);
      });
  }, [searchValue]);

  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-8 px-8 pt-8">
      {resorts.map((resort) => (
        <ResortCard
          key={resort.id}
          name={resort.name}
          climate={resort.climate}
          image={resort.image}
          location={resort.location}
        />
      ))}
    </div>
  );
};

export default ResortInventory;

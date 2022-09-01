import axios from "axios";
import { useEffect, useState } from "react";
import { locationDto } from "../Models/locationDto";
import ResortFormItem from "./ResortFormItem";

const ResortForm: React.FC = () => {
  const [locations, setLocations] = useState<Array<locationDto>>([]);

  useEffect(() => {
    axios
      .get<Array<locationDto>>(`/locations`, {
        baseURL: "https://localhost:7187/api",
      })
      .then((response) => {
        setLocations(response.data);
      });
  }, []);


  return (
    <form className="px-8 md:w-2/3 lg:w-1/3 mx-auto">
      <ResortFormItem label="Resort name" variant={"input"} />
      <ResortFormItem label="Location" variant={"dropdown"} locations={locations} />
      <ResortFormItem label="Climate" variant={"input"} />
      <ResortFormItem label="Image" variant={"input"} />
      <button
        className="bg-white hover:bg-gray-100 text-gray-800 font-semibold py-2 px-4 border border-gray-400 rounded shadow mt-2"
        type="button"
      >
        Add resort
      </button>
    </form>
  );
};

export default ResortForm;

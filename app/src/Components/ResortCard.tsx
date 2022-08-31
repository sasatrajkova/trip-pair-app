import React from "react";
import { Link } from "react-router-dom";
import MonthCollection from "./MonthCollection";
import { ClimateIcon } from "../Icons/ClimateIcon";
import { LocationIcon } from "../Icons/LocationIcon";
import { locationDto } from "../Models/locationDto";

interface Props {
  name: string;
  climate: string;
  image: string;
  location: locationDto;
}

const ResortCard: React.FC<Props> = (props) => {
  const { name, climate, image, location } = props;

  return (
    <div className="flex justify-center">
      <Link to="/details/id">
        <div className="max-w-sm rounded overflow-hidden shadow-lg hover:bg-gray-100">
          <img
            alt={image}
            src={process.env.PUBLIC_URL + "/Images/" + image}
          ></img>
          <p className="font-bold text-xl px-2 py-2">{name}</p>
          <LocationIcon label={location.name}/>
          <ClimateIcon label={climate}/>
          <MonthCollection location={location} />
        </div>
      </Link>
    </div>
  );
};

export default ResortCard;

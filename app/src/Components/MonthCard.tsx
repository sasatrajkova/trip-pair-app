import React, { PropsWithChildren } from "react";
import { locationMonthDto } from "../Models/locationMonthDto";

interface Props {
  locationMonth: locationMonthDto;
}

const MonthCard: React.FC<PropsWithChildren<Props>> = (props) => {
  const { locationMonth } = props;
  return (
    <span
      className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2"
      key={locationMonth.month.id}
    >
      {locationMonth.month.name}
    </span>
  );
};

export default MonthCard;

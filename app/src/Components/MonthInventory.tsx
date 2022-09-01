import { locationDto } from "../Models/locationDto";
import MonthCard from "./MonthCard";

interface Props {
  location: locationDto;
}

const MonthInventory: React.FC<Props> = (props) => {
  const { location } = props;
  return (
    <div className="pt-4 pb-2 px-2">
      {location.locationMonths.map((locationMonth) => (
        <MonthCard locationMonth={locationMonth} />
      ))}
    </div>
  );
};

export default MonthInventory;

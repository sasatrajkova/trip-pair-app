import { locationDto } from "../Models/locationDto";

interface Props {
  location: locationDto;
}

const MonthInventory: React.FC<Props> = (props) => {
    const { location } = props;
  return (
    <div className="pt-4 pb-2 px-2">
      {location.locationMonths.map((locationMonth) => (
        <span
          className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2"
          key={locationMonth.month.name}
        >
          {locationMonth.month.name}
        </span>
      ))}
    </div>
  );
};

export default MonthInventory;
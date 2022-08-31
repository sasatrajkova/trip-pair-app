import { locationDto } from "./locationDto";

export type resortDto = {
  id: number;
  name: string;
  climate: string;
  image: string;
  locationId: number;
  location: locationDto;
};

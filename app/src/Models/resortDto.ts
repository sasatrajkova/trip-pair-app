import { locationDto } from "./locationDto";

export type resortDto = {
  name: string;
  climate: string;
  image: string;
  locationId: number;
  location: locationDto;
};

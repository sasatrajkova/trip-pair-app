import { locationMonthDto } from "./locationMonthDto";

export type locationDto = {
  name: string;
  goodMonthsDescription: string;
  locationMonths: Array<locationMonthDto>;
};

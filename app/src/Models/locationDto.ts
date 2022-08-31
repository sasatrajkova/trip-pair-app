import { locationMonthDto } from "./locationMonthDto";

export type locationDto = {
  id: number;
  name: string;
  goodMonthsDescription: string;
  locationMonths: Array<locationMonthDto>;
};

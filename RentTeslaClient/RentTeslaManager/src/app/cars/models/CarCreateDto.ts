import { ICarRental, ICarType } from "src/app/shared/models/interface";

export interface ICarCreatedDto {
    isPrepared: boolean;
    isFree: boolean;
    dailyPrice: number;
    plates: string;
    carTypeDto: ICarType;
    carRentalDto: ICarRental;
  }
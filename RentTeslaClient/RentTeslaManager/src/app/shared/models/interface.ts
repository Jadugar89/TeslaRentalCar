export interface ICar {
    id: number;
    plates:string;
    name: string;
    carRentalName:string;
    carRentalCity:string;
  }

  export interface ICarType {
    id: number;
    name: string;
    motor: string;
    range: number;
    seats: number;
  }
  
  export interface ICarRental {
    id: number;
    name: string;
    city: string;
    street: string;
    postalCode: string;
  }
  
  export interface ICarDetail {
    id: number;
    isPrepared: boolean;
    isFree: boolean;
    dailyPrice: number;
    plates: string;
    carTypeDto: ICarType;
    carRentalDto: ICarRental;
  }
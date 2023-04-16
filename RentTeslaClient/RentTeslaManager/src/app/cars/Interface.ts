export interface ICar {
    id: number;
    plates:string;
    name: string;
    carRentalName:string;
    carRentalCity:string;
  }

  interface ICarType {
    name: string;
    motor: string;
    range: number;
    seats: number;
  }
  
  interface ICarRental {
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
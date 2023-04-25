import React from 'react';
import styles from '../../styles/CarsDisplay.module.css'
import { useRouter } from 'next/router';
import { encryptObject } from '../../utils/crypto'

function Car(props) {
  const router = useRouter();

  const handleClick = () => {
    let data ={};
    data.car = props.car;
    data.reservation=props.reservation;
    const reservation = encryptObject(data);
    router.push({
      pathname: '/confirmation',
      query: { reservation },
      });
    };

  return(
      <div className={styles.car}>
        <img className={styles['car-img']} src="https://via.placeholder.com/150" alt="Car" />
        <h2 className={styles['car-header']}>{props.car.name}</h2>
        <p className={styles['car-text']}>Price: ${props.car.dailyPrice} per day</p>
        <p className={styles['car-text']}>Motor {props.car.motor}</p> 
        <p className={styles['car-text']}>Range {props.car.range} miles</p>
        <p className={styles['car-text']}>Range {props.car.seats} seats</p>
        <button className={styles['book-button']} onClick={handleClick}>Book Now</button>
      </div>
    )
}
export default Car

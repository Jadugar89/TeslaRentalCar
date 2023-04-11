import React, { useState } from 'react';
import styles from '../../styles/CarsDisplay.module.css'
import Car from './Car';

function CarsDisplay(props) {

return(
   <div className={styles['car-display-container']}>
    <h3 className={styles['car-display-text']} >Find your dream car</h3>
    <div className={styles['car-container']}>
      {props.cars.map(item => (
        <Car key={item.id} car={item} reservation={props.reservation}/>
      ))}
    </div>
  </div>
)


}
export default CarsDisplay
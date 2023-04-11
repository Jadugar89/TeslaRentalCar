import React from 'react';
import styles from '../../styles/Confirmation.module.css';

function ConfirmationTable(props) {

 return(
<div className={styles['confirmation-container']}>
      <h1>Confirmation</h1>
      <table className={styles['confirmation-table']}>
      <thead>
        <tr>
          <th>Name</th>
          <th>Daily Price</th>
          <th>Total Cost</th>
          <th>Motor</th>
          <th>Range</th>
          <th>Start Date</th>
          <th>End Date</th>
          <th>Pickup Location</th>
          <th>Dropoff Location</th>
          <th>Action</th>
        </tr>
      </thead>
      <tbody>
          <tr>
            <td>{props.reservationData.car.name}</td>
            <td>{props.reservationData.car.dailyPrice}$</td>
            <td>{props.reservationData.car.totalCost}$</td>
            <td>{props.reservationData.car.motor}</td>
            <td>{props.reservationData.car.range}</td>
            <td>{props.reservationData.reservation.startDate}</td>
            <td>{props.reservationData.reservation.endDate}</td>
            <td>{props.reservationData.reservation.namePickUp}</td>
            <td>{props.reservationData.reservation.nameDropOff}</td>
            <td>
              <button className={styles['confirmation-button']} onClick={props.confirmData}>Confirm Reservation</button>
            </td>
          </tr>
      </tbody>
    </table>
    </div>
)}
export default ConfirmationTable
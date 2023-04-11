import React, { useEffect,useState } from 'react';
import Head from 'next/head'
import Header from '@/components/Header'
import dayjs from 'dayjs';
import { useRouter } from 'next/router';
import { decryptObject } from '../utils/crypto'; 
import ConfirmationTable from '../components/Confirmation/ConfirmationTable'
import ConfirmButton from '../components/Confirmation/ConfirmButton'
import styles from '../styles/Confirmation.module.css';


export default function Confirmation() {
  const [reservationData, setReservationData] = useState('');
  const [confirm,setConfirm] = useState(false);
  const router = useRouter();
 
  const CalcAmount = (obj) => {
    const startDate = new Date(obj.reservation.startDate);
    const endDate = new Date(obj.reservation.endDate);
    
    
    // Check if the end date is after the start date
    if (endDate > startDate) {
      const days = dayjs(endDate).diff(dayjs(startDate), 'day');
      return days * obj.car.dailyPrice;
    } else {
      return 0;
    }
}


  useEffect(() => {
    if (router.query.reservation) {
      const decryptedObject = decryptObject(router.query.reservation);
      const data = {
              ...decryptedObject,
        car: {
          ...decryptedObject.car,
          totalCost: CalcAmount(decryptedObject)
        }
      }
      setReservationData(data);
    }
  }, [router.query.reservation]);
  
  const ConfirmData =()=>{
    setConfirm(true)
  } 
  // render the data on the page
  return (
    <div className={styles['container-confirm']}>
          <Head>
           <title>Rental Car Search</title>
          </Head>
          <Header></Header>
      {reservationData && (
      <ConfirmationTable
       reservationData={reservationData}
       confirmData={ConfirmData}
      />)}
      {
        confirm && (
         <ConfirmButton
         reservationData={reservationData}/>
        )
      }
    </div>
  )
};

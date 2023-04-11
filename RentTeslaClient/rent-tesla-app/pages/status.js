import React,{useEffect,useState} from 'react';
import { decryptObject } from '../utils/crypto'; 
import { useRouter } from 'next/router';
import Link from 'next/link';
import Head from 'next/head'
import Header from '@/components/Header'
import styles from '../styles/Confirmation.module.css';


export default function status() {
const [reservationNumber, setReservationNumber] = useState('');
const router = useRouter();

useEffect(() => {
    if (router.query.reservationNumber) {
      const decryptedObject = decryptObject(router.query.reservationNumber);
      setReservationNumber(decryptedObject);
      console.log(decryptedObject);
    }
  }, [router.query.reservationNumber]);


  return (
    <div className={styles['container-confirm']}>
          <Head>
           <title>Rental Car Search</title>
          </Head>
          <Header></Header>
      {reservationNumber && (
        <div>
            <p>Your number</p>
            <p>{reservationNumber} </p>
            <p>Thank you for choosing our services!</p>
            <Link href="/">
                Back to Home
            </Link>
        </div>
        )
      }
    </div>
  )


}
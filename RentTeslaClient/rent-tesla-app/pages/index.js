import React, { useState } from 'react';
import styles from '../styles/Home.module.css'
import Head from 'next/head'
import SearchBox from '@/components/SearchBox/SearchBox'
import CarsDisplay from '@/components/CarsDisplay/CarsDisplay' 
import Header from '@/components/Header'


export default function Home() {
   const [cars, setCars] = useState([]);
   const [reservation, setReservation] = useState(null);
   function getFeedback(data) {
    setCars(data.cars);
    setReservation(data.reservation);
  }

  return (

    <div className={styles.container}>
          <Head>
           <title>Rental Car Search</title>
          </Head>
          <Header></Header>
      <main className={styles.main}>
       <SearchBox
        getFeedback={getFeedback}
       />
       { 
       cars.length>0 &&( 
       <CarsDisplay
        reservation={reservation}
        cars={cars}
       />)
       }
      

      </main>

      <footer className='footer'>
        Powered by Next.js
      </footer>
    </div>
  )
}
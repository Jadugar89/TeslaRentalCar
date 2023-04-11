import React,{useState} from 'react';
import { encryptObject } from '../../utils/crypto'
import { useRouter } from 'next/router';
import styles from '../../styles/Confirmation.module.css';

function ConfirmButton(props) {
    const [email,setEmail] = useState("");
    const [error,setError] = useState(false);
    const router = useRouter();

 const emailValidation=()=>{
        const regex = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
        if(!email || regex.test(email) === false){
            setError(true);
            return false;
        }
        setError(false);
        return true;
}
const onChange=(event)=>{
    console.log("onChange");
     setEmail(event.target.value);
}
const onBlur=()=>{
    console.log("onBlur");
    emailValidation();
}

function bookResevation() {
    const startDate = new Date(props.reservationData.reservation.startDate);
    const endDate = new Date(props.reservationData.reservation.endDate);

    const dataToSend = {
        car: props.reservationData.car,
        reservation: {
          ...props.reservationData.reservation,
          startDate: startDate,
          endDate: endDate,
        },
        email: email
      };
      
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(dataToSend)
    };
  
    fetch(process.env.API_URL+'/api/Reservation', requestOptions)
      .then(response => response.json())
      .then(data => {

        const reservationNumber = encryptObject(data);
        router.push({
            pathname: '/status',
            query: { reservationNumber },
          });
      })
      .catch(error => console.error(error));
  }


return (
<div className={styles['container-confirm']}>
    <label className={styles['confirmbutton-label']}>Email for Reservation</label>
    <input className={styles['confirmbutton-input']} type='text' value={email} onBlur={onBlur} onChange={onChange}></input>
    {error && (
        <span className={styles['confirmbutton-error']}>Not valid Email</span>
    )}

    <button onClick={bookResevation} className={styles['confirmation-button']}>Confirm</button>
</div>
)}
export default ConfirmButton
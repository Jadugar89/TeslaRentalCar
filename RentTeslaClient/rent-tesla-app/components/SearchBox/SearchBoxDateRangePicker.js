import React, { useState } from "react";
import { DateRange } from "react-date-range";
import InputSearch from './InputSearch';
import "react-date-range/dist/styles.css"; // main style file
import "react-date-range/dist/theme/default.css"; // theme css file
import styles from '../../styles/SearchBox.module.css'

const SearchBoxDateRangePicker = (props) => {
  const [showDatePicker, setShowDatePicker] = useState(false);


  const toggleDatePicker = () => {
    setShowDatePicker(!showDatePicker);
  };

  const onChangeHandlerPickUp=(event) =>{
   // const updatedState = [...state]; 
  //  updatedState[0].startDate = event.target.value;
   // setState(updatedState);
  }
  const onChangeHandlerDropOff=(event) =>{
   // const updatedState = [...state]; 
   // updatedState[0].endDate = event.target.value; 
   // setState(updatedState);
  }

  return (
    <div className={styles['date-range-picker-container']}>
       <InputSearch
          type="text"
          name="pickup-date"
          label="Pick-up Date"
          value={props.startDate}
          onClickHandler={toggleDatePicker}
          onChangeHandler={onChangeHandlerPickUp}
        />
        <InputSearch
          type="text"
          name="dropoff-date"
          label="Drop-off Date"
          value={props.endDate}
          onClickHandler={toggleDatePicker}
          onChangeHandler={onChangeHandlerDropOff}
        />
       {showDatePicker && (
        <div onMouseLeave={()=>setShowDatePicker(false)} className={styles['date-range-picker-dropdown']} >
        <DateRange
          ranges={props.ranges}
          onChange={props.onChangeHandler}
          direction="horizontal"
          editableDateInputs={true}
          />
      </div>
      )}
    </div>
  );
}
export default SearchBoxDateRangePicker;
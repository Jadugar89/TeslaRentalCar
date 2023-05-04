import React, { useState } from 'react';
import styles from '../../styles/SearchBox.module.css'
import format from 'date-fns/format'
import { addDays } from 'date-fns'
import CheckBox from './CheckBox'
import SearchButton from './SearchButton'
import InputSearchWithSuggestions from './InputSearchWithSuggestions'
import SearchBoxDateRangePicker from './SearchBoxDateRangePicker'


const SearchBox = (props) => {

    const [formData, setFormData] = useState({
      namePickUp:"",
      nameDropOff:"",
      returnToDifferentLocation:false,
      dateRange:[{
        startDate: new Date(),
        endDate: addDays(new Date(), 7),
        key: "selection"
      }]
    });

      const handleChange = (event) => {
        const {id, value, type,textContent,checked} = event.target
            console.log(type)
            let newValue= type==='checkbox'?checked:value

        setFormData(prevFormData => {
          return {
              ...prevFormData,
              [id]: type===''?textContent:newValue
          }
      })
      };

      const handleDateChange =(ranges)=>{
        setFormData(prevFormData => {
          return {
              ...prevFormData,
              dateRange: [ranges.selection]
          }
      })
      }

      const OnClickHandler= async(event)=>
      { 
        event.preventDefault();
        
          let formDataDto ={
            namePickUp:formData.namePickUp,
            nameDropOff: formData.returnToDifferentLocation?formData.nameDropOff:formData.namePickUp,
            startDate: format(formData.dateRange[0].startDate, "MM/dd/yyyy"),
            endDate: format(formData.dateRange[0].endDate, "MM/dd/yyyy"),
          }

              const queryString = new URLSearchParams(formDataDto);

              const response = await fetch(process.env.API_URL+`/api/car/carsInDataRange/${formDataDto.namePickUp}?${queryString}`,);
              const cars = await response.json();
              let data={};
              data.cars=cars;
              data.reservation=formDataDto;
              props.getFeedback(data);
      }

    return ( 
    <div className={styles['container']}>
      <form className={styles['search-form']}>
        <div className={styles['search-form-row']}>
            <InputSearchWithSuggestions
            name="namePickUp"
            value={formData.namePickUp}
            label="Pick-up Location"
            handleNameChange={handleChange}
            />      
        {formData.returnToDifferentLocation && (
                    <InputSearchWithSuggestions
                    name="nameDropOff"
                    label="Drop-off Location"
                    value={formData.nameDropOff}
                    handleNameChange={handleChange}
                  />
        )}
        <SearchBoxDateRangePicker
          onChangeHandler={handleDateChange}
          ranges={formData.dateRange}
          startDate={`${format(formData.dateRange[0].startDate, "MM/dd/yyyy")}`}
          endDate={`${format(formData.dateRange[0].endDate, "MM/dd/yyyy")}`}
        />
       
        </div>
        <div className={styles['search-form-row']}>
        <CheckBox
         label="Return to a different location"
         value={formData.returnToDifferentLocation}
         handleCheckboxChange={handleChange}
         name="returnToDifferentLocation"
        />
        </div>
       <div className={styles['search-form-row']}>
         <SearchButton
          OnClickHandler={OnClickHandler}
         />
       </div>
      </form>
    </div>
     );
}
 
export default SearchBox;
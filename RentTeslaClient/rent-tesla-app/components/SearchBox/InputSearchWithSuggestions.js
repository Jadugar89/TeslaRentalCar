import React, { useState } from 'react';
import styles from '../../styles/SearchBox.module.css'
import SearchBoxSuggestion from './SearchBoxSuggestion' 
import InputSearch from './InputSearch';

const InputSearchWithSuggestions = (props) => {
    const [showSuggestions, setShowSuggestions] = useState(false);
    
    function handleLocalizationChange(event) {
 
        props.handleNameChange(event);
            if(event.target.value.length>2)
            {
              setShowSuggestions(true);
            }
            else
            {
              setShowSuggestions(false);
            }
      }
      function handleItemClick(event) {
        
        props.handleNameChange(event);
        setShowSuggestions(false);
      };

      function handleBlur()
      {
            setShowSuggestions(false);
      };

    return ( 
        <div onBlur={handleBlur} className={styles['container-suggestions']} >
            <InputSearch
            type="text"
            name={props.name}
            value={props.value}
            label= {props.label}
            onChangeHandler={handleLocalizationChange}
            />
            { showSuggestions &&(
            <SearchBoxSuggestion
            onItemClick={handleItemClick}
            value={props.value}
            name={props.name}
            />)}
        </div>    
    )
} 
export default InputSearchWithSuggestions
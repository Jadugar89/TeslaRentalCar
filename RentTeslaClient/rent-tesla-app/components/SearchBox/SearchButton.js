import React from "react";
import styles from '../../styles/SearchBox.module.css'

const SearchButton = (props) => {
    return(
        <div className={styles['button-container']}>
            <button className={styles['form-submit']} type="submit" onClick={props.OnClickHandler}>
            Search
            </button>
        </div>
)}

export default SearchButton;





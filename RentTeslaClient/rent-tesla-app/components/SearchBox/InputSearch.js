
import React from 'react';
import styles from '../../styles/SearchBox.module.css'


const InputSearch = (props) => {
    return ( 
        <div className={styles['input-container']}>
            <label className={styles['form-label']} htmlFor={props.name}>
            {props.label}:
            </label>
            {props.type=="text"?(
                    <input autoComplete="off" className={styles['form-input']} 
                            type={props.type} 
                            onChange={props.onChangeHandler}
                            onClick={props.onClickHandler}
                            value={props.value}
                            name={props.name}
                            id={props.name} />)
                :(
                    <input autoComplete="off" className={styles['form-input']} 
                            type={props.type} 
                            onChange={props.onChangeHandler}
                            name={props.name}  
                            value={props.value}
                            id={props.name}/>
            )} 
        </div>
    )
}
export default InputSearch;
import styles from '../../styles/SearchBox.module.css'

const CheckBox = (props) => {
    return(
        <div className={styles.checkboxContainer}>
            <input
            id={props.name}
            type="checkbox"
            checked={props.value}
            onChange={props.handleCheckboxChange}
            />
            <label>{props.label}</label>
        </div>
        )
}
export default CheckBox;
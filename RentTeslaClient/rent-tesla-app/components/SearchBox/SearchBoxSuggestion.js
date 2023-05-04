import React from "react";
import { useState, useEffect } from 'react';
import styles from '../../styles/SearchBox.module.css'

function SearchBoxSuggestion(props) {
  const [data, setData] = useState([]);
  const [selectedItem, setSelectedItem] = useState(null);

  useEffect(() => {
    fetchData(props.value);
  }, [props.value]);

  async function fetchData(name) {
    if(name)
    {
      const response = await fetch(process.env.API_URL+`/api/CarRental/GetNames/${name}`);
      const newData = await response.json();
      setData(newData);

    }
    else
    {
        console.log("Sugestions string empty")
    }
  }

  const handleMouseOver = (index) => {
    setSelectedItem(index);
  };

  const handleMouseLeave = () => {
    setSelectedItem(null);
  };

  return (
    <div className={styles['dropdown']} >
    {data.length > 0 &&
     (<ul className={styles['list-suggestions']}>
            {data.map((item, index) => (
          <li
            key={index}
            id={props.name}
            onMouseOver={() => handleMouseOver(index)}
            onMouseLeave={() => handleMouseLeave()}
            onMouseDown ={props.onItemClick} 
            className={selectedItem === index ? styles['selected'] : ""}
          >
            {item}
          </li>
        ))}
     </ul>)}
    </div>
  );
}

export default SearchBoxSuggestion



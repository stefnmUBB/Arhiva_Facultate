import { useState } from 'react'
import { FlatList } from 'react-native-web';

import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
//import './SpectacolView.css'
//import FormattedDate from './FormattedDate.jsx'
import SpectacolView from './SpectacolView.jsx'

function SpectacoleList({spectacole}) {
  const [items, setItems] = useState(spectacole);

  return (
    <FlatList data={spectacole} renderItem={({ item }) => ( <SpectacolView spectacol={item}/>)}>	  	  	      
    </FlatList>
  )
}

export default SpectacoleList

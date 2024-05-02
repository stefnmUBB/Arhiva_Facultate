import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './SpectacolView.css'
import FormattedDate from './FormattedDate.jsx'

function SpectacolView({spectacol}) {  
  const [id, setId] = useState(spectacol.id)
  const [artist, setArtist] = useState(spectacol.artist);
  const [data, setData] = useState(new Date(spectacol.data));
  const [locatie, setLocatie] = useState(spectacol.locatie);
  const [nrLDisp, setNrLDisp] = useState(spectacol.nrLocuriDisponibile);
  const [nrLVand, setNrLVand] = useState(spectacol.nrLocuriVandute);

  return (
    <>	  	  
      <div>
		<span>
		{id}, {artist}, <FormattedDate date={data}/>, {locatie}, {nrLDisp}, {nrLVand}
		</span>	  
      </div>
    </>
  )
}

export default SpectacolView

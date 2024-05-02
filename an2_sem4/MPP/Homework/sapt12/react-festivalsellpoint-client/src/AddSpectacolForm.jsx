import { useState } from 'react'
import api from './api.js'

function AddSpectacolForm({spectacole}) {  
  const [artist, setArtist] = useState("exampleArtist");
  const [data, setData] = useState(new Date());
  const [locatie, setLocatie] = useState("exampleLocatie");
  const [nrLDisp, setNrLDisp] = useState(10);
  const [nrLVand, setNrLVand] = useState(15);

  return (
    <form>
		Artist : <input type="text" value={artist} onChange={e=>setArtist(e.target.value)} /><br/>
		Data : <input type="datetime-local" value={data} onChange={e=>setData(e.target.value)} /><br/>
		Locatie : <input type="text" value={locatie} onChange={e=>setLocatie(e.target.value)} /><br/>
		NrLocuriDisponibile : <input type="number" value={nrLDisp} onChange={e=>setNrLDisp(e.target.value)} /><br/>
		NrLocuriVandute : <input type="number" value={nrLVand} onChange={e=>setNrLVand(e.target.value)} /><br/>
		<input type="submit" onClick={async (e)=>{
				e.preventDefault();
				var spectacol = {
					artist:artist,
					data:new Date(data).toISOString().substring(0,19),
					locatie:locatie,
					nrLocuriDisponibile:nrLDisp,
					nrLocuriVandute:nrLVand					
				};
				console.log(spectacol);
				await api.add(spectacol, function(r){
					console.log(r);
					alert("Added spectacol. Id="+r.id);
					window.location.reload(false);
				});						
			}
		}/>
	</form>
  )
}

export default AddSpectacolForm

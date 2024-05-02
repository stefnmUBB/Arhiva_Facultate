import { useState } from 'react'
import api from './api.js'

function DeleteSpectacolForm({spectacole}) {  
  const [id, setId] = useState(59);  

  return (
    <form>
		Id : <input type="number" value={id} onChange={e=>setId(e.target.value)} /><br/>		
		<input type="submit" onClick={async (e)=>{
				e.preventDefault();				
				await api.remove(id, function(r){					
					alert("Deleted spectacol. Id="+id);
					window.location.reload(false);
				});						
			}
		}/>
	</form>
  )
}

export default DeleteSpectacolForm

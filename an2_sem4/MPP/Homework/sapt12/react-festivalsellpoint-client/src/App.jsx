import { useState, useEffect } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import SpectacoleList from './SpectacoleList.jsx'
import AddSpectacolForm from './AddSpectacolForm.jsx'
import UpdateSpectacolForm from './UpdateSpectacolForm.jsx'
import DeleteSpectacolForm from './DeleteSpectacolForm.jsx'
import api from './api.js'

function App() {
	const [count, setCount] = useState(0)
	const [defaultSpec, setDefaultSpec] = useState({"id":17,"artist":"artist0","data":"2023-03-15T08:26:00","locatie":"locatie4","nrLocuriDisponibile":62,"nrLocuriVandute":0})
	const [spectacole, setSpectacole] = useState([])
	
	useEffect(()=>{
		const getData = async()=>{
			await api.getAll(setSpectacole);
			console.log(spectacole);
		};
		getData();
	}, []);

	return (
	<>	  
	  <div id="Container">
		<div id="Get">
		  <h2>Spectacole</h2>
		  <div id="SpectacoleContainer">
			<SpectacoleList spectacole={spectacole}/>
		  </div>	  
		</div>
     	  <div id="Add">
			<h2>Add</h2>
			<AddSpectacolForm/>
		  </div>
		  <div id="Update">
			<h2>Update</h2>
			<UpdateSpectacolForm/>
		  </div>
		  <div id="Delete">
			<h2>Delete</h2>
			<DeleteSpectacolForm/>	  
		  </div>
	  </div>
	</>
	)
}

export default App

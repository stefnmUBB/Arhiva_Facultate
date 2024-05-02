async function textOrError(resp) {
	if (resp.ok) {
    return resp.text();
  }
  throw new Error(resp.status+" : "+ await resp.text());
}

async function jsonOrError(resp) {
	if (resp.ok) {
    return resp.json();
  }
  throw new Error(resp.status+" : "+ await resp.text());
}

const api = {
	getAll : function(callback) {
		fetch('http://localhost:8080/festival/spectacole')
			.then(jsonOrError)			
			.then(callback)
			.catch(e=>alert(e));
	},			
	add : function(spectacol, callback) {
		fetch('http://localhost:8080/festival/spectacole',{
			method: 'POST',
			cors:"no-cors",
		    headers: {
			  'Content-Type': 'application/json',
			  'Accept': 'application/json',
		    },
		    body: JSON.stringify(spectacol)
		})		
			.then(jsonOrError)				
			.then(callback)
			.catch(e=>alert(e));
	},
	update : function(spectacol, callback) {
		fetch('http://localhost:8080/festival/spectacole/'+spectacol.id,{
			method: 'PUT',
			cors:"no-cors",
		    headers: {
			  'Content-Type': 'application/json',
			  'Accept': 'application/json',
		    },
		    body: JSON.stringify(spectacol)
		})
			.then(jsonOrError)			
			.then(callback)
			.catch(e=>alert(e));
	},
	remove : function(id, callback) {
		fetch('http://localhost:8080/festival/spectacole/'+id,{
			method: 'DELETE',
			cors:"no-cors",
		    headers: {
			  'Content-Type': 'application/json',
			  'Accept': 'application/json',
		    },		    
		})
			.then(textOrError)
			.then(callback)
			.catch(e=>alert(e));
	}
};

export default api;
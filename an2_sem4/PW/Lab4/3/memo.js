Array.prototype.shuffle = function() {
    for (var i = this.length - 1; i > 0; i--) {
        var j = Math.floor(Math.random() * (i + 1));
        var temp = this[i];
        this[i] = this[j];
        this[j] = temp;
    }
}

function MemoGame(hostElement) {
	var self=this;
	
	self.hostElement = hostElement;	
	self.hostElement.innerHTML = "";
	
	var rows = hostElement.getAttribute("rows");
	var cols = hostElement.getAttribute("cols");
	var face = hostElement.getAttribute("cards");
	
	if(rows*cols % 2 !=0)
		throw "There must be an even number of cells on the board";
	
	self.rowsCount = rows;
	self.colsCount = cols;
	
	hostElement.style.display = "flex";
	hostElement.style.flexWrap = "wrap";
	hostElement.style.flexDirection = "column";
	hostElement.style.justifyContent = "space-evenly";

	self.items = [];	
	self.selectedCards = [];
	
	var k=0;
	for(var r=0;r<self.rowsCount;r++) {				
		for(var c=0;c<self.colsCount;c++) {
			var item = document.createElement("div");					
			
			item.style.flex="0 0 auto";				
			
			item.className="card";			
			
			var back = document.createElement("div");
			back.className="card-back";			
			
			var front = document.createElement("div");			
			front.className="card-front";
			
			var cid = Math.floor((k++)/2);
			
			if(face=="numbers")
				front.innerHTML = cid;			
			else
				front.innerHTML = `<img src="faces/${cid}" style="width:100%; height:100%;"/>`;			
			
			item.appendChild(back);	
			item.appendChild(front);				
			
			item.cardData = { id : cid };
			
			var working=false;
			
			item.onclick = function() {					
				if(this.classList.contains("flipped") || this.classList.contains("hidden"))
					return;
				if(working)
					return;
				
				this.classList.add("flipped");
				self.selectedCards.push(this);
								
				if(self.selectedCards.length==2) {
					working=true;
					setTimeout(function(){										
						var card1 = self.selectedCards[0].cardData;
						var card2 = self.selectedCards[1].cardData;						
						
						if(card1.id == card2.id) {													
							self.selectedCards[0].classList.add("hidden");
							self.selectedCards[1].classList.add("hidden");
						}
						else{
							self.selectedCards[0].classList.remove("flipped");
							self.selectedCards[1].classList.remove("flipped");
						}							
						self.selectedCards=[];				
						working=false;
					}, 1500);			
				}
			}
			
			self.items.push(item);			
		}
	}
	
	self.items.shuffle();
	
	for(var r=0;r<self.rowsCount;r++) {		
		var rowPadLeft = document.createElement("div");
		rowPadLeft.style.display="flex";
		rowPadLeft.style.flex="1 0 40%";
		rowPadLeft.className="padLeft";
		rowPadLeft.style.minHeight="0";
	
		var rowElem = document.createElement("div");
		rowElem.style.display="flex";
		rowElem.style.flexDirection = "row";		
		rowElem.style.flex="0 0 auto";		
		rowElem.style.minHeight="0";
		rowElem.style.justifyContent="space-evenly";
		rowElem.style.alignItems="center";
		
		var rowPadRight = document.createElement("div");
		rowPadRight.style.display="flex";
		rowPadRight.style.flex="1 0 0";
		rowPadRight.className="padRight";
		rowPadRight.style.minHeight="0";
		
		for(var c=0;c<self.colsCount;c++) {
			rowElem.appendChild(self.items[r*self.colsCount+c]);
		}
		//self.hostElement.appendChild(rowPadLeft);
		self.hostElement.appendChild(rowElem);
		//self.hostElement.appendChild(rowPadRight);
	}	
	
	
	
	
	return self;	
}
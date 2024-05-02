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
	self.hostElement.html("");
	
	var rows = hostElement.attr("rows");
	var cols = hostElement.attr("cols");
	var face = hostElement.attr("cards");
	
	if(rows*cols % 2 !=0)
		throw "There must be an even number of cells on the board";
	
	self.rowsCount = rows;
	self.colsCount = cols;
	
	hostElement.css("display", "flex");
	hostElement.css("flexWrap", "wrap");
	hostElement.css("flexDirection", "column");
	hostElement.css("justifyContent", "space-evenly");

	self.items = [];	
	self.selectedCards = [];
	
	var k=0;
	for(var r=0;r<self.rowsCount;r++) {				
		for(var c=0;c<self.colsCount;c++) {
			var item = $("<div></div>");
			
			item.css("flex", "0 0 auto");
			
			item.addClass("card");
			
			var back = $("<div></div>");
			back.addClass("card-back");
			
			var front = $("<div></div>");			
			front.addClass("card-front");
			
			var cid = Math.floor((k++)/2);
			
			if(face=="numbers")
				front.html(cid);			
			else
				front.html(`<img src="faces/${cid}" style="width:100%; height:100%;"/>`);			
			
			item.append(back);
			item.append(front);				
			
			item.data("cardData", { id : cid });
			
			var working=false;
			
			item.click(function() {									
				if($(this).hasClass("flipped") || $(this).hasClass("hidden"))
					return;
				if(working)
					return;
				
				$(this).addClass("flipped");
				self.selectedCards.push($(this));
								
				if(self.selectedCards.length==2) {
					working=true;
					setTimeout(function(){										
						var card1 = self.selectedCards[0].data("cardData");
						var card2 = self.selectedCards[1].data("cardData");
						
						if(card1.id == card2.id) {													
							self.selectedCards[0].addClass("hidden");
							self.selectedCards[1].addClass("hidden");
						}
						else{
							self.selectedCards[0].removeClass("flipped");
							self.selectedCards[1].removeClass("flipped");
						}							
						self.selectedCards=[];				
						working=false;
					}, 1500);			
				}
			});
			
			self.items.push(item);			
		}
	}
	
	self.items.shuffle();
	
	for(var r=0;r<self.rowsCount;r++) {		
		var rowPadLeft = $("<div></div>");
		rowPadLeft.css("display","flex");
		rowPadLeft.css("flex","1 0 40%");
		rowPadLeft.addClass("padLeft");
		rowPadLeft.css("minHeight","0");
	
		var rowElem = $("<div></div>");
		rowElem.css("display", "flex");
		rowElem.css("flexDirection", "row");		
		rowElem.css("flex", "0 0 auto");
		rowElem.css("minHeight", "0");
		rowElem.css("justifyContent","space-evenly");
		rowElem.css("alignItems","center");
		
		var rowPadRight = $("<div></div>");
		rowPadRight.css("display", "flex");
		rowPadRight.css("flex", "1 0 0");
		rowPadRight.addClass("padRight");
		rowPadRight.css("minHeight","0");
		
		for(var c=0;c<self.colsCount;c++) {
			rowElem.append(self.items[r*self.colsCount+c]);
		}
		//self.hostElement.appendChild(rowPadLeft);
		self.hostElement.append(rowElem);
		//self.hostElement.appendChild(rowPadRight);
	}	
	
	
	
	
	return self;	
}
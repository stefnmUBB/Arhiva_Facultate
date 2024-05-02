async function imageSize(url) {
	const img = new Image();
	img.src = url;
	await img.decode();  
	return {'width':img.width, 'height':img.height};
}

async function SlidingPuzzle(div) {
	var self={};
	
	div.css("display","flex");
	div.css("justifyContent","center");
	
	self.host = div;	
	self.size = div.attr("n");
	self.source = div.attr("src");	

	if(self.source!="#") {
		var size = await imageSize(self.source);
		self.imageWidth = size.width;
		self.imageHeight = size.height;
	}
	
	var table = $("<table></table>");
	self.table=table;		

	self.board = function(i, j) {										
		return $("tr",self.table)[i].children[j];
	}
	
	for(var i=0;i<self.size;i++) {
		var row = $("<tr></tr>");
		for(var j=0;j<self.size;j++) {			
			var cell = $("<td></td>");
			cell.pieceId = i*self.size+j;
			cell.attr("pieceId",i*self.size+j);
			if(i!=self.size-1 || j!=self.size-1){
				if(self.source=="#") {
					cell.html(cell.pieceId);				
					cell.css("textAlign","center");
				}
				else {
					var cw = self.imageWidth / self.size;
					var ch = self.imageHeight / self.size;
					cell.css("width",cw+"px");
					cell.css("height",ch+"px");
					cell.css("background",`url('${self.source}') -${cw*j}px -${ch*i}px`);
				}					
			}						
			row.append(cell);			
		}
		table.append(row);
	}	
	
	self.emptyCell = self.size*self.size-1;	
	
	self.host.html("");
	self.host.append(table);		
	
	function swap(node1, node2) {		
		var tmp = $(node1).clone(true);
		$(node1).replaceWith($(node2).clone(true));
		$(node2).replaceWith(tmp);		
	}
	
	self.moveRight = function() {
		var r = Math.floor(self.emptyCell/self.size);
		var c = Math.floor(self.emptyCell%self.size);			
		if(c==0) return;				
		swap(self.board(r,c), self.board(r,c-1));
		self.emptyCell--;		
	}
	
	self.moveLeft = function() {
		var r = Math.floor(self.emptyCell/self.size);
		var c = Math.floor(self.emptyCell%self.size);							
		if(c==self.size-1) return;				
		swap(self.board(r,c), self.board(r,c+1));
		self.emptyCell++;		
	}
	
	self.moveDown = function() {
		var r = Math.floor(self.emptyCell/self.size);
		var c = Math.floor(self.emptyCell%self.size);			
		if(r==0) return;				
		swap(self.board(r,c), self.board(r-1,c));
		self.emptyCell=Number(self.emptyCell) - Number(self.size)
	}	
	
	self.moveUp = function() {
		var r = Math.floor(self.emptyCell/self.size);
		var c = Math.floor(self.emptyCell%self.size);			
		if(r==self.size-1) return;				
		swap(self.board(r,c), self.board(r+1,c));		
		self.emptyCell=Number(self.emptyCell) + Number(self.size);
	}
	
	self.check = function() {		
		for(var i=0;i<self.size;i++) {
			for(var j=0;j<self.size;j++) {				
				if(self.board(i,j).getAttribute("pieceId")!=i*self.size+j)
					return false;
			}				
		}
		return true;
	}
	
	
	for(i=0;i<100;i++) {
		var x = Math.floor(Math.random()*4);
		if(x==0)
			self.moveLeft();
		else if(x==1)
			self.moveRight();
		else if(x==2)
			self.moveUp();
		else if(x==3)
			self.moveDown();
	}
		
	
	table.click(function() {						
		console.log(this);
		$(this).attr("tabindex",0);
		$(this).focus();
	});
	
	table.on("keydown",function(e) {			
		if(e.keyCode==37) {						
			$(this).data("board").moveLeft();			
			if($(this).data("board").check()){
				alert("Well done!!");
			}
		}
		else if(e.keyCode==39) {
			$(this).data("board").moveRight();			
			if($(this).data("board").check()){
				alert("Well done!!");
			}
		}
		else if(e.keyCode==38) {
			$(this).data("board").moveUp();			
			if($(this).data("board").check()){
				alert("Well done!!");
			}
		}
		else if(e.keyCode==40) {
			$(this).data("board").moveDown();
			if($(this).data("board").check()){
				alert("Well done!!");
			}
		}				
	});
		
	table.data("board", self);
	
	
	return self;				
}

window.addEventListener("keydown", function(e) {
    if(["Space","ArrowUp","ArrowDown","ArrowLeft","ArrowRight"].indexOf(e.code) > -1) {
        e.preventDefault();
    }
}, false);

function initSlidingPuzzles() {
	$('div.slidingpuzzle').each((i,e)=>SlidingPuzzle($(e)));
	/*var puzzles = document.querySelectorAll('div.slidingpuzzle');
	for(var i=0;i<puzzles.length;i++)
		SlidingPuzzle(puzzles[i]);*/
}
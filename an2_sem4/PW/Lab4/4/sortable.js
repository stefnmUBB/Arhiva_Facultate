function TableAccessor(table) {
	var self=this;
	
	function Cell(cell, row, col) {
		this.row = row;
		this.col = col;
		this.accessor = self;
		this.cell = cell;
		this.cell.cellData = this;
		this.isHeader = cell.tagName=="TH" || cell.tagName=="th";		
		this.value = cell.innerHTML;		
		return this;
	}
	
	self.table = table;
	
	var rows = table.querySelectorAll("tr");
	
	loadTable();
	
	self.sortStartingPoint = {'row':0, 'col':0 };
	
	if(self[0][1].isHeader) {
		self.sortStartingPoint.row = 1;
	}
	
	if(self[1][0].isHeader) {
		self.sortStartingPoint.col = 1;
	}
	
	console.log(self.sortStartingPoint);
	
	function getRows() {
		var r0 = self.sortStartingPoint.row;
		var c0 = self.sortStartingPoint.col;		
		var rows = [];		
		for(var i=r0;i<self.rowsCount;i++) {
			var row=[];
			for(var j=0;j<self.colsCount;j++) row.push(self[i][j]);			
			rows.push(row);
		}
		return rows;
	}	
	
	function getCols() {
		var r0 = self.sortStartingPoint.row;
		var c0 = self.sortStartingPoint.col;		
		var cols = [];		
		for(var j=c0;j<self.colsCount;j++) {
			var col=[];
			for(var i=0;i<self.rowsCount;i++) col.push(self[i][j]);			
			cols.push(col);
		}
		return cols;
	}		
	
	self.orderByColumn = function(colId) {
		//colId-=self.sortStartingPoint.col;
		var rows = getRows();		
		console.log(rows);
		rows.sort((a,b)=>{ var x=a[colId].value; var y=b[colId].value; if(isNaN(x)||isNaN(y)) return (x<y)?-1:1; else return x-y; });								
		commitRows(rows);		
	}	
	
	self.orderByRow = function(rowId) {
		//rowId-=self.sortStartingPoint.row;
		var cols = getCols();			
		cols.sort((a,b)=>{ var x=a[rowId].value; var y=b[rowId].value; if(isNaN(x)||isNaN(y)) return (x<y)?-1:1; else return x-y; });								
		commitCols(cols);		
	}	
	
	function commitRows(rows) {
		console.log(rows);
		var r0 = self.sortStartingPoint.row;
		var c0 = self.sortStartingPoint.col;				
		for(var i=r0;i<self.rowsCount;i++) {			
			for(var j=0;j<self.colsCount;j++) 
				self[i][j] = rows[i-r0][j];
		}
		commitTable();
	}
	
	function commitCols(cols) {
		console.log(cols);
		var r0 = self.sortStartingPoint.row;
		var c0 = self.sortStartingPoint.col;			
		for(var i=0;i<self.rowsCount;i++) {			
			for(var j=c0;j<self.colsCount;j++) 
				self[i][j] = cols[j-c0][i];
		}
		commitTable();
	}
	
	function commitTable() {
		table.innerHTML = "";
		
		for(var i=0;i<self.rowsCount;i++) {
			var row = document.createElement("tr");
			for(var j=0;j<self.colsCount;j++) {
				row.appendChild(self[i][j].cell);
				self[i][j].row=i;
				self[i][j].col=j;
			}
			table.appendChild(row);
		}
	}
	
	function loadTable(){
		self.rowsCount = rows.length;	
		for(var i=0;i<rows.length;i++) {
			var row = rows[i];
			
			self[i] = {};
			var cols = row.querySelectorAll("th, td");
			console.log(cols);			
			for(var j=0;j<cols.length;j++) {
				self[i][j] = new Cell(cols[j], i, j);
			}			
			self.colsCount = cols.length;
		}	
	}
	
	for(var i=self.sortStartingPoint.col;i<self.colsCount;i++) {
		if(self[0][i].isHeader) {
			self[0][i].cell.onclick=function(){				
				console.log(this.cellData);
				this.cellData.accessor.orderByColumn(this.cellData.col);
			}
		}
	}
	
	for(var i=self.sortStartingPoint.row;i<self.rowsCount;i++) {
		if(self[i][0].isHeader) {
			self[i][0].cell.onclick=function(){				
				console.log(this.cellData);
				this.cellData.accessor.orderByRow(this.cellData.row);
			}
		}
	}
	
	return this;
}

function initSortable(){
	var tables = document.querySelectorAll("table.sortable");	
	for(var i=0;i<tables.length;i++) {
		new TableAccessor(tables[i]);
	}
}
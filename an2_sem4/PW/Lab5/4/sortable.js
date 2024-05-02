function TableAccessor(table) {
	var self=this;
	
	function Cell(cell, row, col) {		
		this.row = row;
		this.col = col;
		this.accessor = self;
		this.cell = cell;		
		this.clickHandler=null;
		this.cell.data("cellData", this);		
		this.isHeader = this.cell[0].tagName=="TH" || this.cell[0].tagName=="th";
		this.value = cell.html();		
		return this;
	}
	
	self.table = table;
	
	var rows = $("tr", table);
	
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
		var r0 = self.sortStartingPoint.row;
		var c0 = self.sortStartingPoint.col;				
		for(var i=r0;i<self.rowsCount;i++) {			
			for(var j=0;j<self.colsCount;j++) 
				self[i][j] = rows[i-r0][j];
		}
		commitTable();
	}
	
	function commitCols(cols) {		
		var r0 = self.sortStartingPoint.row;
		var c0 = self.sortStartingPoint.col;			
		for(var i=0;i<self.rowsCount;i++) {			
			for(var j=c0;j<self.colsCount;j++) 
				self[i][j] = cols[j-c0][i];
		}
		commitTable();
	}
	
	function commitTable() {
		
		var dataBak = {};
		for(var i=0;i<self.rowsCount;i++) {
			dataBak[i]={};
			for(var j=0;j<self.colsCount;j++) {
				dataBak[i][j]=self[i][j].cell.data("cellData");
				console.log(dataBak[i][j])
			}			
		}
		
		table.html("");
		
		for(var i=0;i<self.rowsCount;i++) {
			var row = $("<tr></tr>");
			for(var j=0;j<self.colsCount;j++) {
				var data = dataBak[i][j];
				var cell = data.isHeader  ? $("<th></th>") : $("<td></td>");
				cell.data("cellData", data);
				cell.html(data.value);				
				row.append(cell);
				data.cell=cell;				
				self[i][j].data=data;				
				self[i][j].row=i;
				self[i][j].col=j;										
			}
			table.append(row);
		}
		for(var i=self.sortStartingPoint.col;i<self.colsCount;i++) {
			if(self[0][i].isHeader) {
				self[0][i].clickHandler=(e)=> { var th = $(e.target); var data = th.data("cellData"); data.accessor.orderByColumn(data.col)};			
				self[0][i].cell.click(self[0][i].clickHandler);
			}
		}
		
		for(var i=self.sortStartingPoint.row;i<self.rowsCount;i++) {
			if(self[i][0].isHeader) {
					self[i][0].clickHandler=(e)=> { var th = $(e.target); var data = th.data("cellData"); data.accessor.orderByRow(data.row)};
					self[i][0].cell.click(self[i][0].clickHandler);						
			}
		}
	}
	
	function loadTable(){		
		self.rowsCount=rows.length;
		rows.each((i,row)=>{
			self[i] = {};
			var cols = $("th, td", row);			
					
			cols.each((j, col)=> self[i][j] = new Cell($(col), i, j));			
			self.colsCount = cols.length;
		});			
	}
	
	for(var i=self.sortStartingPoint.col;i<self.colsCount;i++) {
		if(self[0][i].isHeader) {
			self[0][i].clickHandler=(e)=> { var th = $(e.target); var data = th.data("cellData"); data.accessor.orderByColumn(data.col)};			
			self[0][i].cell.click(self[0][i].clickHandler);
		}
	}
	
	for(var i=self.sortStartingPoint.row;i<self.rowsCount;i++) {
		if(self[i][0].isHeader) {
				self[i][0].clickHandler=(e)=> { var th = $(e.target); var data = th.data("cellData"); data.accessor.orderByRow(data.row)};
				self[i][0].cell.click(self[i][0].clickHandler);						
		}
	}	
	return this;	
}

function initSortable(){
	$("table.sortable").each((i,e)=>new TableAccessor($(e)));	
}
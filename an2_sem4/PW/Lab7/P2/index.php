<?php	
	$page = isset($_GET['page']) ? $_GET['page'] : 1;	
	$act = isset($_GET['act']) ? $_GET['act'] : "";
	$perPage = isset($_GET['perPage']) ? $_GET['perPage'] : 5;		
	
	if($act=="Prev" and $page>1) {
		$page-=1;		
	}	
	if($act=="Next") {
		$page+=1;	
	}	
	$firstRow = ($page-1)*$perPage;
	
	mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
	$mysqli = new mysqli("localhost", "root", "", "pw_php");
?>

<html>	
	<head>
	
	</head>
	
	<body>	
		<form action="#" method="GET">			
			<input type="text" name="firstRow" value="<?php echo $firstRow;?>" hidden>
			<input type="submit" name="act" value="Prev">						
			Page
			<input type="text" name="page" value="<?php echo $page;?>" readonly style="width:40px">
			<input type="submit" name="act" value="Next">
			Entries per page
			<select name="perPage">
				<option <?php echo $perPage== 5 ? "selected":"" ?> >5</option>
				<option <?php echo $perPage==10 ? "selected":"" ?> >10</option>
				<option <?php echo $perPage==15 ? "selected":"" ?> >15</option>
				<option <?php echo $perPage==20 ? "selected":"" ?> >20</option>
			</select>
		</form>
		
		<table>
			<?php
				$query = "SELECT * FROM `chestii` ORDER BY `id` LIMIT {$perPage} OFFSET {$firstRow}";
				$result = $mysqli->query($query);
				
				echo "<tr><th>id</th><th>nume</th><th>tip</th><th>greutate</th></tr>";		
				foreach($result as $row) {
					echo "<tr>";					
					echo "<td>{$row["id"]}</td>";
					echo "<td>{$row["nume"]}</td>";
					echo "<td>{$row["tip"]}</td>";
					echo "<td>{$row["greutate"]}</td>";
					echo "</tr>";

				}
		
			?>
		</table>
		
	</body>		
</html>
<?php

	mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
	$mysqli = new mysqli("localhost", "root", "", "pw_ajax");

	$result = $mysqli->query("SELECT DISTINCT Oras1 FROM problema1");	
	
	echo '[';
	$i=0;
	foreach ($result as $row) {
		if($i>0) echo ', ';
		echo '"'.$row['Oras1'].'"';		
		$i+=1;
	}
	
	echo ']';
?>
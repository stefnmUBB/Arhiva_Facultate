<?php
	mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
	$mysqli = new mysqli("localhost", "root", "", "pw_ajax");

	$result = $mysqli->query("SELECT id FROM problema3");
	
	echo "[";
	$first=true;
	foreach($result as $row) {
		if(!$first) echo ",";		
		$first=false;
		echo $row["id"];		
	}
	echo "]";
?>
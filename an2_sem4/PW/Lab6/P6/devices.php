<?php
	mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
	$mysqli = new mysqli("localhost", "root", "", "pw_ajax");

	$query = "SELECT * FROM problema6";

	if(isset($_GET['manu'])){
		$query=$query . " WHERE `manufacturer` LIKE '%{$_GET['manu']}%'";
		$query=$query . " AND `cpu` LIKE '%{$_GET['cpu']}%'";
		$query=$query . " AND `ram` LIKE '%{$_GET['ram']}%'";
	}	

	$result = $mysqli->query($query);
	
	echo "[";
	$first=true;
	foreach($result as $row) {
		if(!$first) echo",";
		$first=false;
		echo "[\"{$row['manufacturer']}\",\"{$row['model']}\",\"{$row['cpu']}\",\"{$row['ram']}\"]";
	}
	echo "]";

?>
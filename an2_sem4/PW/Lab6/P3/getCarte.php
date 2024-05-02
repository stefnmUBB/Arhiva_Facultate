<?php

	mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
	$mysqli = new mysqli("localhost", "root", "", "pw_ajax");
	
	$id = mysqli_real_escape_string($mysqli, $_GET['id']);

	$result = mysqli_fetch_assoc($mysqli->query("SELECT * FROM problema3 WHERE `id` = '". $id ."'"));
		
	echo '["'. $result['titlu'] . '","' . $result['autor'] . '","' . $result['gen'] . '"]';
	
	

?>
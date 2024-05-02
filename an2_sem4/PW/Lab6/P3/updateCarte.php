<?php

	mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
	$mysqli = new mysqli("localhost", "root", "", "pw_ajax");
	
	
	$id = mysqli_real_escape_string($mysqli, $_POST['id']);
	$titlu = mysqli_real_escape_string($mysqli, $_POST['titlu']);
	$autor = mysqli_real_escape_string($mysqli, $_POST['autor']);
	$gen = mysqli_real_escape_string($mysqli, $_POST['gen']);
	
	
	$q = "UPDATE problema3 SET `titlu`='{$titlu}', `autor`='{$autor}', gen='{$gen}' where `id` = {$id}";	
	
	$mysqli->query($q);	
	
	echo "OK";
?>
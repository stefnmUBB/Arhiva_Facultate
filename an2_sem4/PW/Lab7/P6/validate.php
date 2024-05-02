<?php
	mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
	$mysqli = new mysqli("localhost", "root", "", "pw_php");

	$i = mysqli_escape_string($mysqli, $_POST["id"]);	
	$mysqli->query("UPDATE `comentarii` SET `valid`='1' WHERE `id`='{$i}'");
	echo "Ok.";

?>
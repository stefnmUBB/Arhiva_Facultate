<?php		
	mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
	$mysqli = new mysqli("localhost", "root", "", "pw_php");
	
	$n = mysqli_escape_string($mysqli, $_POST["nume"]);
	$t = mysqli_escape_string($mysqli, $_POST["text"]);	
	$mysqli->query("INSERT INTO `comentarii` (`nume`, `text`, `valid`) VALUES ('{$n}', '{$t}', '0')");
	echo "Ok.";

?>
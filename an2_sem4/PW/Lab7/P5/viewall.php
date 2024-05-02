<?php
	$q = 'SELECT * FROM `profi2`';
	mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
	$mysqli = new mysqli("localhost", "root", "", "pw_php");
	
	echo "<table>";
	echo "<tr>";
	echo "<th>user</th>";
	echo "<th>profile pic</th>";
	echo "</tr>";
	foreach($mysqli->query($q) as $row){
		echo "<tr>";
		echo "<td>{$row["user"]}</td>";
		echo "<td><img src=\"{$row["pp64"]}\"></td>";		
		echo "</tr>";
	}
	echo "</table>";

?>
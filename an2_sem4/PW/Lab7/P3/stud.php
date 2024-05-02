<?php
	$q = 'SELECT S.nume, S.prenume, M.nume materie, N.valoare nota FROM `note` N INNER JOIN `materii` M ON N.idMaterie=M.id INNER JOIN `studenti` S ON N.idStudent=S.id';
	mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
	$mysqli = new mysqli("localhost", "root", "", "pw_php");
	
	echo "<table>";
	echo "<tr>";
	echo "<th>Nume</th>";
	echo "<th>Prenume</th>";
	echo "<th>Materie</th>";
	echo "<th>Nota</th>";
	echo "</tr>";
	foreach($mysqli->query($q) as $row){
		echo "<tr>";
		echo "<td>{$row["nume"]}</td>";
		echo "<td>{$row["prenume"]}</td>";
		echo "<td>{$row["materie"]}</td>";
		echo "<td>{$row["nota"]}</td>";		
		echo "</tr>";
	}
	echo "</table>";

?>
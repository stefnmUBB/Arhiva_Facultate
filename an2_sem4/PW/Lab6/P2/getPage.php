<?php
	mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
	$mysqli = new mysqli("localhost", "root", "", "pw_ajax");



	if(isset($_GET['page']))
		$page = mysqli_real_escape_string($mysqli, $_GET['page']);
	else 
		$page = 1;
		
	$firstRow = 3*($page-1);

	$query = "SELECT * FROM `problema2` ORDER BY prenume LIMIT 3 OFFSET ". $firstRow;
		
	$result = $mysqli->query($query);
	
	$count = mysqli_fetch_assoc($mysqli->query("SELECT COUNT(*) cnt FROM `problema2`"))["cnt"];
		
		
	echo "{\"records\":[";
	$first = true;
	foreach($result as $row) {
		if(!$first) echo ", ";
		$first=false;
		echo "["
			. "\"". $row["nume"] . "\", "			
			. "\"". $row["prenume"] . "\", "			
			. "\"". $row["telefon"] . "\", "			
			. "\"". $row["email"] . "\"]"						
			;		
	}
	echo "], \"hasNext\":" . ($firstRow+3<$count ? "true":"false") . ", \"hasPrev\":" . ($firstRow>1 ? "true":"false") 
		. ", \"currentPage\":". $page
		. ", \"pagesCount\":" . floor(($count+2)/3)
	. "}";
?>

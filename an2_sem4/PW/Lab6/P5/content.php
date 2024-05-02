<?php

	$path = $_GET['file'];
	
	if(str_ends_with($path,'.jpg')){
		echo "<img src='{$path}'>";
	}
	else {			
		$content = file_get_contents($path);
		echo $content;
	}

?>
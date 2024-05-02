<?php
	function getDirs($dr){
		//echo $dr."<br/>";
		$dirs = scandir($dr);
		
		$result="[";
		$first=true;
		foreach($dirs as $item) {
			if ($item=="." or $item=="..")
				continue;
			
			if(!$first) $result=$result.",";
			$first=false;					
			$path = $dr . '/' . $item;
			if(!is_dir($path)) {
				$result = $result. "{\"name\":\"{$item}\",\"type\":\"file\", \"path\":\"{$path}\"}";
			} else {
				$subtree = getDirs($path);
				$result = $result. "{\"name\":\"{$item}\",\"type\":\"dir\", \"path\":\"{$path}\", \"children\":{$subtree}}";
			}			
		}
		$result=$result.']';
		return $result;
	}

	echo getDirs('root');

?>
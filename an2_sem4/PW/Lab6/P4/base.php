<?php
	function generateBoard() {
		$board = array('','','','','','','','','');
		$serverRole = rand(0,10)<5 ? 'X':'O';
		$clientRole = $serverRole=='X'?'O':'X';
		
		if($serverRole=='X') {
			return place('X', $board);			
		}
		
		return array(
			"board" => $board,
			"player_turn"=> $clientRole,
			"game_status"=> game_status($board)
		);						
	}

	function symbol($board, $row, $col) { return $board[3*$row+$col]; }
	
	function checkSame($a, $b, $c) { return $a==$b && $a==$c && $a!=""; }

	function game_status($board) {			
		for($i=0;$i<3;$i++) {
			if(checkSame(symbol($board, $i, 0), symbol($board, $i, 1), symbol($board, $i, 2))
			 ||checkSame(symbol($board, 0, $i), symbol($board, 1, $i), symbol($board, 2, $i)))
			return symbol($board, $i, $i);					
		}
		
		if(checkSame(symbol($board, 0, 0), symbol($board, 1, 1), symbol($board, 2, 2))
			 ||checkSame(symbol($board, 0, 2), symbol($board, 1, 1), symbol($board, 2, 0)))
			return symbol($board, 1,1);
			
		for($i=0;$i<9;$i++){
			if($board[$i]=='')
				return "going";
		}
		return "draw";
	}

	function place($symbol, $board) {
		$emptyIndices = array();
		for($i=0;$i<9;$i++) {
			if($board[$i]=='') {
				array_push($emptyIndices, $i);
			}
		}		
		
		if(count($emptyIndices)==0) {
			return array(
				"board" => $board,
				"player_turn"=> ($symbol=='X'?'O':'X'),
				"game_status"=> game_status($board)
			);
		}
			
		$eindex = array_rand($emptyIndices,1);
		
		$board[$emptyIndices[$eindex]]=$symbol;
		
		return array(
			"board" => $board,
			"player_turn"=> ($symbol=='X'?'O':'X'),
			"game_status"=> game_status($board)
		);		
	}
	
	function show($board) {
		echo "{\"board\":[\"{$board['board'][0]}\",\"{$board['board'][1]}\",\"{$board['board'][2]}\",\"{$board['board'][3]}\",\"{$board['board'][4]}\",\"{$board['board'][5]}\",\"{$board['board'][6]}\",\"{$board['board'][7]}\",\"{$board['board'][8]}\"],".
			"\"player_turn\":\"{$board['player_turn']}\", \"status\":\"{$board['game_status']}\""		
		
			."}";		
	}

?>
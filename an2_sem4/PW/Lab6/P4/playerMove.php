<?php
	header("application/plain; charset=UTF-8", true);

	session_start();
	include './base.php';
	
	$game=$_SESSION['game'];
	
	$cellId = $_POST["cellId"][1];	
	
	$cell=intval($cellId);	
		
	
	$game["board"][$cell]=$game["player_turn"];
	$game["player_turn"] = $game["player_turn"]=='X' ? 'O':'X';	
	
	$game["game_status"]=game_status($game["board"]);
	
	
	if($game["game_status"]=="going"){	
		$game = place($game["player_turn"], $game["board"]);
	}
	else
	{		
		
	}
	
	$_SESSION['game'] = $game;
	
	show($game);	 
?>
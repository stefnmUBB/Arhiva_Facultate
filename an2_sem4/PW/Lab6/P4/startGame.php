<?php		
	session_start();

	include './base.php';
	$game = generateBoard();
	
	$_SESSION['game'] = $game;
	
	show($game);
	/*$game = place($game["player_turn"], $game["board"]); show($game);
	$game = place($game["player_turn"], $game["board"]); show($game);
	$game = place($game["player_turn"], $game["board"]); show($game);
	$game = place($game["player_turn"], $game["board"]); show($game);
	$game = place($game["player_turn"], $game["board"]); show($game);
	$game = place($game["player_turn"], $game["board"]); show($game);
	$game = place($game["player_turn"], $game["board"]); show($game);
	$game = place($game["player_turn"], $game["board"]); show($game);*/	
	
	

?>
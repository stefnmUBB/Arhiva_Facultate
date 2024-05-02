<?php
	session_start();			
	mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
	$mysqli = new mysqli("localhost", "root", "", "pw_php");

	$logged = isset($_SESSION["logged"]) ? $_SESSION["logged"] : false;				
	
	if(isset($_POST["act"])) {
		if($_POST["act"]=="Login"){
			$user = mysqli_real_escape_string($mysqli, $_POST["user"]);
			$pass = md5(mysqli_real_escape_string($mysqli, $_POST["pass"]));				
			if(mysqli_num_rows($mysqli->query("SELECT * FROM `profi` WHERE `user`='{$user}' AND `pass`='{$pass}'"))>0){
				$_SESSION["logged"] = true;
				header("Refresh:0");
			}
			
		}			
		else if($_POST["act"]=="Logout"){
			$_SESSION["logged"] = false;
			echo $logged;
			header("Refresh:0");			
		}	
		else if($_POST["act"]=="AddNota") {
			$st = mysqli_real_escape_string($mysqli, $_POST["student"]);
			$mt = mysqli_real_escape_string($mysqli, $_POST["materie"]);
			$nt = mysqli_real_escape_string($mysqli, $_POST["nota"]);
			$mysqli->query("INSERT INTO `note` (`idStudent`, `idMaterie`, `valoare`) VALUES ('{$st}', '{$mt}', '{$nt}')");			
			header("Refresh:0");
		}
	}		
?>

<html>
	<head></head>

	<body>		
		<?php if(!$logged) { ?>
			<h2>Login</h2>
			<form action="#" method="POST">
				Username <input type="text" name="user"><br/>
				Password <input type="password" name="pass"><br/>
				<input type="submit" name="act" value="Login">
			</form>	
		<?php } else { ?>
			
			<form action="#" method="POST">
				Student:
				<select name="student">
					<?php 
						$result = $mysqli->query("SELECT * FROM `studenti`");
						foreach($result as $row){
							echo "<option value='{$row["id"]}'>";
							echo $row["nume"] . " " . $row["prenume"];
							echo "</option>";
						}																
					?>
				</select><br/>
				
				Materie:
				<select name="materie">
					<?php 
						$result = $mysqli->query("SELECT * FROM `materii`");
						foreach($result as $row){
							echo "<option value='{$row["id"]}'>";
							echo $row["nume"];
							echo "</option>";
						}																
					?>
				</select><br/>
				Nota : <input type="number" name="nota" value="10"><br/>
				
				<input type="submit" name="act" value="AddNota">
			</form>
		
		
		
			<form action="#" method="POST">			
				<input type="submit" name="act" value="Logout">
			</form>
		
		<?php } ?>			
	</body>
</html>
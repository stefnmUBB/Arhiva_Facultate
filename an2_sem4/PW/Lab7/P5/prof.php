<?php
	session_start();			
	mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
	$mysqli = new mysqli("localhost", "root", "", "pw_php");

	$logged = isset($_SESSION["logged"]) ? $_SESSION["logged"] : false;				
	
	if(isset($_POST["act"])) {
		if($_POST["act"]=="Login"){
			$user = mysqli_real_escape_string($mysqli, $_POST["user"]);
			$pass = md5(mysqli_real_escape_string($mysqli, $_POST["pass"]));				
			if(mysqli_num_rows($mysqli->query("SELECT * FROM `profi2` WHERE `user`='{$user}' AND `pass`='{$pass}'"))>0){
				$r  = mysqli_fetch_assoc($mysqli->query("SELECT * FROM `profi2` WHERE `user`='{$user}' AND `pass`='{$pass}'"));
				
				$_SESSION["logged"] = true;
				$_SESSION["user"] = $user;				
				$_SESSION["pp"] = $r["pp64"];				
				echo $pp;
				header("Refresh:0");
			}
			
		}			
		else if($_POST["act"]=="Logout"){
			$_SESSION["logged"] = false;
			echo $logged;
			header("Refresh:0");			
		}			
		else if($_POST["act"]=="UploadPic"){			
			$pp="data:image/jpg;base64," . base64_encode(file_get_contents($_FILES['ppic']["tmp_name"]));
			$mysqli->query("UPDATE `profi2` SET `pp64`='$pp' WHERE `user`='{$_SESSION["user"]}'");						
			$_SESSION["pp"] = $pp;							
		}		
		else if($_POST["act"]=="ClearPic"){			
			$pp="data:image/jpg;base64,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";
			$mysqli->query("UPDATE `profi2` SET `pp64`='$pp' WHERE `user`='{$_SESSION["user"]}'");						
			$_SESSION["pp"] = $pp;							
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
			<?php echo $_SESSION["user"];?>
			<img src="<?php echo $_SESSION["pp"];?>">
			<form action="#" method="POST" enctype="multipart/form-data">			
				<input type="file" name="ppic" accept=".jpg, .jpeg, .png">
				<input type="submit" name="act" value="UploadPic">
				<input type="submit" name="act" value="ClearPic">
			</form>
		
		
		
			<form action="#" method="POST">			
				<input type="submit" name="act" value="Logout">
			</form>
		
		<?php } ?>			
	</body>
</html>
<html>

	<body>
		<p>Mihai Viteazul (n. 1558, Târgu de Floci, Țara Românească – d. 19 august 1601, Câmpia Turzii, Principatul Transilvaniei) a fost domnul Țării Românești între 1593-1600. Pentru o perioadă (în 1600), a fost conducător de facto al celor trei state medievale care formează România de astăzi: Țara Românească, Transilvania și Moldova. Înainte de a ajunge pe tron, ca boier, a deținut dregătoriile de bănișor de Strehaia, stolnic domnesc și ban al Craiovei.
Figura lui Mihai Viteazul a ajuns în panteonul național românesc după ce a fost recuperată de istoriografia românească a secolului al XIX-lea, un rol important jucându-l opul Românii supt Mihai-Voievod Viteazul al lui Nicolae Bălcescu. Astfel voievodul a ajuns un precursor important al unificării românilor, care avea să se realizeze în secolul al XX-lea.
		</p>
		
		<h1>Comentarii</h1>
		
		<form action="addcom.php" method="POST">
			Nume<input type="text" name="nume"><br/>
			Text<Textarea type="text" name="text"></textarea><br/>
			<input type="submit">
		</form>
		
		<?php
			mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
			$mysqli = new mysqli("localhost", "root", "", "pw_php");
		
			$res = $mysqli->query("SELECT * FROM `comentarii` WHERE `valid`='1'");
			foreach($res as $row) {
				echo "<b>{$row["nume"]}</b>";
				echo "<p>{$row["text"]}</p>";
				echo "<hr/>";
				
			}
		?>
	</body>
</html>
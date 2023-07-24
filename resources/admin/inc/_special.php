
<!-- to: Richard #SPECIAL nezobrazovat, to je pro Emila :o) -->

<div id="special">
  <ul>
  <?php
		$url = explode ('/', $_SERVER['SCRIPT_NAME']);
		list (,,$url1, $url2, $url3) = $url;
		
		$a = array(
			1 => "Přihlášení",
			2 => "Přidat nemovitost: step 1",
			3 => "Přidat nemovitost: step 2",
			4 => "Přidat nemovitost: step 3",
			5 => "Přidat nemovitost: step 4",
			6 => "Editovat nemovitost: step 1",
		);
		
		$b = "style=\"border-bottom: 1px solid green; padding-bottom: 5px;\"";
		
		echo ($url1 != "index.php")
			? "<li $b><a href=\"./\">$a[1]</a></li>"
			: "<li $b><b>$a[1]</b></li>";
		echo ($url1 != "01step1.php")
			? "<li><a href=\"01step1.php\">$a[2]</a></li>"
			: "<li><b>$a[2]</b></li>";
		echo ($url1 != "01step2.php")
			? "<li><a href=\"01step2.php\">$a[3]</a></li>"
			: "<li><b>$a[3]</b></li>";
		echo ($url1 != "01step3.php")
			? "<li><a href=\"01step3.php\">$a[4]</a></li>"
			: "<li><b>$a[4]</b></li>";
    echo ($url1 != "01step4.php")
			? "<li $b><a href=\"01step4.php\">$a[5]</a></li>"
			: "<li $b><b>$a[5]</b></li>";
  	echo ($url1 != "02step1.php")
			? "<li><a href=\"02step1.php\">$a[6]</a></li>"
			: "<li><b>$a[6]</b></li>";

  ?>
    <li>
			dále pak vypadá jako přidat nemovitost, ale s tím rozílem, že jsou některé části již vyplněné
		</li>
  </ul>
</div>

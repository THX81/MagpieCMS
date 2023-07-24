<?php
  require_once 'inc/header.php';
?>

<h1>Správa nemovitostí</h1>

<h2>Vložit nemovitost: krok 4</h2>

<form action="neco.aspx" method="post">
  <fieldset>
	  <legend>Vložit foto</legend>

	  <table cellspacing="1" class="edits">
      <tr>
        <th>Soubor:</th>
        <td>
					<input type="file" /> <br />
					<input type="text" maxlength="60" /> - popis CZ <small>(max 60 znaků)</small><br />
          <input type="text" maxlength="60" /> - popis EN <small>(max 60 znaků)</small><br />
				</td>
				<td><a href="#">nahrát obrázek</a></td>
			</tr>
		</table>
	</fieldset>

	
	<fieldset>
    <legend>Nahrané obrázky</legend>
    
    <table cellspacing="1" class="edits">
	    <tr>
	      <th><img src="img/k002/02s.jpg" height="56" width="75" /></th>
	      <td>
	        [cz] název obrázku<br />
	        [en] název obrázku<br /><br />
					<input type="radio" name="mainpic" /> hlavní obrázek
				</td>
				<td>
          <a href="#">editovat</a> | <a href="#">smazat</a>
				</td>
			</tr>
			<tr>
	      <th><img src="img/k002/03s.jpg" height="56" width="75" /></th>
	      <td>
	        [cz] název obrázku<br />
	        [en] název obrázku<br /><br />

          <input type="radio" name="mainpic" /> hlavní obrázek
					
				</td>
				<td>
          <a href="#">editovat</a> | <a href="#">smazat</a>
				</td>
			</tr>
    </table>
	</fieldset>
	
	<fieldset>
	  <legend>Vložit PDF</legend>

	  <table cellspacing="1" class="edits">
      <tr>
        <th>Soubor:</th>
        <td>
					<input type="file" /> <br />
					<input type="text" maxlength="60" /> - popis CZ <small>(max 60 znaků)</small><br />
          <input type="text" maxlength="60" /> - popis EN <small>(max 60 znaků)</small><br />
				</td>
				<td><a href="#">nahrát PDF</a></td>
			</tr>
		</table>
	</fieldset>

  <fieldset>
    <legend>Nahrané soubory</legend>

    <table cellspacing="1" class="edits">
	    <tr>
	      <th><img src="img/ico-pdf.gif" height="50" width="50" /></th>
	      <td>
	        [cz] název PDF<br />
	        [en] název PDF<br /><br />
				</td>
				<td>
          <a href="#">editovat</a> | <a href="#">smazat</a>
				</td>
			</tr>
    </table>
	</fieldset>

	<p class="right">
	  <a href="#">zobrazit náhled</a> |
	  <a href="#">uložit</a>
	</p>

</form>


<?php
  require_once 'inc/menu-footer.php';
?>



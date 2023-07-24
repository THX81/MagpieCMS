<?php
  require_once 'inc/header.php';
?>

<h1>Správa nemovitostí</h1>

<h2>Vložit nemovitost: krok 3</h2>

<form action="neco.aspx" method="post">
  <fieldset>
	  <legend>Vlastnosti nemovitosti</legend>

	  <table cellspacing="1" class="edits">
	    <tr>
	      <th>Vlastnictví: *</th>
	      <td>
          <select>
            <option value="osobni">Osobní</option>
            <option value="druzstvo">Družstevní</option>
            <option value="jine">Jiné</option>
          </select>
				</td>
			</tr>
			
			<tr>
	      <th>Počet podlaží: *</th>
	      <td>
					<input type="text" />
				</td>
			</tr>
			
			<tr>
	      <th>Nemovitost je v podlaží:</th>
	      <td>
					<input type="text" />
				</td>
			</tr>
			
			<tr>
	      <th>Velikost nemovitosti: *<br /> <small>zastavěná plocha</small></th>
	      <td>
					<input type="text" class="w40" /> m<sup>2</sup><br />
					<input type="text" /> <small>(1+kk, 2+kk, ...)</small>
				</td>
			</tr>
			
			<tr>
	      <th>Užitá plocha: *</th>
	      <td>
					<input type="text" class="w40" /> m<sup>2</sup><br />
				</td>
			</tr>
			
			<tr>
	      <th>Typ budovy: *</th>
	      <td>
          <select>
            <option value="">---- vyber typ budovy ----</option>
            <option value="cihla">Cihlový</option>
            <option value="panel">Panelový</option>
            <option value="drevo">Dřevěný</option>
            <option value="jine">Jiné</option>
          </select>
				</td>
			</tr>
			<tr>
	      <th>Vybavenost:</th>
	      <td>
          <select>
            <option value="full">Úplné</option>
            <option value="">Částečně</option>
            <option value="">Nevybaven</option>
          </select>
          <br />
          Seznam co byt obsahuje: <small>lednice, pračka, nábytek, atp.</small>
          <textarea></textarea>
				</td>
			</tr>
			<tr>
	      <th>Stav nemovitosti: *</th>
	      <td>
	        <select>
           <option value="">---- vyber stav budovy ----</option>
           <option value="rek">Po rekonstrukci</option>
           <option value="norek">Před rekonstrukci</option>
           <option value="good">Dobrý</option>
           <option value="bad">Špatný</option>
           <option value="noexist">Ve výstavbě</option>
         </select>
				</td>
			</tr>
	  </table>
	  
	  <fieldset>
     <legend>Ostatní data</legend>

		 <table cellspacing="1" class="edits">
      <tr>
        <th>Rok výstavby</th>
        <td><input type="text" /></td>
			</tr>
			<tr>
        <th>Rok kolaudace</th>
        <td><input type="text" /></td>
			</tr>
			<tr>
        <th>Rok rekonstrukce</th>
        <td><input type="text" /></td>
			</tr>
			<tr>
        <th>K nastěhování</th>
        <td>
          <input type="checkbox" /> ihned<br />
					od data <input type="text" />
				</td>
			</tr>
     </table>
   	</fieldset>
	  
	</fieldset>

	<fieldset>
	  <legend>Nebytové prostory</legend>

		<table cellspacing="1" class="edits">
	    <tr>
	      <th>Nebytové prostory:</th>
	      <td>
	        <input type="checkbox" /> Sklep <input type="text" class="w40" />m<sup>2</sup><br />
          <input type="checkbox" /> Terasa <input type="text" class="w40" />m<sup>2</sup><br />
					<input type="checkbox" /> Zahrada <input type="text" class="w40" />m<sup>2</sup><br />
	        <input type="checkbox" /> Garáž <input type="text" class="w40" />m<sup>2</sup><br />
	        <input type="checkbox" /> Garážové stání <input type="text" class="w40" />m<sup>2</sup><br />
	        <input type="checkbox" /> Dílna <input type="text" class="w40" />m<sup>2</sup>
				</td>
			</tr>
		</table>
	</fieldset>

	<fieldset>
	  <legend>Telekomunikace</legend>

	  <table cellspacing="1" class="edits">
	    <tr>
				<th>Telekomonukace:</th>
	    	<td>
					<input type="checkbox" /> telefon<br />
					<input type="checkbox" /> internet
        </td>
			</tr>
			<tr>
      	<th>Ostatní</th>
      	<td>
					<input type="checkbox" /> kabelová televize<br />
					<input type="checkbox" /> satelit<br />
					Jiné <input type="text" />
				</td>
			</tr>
	  </table>
	</fieldset>

	<fieldset>
	  <legend>Inženýrské sítě</legend>

	  <table cellspacing="1" class="edits">
	    <tr>
	      <th>Topení: </th>
	      <td>
					<input type="checkbox" /> plynové<br />
					<input type="checkbox" /> elekrické<br />
					<input type="checkbox" /> ústřední dálkové<br />
					<input type="checkbox" /> na tuhá paliva<br />
					Jiné: <input type="text" />
				</td>
			</tr>
			<tr>
	      <th>Voda:</th>
	      <td><input type="checkbox" /> (=ano)</td>
			</tr>
			<tr>
	      <th>Odpad:</th>
	      <td><input type="checkbox" /> (=ano)</td>
			</tr>
			<tr>
	      <th>Kanalizace:</th>
	      <td><input type="checkbox" /> (=ano)</td>
			</tr>
			<tr>
	      <th>Elektrika:</th>
	      <td>
				 <input type="checkbox" /> 230 V<br />
				 <input type="checkbox" /> 380 V<br />
				 Jiné: <input type="text" />
				</td>
			</tr>
	  </table>
	</fieldset>

	<fieldset>
	  <legend>Občanská vybavenost</legend>

	  <table cellspacing="1" class="edits">
	    <tr>
      	<th>Občanská vybavenost</th>
      	<td>
					<input type="checkbox" /> škola<br />
					<input type="checkbox" /> školka<br />
					<input type="checkbox" /> pošta<br />
					<input type="checkbox" /> supermarket<br />
					<input type="checkbox" /> obchod/supermarket<br />
				</td>
			</tr>
	  </table>
	</fieldset>

	<p class="right">
	  <a href="#">zpět</a> |
	  <a href="#">pokračovat</a>
	</p>

</form>


<?php
  require_once 'inc/menu-footer.php';
?>



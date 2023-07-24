<?php
  require_once 'inc/header.php';
?>

<h1>Správa nemovitostí</h1>

<h2>Vložit nemovitost: krok 2</h2>

<form action="neco.aspx" method="post">
<fieldset>
  <legend>Adresa</legend>

	<table cellspacing="1" class="edits">
    <tr>
      <th>Stát: <span class="pink">*</span></th>
      <td>
        <select name="state">
          <option value="czechRepublic">Česká Republika</option>
          <option value="uae">Spojené arabské emiráty</option>
        </select>
			</td>
		</tr>
		<tr>
      <th>Region: <span class="pink">*</span></th>
      <td>
        <select>
          <option value="pha">Praha</option>
          <option value="lbc">Liberecký kraj</option>
          <option value="kv">Karlovarský kraj</option>
        </select>
			</td>
		</tr>
		<tr>
      <th>Město: <span class="pink">*</span></th>
      <td>
				<input type="text" />
			</td>
		</tr>
		<tr>
      <th>PSČ: <span class="pink">*</span></th>
      <td>
				<input type="text" />
			</td>
		</tr>
  </table>
    
  
</fieldset>

<fieldset>
  <legend>Text inzerátu</legend>

  <table cellspacing="1" class="edits">
		<tr>
      <th>Číslo inzerátu: <span class="pink">*</span></th>
      <td>
        <input type="text" />
			</td>
		</tr>
		<tr>
      <th>Název: <span class="pink">*</span></th>
      <td>
        <input type="text" />
			</td>
		</tr>
		<tr>
      <th>Základní popis: <span class="pink">*</span><br /> <small>(Max 200 znaků, načítá se do perexů)</small></th>
      <td>
        <textarea></textarea>
			</td>
		</tr>
		<tr>
      <th>Hlavní popis</th>
      <td>
        <textarea></textarea>
			</td>
		</tr>
		<tr>
      <th>Ostatní text</th>
      <td>
        <textarea></textarea>
			</td>
		</tr>
  </table>
</fieldset>


<fieldset>
  <legend>Cena nemovitosti</legend>
  
  <table cellspacing="1" class="edits">
    <tr>
      <th>Cena: <span class="pink">*</span></th>
      <td>
        <input type="text" />
        <select>
          <option value="czk">CZK</option>
          <option value="usd">USD</option>
          <option value="euro">Euro</option>
        </select>
        <select>
          <option value="property">Za nemovitost</option>
          <option value="sqm">za m2</option>
        </select>
        <select>
          <option value="vat">s DPH</option>
          <option value="novat">bez DPH</option>
        </select>
			</td>
		</tr>
		<tr>
      <th>Poznámka</th>
      <td><input type="text" /></td>
		</tr>
		<tr>
      <th>Prodej z konkurzní podstaty</th>
      <td><input type="checkbox" />jestli zaškrtnuto (=ano)</td>
		</tr>
  </table>

</fieldset>

<fieldset>
  <legend>Realitní makléř</legend>
  <table cellspacing="1" class="edits">
    <tr>
      <th>Realitní makléř <span class="pink">*</span></th>
      <td>
        <select>
          <option value="">----- vyber makléře -----</option>
          <option value="makl01">Makléř jméno</option>
          <option value="makl02">Makléř jméno</option>
          <option value="makl02">Makléř jméno</option>
        </select>
			</td>
		</tr>
  </table>
</fieldset>

<fieldset>
  <legend>Stav inzerátu</legend>
  
  <table cellspacing="1" class="edits">
    <tr>
      <th>Stav inzerátu <span class="pink">*</span></th>
      <td>
        <select>
          <option value="active">Aktivní</option>
          <option value="nonactive">Neaktivní</option>
        </select>
			</td>
		</tr>
		<tr>
      <th>Doporučeno<br /> <small>zobrazí se na HP</small></th>
      <td><input type="checkbox" /></td>
		</tr>
		<tr>
      <th>Další info:</th>
      <td>
				<label for="res"><input type="checkbox" id="res" /> rezervace</label><br />
				<label for="sell"><input type="checkbox" id="sell" /> prodáno</label>
			</td>
		</tr>
		<tr>
      <th>Vloženo</th>
      <td><input type="text"></td>
		</tr>
  </table>
</fieldset>

<p class="right">
  <a href="#">zpět</a> | <a href="#">pokračovat</a>
</p>

</form>


<?php
  require_once 'inc/menu-footer.php';
?>


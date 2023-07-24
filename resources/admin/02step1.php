<?php
  require_once 'inc/header.php';
?>

<h1>Správa nemovitostí</h1>

<p class="pink">
  <strong>+ <a href="#">vložit nemovitost</a></strong>
</p>

<form action="neco.aspx" method="post">
	<fieldset class="sort">
  	<legend>Třídit</legend>

		<label for="offer" class="w120 fll">Druh nabídky</label>
		<select id="offer" class="w250 fll">
     <option value="all">Vše</option>
		 <option value="sell">Prodej</option>
		 <option value="hire">Pronájem</option>
		</select>
		
		<div class="cleaner"></div>

		<label for="property" class="w120 fll">Druh nemovitosti</label>
		<select id="property" class="w250 fll">
			<option value="familyhouse">Rodinné domy</option>
			<option value="house">Domy</option>
			<option value="lands">Stavební Pozemky</option>
			<option value="industrialbuildings">Objekty pro podnikání</option>
		</select>

	  
	  <p class="right">
     <input type="submit" class="submit" value="třídit &raquo;" />
   	</p>
	</fieldset>
</form>

<table cellspacing="1">
    <tr>
      <th style="width: 120px;">Kód nemovitosti</th>
      <th >Název</th>
      <th style="width: 100px;">Datum vložení</th>
      <th style="width: 40px;">Typ</th>
      <th style="width: 120px;" colspan="2">&nbsp;</th>
    </tr>
    <tr>
      <td>K110</td>
      <td>Pronájem restaurace v NC Chodov</td>
      <td>8. 11. 2007</td>
      <td align="center"><input type="checkbox" /></td>
      <td align="center"><a href="#">editovat</a></td>
      <td align="center"><a href="#">smazat</a></td>
		</tr>
		<tr>
      <td>K110</td>
      <td>Pronájem restaurace v NC Chodov</td>
      <td>8. 11. 2007</td>
      <td align="center"><input type="checkbox" /></td>
      <td align="center"><a href="#">editovat</a></td>
      <td align="center"><a href="#">smazat</a></td>
		</tr>
</table>

<p class="right">
  <strong><a href="#">předchozí</a></strong>   <b>1</b>, <a href="#">2</a>, <a href="#">3</a>, <a href="#">4</a>, <a href="#">5</a>  <strong><a href="#">další</a></strong>
</p>



<?php
  require_once 'inc/menu-footer.php';
?>


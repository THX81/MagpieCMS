<?php
  require_once 'inc/header.php';
?>

<h1>Správa nemovitostí</h1>

<h2>Vložit nemovitost: krok 1</h2>

<fieldset class="sort">
  <legend>Inzerát</legend>
  <form action="neco.aspx" method="post">

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
     <input type="submit" class="submit" value="pokračovat &raquo;" />
   	</p>
	  
	</form>
</fieldset>



<?php
  require_once 'inc/menu-footer.php';
?>


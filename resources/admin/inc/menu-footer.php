<?php
  require_once '_special.php';
?>


      </div><!-- // hlavní obsah -->
			<hr class="nod" />
      
      <!-- Menu -->
      <div id="menu">
        <h3 class="nod">Menu</h3>

          <?php
            $url = explode ('/', $_SERVER['SCRIPT_NAME']);
            list (,$url1, $url2, $url3) = $url;
          ?>
          
          <ul>
			<li><a href="/">Homepage</a></li>
			<li><a href="#">Sekce/stránky</a></li>
			<li><a href="#">Šablony stránek</a></li>
			<li class="active"><a href="#">Správa inzerátů</a>
			  <ul>
				<li class="active"><a href="#">Správa nemovitostí</a>
				  <ul>
					<li><a href="#">level 3 odkaz 1</a></li>
					<li><a href="#">level 3 odkaz 2</a></li>
				  </ul>
				</li>
				<li><a href="#">Správa dev. projektů</a></li>
				<li><a href="#">Realitní poradci</a></li>
				<li><a href="#">Regiony</a></li>
				<li><a href="#">Typy nemovitostí</a></li>
				<li><a href="#">Typy nabídek</a></li>
				<li><a href="#">Typy měn</a></li>
			  </ul>
			</li>
			<li><a href="#">Cokoliv</a></li>
		  </ul>
      </div><!-- // menu -->

	  <div class="cleaner"></div>
        
      <p class="spaceman">
        <a href="#text-box">k hlavnímu obsahu &uarr;</a>
      </p>
    </div><!-- // balicek -->
    
    <hr class="nod" />

		<div id="paticka">
		    <p class="fll">
		      &copy; <a href="#">MagPie Team</a>
		    </p>
		</div>
    
    <script type="text/javascript" src="js/thumbnailviewer.js"></script>
  </body>
</html>

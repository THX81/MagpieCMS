
      </div><!-- // hlavní obsah -->

      <hr class="nod" />
      <!-- Menu -->
      <div id="menu">
        <h3 class="nod">Menu</h3>

          <?php
            $url = explode ('/', $_SERVER['SCRIPT_NAME']);
            list (,$url1, $url2, $url3) = $url;
            
              echo "<ul>";
            	echo ($url1 != "index.php")
                ? "<li><a href=\"/\">Úvod</a></li>"
                : "<li class=\"active\"><a href=\"/\">Úvod</a></li>";
              
              echo ($url1 != "nemovitosti.php")
                ? "<li><a href=\"nemovitosti.php\">Nabídka nemovitostí</a></li>"
                : "<li class=\"active\"><a href=\"nemovitosti.php\">Nabídka nemovitostí</a></li>";
              
              echo ($url1 != "sluzby.php")
                ? "<li><a href=\"sluzby.php\">Naše služby</a></li>"
                : "<li class=\"active\"><a href=\"sluzby.php\">Naše služby</a></li>";

              echo ($url1 != "poradna.php")
                ? "<li><a href=\"poradna.php\">Poradna</a></li>"
                : "<li class=\"active\"><a href=\"poradna.php\">Poradna</a></li>";

              echo ($url1 != "o-nas.php")
                ? "<li><a href=\"o-nas.php\">O nás</a></li>"
                : "<li class=\"active\"><a href=\"o-nas.php\">O nás</a></li>";

              echo ($url1 != "kontakt.php")
                ? "<li><a href=\"kontakt.php\">Kontakt</a></li>"
                : "<li class=\"active\"><a href=\"kontakt.php\">Kontakt</a></li>";
              
              echo ($url1 != "search.php")
                ? "<li class=\"last\"><a href=\"search.php\">Vyhledávání</a></li>"
                : "<li class=\"last active\"><a href=\"search.php\">Vyhledávání</a></li>";                
              echo "</ul>";
          ?>
          
        </div>
        
        <p class="spaceman">
          <a href="#text-box">k hlavnímu obsahu &uarr;</a>
        </p>
      <!-- // menu -->

      <hr class="nod" />

      <div id="paticka">          
          <p class="flr">
            <a href="#">tisk</a> | <a href="#balicek">nahoru</a>
          </p>
          <p class="fll">
            &copy; <a href="#">RK Endimión</a>, Václavské náměstí, 111 50 Praha 2 
          </p>
      </div>

    </div><!-- // balicek -->
    </div><!-- // balicek1 top shd -->
    </div><!-- // balicek2 btn shd -->
  </body>
</html>

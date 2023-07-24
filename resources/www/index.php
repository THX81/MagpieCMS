<?php
  require_once 'inc/header.php';
?>


<div id="right">
<h2>Realitní kancelář Endimión</h2>
<address class="hp">
  <strong>RK Endimión</strong><br />
  Václavské náměstí<br /> 
  111 50  Praha 2<br />
  <br />
  Tel.: +420 111 999 111<br />
  E-mail: <a href="mailto:info@domena.com">info@domena.com</a><br />
  <br />
  <a href="#" class="ico">Zobrazit mapu</a>
</address>
<p>
  Lorem ipsum dolor sit amet consectetuer Morbi amet Vestibulum Curabitur elit. Sed adipiscing In fermentum sed arcu id magna ligula Phasellus senectus.
</p>

<p>
   Dui feugiat egestas consequat et mauris quis auctor egestas felis iaculis. Dui feugiat egestas consequat et mauris quis auctor egestas felis iaculis. 
</p>

<hr class="hid cleaner" />

<h2>Vyhledat nemovitost</h2>

<form action="search.phh" method="post" class="search">
  
  <fieldset id="pch">
    <legend>Typ nemovitosti</legend>
    <select>
      <option value="byty">byty</option>
      <option value="rodinné domy">rodinné domy</option>
      <option value="pozemky">pozemky</option>
    </select>
    <select>
      <option value="pronajem">pronájem</option>
      <option value="prodej">prodej</option>
    </select>
  </fieldset>
  
  <fieldset id="ps">
    <legend>Velikost nemovitosti</legend>
    <label for="ps01"><input type="checkbox" id="ps01" /> garsoniéra</label>
    <label for="ps02"><input type="checkbox" id="ps02" /> 1+kk</label>
    <label for="ps03"><input type="checkbox" id="ps03" /> 1+1</label>
    <label for="ps04"><input type="checkbox" id="ps04" /> 2+kk</label>
    <label for="ps05"><input type="checkbox" id="ps05" /> 2+1</label>
    <label for="ps06"><input type="checkbox" id="ps06" /> 3+kk</label>
    <label for="ps07"><input type="checkbox" id="ps07" /> 3+1</label>
    <label for="ps08"><input type="checkbox" id="ps08" /> 4+kk</label>
    <label for="ps09"><input type="checkbox" id="ps09" /> 4+1</label>
    <label for="ps10"><input type="checkbox" id="ps10" /> 5 a více</label>
    <br class="cleaner" /><!-- //for IE6,5 -->
  </fieldset>
    
  <fieldset id="pp">
    <legend>Cena nemovitosti</legend>
    od <input type="text" name="from" />
    do <input type="text" name="to" />
  </fieldset>
    
  <fieldset id="pl">
    <legend>Lokalita nemovitosti</legend>
    <label for="pl01"><input type="checkbox" id="pl01" /> Praha 1</label>
    <label for="pl02"><input type="checkbox" id="pl02" /> Praha 2</label>
    <label for="pl03"><input type="checkbox" id="pl03" /> Praha 3</label>
    <label for="pl04"><input type="checkbox" id="pl04" /> Praha 4</label>
    <label for="pl05"><input type="checkbox" id="pl05" /> Praha 5</label>
    <label for="pl06"><input type="checkbox" id="pl06" /> Praha 6</label>
    <label for="pl07"><input type="checkbox" id="pl07" /> Praha 7</label>
    <label for="pl08"><input type="checkbox" id="pl08" /> Praha 8</label>
    <label for="pl09"><input type="checkbox" id="pl09" /> Praha 9</label>
    <label for="pl10"><input type="checkbox" id="pl10" /> Praha 10</label>
    <label for="pl11"><input type="checkbox" id="pl11" /> Praha východ</label>
    <label for="pl12"><input type="checkbox" id="pl12" /> Praha západ</label>
    <label for="pl13"><input type="checkbox" id="pl13" /> Ostatní</label>
  </fieldset>
  <input type="submit" value="Vyhledat" class="send" />
 
</form>

  
</div><!-- //right -->


<div id="left">
  <h2>Aktuality</h2>
  <dl>
    <dt><small>1. 1. 2008</small> | Kancelář v Brně</dt>
    <dd>Lorem ipsum dolor sit amet consectetuer amet parturient quis convallis malesuada. Lacus at elit fermentum Aliquam nec metus auctor ac In platea. Auctor Nullam at arcu quis est et feugiat.</dd>
    <dd class="right"><a href="#">více</a></dd>
    
    <dt><small>16. 12. 2007</small> | 50 pronajatý byt</dt>
    <dd>Lacus at elit fermentum Aliquam nec metus auctor ac In platea. Auctor Nullam at arcu quis est et feugiat.</dd>
    <dd class="right"><a href="#">více</a></dd>    
  </dl>
  

  <h2>Doporučujeme</h2>
  <a href="#"><img src="img/12345-01.jpg" alt="prodej domu - 12345-01" height="147" width="220" /></a>
  <dl class="offer">
    <dt class="head"><a href="#"><strong>#1234 |</strong> prodej, rodinný dům</a></dt>
    <dd>Lokalita: Praha 4, Krč</dd>
    <dd>Pčet místností: 7</dd>
    <dd>Cena: 8 650 000,- Kč</dd>
    <dd class="right"><a href="#">více</a></dd>
  </dl>  

</div> <!-- //left -->

<div class="cleaner" /></div>

<?php
  require_once 'inc/menu-footer.php';
?>


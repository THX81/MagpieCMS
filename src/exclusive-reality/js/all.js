// JavaScript Document
/* init ondomready
----------------------------------- */
onDomReady(init);

function init() {
  myPrint.init();
};

function onDomReady(f){
  var a = onDomReady, b = navigator.userAgent, d = document, w = window, c="onDomReady", e = "addEventListener", o = "opera", r = "readyState", s = "<scr".concat("ipt defer src='//:' on",r,"change='if(this.",r," == \"complete\"){this.parentNode.removeChild(this);",c,".",c,"()}'></scr","ipt>");
  a[c] = (function(o){return function(){a[c] = function(){}; for(a=arguments.callee;!a.done; a.done = 1) f(o ? o() : o)}}) (a[c]);
  if(d[e])d[e]("DOMContentLoaded",a[c],false);
  if(/WebKit|Khtml/i.test(b)||(w[o]&&parseInt(w[o].version())<9)) (function(){/loaded|complete/.test(d[r])?a[c]() : setTimeout(arguments.callee,1)})();
  else if(/MSIE/i.test(b))d.write(s);
};

function getObj(id) {
  var obj = document.getElementById ? document.getElementById(id) : document.all[id];
  return obj;
};


/* tisk
----------------------------------- */
var myPrint = {
  init: function () {
    this.page();
    //this.recipePage();
  },

  // tisk receptu
  page: function () {
    if(!getObj('right')) return;

    var span = document.createElement('span');

    var a = document.createElement('a');
    a.href = '#';
    a.innerHTML = 'tisk';
    a.onclick = function () {
      myPrint.show();
      return false;
    };

    span.appendChild(a);

    // umisteni
    var printer = getObj('print');
    printer.parentNode.insertBefore(span, printer);
  },

  show: function () {
    if(!getObj('right')) return;

    var title = getObj('text-box').getElementsByTagName('h2')[0].innerHTML;

    // template
    var tpl = '<?xml version="1.0" encoding="utf-8"?>';
    tpl += '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">';
    tpl += '<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="cs">';
    tpl += '<head>';
    tpl += '<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />';
    tpl += '<meta http-equiv="Content-language" content="cs" />';
    tpl += '<title>' + title + ' | Company</title>';
    tpl += '<link rel="stylesheet" type="text/css" href="/css/print_page.css" media="screen,projection" />';
    tpl += '<link rel="stylesheet" type="text/css" href="/css/print.css" media="print" />';
    tpl += '</head>';
    tpl += '<body>';
    tpl += '<p id="option"><a href="#" onclick="window.print(); return false;">vytisknout</a> | <a href="#" onclick="window.close(); return false;">zavřít okno</a></p>';
    tpl += '<div id="printable">';
    tpl += '<img src="/gfx/print_logo.png" height="101" width="323">';
    tpl += '<hr>';
    tpl += getObj('right').innerHTML;
    tpl += '</div>';
    tpl += '</body>';
    tpl += '</html>';

    var okno = window.open('','Company','width=660, height=520, top=50, left=50, scrollbars=1, resizable=1');
    okno.focus();

    var doc = okno.document;
    doc.write(tpl);
    doc.close();

    //okno.print();
  },
}

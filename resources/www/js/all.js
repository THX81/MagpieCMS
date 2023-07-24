// Flash player detection
var agt = navigator.userAgent.toLowerCase();
var ie  = (agt.indexOf("msie") != -1);
var ns  = (navigator.appName.indexOf("Netscape") != -1) || (navigator.appName.indexOf("Opera") != -1);
var win = ((agt.indexOf("win")!=-1) || (agt.indexOf("32bit")!=-1));
var mac = (agt.indexOf("mac")!=-1);
var rqversion = 8;
var result = false;

if (ie && win) {
  document.writeln('<script type="text/vbscript">');
  document.writeln('  On Error Resume Next');
  document.writeln('  result = IsObject(CreateObject("ShockwaveFlash.ShockwaveFlash.'+rqversion+'"))');
  document.writeln('</script>');
}
if (ns || !win) {
  if (navigator.mimeTypes && navigator.mimeTypes["application/x-shockwave-flash"] && navigator.mimeTypes["application/x-shockwave-flash"].enabledPlugin) {
    if (navigator.plugins && navigator.plugins["Shockwave Flash"] && (versionIndex = navigator.plugins["Shockwave Flash"].description.indexOf(".")) != - 1) {
      var versionString = navigator.plugins["Shockwave Flash"].description.substring(versionIndex-1,versionIndex);
      versionIndex = parseInt( versionString );
      if ( versionIndex >= rqversion ) result = true;
    }
  }
}
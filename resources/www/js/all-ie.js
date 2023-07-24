// -------------------------
// osefujeme si Flash pod IE
// -------------------------
var objects = document.getElementsByTagName("object");

for (var i=0; i<objects.length; i++)
    objects[i].outerHTML = objects[i].outerHTML;
/*
  @source: dynamicdrive.com
	@update: o13.cz
----------------------------------------------------------- */
var viewer = {
  enableAlt:        true,  
  enableTitle:      true,
  enableCounter:    false, 
  enableAnimation:  false,
  enableOverlay:    true,
  footer:           '<p class="close"><a href="#" onclick="viewer.close(); return false;">close</a></p>',
  loader:           '<img src="/gfx/viewer/tico_loading.gif" alt="loading.." />',
   
  opacitystring:    'filter:progid:DXImageTransform.Microsoft.alpha(opacity=10); -moz-opacity: 0.1; opacity: 0.1',
  targetlinks:      [],



  //initialize viewer
  init: function() { 
    if(!document.getElementById) return false;
    
    if(!this.enableAnimation) this.opacitystring = "";
    var pagelinks = document.getElementsByTagName("a");
    
    var i=0, j=0;
    for(i; i<pagelinks.length; i++) { 
      if(pagelinks[i].getAttribute("rel") && pagelinks[i].getAttribute("rel") == "viewer") { 
        this.targetlinks[j] = pagelinks[i];
        this.targetlinks[j].index = j;
        
        this.targetlinks[j].onclick = function() {
          //stop any currently running fade animation on "viewerBox" div before proceeding
          viewer.stopanimation(); 
          //load object
          viewer.loadobject(this);
          return false;
        };        
        j++;                 
      }    
    } 
     
    //reposition
    this.dotask(window, function() {if(viewer.viewerBox.style.visibility == "visible") viewer.center(viewer.viewerBox)}, "resize"); 
    this.dotask(window, function() {if(viewer.viewerBox.style.visibility == "visible") viewer.center(viewer.viewerBox)}, "scroll");     
  },
  


  create: function() {
    document.write('<div id="vBox"><div id="vImg"></div>' + this.footer + '</div>');  
    document.write('<div id="vLoader">' + this.loader + '</div>'); 
    
    this.viewerBox = document.getElementById("vBox"); 
    this.viewerObject = document.getElementById("vImg"); 
    this.viewerLoading = document.getElementById("vLoader"); 
    
    //create reference to common "body" across doctypes  
    this.standardbody = (document.compatMode == "CSS1Compat") ? document.documentElement : document.body; 
  },



  center: function(divobj) {     
    var ie = document.all && !window.opera;  
    var dom = document.getElementById;  
    var scroll_top = (ie) ? this.standardbody.scrollTop : window.pageYOffset;  
    var scroll_left = (ie) ? this.standardbody.scrollLeft : window.pageXOffset;  
    
    var docwidth = this.viewport().width/* - this.scrollbarwidth*/;
    var docheight = this.viewport().height;  
    
    //full scroll height of document 
    var docheightcomplete = (this.standardbody.offsetHeight > this.standardbody.scrollHeight) ? this.standardbody.offsetHeight : this.standardbody.scrollHeight;
    
    //div element dimensions  
    var objwidth = divobj.offsetWidth; 
    var objheight = divobj.offsetHeight; 
    
    //vertical position of div element 
    var topposition = (docheight > objheight) ? scroll_top + (docheight - objheight)/2 + "px" : scroll_top + 'px';
    
    //docheight < objheight nevyreseno
    var type = window.event ? window.event.type : 'scroll';
    if(docheight < objheight && type == 'scroll') topposition = 10 + 'px';
    
    //center div element horizontally
    divobj.style.left = (docwidth - objwidth)/2 + "px";   
    divobj.style.top = Math.floor(parseInt(topposition)) + "px";  
  },


   
  show: function() {          
    this.viewerObject.style.width = this.featureObject.clientWidth + 'px';
    this.prevBtn.style.height = this.featureObject.clientHeight + 'px';
    this.nextBtn.style.height = this.featureObject.clientHeight + 'px';
    
    this.center(this.viewerBox);
    this.viewerBox.style.visibility = "visible";
         
    if(this.enableAnimation) {  
      this.currentopacity = 0.1;
      this.opacitytimer = setInterval("viewer.opacityanimation()", 15);
    }
    
    window.onkeydown = function(e) {
      if(viewer.viewerBox.style.visibility == "visible") viewer.keyboard(e);
    };
          
  },



  loadobject: function(link) { 
    this.index = link.index; 
    //positioning bug in Firefox  
    if(this.viewerBox.style.visibility == "visible") this.close();             
    
    var objectHTML = '';
    objectHTML += '<img src="' + link.getAttribute("href") + '" style="' + this.opacitystring + '" />';
    
    objectHTML += '<div>';    
    //use alt attr of the object as heading?
    if(this.enableAlt && link.firstChild.getAttribute("alt")) 
      objectHTML += '<h3>'+ link.firstChild.getAttribute("alt") + '</h3>';
    
    //use title attr of the link as description?
    if(this.enableTitle && link.getAttribute("title")) 
      objectHTML += '<p>'+ link.getAttribute("title") + '</p>';
    
    // counter?
    if(this.enableCounter) {
      var index = link.index + 1;
      objectHTML += '<big>' + index + '</big> / ' + this.targetlinks.length;
    }     
    objectHTML += '</div>';  
        
    
    // overlay    
    if(this.enableOverlay) this.overlay();
        
    //center and display "loading" div while we set up the object to be shown
    this.center(this.viewerLoading); 
    this.viewerLoading.style.visibility = 'visible';

    this.viewerObject.innerHTML = objectHTML;
    
    
    if(this.targetlinks.length > 0) {    
      // PREV button 
      this.prevBtn = document.createElement('a');
      this.prevBtn.href = '#';
      this.prevBtn.className = 'prev'; 
      this.prevBtn.innerHTML = '<span>předchozí</span>';
      this.prevBtn.onclick = function() {viewer.previous();return false;};
      
      // NEXT button
      this.nextBtn = document.createElement('a');
      this.nextBtn.href = '#';
      this.nextBtn.className = 'next';
      this.nextBtn.innerHTML = '<span>následující</span>';
      this.nextBtn.onclick = function() {viewer.next(); return false;};
              
      // spacer
      var spacer = document.createElement('span'); 
      spacer.innerHTML = '&nbsp;|&nbsp;'; 
          
      if(this.index > 0) {
        this.viewerObject.appendChild(this.prevBtn);
      }
      if(this.index < this.targetlinks.length-1) {
        this.viewerObject.appendChild(spacer);
        this.viewerObject.appendChild(this.nextBtn);
      }

    } else {
      // CLOSE button
      /*
      this.closeBtn = document.createElement('a');
      this.closeBtn.href = '#';
      this.closeBtn.innerHTML = '&nbsp;AAAAA';      
      this.closeBtn.onclick = function() {viewer.close(); return false;};
      
      this.viewerObject.appendChild(this.closeBtn);
      */
    }     
             
    // object        
    this.featureObject = this.viewerObject.getElementsByTagName("img")[0];       
    this.featureObject.onload = function() {      
      viewer.viewerLoading.style.visibility = "hidden";      
      viewer.show(); 
    }; 
    
           
    //msie 5.0  bug: panoramio.com/blog/onload-event/
    if(document.all && !window.createPopup) this.featureObject.src = link.getAttribute("href");
    
    //if an error has occurred
    this.featureObject.onerror = function() {  
      viewer.viewerLoading.style.visibility = "hidden"; 
    };
  },
  
  keyboard: function(e){
    if(!e) e = window.event;
		switch (e.keyCode) {
		  case 27: case 90: case 122: this.close(); break; // ESC, Z, z
			case 80: case 112: this.previous(); break; // P, p	
			//case 68: case 100: this.next(); // D, d opera
			case 78: case 110: this.next(); // N, n
		}
	},
	
	
	previous: function() {
    viewer.stopanimation(); 
    if(this.targetlinks[this.index-1]) 
      viewer.loadobject(this.targetlinks[this.index-1]);
    return false;  
  },
  
  next: function() {
    viewer.stopanimation(); 
    if(this.targetlinks[this.index+1])
      viewer.loadobject(this.targetlinks[this.index+1]);
    return false;  
  },
  
  close: function() { 
    this.stopanimation();      
    this.viewerBox.style.visibility = "hidden";  
    this.viewerObject.style.width = 0 + 'px';    
    this.viewerObject.innerHTML = "";  
    this.viewerBox.style.left = "-2000px"; 
    this.viewerBox.style.top = "-2000px";   
    if(this.enableOverlay) this.overlayclose();      
  },

  //clean up
  cleanup: function() { 
    this.viewerLoading = null;    
    if(this.featureObject) this.featureObject.onload = null;      
    this.featureObject = null;
    this.viewerObject = null;
    this.overBox = null;
    
    var i=0;
    for(i; i<this.targetlinks.length; i++) {
      this.targetlinks[i].onclick = null;
      this.viewerBox = null;
    }
  },  
  
  

  setimgopacity: function(value) { 
    var targetobject = this.featureObject;
    
    if(targetobject.filters && targetobject.filters[0]) { 
      //msie
      if(typeof targetobject.filters[0].opacity == "number") {
        //msie 6.0
        targetobject.filters[0].opacity = value*100;
      } else {
        //msie 5.5
        targetobject.style.filter = "alpha(opacity="+ value*100 +")";
      }
      
    } else if (typeof targetobject.style.MozOpacity != "undefined") {
      //mozilla
      targetobject.style.MozOpacity = value;
            
    } else if (typeof targetobject.style.opacity != "undefined") {
      //standard
      targetobject.style.opacity = value;
      
    } else {
      this.stopanimation();
    } 
  },

  opacityanimation: function() { 
    this.setimgopacity(this.currentopacity);    
    this.currentopacity += 0.1;
    if(this.currentopacity>1) this.stopanimation();
  },



  stopanimation: function() {
    if(typeof this.opacitytimer != "undefined") clearInterval(this.opacitytimer);
  },
 






  //event handler
  dotask: function(target, functionref, tasktype) { 
    var tasktype = (window.addEventListener) ? tasktype : "on" + tasktype;
    
    if(target.addEventListener) {
      target.addEventListener(tasktype, functionref, false);    
    } else if (target.attachEvent) {
      target.attachEvent(tasktype, functionref);
    }
  },
  


  /* overlay
  --------------------------------------- */
  overlay: function() {
    var overBox = document.createElement('div');
    overBox.id = 'vOverlay';
    overBox.style.width = this.viewport().width + 'px';
    
    document.body.appendChild(overBox);
    this.overBox = document.getElementById('vOverlay');
    this.center(this.overBox);
    
    this.dotask(window, function() {viewer.center(viewer.overBox)}, "scroll"); 
    this.dotask(window, function() {viewer.center(viewer.overBox)}, "resize");
    this.dotask(this.overBox, function() {viewer.close()}, "click");
  }, 
    
  overlayclose: function() {
    document.body.removeChild(this.overBox);
  },
  
  

  /* viewport
  --------------------------------------- */
  viewport: function() {
    return { 
      width: this.viewportwidth(), 
      height: this.viewportheight() 
    };
  },
    
  viewportwidth: function() {
    return this.innerWidth || (document.documentElement.clientWidth || document.body.clientWidth);
  },
  
  viewportheight: function() {
    return this.innerHeight || (document.documentElement.clientHeight || document.body.clientHeight);
  }   

};




/* 
----------------------------------------------------------- */
viewer.create(); 
viewer.dotask(window, function(){viewer.init()}, "load"); 
viewer.dotask(window, function(){viewer.cleanup()}, "unload");

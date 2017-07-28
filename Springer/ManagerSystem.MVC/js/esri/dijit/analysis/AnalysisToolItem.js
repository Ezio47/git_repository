// All material copyright ESRI, All Rights Reserved, unless otherwise specified.
// See http://js.arcgis.com/3.14/esri/copyright.txt for details.
//>>built
require({cache:{"url:esri/dijit/analysis/templates/AnalysisToolItem.html":'\x3cdiv class\x3d\'toolContainer\' data-dojo-attach-point\x3d"_toolCtr" style\x3d"cursor:pointer;cursor:hand;" data-dojo-attach-event\x3d"onclick:_handleToolIconClick"\x3e\r\n  \x3cdiv data-dojo-attach-point\x3d\'_toolIcon\'\x3e\x3c/div\x3e\r\n  \x3cdiv class\x3d\'esriLeadingMargin5 toolContent\'\x3e\r\n    \x3ca  href\x3d"#" class\x3d\'esriFloatTrailing helpIcon\' esriHelpTopic\x3d"toolDescription" data-dojo-attach-point\x3d"_helpIconNode"\x3e\x3c/a\x3e\r\n  \t\x3clabel data-dojo-attach-point\x3d\'_toolNameLabel\' style\x3d"cursor:pointer;cursor:hand;"\x3e\x3c/label\x3e\r\n  \x3c/div\x3e\r\n  \x3cdiv class\x3d\'esriLeadingMargin2\' data-dojo-attach-point\x3d"optionsDiv" style\x3d"margin-top:0.5em;font-size:0.85em;"\x3e\x3clabel class\x3d"esriLeadingMargin5 comingSoonIcon"\x3e${i18n.comingSoonLabel}\x3c/label\x3e\x3c/div\x3e\t\r\n\x3c/div\x3e\r\n'}});
define("esri/dijit/analysis/AnalysisToolItem","require dojo/_base/declare dojo/_base/lang dojo/_base/connect dojo/_base/event dojo/has dojo/dom-class dojo/dom-attr dojo/dom-style dijit/_WidgetBase dijit/_TemplatedMixin dijit/_OnDijitClickMixin dijit/_FocusMixin ../../kernel dojo/i18n!../../nls/jsapi dojo/text!./templates/AnalysisToolItem.html".split(" "),function(c,g,d,t,h,k,b,l,e,m,n,p,q,r,f,s){c=g([m,n,p,q],{declaredClass:"esri.dijit.analysis.AnalysisToolItem",templateString:s,widgetsInTemplate:!0,
i18n:null,_helpIconNode:null,_toolIcon:null,_toolIconClass:null,_toolNameLabel:null,toolName:null,helpTopic:null,helpFileName:"Analysis",constructor:function(a,b){a.toolIcon&&(this._toolIconClass=a.toolIcon);a.name&&(this.toolName=a.name,this.helpTopic=a.helpTopic)},postCreate:function(){this.inherited(arguments);this._toolNameLabel.innerHTML=this.toolName;b.add(this._toolIcon,this._toolIconClass);l.set(this._helpIconNode,"esriHelpTopic",this.helpTopic);this.set("showComingSoonLabel",!0)},postMixInProperties:function(){this.inherited(arguments);
this.i18n={};d.mixin(this.i18n,f.common);d.mixin(this.i18n,f.analysisTools)},_handleToolNameClick:function(){this.onToolSelect(this)},_handleToolIconClick:function(a){h.stop(a);this.onToolSelect(this)},_setShowComingSoonLabelAttr:function(a){e.set(this.optionsDiv,"display",!0===a?"block":"none");b.toggle(this._toolCtr,"esriToolContainerDisabled",a);b.toggle(this._toolNameLabel,"esriTransparentNode",a);b.toggle(this._toolIcon,"esriTransparentNode",a);e.set(this._toolNameLabel,"cursor",!0===a?"default":
"pointer");e.set(this._toolCtr,"cursor",!0===a?"default":"pointer")},onToolSelect:function(a){}});k("extend-esri")&&d.setObject("dijit.analysis.AnalysisToolItem",c,r);return c});
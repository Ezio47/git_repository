//>>built
define("dstore/Cache","dojo/_base/array dojo/when dojo/_base/declare dojo/_base/lang ./Store ./Memory ./QueryResults".split(" "),function(q,f,h,l,k,r,s){function m(a){return function(){var b=this.inherited(arguments),d=this.cachingCollection||this.cachingStore;b.cachingCollection=d[a].apply(d,arguments);b.isValidFetchCache=!0===this.canCacheQuery||this.canCacheQuery(a,arguments);return b}}function n(a){a.cachingStore||(a.cachingStore=new r);a.cachingStore.Model=a.Model;a.cachingStore.idProperty=a.idProperty}
var p={cachingStore:null,constructor:function(){n(this)},canCacheQuery:function(a,b){return!1},isAvailableInCache:function(){return this.isValidFetchCache&&(this.allLoaded||this.fetchRequest)||this._parent&&this._parent.isAvailableInCache()},fetch:function(){return this._fetch(arguments)},fetchRange:function(){return this._fetch(arguments,!0)},_fetch:function(a,b){var d=this.cachingStore,c=this.cachingCollection||d,e=this,g=this.isAvailableInCache();if(g)return new s(f(g,function(){return e.isAvailableInCache()?
b?c.fetchRange(a[0]):c.fetch():e.inherited(a)}));g=this.fetchRequest=this.inherited(a);f(g,function(a){var c=!b;e.fetchRequest=null;q.forEach(a,function(a){!e.isLoaded||e.isLoaded(a)?d.put(a):c=!1});c&&(e.allLoaded=!0);return a});return g},isValidFetchCache:!1,get:function(a,b){var d=this.cachingStore,c=this.getInherited(arguments),e=this;return f(this.fetchRequest,function(){return f(d.get(a),function(g){if(void 0!==g)return g;if(c)return f(c.call(e,a,b),function(b){b&&d.put(b,{id:a});return b})})})},
add:function(a,b){var d=this.cachingStore;return f(this.inherited(arguments),function(c){var e=d.put(a&&"object"===typeof c?c:a,b);return c||e})},put:function(a,b){var d=this.cachingStore;d.remove(b&&b.id||this.getIdentity(a));return f(this.inherited(arguments),function(c){var e=d.put(a&&"object"===typeof c?c:a,b);return c||e})},remove:function(a,b){var d=this.cachingStore;return f(this.inherited(arguments),function(c){return f(d.remove(a,b),function(){return c})})},evict:function(a){this.allLoaded=
!1;return this.cachingStore.remove(a)},invalidate:function(){this.allLoaded=!1},_createSubCollection:function(){var a=this.inherited(arguments);a._parent=this;return a},sort:m("sort"),filter:m("filter"),_getQuerierFactory:function(a){var b=this.cachingStore;return this.inherited(arguments)||l.hitch(b,b._getQuerierFactory(a))}};k=h(null,p);k.create=function(a,b){a=h.safeMixin(l.delegate(a),p);h.safeMixin(a,b);n(a);return a};return k});
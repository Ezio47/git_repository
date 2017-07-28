﻿/// <reference path="_references.js" />

///第一种是默认地球是一个光滑的球面，然后计算任意两点间的距离


var EARTH_RADIUS = 6378137.0; //单位M 
var PI = Math.PI;

function getRad(d) {
    return d * PI / 180.0;
}

/** 
* caculate the great circle distance 
* @param {Object} lat1 
* @param {Object} lng1 
* @param {Object} lat2 
* @param {Object} lng2 
*/
function getGreatCircleDistance(lat1, lng1, lat2, lng2) {
    var radLat1 = getRad(lat1);
    var radLat2 = getRad(lat2);

    var a = radLat1 - radLat2;
    var b = getRad(lng1) - getRad(lng2);

    var s = 2 * Math.asin(Math.sqrt(Math.pow(Math.sin(a / 2), 2) + Math.cos(radLat1) * Math.cos(radLat2) * Math.pow(Math.sin(b / 2), 2)));
    s = s * EARTH_RADIUS;
    s = Math.round(s * 10000) / 10000.0;

    return s;
}

//地球其实并不是一个真正的圆球体，而是椭球，所以有了下面的公式
/** 
* approx distance between two points on earth ellipsoid 
* @param {Object} lat1 
* @param {Object} lng1 
* @param {Object} lat2 
* @param {Object} lng2 
*/
function getFlatternDistance(lat1, lng1, lat2, lng2) {
    var f = getRad((lat1 + lat2) / 2);
    var g = getRad((lat1 - lat2) / 2);
    var l = getRad((lng1 - lng2) / 2);

    var sg = Math.sin(g);
    var sl = Math.sin(l);
    var sf = Math.sin(f);

    var s, c, w, r, d, h1, h2;
    var a = EARTH_RADIUS;
    var fl = 1 / 298.257;

    sg = sg * sg;
    sl = sl * sl;
    sf = sf * sf;

    s = sg * (1 - sl) + (1 - sf) * sl;
    c = (1 - sg) * (1 - sl) + sf * sl;

    w = Math.atan(Math.sqrt(s / c));
    r = Math.sqrt(s * c) / w;
    d = 2 * w * a;
    h1 = (3 * r - 1) / 2 / c;
    h2 = (3 * r + 1) / 2 / s;

    return d * (1 + fl * (h1 * sf * (1 - sg) - h2 * (1 - sf) * sg));
}
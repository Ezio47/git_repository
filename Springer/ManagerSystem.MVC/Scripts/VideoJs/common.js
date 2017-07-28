/// <reference path="../_references.js" />
// json请求封装类
var jsonrpc = function () {
    this.jsonrpc = '2.0';
    this.method = 'webrpc';
    this.id = 1;
    this.params = {};
}

jsonrpc.prototype = {
    setvalue: function (pname, pvalue) {
        this.params[pname] = pvalue;
    },

    setparams: function (pvalue) {
        this.params = pvalue;
    },

    jsonstr: function () {
        return JSON.stringify(this);
    }
};


function writeObj(obj) {
    var description = "";
    for (var i in obj) {
        var property = obj[i];
        description += i + " = " + property + "\n";
    }
    alert(description);
};

var templateObj = function () {
    this.AreaLevel = '';
    this.Catalog = '';
    this.ChidrenId = new Array();
    this.ClassName = '';
    this.DomainName = '';
    this.FunctionType = '';
    this.Id = '';
    this.IsPart = 'false';
    this.Metes = new Array();
    this.Name = '';
    this.Properties = new Array();
    this.Type = '';
}

var catlogStationPrefix = "Station";
var catlogStationCountry = "StationCountry";
var catlogSationProvince = "StationProvince";
var catlogStationCity = "StationCity";
var catlogSationDistrict = "StationDistrict";
var catlogNode = "Node";
var catlogGateway = "Gateway";
var catlogIPC = "IPC";
var catlogNVR = "NVR";
var catlogDome = "Dome";
var catlogModule = "Module";
var catlogSensor = "Sensor";
var catlogServer = "Server";
var catlogSmart = "Smart";


//devObj->objType
var AREA_TYPE = 0;
var SMART_DEV_TYPE = 1;
var GATEWAY_TYPE = 2;
var SERVER_TYPE = 3;
var SENSOR_TYPE = 4;
var ORGANIZATION_TYPE = 5;
var MODULE_TYPE = 6;

var devObj = function () {
    this.parentid = 0;
    this.name = '';
    this.objId = '';
    this.templateId = '';
    this.objState = 0;
    this.objType = 0;
    this.parentConnection = '';
    this.catalog = '';
    this.summary = '';
    this.summaryAddition = '';
    this.img = '';
    this.classes = 'devListView';
    this.isVideo = false;
}


var meteObj = function () {
    this.objId = 0;
    this.devName = '';
    this.Name = '';
    this.Id = 0;
    this.Value = 0;
    this.token = '';
    this.gatewayId = 0;
    this.transType = 0;
    this.meteType = 0;
}

var keyValue = function () {
    this.name = '';
    this.value = '';
}


var ptzCtrlParam = function () {
    this.ctrType = -1;
    this.aliasMeteName = '';
    this.gatewayId = 0;
    this.objId = 0;
    this.userId = 1;
    this.userName = '';
    this.meteId = '';
    this.cmdName = '';
    this.action = ''; //begin; stop
    this.params = new Array();
    this.token = '';
}

var streamInfo = function () {
    this.objId = 0;
    this.streamType = 0;
    this.meteId = 0;
    this.aliasMeteName = '';
    this.token = '';
}

var ptzModel = function () {
    this.objId = "";
    this.meteId = "";
    this.cmd = "";
    this.param = "";
    this.speed = "";
    this.value = "";
    this.protocol = "";
    this.ptzId = 1;
}




devObj.prototype = {

}

var summaryNodeValue = function () {
    this.name = '';
    this.value = '';
}


var treeNode = function () {
    this.children = new Array();
    this.value = null;
}
var templateNode = function () {
    this.chilren = new Array();
    this.value = null;
}

var presetInfo = function () {
    this.seq = 0;
    this.name = '';
}

function insertNode(node, tree) {
    // alert("node id:"+node.objId +" tree objid:"+ tree.value.objId+" parentid:"+node.parentid);
    if (node.parentid == tree.value.objId) {
        // alert("inset Array:"+node.objId);
        for (var i = 0; i < tree.children.length; i++) {
            if (tree.children[i].value.objId == node.objId) {
                if (this.parentConnection != "") {
                    tree.children[i].value = node;
                }
                return 0;
            }
        }
        var nodeValue = new treeNode();
        nodeValue.value = node;
        tree.children[tree.children.length] = nodeValue;
        return 0;
    }
    else {
        //alert("insert to children:"+tree.children.length);
        for (var i = 0; i < tree.children.length; i++) {
            if (0 == insertNode(node, tree.children[i])) {
                //      alert("insert success");
                return 0;
            }
        }
    }
    return -1;

}


function insertByConnection(node, tree, connstr) {
    //alert("insert byconn:"+connstr);
    var level = connstr.split(".");
    //writeObj(level);
    var parentid = 0;
    for (var i = 0; i < level.length - 1; i++) {
        var typeId = level[i].split("_");
        //writeObj(typeId);
        var newNode = new devObj();
        newNode.parentid = parentid;
        newNode.objId = typeId[typeId.length - 1];
        if (getChildNode(newNode.objId, tree) == null) {
            insertNode(newNode, tree);
        }
        //alert("p:"+newNode.parentid+" o:"+newNode.objid);        
        parentid = newNode.objId;
    }
    insertNode(node, tree);
}

function getChildNode(objId, tree) {
    //alert("get child objid:"+objId+" tree:"+tree.value.objId+" length:"+tree.children.length);
    if (tree.value.objId == objId) {
        //alert("get node"+tree.children.length);
        return tree;
    }
    for (var i = 0; i < tree.children.length; i++) {
        var node = getChildNode(objId, tree.children[i]);
        if (node != null) {
            //alert("get node 1  "+node.children.length);
            return node;
        }
    }
    return null;
}

function addSummaryNodeValue(sumarryArry, name, value) {
    var node = new summaryNodeValue();
    node.name = name;
    node.value = value;
    sumarryArry.push(node);
}


function getAreaSummary(childCoun, sumaryJson) {
    var summary = new Array();
    addSummaryNodeValue(summary, "子设备信息:", "局站数量 " + childCoun);
    addSummaryNodeValue(summary, "告警信息:", "未消除告警数 " + sumaryJson["alarmNotCleared"]);
    return summary;
}

function getNodeSummary(childCoun, summaryJson) {
    var summary = new Array();
    var street = '', pos = '';
    var tem = '0', hum = '0';
    var addition = summaryJson["addition"];
    if (addition != null) {
        for (var i = 0; i < addition.length; i++) {
            if (addition[i].meteId == "1006") {
                //addSummaryNodeValue(summary, "面积", addition[i].value);
            }
            if (addition[i].meteId == "12001") {
                street = addition[i].value;
                addSummaryNodeValue(summary, "基站位置信息:", "街道 " + street);
                // addSummaryNodeValue(summary, "街道", addition[i].value);
            }
            if (addition[i].meteId == "12002") {
                pos = addition[i].value;
                //addSummaryNodeValue(summary, "经纬度", addition[i].value);
            }
            if (addition[i].meteId == "118101001") {
                tem = addition[i].value;
            }
            if (addition[i].meteId == "118102001") {
                hum = addition[i].value;
            }
        }
    }

    addSummaryNodeValue(summary, "子设备信息:", "FSU数量 " + childCoun + ",离线设备 " + summaryJson["offLineCount"]);
    addSummaryNodeValue(summary, "告警信息:", "未消除告警数 " + summaryJson["alarmNotCleared"]);

    addSummaryNodeValue(summary, "基站环境信息:", "温度 " + tem + ",湿度 " + hum);
    return summary;
}


function getFsuSummary(childCoun, summaryJson) {
    var summary = new Array();
    var onlineTm = '', offlineTm = '';
    if (summaryJson["onLineTime"] != null) {
        onlineTm = summaryJson["onLineTime"];
        //addSummaryNodeValue(summary, "上线时间", summaryJson["onLineTime"]);
    }
    if (summaryJson["offLineTime"] != null) {
        offlineTm = summaryJson["offLineTime"];
        //addSummaryNodeValue(summary, "离线时间", summaryJson["offLineTime"]); 
    }
    //addSummaryNodeValue(summary, "监控量个数", summaryJson["monitorMeteCount"]);
    addSummaryNodeValue(summary, "告警信息:", "未消除告警数 " + summaryJson["alarmNotCleared"]);
    var stsInfo = "在线状态 " + summaryJson["curState"];
    if (summaryJson["curState"] == "在线") {
        stsInfo += ",上线时间 " + onlineTm;
    }
    else {
        stsInfo += ",离线时间 " + offlineTm;
    }
    addSummaryNodeValue(summary, "设备状态:", stsInfo);
    return summary;
}

function getAreaNodeSummary(childCoun, summaryJson) {
    var summary = new Array();
    addSummaryNodeValue(summary, "子设备信息:", "基站数量" + childCoun);
    addSummaryNodeValue(summary, "未消除告警数", summaryJson["alarmNotCleared"]);
    return summary;
}

function getAreaFsuSummary(childCoun, sumaryJson) {
    var summary = new Array();
    addSummaryNodeValue(summary, "FSU数量", childCoun);
    addSummaryNodeValue(summary, "告警信息:", "未消除告警数" + sumaryJson["alarmNotCleared"]);
    return summary;
}



//0:area-area  1:area-node 2:area-fsu 3:node 4:fsu
function getSummaryStr(childCoun, type, summaryJson) {
    switch (type) {
        case 0:
            return getAreaSummary(childCoun, summaryJson);
        case 1:
            return getAreaNodeSummary(childCoun, summaryJson);
        case 2:
            return getAreaFsuSummary(childCoun, summaryJson);
        case 3:
            return getNodeSummary(childCoun, summaryJson);
        case 4:
            return getFsuSummary(childCoun, summaryJson);
        case 5:
            break;
    }
}

function getStationType(node) {
    //alert("get station type");

    var catlog = node.value.catalog;
    var type = 0; //0:area-area  1:area-node 2:area-fsu 3:node 4:fsu
    if (node.value.objType == AREA_TYPE) {
        if (node.children.length > 0) {
            var subNode = node.children[0];
            if (subNode.value.objType == AREA_TYPE) {
                if (subNode.value.catalog == "机房"
                || subNode.value.catalog == "基站") {
                    type = 1;
                }
            }
            else {
                type = 2;
            }
        }
    }
    else if (catlog == "机房"
        || catlog == "基站") {
        type = 3;
    }
    else if (node.value.objType == GATEWAY_TYPE) {
        type = 4;
    }
    else {
        type = 5;
    }
    return type;
}

var httpInvoke = function (url, type, data, successCb, failedCb, userData, async) {
    return $.ajax({
        url: url,
        type: type,
        data: data,
        async: async,

        success: function (response, status) {
            if (successCb != null) {
                successCb(response, status, userData);
            }
        },
        error: function (err) {
            if (failedCb != null) {
                failedCb(err, userData);
            }
        }

    })
}

var httpPost = function (url, data, successCb, failedCb, userData) {
    return httpInvoke(url, "post", data, successCb, failedCb, userData, true);
}

var httpGet = function (url, successCb, failedCb, userData) {
    return httpInvoke(url, "GET", null, successCb, failedCb, userData, true);
}


var httpPut = function (url, data, successCb, failedCb, userData) {
    return httpInvoke(url, "PUT", data, successCb, failedCb, userData, true);
}

var httpGetSyn = function (url, successCb, failedCb, userData) {
    return httpInvoke(url, "GET", null, successCb, failedCb, userData, false);
}


function drawChart(container, option) {
    var myChart = echarts.init(container);
    myChart.setOption(option, true);
}

function GetRequestParams(params) {
    var theRequest = new Object();
    strs = params.split("&");
    for (var i = 0; i < strs.length; i++) {
        theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
    }

    return theRequest;
}


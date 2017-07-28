
/*
 cacheService为SDK的缓存服务，缓存服务器设备列表、设备模板、量的值、量摘要等信息
 */
var CacheService = function () {
    this.devTree = null;
    this.curParentId = 0;
    this.templateCache = new Array();
    // this.temp=new templateObj();
    this.devChangeNotify = null;
    this.setDevChangeNotifyCb = function (fun) {
        this.devChangeNotify = fun;
    }
    // this.initTemplateService=function(){
    //        this.temp = new templateNode();
    //        this.temp.value=new templateObj();
    //        // this.temp.value.AreaLevel=0;
    //        this.temp.value.catalog = catlogSationProvince;
    //        // this.temp.value.FunctionType=0;
    //        // this.temp.value.Type=0;

    // }
    this.initDevService = function () {
        this.devTree = new treeNode();
        this.devTree.value = new devObj();
        this.devTree.value.objId = 0;
        this.devTree.value.catalog = catlogStationPrefix;
        this.devTree.value.name = "退出";
        this.curParentId = 0;
    };
    this.getSummary = function (objId, successCb, failedCb) {
        var url = "http://" + serverInfoService.getServerIp() + ":" + serverInfoService.getServerPort() + "/summary/" + objId;
        httpGet(url, successCb, failedCb);
    };

    this.getMetesByType = function (node, template, type) {
        if (template == null
         || template == undefined) return;
        var metes = new Array();
        for (var i = 0; i < template["Properties"].length; i++) {
            for (var j = 0; j < template["Properties"][i]["Metes"].length; j++) {
                if (template["Properties"][i]["Metes"][j]["MeteType"] == type || type == 255) {
                    var mete = new meteObj();
                    mete.devName = node.value.devName;
                    mete.objId = node.value.objId;
                    mete.Name = template["Properties"][i]["Metes"][j].Name;
                    mete.Id = template["Properties"][i]["Metes"][j].Id;
                    mete.transType = template["Properties"][i]["Metes"][j]["TransType"];
                    mete.meteType = template["Properties"][i]["Metes"][j]["MeteType"];
                    metes.push(mete);
                }
            }
        }

        for (var i = 0; i < template["Metes"].length; i++) {
            if (template["Metes"][i]["MeteType"] == type || type == 255) {
                var mete = new meteObj();
                mete.devName = node.value.devName;
                mete.objId = node.value.objId;
                mete.Name = template["Metes"][i].Name;
                mete.Id = template["Metes"][i].Id;
                mete.transType = template["Metes"][i]["TransType"];
                mete.meteType = template["Metes"][i]["MeteType"];
                metes.push(mete);
            }
        }
        return metes;
    };

    this.getDevList = function (url, successCb, failedCb) {
        //从服务器获取设备列表，缓存中将设备列表组织成一个树
        this.initDevService();
        function onSuccess(response, status, userData) {
            if (response["error"] != null) {
                alert("获取设备列表失败!");
            }
            else {
                var tmpDevList = response["result"];
                for (var i = 0; i < tmpDevList.length; i++) {
                    if (tmpDevList[i]["objType"] == SERVER_TYPE) {
                        continue;
                    }
                    if (tmpDevList[i]["obj"]["templateId"] == "" || tmpDevList[i]["obj"]["templateId"] == "50") {
                        continue;
                    }
                    var node = new devObj();
                    node.parentid = tmpDevList[i]["obj"]["parentId"];
                    node.name = tmpDevList[i]["obj"]["name"];
                    node.objId = tmpDevList[i]["obj"]["objId"];
                    node.templateId = tmpDevList[i]["obj"]["templateId"];
                    node.objState = tmpDevList[i]["objState"];
                    node.objType = tmpDevList[i]["objType"];
                    node.catalog = tmpDevList[i]["catalog"];
                    node.parentConnection = tmpDevList[i]["obj"]["parentConnection"];
                    node.isVideo = tmpDevList[i]["isVideo"]
                    if (node.catalog != null) {
                        if (node.objType == SMART_DEV_TYPE) {
                            node.img = "../img/smart.png";
                        }
                        else if (node.objType == GATEWAY_TYPE
                      && node.isVideo == false) {
                            node.img = "../../img/fsu.png";
                        }
                        else if (node.objType == AREA_TYPE
                            && (node.catalog == "基站" || node.catalog == "机房")) {
                            node.img = "../../img/node.png";
                        }
                        else if (node.objType == AREA_TYPE) {
                            node.img = "../../img/station.png"
                        }
                        else if (node.objType == SENSOR_TYPE) {
                            node.img = "../img/sensor.png";
                        }
                        else if (node.isVideo == true) {
                            node.img = "../img/ipc.png";
                        }
                    }
                    if (insertNode(node, userData.devTree) < 0) {
                        insertByConnection(node, userData.devTree, node.parentConnection);
                    }
                }
                if (userData.devChangeNotify != null) {
                    userData.devChangeNotify();
                }
                if (successCb != null) {
                    successCb();
                }
            }
        }

        function onFailed(error) {
            if (failedCb != null) {
                failedCb(error);
            }
            alert("获取设备列表失败 :" + error);
        }
        //alert(url);
        httpGet(url, onSuccess, onFailed, this);

        return '';

    };

    this.getRootStations = function () {//获取根局站下所有局站信息
        return this.devTree.children;
    };

    this.getSubDev = function (id) {//获取设备子节点,id为设备ID
        return getChildNode(id, this.devTree);

    };

    this.setCurParentId = function (id) {//设置当前显示父节点ID
        this.curParentId = id;
    };

    this.getCurParentId = function () {//获取当前父节点信息
        return this.curParentId;
    };

    this.getTemplateFromServer = function (templateId, successCb, failedCb) {
        if (templateId == "") {
            return;
        }
        // 从服务器获取设备模板信息，当getTemplateFromCache返回为null时说明缓存中没有模板信息,此时需要调用这个接口从服务器上获取
        var url = "http://" + serverInfoService.getServerIp() + ":" + serverInfoService.getServerPort() + "/resource/template/detail/" + templateId + "/";
        // var userData=cacheService;

        // this.onSuccess=successCb;
        //var temp=new templateObj();
        // this.initTemplateService();
        this.onSuccess = successCb;

        function onSuccess(response, status, userData) {
            // function onSuccess(response,status){
            if (response["error"] != null) {
                failedCb();
            }
            else {
                // var temp=new templateObj();
                // temp=JSON.parse(response["result"]["body"]);
                // 
                // userData.temp=JSON.parse(response["result"]["body"]);


                userData.templateCache.push(JSON.parse(response["result"]["body"]));
                if (successCb != null) {
                    successCb();
                }
            }
        }

        function onFailed(error) {
            if (failedCb != null) {
                failedCb(error);
            }
        }
        httpGetSyn(url, onSuccess, onFailed, this);
    };

    this.getTemplateFromCache = function (templateId) {
        //获取模板信息，templateId为模板ID

        for (var i = 0; i < this.templateCache.length; i++) {
            if (this.templateCache[i].Id == templateId) {
                //console.info("templateCache==" + JSON.stringify(this.templateCache[i]));
                return this.templateCache[i];
            }
        }
        return null;
    };


    this.getDevAllMetes = function (node) {//获取设备所有量信息，node为设备节点
        return this.getMetesByType(node, this.getTemplateFromCache(node.value.templateId), 255);
    };

    this.getDevPickMetes = function (node) {//获取设备采集量信息，node为设备节点
        return this.getMetesByType(node, this.getTemplateFromCache(node.value.templateId), 1);
    };

    this.getDevAlarmMetes = function (node) {//获取设备告警量信息，node为设备节点
        return this.getMetesByType(node, this.getTemplateFromCache(node.value.templateId), 0);
    };

    this.getDevTree = function () {//获取设备树
        return this.devTree;
    }
}


var cacheService = new CacheService();
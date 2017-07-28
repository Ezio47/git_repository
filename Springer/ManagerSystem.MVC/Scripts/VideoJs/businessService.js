/*
 标题：业务服务接口
 功能：处理登录后业务，获取实时数据的值
 */
var BusinessService = function () {
    var streamId = 0;
    this.streamInfoCache = new Array();
    this.getMete = function (mete, succeFun, failFun) {
        //调用服务器信息缓存接口serverInfoService，获取当前登录的服务器信息
        var url = "http://" + serverInfoService.getServerIp() + ":" + serverInfoService.getServerPort() + "/metes/values/";
        url += mete.gatewayId;
        //调用SDK的缓存服务接口cacheService，获取当前父节点信息
        url += "/" + mete.objId + "/" + mete.Id + "?token=" + mete.token;

        function onSuccess(response) {//服务器成功响应
            if (response["result"] != null) {
                mete.Value = response["result"]["value"];
                if (succeFun != null) {
                    succeFun(mete);
                }
            }
        }

        function onFailed(error) {//服务器响应失败
            if (failFun != null) {
                failFun(error);
            }
        }

        //alert("url====" + url);
        httpGet(url, onSuccess, onFailed);
    };

    this.getByToken = function (mete, succeFun, failFun) {
        var url = "http://" + serverInfoService.getServerIp() + ":" + serverInfoService.getServerPort() + "/metes/values/";
        url += mete.objId + "/" + mete.Id + "?isVideo=false";

        function onSuccess(response, status, userData) {
            if (response["result"] != null) {
                mete.gatewayId = response["result"]["reDicInfo"]["gatewayId"];
                mete.token = response["result"]["reDicInfo"]["token"];
                userData.getMete(mete, succeFun, failFun);
            }
        }

        function onFailed(error) {
            if (failFun != null) {
                failFun(response);
            }
        }

        httpGet(url, onSuccess, onFailed, this);
    };
    //登录成功后调用此接口，用于获取设备列表、获取活动告警和告警订阅

    this.afterLogin = function (wsUri, onLoginSuccesCb, onLoginFailedCb) {
        //wsUri：Websocket地址，onLoginSuccesCb:处理成功回调函数，onLoginFailedCb:处理失败回调函数      
        //调用serverInfoService
        var devUrl = "http://" + serverInfoService.getServerIp() + ":"
           + serverInfoService.getServerPort() + "/objs/userObjs/" + serverInfoService.getuserid();
        cacheService.getDevList(devUrl, function () {
            function getTemplate(treeNode) {
                cacheService.getTemplateFromServer(treeNode.value.templateId);
                for (var i = 0; i < treeNode.children.length; i++) {
                    //alert("lllll" + treeNode.children[i].name);
                    getTemplate(treeNode.children[i]);
                }
            }
            var tree = cacheService.getDevTree();
            //console.info("treecash=="+JSON.stringify(tree));
            getTemplate(tree.children[0]);
            //            alarmService.getAlarmsFromServer("http://" + serverInfoService.getServerIp() + ":"
            //                  + serverInfoService.getServerPort() + "/alarms/",
            //                  function () {
            //                      var wsUrl = "ws://" + serverInfoService.getServerIp() + ":" + serverInfoService.getServerPort() + wsUri;
            //                      startWebsocket(wsUrl, alarmService);
            //                      if (onLoginSuccesCb != null) {
            //                          onLoginSuccesCb();
            //                      }
            //                  });

            if (onLoginSuccesCb != null) {
                onLoginSuccesCb();
            }

        });


    };
    //获取量的实时值
    this.getMeteValue = function (mete, succeFun, failFun) {
        //mete:common.js中的meteObj（用于存放量相关的信息）函数，succeFun:获取成功回调函数例，failFun:获取失败回调函数实例
        if (mete.token == '') {
            this.getByToken(mete, succeFun, failFun);
        }
        else {
            this.getMete(mete, succeFun, failFun);
        }
    };

    this.getStreamId = function (node, streamType) {
        var metes = cacheService.getDevPickMetes(node);
        console.info("metes==" + metes);
        streamId = 0;
        if (metes != null) {
            alert("00000");
            for (var i = 0; i < metes.length; i++) {
                if (metes[i].transType == 1) {
                    if (streamId == 0) {
                        streamId = metes[i].Id;
                    }
                    else if (streamType == 0 && streamId > metes[i].Id) {
                        streamId = metes[i].Id;
                    }
                    else if (streamType == 1 && streamId < metes[i].Id) {
                        streamId = metes[i].Id;
                    }
                }
            }
        }
        alert("streamId=" + streamId);
    }
    //获取视频播放url
    this.getVideoUrl = function (node, streamType, onSuccesCb) {
        //node:设备节点，streamType:码流类型（0-主码流，1-子码流）,onSuccesCb：获取成功回调函数
        //var metes = cacheService.getDevPickMetes(node);
        //var streamId = 0;
        //for (var i = 0; i < metes.length; i++) {
        //    if (metes[i].transType == 1) {
        //        if (streamId == 0) {
        //            streamId = metes[i].Id;
        //        }
        //        else if (streamType == 0 && streamId > metes[i].Id) {
        //            streamId = metes[i].Id;
        //        }
        //        else if (streamType == 1 && streamId < metes[i].Id) {
        //            streamId = metes[i].Id;
        //        }
        //    }
        //}

        console.log("======="+JSON.stringify(node));
        //this.getStreamId(node, streamType);
        streamId = 1;
        if (streamId == 0) {
            return -1;
        }
        var found = false;
        for (var i = 0; i < this.streamInfoCache.length; i++) {
            if (this.streamInfoCache[i].objId == node.value.objId
                  && this.streamInfoCache[i].streamType == streamType) {
                found = true;
            }
        }
        if (found == false) {
            var info = new streamInfo();
            info.objId = node.value.objId;
            info.streamType = streamType;
            info.meteId = streamId;
            this.streamInfoCache.push(info);
        }
       // alert("streamId==" + streamId);
        var url = "http://" + serverInfoService.getServerIp() + ":" + serverInfoService.getServerPort() + "/metes/values/" + node.value.objId
                + "/" + streamId + "?isVideo=True&meteNo=-1";
        alert("==获取视频播放==" + url);
        console.log("==获取视频播放==" + url);
        function onSuccess(response, status, business) {
            console.info("error=" + JSON.stringify(response["error"]));
            if (response["error"] != null) {
                alert("获取视频url失败!");
            }
            else {
                var objId = response["result"]["objId"];
                var meteId = response["result"]["meteId"];
                for (var i = 0; i < business.streamInfoCache.length; i++) {
                    if (business.streamInfoCache[i].objId == objId
                            && business.streamInfoCache[i].meteId == meteId) {
                        business.streamInfoCache[i].aliasMeteName = response["result"]["meteAliasName"];
                        business.streamInfoCache[i].token = response["result"]["reDicInfo"]["token"];
                        break;
                    }
                }
                if (onSuccesCb != null) {
                    onSuccesCb(response["result"]["reDicInfo"]["rtmpToken"]);
                }
            }
        }
        function onFailed(error) {
            alert("获取视频url失败! :" + error);
        }
        httpGet(url, onSuccess, onFailed, this);
    }

    //2DZoom 焦距控制 +：0~1   -：-1~0  参数z  action：stop  停止 0.02
    //2DMove 上下左右控制  参数x，y 范围-1 ~1  action：begin 开始， stop 停止
    //set_preset 设置预制位 参数name 预制位名称 token 预制位序号
    //goto_preset 跳转预制位 参数token 预制位号
    this.ctrlPTZ = function (node, streamType, params, cmd, action) {
        var streamInfo = null;
        for (var i = 0; i < this.streamInfoCache.length; i++) {
            if (this.streamInfoCache[i].objId == node.value.objId
                && this.streamInfoCache[i].streamType == streamType) {
                streamInfo = this.streamInfoCache[i];
                break;
            }
        }
        if (streamInfo == null) {
            return;
        }
        var url = "http://" + serverInfoService.getServerIp() + ":" + serverInfoService.getServerPort() + "/metes/metectrl/";
        var jsonreq = new jsonrpc();
        var ptzParams = new ptzCtrlParam();
        ptzParams.gatewayId = node.value.objId;
        ptzParams.objId = node.value.objId;
        ptzParams.userName = serverInfoService.username;
        ptzParams.cmdName = cmd;
        ptzParams.action = action;
        ptzParams.params = params;
        ptzParams.aliasMeteName = streamInfo.aliasMeteName;
        ptzParams.meteId = streamInfo.meteId;
        ptzParams.token = streamInfo.token;
        jsonreq.setparams(ptzParams);
        // alert("==PTZ操作==" + url + "==json==" + jsonreq.jsonstr());
        httpPut(url, jsonreq.jsonstr());


    }

    this.getPreset = function (node, streamType, onSuccesCb) {
        var streamInfo = null;
        for (var i = 0; i < this.streamInfoCache.length; i++) {
            if (this.streamInfoCache[i].objId == node.value.objId
                && this.streamInfoCache[i].streamType == streamType) {
                streamInfo = this.streamInfoCache[i];
                break;
            }
        }
        if (streamInfo == null) {
            return;
        }
        var url = "http://" + serverInfoService.getServerIp() + ":" + serverInfoService.getServerPort()
          + "/metes/values/" + node.value.objId + "/" + node.value.objId + "/" + streamInfo.aliasMeteName + "?token=preset";
        function success(response) {
            if (response["result"]["meteValues"] != undefined) {
                var presetArray = new Array();
                for (var i = 0; i < response["result"]["meteValues"].length; i++) {
                    var preset = new presetInfo();
                    preset.seq = response["result"]["meteValues"][i].value;
                    preset.name = response["result"]["meteValues"][i].meteId;
                    presetArray.push(preset);
                }
                if (onSuccesCb != null) {

                    onSuccesCb(presetArray);
                }
            }
        }
        httpGet(url, success);

    }

    this.PTZOTHER = function (node, streamType, params, cmd, action) {
        var streamInfo = null;
        for (var i = 0; i < this.streamInfoCache.length; i++) {
            if (this.streamInfoCache[i].objId == node.value.objId
                && this.streamInfoCache[i].streamType == streamType) {
                streamInfo = this.streamInfoCache[i];
                break;
            }
        }
        if (streamInfo == null) {
            return;
        }
        var url = "http://" + serverInfoService.getServerIp() + ":" + serverInfoService.getServerPort() + "/metes/metectrl/";
    }
}

var businessService = new BusinessService();
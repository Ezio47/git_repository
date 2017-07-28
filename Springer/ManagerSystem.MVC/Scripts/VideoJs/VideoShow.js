/// <reference path="../_references.js" />
var videoNode;
var valuedatas;
var userip;
var userport;
//视频登录
function login(node) {
    valuedatas = node;
    var user =
   {
       username: "thl",
       password: "123456",
       serverip: "114.64.239.254", //114.64.239.254 window.location.hostname,
       serverport: 8080 //window.location.port
   };
    userip = user.serverip;
    userport = user.serverport;
    var date = new Date();
    date.setDate(date.getDate() + 10);　　//date设置为十天之后
    document.cookie = "usernames=user.username^password=user.username^expires=" + date.toGMTString();　　//toGMTString方法将date转换成格林尼治时间格式
    loginService.loginOnly(user, loginSuccess, loginFailed);


}

function loginSuccess(response) {
    var param = "name=" + serverInfoService.username + "&pwd=" + serverInfoService.password + "&ws=" + response["result"]["webSocketUrl"] + "&userid=" + response["result"]["userId"];
    //  window.location = 'pages/main.html?' + base64encode(param);
    //console.info(response);
    alert("登录成功");
    //var param = base64decode(window.location.search.substring(1, window.location.search.length))
     var params = GetRequestParams(param);
     console.info(params);
    serverInfoService.serverIp = "114.64.239.254";// params["serverip"]; //"114.64.239.254"; //window.location.hostname;
    serverInfoService.serverPort = 8080; //window.location.port;
    serverInfoService.username = params["name"];

    serverInfoService.password = params["pwd"];
    serverInfoService.userid = params["userid"];
    getVideoUrl(valuedatas, 0, function (url) {
        play(url, "videoScreen");
    });

    //loginService.loadResource(params["ws"], chenggong, shibai);
}
function loginFailed() {
    alert("用户名或密码输入有误！");
}


//获取视频播放url
this.getVideoUrl = function (node, streamType, onSuccesCb) {
    //node:设备节点，streamType:码流类型（0-主码流，1-子码流）,onSuccesCb：获取成功回调函数
    //this.getStreamId(node, streamType);
    var streamId = 1;
    var url = "http://" + userip + ":" + userport + "/metes/values/" + node.objId
            + "/" + streamId + "?isVideo=True&meteNo=-1";
    alert("==获取视频播放==" + url);
    console.log("==获取视频播放==" + url);
    function onSuccess(response, status, business) {
        if (response["error"] != null) {
            alert("获取视频url失败!");
        }
    }
    function onFailed(error) {
        alert("获取视频url失败! :" + error);
    }
    httpGet(url, onSuccess, onFailed, this);
}

//加载视频信息
function CreateVideoView() {
    //CreateVideoDiv();
    // createVideoView();
    //createControl();
    // createFourVideoView();
    // CreateVideoSelect();
    //CreateFocusing();
    // CreatePresetPosition();
    // playFourVideo();
}

//播放4屏视频
function playFourVideo() {
    //alert(videoNode);
    businessService.getVideoUrl(videoNode, 0, function (url) {
        play(url, "videoScreen");
    })
    //businessService.getVideoUrl(videoNode, 0, function (url) {
    //    play(url, "video2");
    //})
    //businessService.getVideoUrl(videoNode, 0, function (url) {
    //    play(url, "video3");
    //})
    //businessService.getVideoUrl(videoNode, 0, function (url) {
    //    play(url, "video4");
    //})
}
//播放单个视频
function play(url, id) {
    var div = document.getElementById(id);
    var width = div.offsetWidth;
    var height = div.offsetHeight;
    var flashvars = {
        f: url,
        c: 0,
        p: 1,
        lv: 1
    };
    CKobject.embed('../Scripts/VideoJs/ckplayer/ckplayer.swf', id, 'ckplayer_' + id, '100%', height, false, flashvars);

}
function stop(id) {
    CKobject.embed('', id, 'ckplayer_' + id);
}


//移动摄像头
function moveVideo(x, y) {
    var beginParams = new Array();
    var beginx = new keyValue();
    beginx.name = 'X';
    beginx.value = x;
    var beginy = new keyValue();
    beginy.name = 'Y';
    beginy.value = y;
    beginParams.push(beginx);
    beginParams.push(beginy);
    businessService.ctrlPTZ(videoNode, 0, beginParams, '2DMove', 'begin');
}
//停止移动摄像头
function moveVideoStop() {
    var stopParams = new Array();
    businessService.ctrlPTZ(videoNode, 0, stopParams, '2DMove', 'stop');
}

//焦距
var z = 0;

function reduicejjMouseDown() {
    if (z > -1) {
        z = z - 0.02;
    }
    var beginParams = new Array();
    var beginz = new keyValue();
    //        beginz.name = 'Z';
    //        beginz.value = z;
    //        beginParams.push(beginz);

    beginz.name = "z";
    beginz.value = "-0.447058823529412";
    beginParams.push(beginz);

    var extern = new keyValue();
    var ptzctrl = new ptzModel();
    ptzctrl.objId = videoNode.value.objId;
    ptzctrl.meteId = 49020003;
    ptzctrl.cmd = "ZOUT";
    ptzctrl.speed = 114;
    extern.name = "extern";
    extern.value = JSON.stringify(ptzctrl);
    beginParams.push(extern);

    var externcmd = new keyValue();
    externcmd.name = "externcmd";
    externcmd.value = "PTZ";
    beginParams.push(externcmd);

    businessService.ctrlPTZ(videoNode, 0, beginParams, '2DZoom', 'begin');
};
function reduicejjMouseUp() {
    var stopParams = new Array();
    var beginz = new keyValue();
    //        beginz.name = 'Z';
    //        beginz.value = z;
    //        beginParams.push(beginz);

    beginz.name = "z";
    beginz.value = "0.447058823529412";
    stopParams.push(beginz);

    var extern = new keyValue();
    var ptzctrl = new ptzModel();
    ptzctrl.objId = videoNode.value.objId;
    ptzctrl.meteId = 49020003;
    ptzctrl.cmd = "STOP";
    ptzctrl.speed = 114;
    ptzctrl.param = "ZOUT";
    extern.name = "extern";
    extern.value = JSON.stringify(ptzctrl);
    stopParams.push(extern);

    var externcmd = new keyValue();
    externcmd.name = "externcmd";
    externcmd.value = "PTZ";
    stopParams.push(externcmd);

    businessService.ctrlPTZ(videoNode, 0, stopParams, '2DZoom', 'stop');
};

function addjjMouseDown() {
    if (z < 1) {
        z = z + 0.02;
    }
    var beginParams = new Array();
    var beginz = new keyValue();
    //        beginz.name = 'Z';
    //        beginz.value = z;
    //        beginParams.push(beginz);

    var beginz = new keyValue();
    //        beginz.name = 'Z';
    //        beginz.value = z;
    //        beginParams.push(beginz);

    beginz.name = "z";
    beginz.value = "0.447058823529412";
    beginParams.push(beginz);

    var extern = new keyValue();
    var ptzctrl = new ptzModel();
    ptzctrl.objId = videoNode.value.objId;
    ptzctrl.meteId = 49020003;
    ptzctrl.cmd = "ZIN";
    ptzctrl.speed = 114;
    extern.name = "extern";
    extern.value = JSON.stringify(ptzctrl);
    beginParams.push(extern);

    var externcmd = new keyValue();
    externcmd.name = "externcmd";
    externcmd.value = "PTZ";
    beginParams.push(externcmd);

    businessService.ctrlPTZ(videoNode, 0, beginParams, '2DZoom', 'begin');
};
function addjjMouseUp() {
    var stopParams = new Array();

    var beginz = new keyValue();
    //        beginz.name = 'Z';
    //        beginz.value = z;
    //        beginParams.push(beginz);

    beginz.name = "z";
    beginz.value = "0.447058823529412";
    stopParams.push(beginz);

    var extern = new keyValue();
    var ptzctrl = new ptzModel();
    ptzctrl.objId = videoNode.value.objId;
    ptzctrl.meteId = 49020003;
    ptzctrl.cmd = "STOP";
    ptzctrl.speed = 114;
    ptzctrl.param = "ZIN";
    extern.name = "extern";
    extern.value = JSON.stringify(ptzctrl);
    stopParams.push(extern);

    var externcmd = new keyValue();
    externcmd.name = "externcmd";
    externcmd.value = "PTZ";
    stopParams.push(externcmd);

    businessService.ctrlPTZ(videoNode, 0, stopParams, '2DZoom', 'stop');
};



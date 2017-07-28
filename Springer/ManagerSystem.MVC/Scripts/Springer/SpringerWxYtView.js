/// <reference path="../_references.js" />
//播放
$("#IMG_Play").bind("click", function () {
    if ($("#IMG_Play").val() == "播放") {
        $("#IMG_Play").attr("value", "停止");
        playInterVal = self.setInterval('getImage()', speed);
        $("#IMG_Quick").removeAttr("style disabled");
        $("#IMG_Slow").removeAttr("style disabled");
    }
    else if ($("#IMG_Play").val() == "停止") {
        $("#IMG_Play").attr("value", "播放");
        i = 0;
        speed = 1000;
        getImage();
        window.clearInterval(playInterVal);
        $("#IMG_Quick").attr({"style": "background-color:#D8D3D3", "disabled": "disabled" });
        $("#IMG_Slow").attr({"style": "background-color:#D8D3D3", "disabled": "disabled" });
    }
});

//加速
$("#IMG_Quick").bind("click", function () {
    if (speed > 500) {
        speed = speed - 500;
        window.clearInterval(playInterVal);
        playInterVal = self.setInterval('getImage()', speed);
    }

});
//减速
$("#IMG_Slow").bind("click", function () {
    if (speed < 5000) {
        speed = speed + 500;
        window.clearInterval(playInterVal);
        playInterVal = self.setInterval('getImage()', speed);
    }

});
///获取图片
function getImage() {
    if (i != icount) {
        spath = ytlist[i].CLOUDFILENAME
        opath = ytlist[i].CLOUDORIGIONNAME
        $('#image_viewer').attr('src', spath);
        $('#imageShow').attr('href', opath);
        i++;
    }
    else {
        i = 0;
    }
}
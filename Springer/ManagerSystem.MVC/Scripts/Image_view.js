$(document).ready(function () {

    scrollTo(0, 255);

    $("body").on({
        ajaxStart: function () {
            //$(this).addClass("loading"); 
        },
        ajaxStop: function () {
            //$(this).removeClass("loading");
            if ($("#filelist")[0] != undefined && $("#filelist")[0].length == 0) {
                $("#image_viewer").attr("src", "/resources/images/noproduct.jpg");
            }
        }
    });

    $(".image_viewer").css("height", "auto");
    $(".image_viewer").css("float", "left");
    $(".filelist").css("float", "right");
    $("#image_viewer").css("height", "auto");

    var speed = 1000;
    var playInterVal = null;

    if ($("#IMG_Order").size() == 0) {
        var dom = "<input type=\"button\" id=\"IMG_Order\" value=\"正向\" />";
        $('#IMG_Play').after(dom);
    }

    if ($("#IMG_HideList").size() == 0) {
        var dom = "<input type=\"button\" id=\"IMG_HideList\" value=\"隐藏文件名列表\" />";
        $('#IMG_Quick').before(dom);
    }

    $("#IMG_HideList").bind("click", function () {
        if ($("#IMG_HideList").val() == "隐藏文件名列表") {
            $("#IMG_HideList").attr("value", "显示文件名列表");
            $(".filelist").hide();
            $(".image_viewer").css("width", ($(".image_viewer").width() + $(".filelist").width()) + "px");
            $(".image_viewer").css("margin-left", "");
            $("#image_viewer").css("width", ($("#image_viewer").width() + $(".filelist").width()) + "px");
        }
        else {
            $("#IMG_HideList").attr("value", "隐藏文件名列表");
            $(".filelist").show();
            $(".image_viewer").css("width", ($(".image_viewer").width() - $(".filelist").width()) + "px");
            $(".image_viewer").css("margin-left", "10px");
            $("#image_viewer").css("width", ($("#image_viewer").width() - $(".filelist").width()) + "px");
        }
    });

    $("#IMG_HideList").click();

    if ($("#IMG_VIEWER_CONTAINER").size() == 0) {
        var height = $(window).height() - 20;
        var scrollHeight = $(document).scrollTop();
        var width = $(window).width() - 20;
        var dom = "<div id=\"IMG_VIEWER_CONTAINER\" style=\"height: " + height + "px; left: 10px; top: " + (10 + scrollHeight) + "px; width: " + width + "px;\"><div id=\"IMG_VIEWER_CONTAINER_TOOLBAR\" style=\"width: " + width + "px;\"><a id=\"IMG_VIEWER_ZOOMIN\" href=\"\">放大</a><a id=\"IMG_VIEWER_REALSIZE\" href=\"\">实际大小</a><a id=\"IMG_VIEWER_ZOOMOUT\" href=\"\">缩小</a><a id=\"IMG_VIEWER_NEXT\" href=\"\">上一张</a><a id=\"IMG_VIEWER_PLAY\" href=\"\">播放</a><a id=\"IMG_VIEWER_PREV\" href=\"\">下一张</a></div><div id=\"IMG_VIEWER_DISPLAY\" style=\"overflow:auto;height: " + (height - 32) + "px;width: " + (width - 1) + "px;\"><img style=\"height: " + (height - 47) + "px;width: " + (width - 1) + "px;\" id=\"IMG_VIEWER\" src=\"\" title=\"点击关闭\" /></div></div>";
        $(window.document.body).append(dom);
    }

    function zoom_image(src) {

        var realImage = new Image();
        var realImage_height = 0;
        var realImage_width = 0;
        realImage.onload = function () {
            realImage_height = realImage.height;
            realImage_width = realImage.width;
        }

        $("#IMG_VIEWER_ZOOMIN").bind("click", function () {
            $("#IMG_VIEWER").css("height", ($("#IMG_VIEWER").height() + 20) + "px");
            $("#IMG_VIEWER").css("width", ($("#IMG_VIEWER").width() + 20) + "px");
            return false;
        });
        $("#IMG_VIEWER_ZOOMOUT").bind("click", function () {
            $("#IMG_VIEWER").css("height", ($("#IMG_VIEWER").height() - 20) + "px");
            $("#IMG_VIEWER").css("width", ($("#IMG_VIEWER").width() - 20) + "px");
            return false;
        });
        $("#IMG_VIEWER_REALSIZE").bind("click", function () {
            $("#IMG_VIEWER").css("height", realImage_height + "px");
            $("#IMG_VIEWER").css("width", realImage_width + "px");
            return false;
        });
        $("#IMG_VIEWER_NEXT").bind("click", function () {
            $("#IMG_Prev").click();
            $("#IMG_VIEWER").attr("src", $("#image_viewer").attr("src")).attr("title", "点击关闭");
            return false;
        });
        $("#IMG_VIEWER_PREV").bind("click", function () {
            $("#IMG_Next").click();
            $("#IMG_VIEWER").attr("src", $("#image_viewer").attr("src")).attr("title", "点击关闭");
            return false;
        });
        $("#IMG_VIEWER_PLAY").bind("click", function () {
            if ($("#IMG_VIEWER_PLAY").html() == "播放") {
                $("#IMG_Play").attr("value", "停止");
                $("#IMG_VIEWER_PLAY").html("停止");
                playInterVal = setInterval("$(\"#IMG_Next\").click();$(\"#IMG_VIEWER\").attr(\"src\", $(\"#image_viewer\").attr(\"src\")).attr(\"title\", \"点击关闭\");", 1000);
            }
            else {
                $("#IMG_VIEWER_PLAY").html("播放");
                $("#IMG_Play").attr("value", "播放");
                clearInterval(playInterVal);
            }
            return false;
        });

        $("#IMG_VIEWER_CONTAINER").show().css("top", (10 + $(document).scrollTop())).click(function () {
            $("#IMG_VIEWER_CONTAINER").hide();

            $("#IMG_VIEWER").css("height", (height - 47) + "px");
            $("#IMG_VIEWER").css("width", (width - 1) + "px");
        });

        $("#IMG_VIEWER").attr("src", src).attr("title", "点击关闭");
        $("#IMG_VIEWER").draggable();
        realImage.src = $("#IMG_VIEWER").attr("src");
    }

    $("#image_viewer").click(function () {
        zoom_image($("#image_viewer").attr("src"));
    });

    if ($("#play_image").size() != 0) {
        $("#play_image").show();
        $("#IMG_Quick").bind("click", function () {
            if (speed < 5000) {
                speed = speed + 500;
                clearInterval(playInterVal);
                $("#IMG_Play").attr("value", "停止");
                $("#IMG_VIEWER_PLAY").html("停止");
                if ($("#IMG_Order").val() == "正向") {
                    playInterVal = setInterval("$(\"#IMG_Prev\").click();$(\"#IMG_VIEWER\").attr(\"src\", $(\"#image_viewer\").attr(\"src\")).attr(\"title\", \"点击关闭\");", speed);
                } else {
                    playInterVal = setInterval("$(\"#IMG_Next\").click();$(\"#IMG_VIEWER\").attr(\"src\", $(\"#image_viewer\").attr(\"src\")).attr(\"title\", \"点击关闭\");", speed);
                };
            }
        });
        $("#IMG_Play").bind("click", function () {

            if ($("#IMG_Play").val() == "播放") {
                $("#IMG_Play").attr("value", "停止");
                $("#IMG_VIEWER_PLAY").html("停止");
                if ($("#IMG_Order").val() == "正向") {
                    playInterVal = setInterval("$(\"#IMG_Prev\").click();$(\"#IMG_VIEWER\").attr(\"src\", $(\"#image_viewer\").attr(\"src\")).attr(\"title\", \"点击关闭\");", speed);
                } else {
                    playInterVal = setInterval("$(\"#IMG_Next\").click();$(\"#IMG_VIEWER\").attr(\"src\", $(\"#image_viewer\").attr(\"src\")).attr(\"title\", \"点击关闭\");", speed);
                };
            }
            else {
                $("#IMG_Play").attr("value", "播放");
                $("#IMG_VIEWER_PLAY").html("播放");
                clearInterval(playInterVal);
            }
        });
        $("#IMG_Order").bind("click", function () {
            if ($("#IMG_Order").val() == "反向") {
                $("#IMG_Order").val("正向");
                clearInterval(playInterVal);
                $("#IMG_Play").attr("value", "停止");
                $("#IMG_VIEWER_PLAY").html("停止");
                playInterVal = setInterval("$(\"#IMG_Prev\").click();$(\"#IMG_VIEWER\").attr(\"src\", $(\"#image_viewer\").attr(\"src\")).attr(\"title\", \"点击关闭\");", speed);
            } else {
                $("#IMG_Order").val("反向");
                clearInterval(playInterVal);
                $("#IMG_Play").attr("value", "停止");
                $("#IMG_VIEWER_PLAY").html("停止");
                playInterVal = setInterval("$(\"#IMG_Next\").click();$(\"#IMG_VIEWER\").attr(\"src\", $(\"#image_viewer\").attr(\"src\")).attr(\"title\", \"点击关闭\");", speed);
            };
        });
        $("#IMG_Slow").bind("click", function () {
            if (speed > 500) {
                speed = speed - 500;
                clearInterval(playInterVal);
                $("#IMG_Play").attr("value", "停止");
                $("#IMG_VIEWER_PLAY").html("停止");
                if ($("#IMG_Order").val() == "正向") {
                    playInterVal = setInterval("$(\"#IMG_Prev\").click();$(\"#IMG_VIEWER\").attr(\"src\", $(\"#image_viewer\").attr(\"src\")).attr(\"title\", \"点击关闭\");", speed);
                } else {
                    playInterVal = setInterval("$(\"#IMG_Next\").click();$(\"#IMG_VIEWER\").attr(\"src\", $(\"#image_viewer\").attr(\"src\")).attr(\"title\", \"点击关闭\");", speed);
                };
            }
        });

        $("#IMG_Prev").bind("click", function () {
            var prev = $("#filelist")[0].selectedIndex - 1;
            if (prev >= 0) {
                $("#filelist")[0].options[prev].selected = true;
                $("#filelist")[0].click();
            }
            else {
                $("#filelist")[0].options[$("#filelist")[0].length - 1].selected = true;
                $("#filelist")[0].click();
            }
        });
        $("#IMG_Next").bind("click", function () {
            var next = $("#filelist")[0].selectedIndex + 1;
            if (next < $("#filelist")[0].length) {
                $("#filelist")[0].options[next].selected = true;
                $("#filelist")[0].click();
            }
            else {
                $("#filelist")[0].options[0].selected = true;
                $("#filelist")[0].click();
            }
        });
    }

    var dom = "<input type=\"button\" class=\"PrevShixiao\" value=\"上一时效\"><input type=\"button\" class=\"NextShixiao\" value=\"下一时效\">";
    $(dom).insertAfter(".toolbar [name=shixiao]");
    $(".PrevShixiao").live("click", function () {

        var next = $(this).prev()[0].selectedIndex - 1;
        if (next < $(this).prev()[0].length) {
            $(this).prev()[0].options[next].selected = true;
            $(this).prev()[0].click();
        }
        else {
            $(this).prev()[0].options[0].selected = true;
            $(this).prev()[0].click();
        }
        $($(this).prev()[0]).change();

        return false;
    });
    $(".NextShixiao").live("click", function () {
        var next = $(this).prev().prev()[0].selectedIndex + 1;
        if (next < $(this).prev().prev()[0].length) {
            $(this).prev().prev()[0].options[next].selected = true;
            $(this).prev().prev()[0].click();
        }
        else {
            $(this).prev().prev()[0].options[0].selected = true;
            $(this).prev().prev()[0].click();
        }
        $($(this).prev().prev()[0]).change();

        return false;
    });


});
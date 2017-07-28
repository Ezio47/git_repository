var LoginService = function () {
    this.login = function (user, onLoginSuccesCb, onLoginFailedCb) {
        serverInfoService.serverIp = user.serverip;
        serverInfoService.serverPort = user.serverport;
        serverInfoService.username = user.username;
        serverInfoService.password = user.password;
        serverInfoService.setServerAddr(user.serverip, user.serverport);
        var loginurl = "http://" + user.serverip + ":" + user.serverport + "/login/";
        var jsonreq = new jsonrpc();
        jsonreq.setvalue("userName", user.username);
        jsonreq.setvalue("paswd", hex_md5(user.password));
        var req = {
            method: 'POST',
            url: loginurl,
            data: jsonreq.jsonstr(),
            crossDomain: true // enable this
        };

        function SuccessCb(response, status) {
            if (response["error"] != null) {
                alert("用户名密码不正确!");
                if (onLoginFailedCb != null) {
                    onLoginFailedCb(response);
                }
            }
            else {
                businessService.afterLogin(response["result"]["webSocketUrl"], onLoginSuccesCb, onLoginFailedCb);
            }
        }

        function FailedCb(error) {
            var sts = error.statusCode();
            alert("登录失败 :" + error.statusCode());
            if (onLoginFailedCb != null) {
                onLoginFailedCb(error);
            }
        }
        httpPost(loginurl, jsonreq.jsonstr(), SuccessCb, FailedCb);
    }
    this.loginOnly = function (user, onLoginSuccesCb, onLoginFailedCb) {
        //serverInfoService.serverIp = user.serverip;
        //serverInfoService.serverPort = user.serverport;
        //serverInfoService.username = user.username;
        //serverInfoService.password = user.password;
        var loginurl = "http://" + user.serverip + ":" + user.serverport + "/login/";
        var jsonreq = new jsonrpc();
        jsonreq.setvalue("userName", user.username);
        jsonreq.setvalue("paswd", hex_md5(user.password));
        var req = {
            method: 'POST',
            url: loginurl,
            data: jsonreq.jsonstr(),
            crossDomain: true // enable this
        };

        function SuccessCb(response, status) {
            if (response["error"] != null) {
                // alert("用户名密码不正确!");
                if (onLoginFailedCb != null) {
                    onLoginFailedCb(response);
                }
            }
            else {
                if (onLoginSuccesCb != null) {
                    onLoginSuccesCb(response);
                }
            }
        }

        function FailedCb(error) {
            console.info(error);
            var sts = error.statusCode();
            //alert("登录失败 :" + error.statusCode());
            if (onLoginFailedCb != null) {
                onLoginFailedCb(error);
            }
        }
        httpPost(loginurl, jsonreq.jsonstr(), SuccessCb, FailedCb);
    }

    this.loadResource = function (wsUrl, onLoginSuccesCb, onLoginFailedCb) {
        businessService.afterLogin(wsUrl, onLoginSuccesCb, onLoginFailedCb);
    }
}
var loginService = new LoginService();


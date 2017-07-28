/*
 serverInfoService为服务器信息缓存接口，可以通过此接口设置和获取当前登录的服务器信息
 */
var serverInfo=function() {
    this.serverIp ="";
    this.serverPort="";
    this.username="";
    this.password = "";
    this.userid = "";
    this.setServerAddr=function(ip, port){//设置服务器信息
          this.serverIp = ip;
          this.serverPort = port;
        };
    this.getServerIp=function(){//获取服务器Ip
          return this.serverIp;
        };
    this.getServerPort=function(){//获取服务器端口
          return this.serverPort;
    }

    this.getuserid=function()
    {
        return this.userid;
    }
}

var serverInfoService = new serverInfo();
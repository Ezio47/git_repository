
function createWebsocket(url, alarmService)
{
   var myWebSocket = new WebSocket(url);
   myWebSocket.onopen = function (openEvent) {  
                  
    };
    myWebSocket.onmessage = function (messageEvent) { 
         var jsonObj = JSON.parse(messageEvent.data);
         if(jsonObj != null){            
            if(jsonObj["method"] == "alarm_notify"){
                if(jsonObj["params"]["AlarmFlag"] == "BEGIN" && jsonObj["params"]["State"]["currentState"] == "alarm"){
                      var msg = ["新的告警",jsonObj["params"]["AreaName"]+" "+jsonObj["params"]["DeviceName"]+"发生告警"];
                       ///window.plugins.MsgNotify(msg ,function(data){alert("success");}, function(data){alert("error");});
                }
                alarmService.addAlarm(jsonObj["params"]);  
            }                   
         }

    };
    myWebSocket.onerror = function (errorEvent) {   
          
    };
    myWebSocket.onclose = function (closeEvent) {         
    }
    return myWebSocket;
}

function startWebsocket(url, alarmService){
       if(window.WebSocket == null)
       {
           alert("not support WebSocket");
       }
       else
       {
           var wsSocket = createWebsocket(url, alarmService);
           /*
            CONNECTING  (0) Default
            OPEN (1)
            CLOSING (2)
            CLOSED (3)
           */
           setInterval(function(){
              if(wsSocket.readyState == 1){
                  wsSocket.send("heartbeat");                    
              }
              else
              {
                  wsSocket = createWebsocket(url, alarmService);
              }
              
          },10000);
       }

}





function moveto(x, y) {

    var sgworld = CreateSGObj();
    if (x == null || x == '') {
        alert("无坐标信息！");
        retun;
    }

    var dXCoord = x;
    var dYCoord = y;
    var dAltitude = 0;
    var eAltitudeTypeCode = 2; //AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
    var dYaw = 0.0;
    var dPitch = -90.0;
    var dRoll = 0.0;
    var dDistance = 5000;
    var position = sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
    sgworld.Navigate.FlyTo(position);
    
}


var MyShuttle = MyShuttle || {};

MyShuttle.Config = function(){
    var bingMapsKey = 'AiOp5RxYtMGivuaQLXhKiGK4m2xBNk2WRXHWHPbXCVrCHvX2_ozTtMZPxtgO4QRO',
        infobBoxCompanyAddress = 'Spring Studios, 50 Varick St',
        companyLocation = {
            Latitude: 40.72109533886229,
            Longitude: -74.006596745813
        },
        smallScreenMinWidth = 768;

    return {
        bingMapsKey: bingMapsKey,
        infoBoxCompanyAddress: infobBoxCompanyAddress,
        companyLocation: companyLocation,
        smallScreenMinWidth: smallScreenMinWidth
    }
}();
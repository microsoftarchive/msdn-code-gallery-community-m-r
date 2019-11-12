define(['services/dataservice', 'services/context'], function (dataservice, context) {
    var initialized = false;
    
    var vm = {
        activate: activate,
        name: context.currentUser.fullName,
        picture: context.currentUser.picture
    };

    return vm;

    function activate() {
        if (initialized) return;

        initialized = true;
    }
});
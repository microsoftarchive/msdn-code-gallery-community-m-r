Array.prototype.replaceValues = function (newArrayValues) {
    this.splice(0, this.length);
    this.push.apply(this, newArrayValues);
};

Array.prototype.pushRange = function (newArrayValues) {
    this.push.apply(this, newArrayValues);
};

Array.prototype.remove = function (filter) {
    var array = this;
    var objectsToRemove = array.filter(filter);

    objectsToRemove.forEach(function (obj) {
        array.splice(array.indexOf(obj), 1);
    });    
};
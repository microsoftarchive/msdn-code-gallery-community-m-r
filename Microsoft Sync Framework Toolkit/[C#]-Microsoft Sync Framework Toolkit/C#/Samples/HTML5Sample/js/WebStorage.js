// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

// WebStorage is the class that provides offline storage for users, entities and
// server blob on top of HTML5 web storage.
function WebStorage(schema) {
    var _tables = null;     // List of tables which contain different kinds of entities
    var _user = null;       // Cached user identity
    var _schema = schema;   // Schema information of entities
    // Helper function to determine whether a field of an entity is a key
    function _IsKey(type, field) {
        try {
            return _schema[type][field].key;
        }
        catch (err) {
            return false;
        }
    }
    // Get corresponding value in cascade structure in schema
    function _GetCascadeValue(type, field, flag, defaultValue) {
        try {
            return _schema[type][field].cascade[flag];
        }
        catch (err) {
            return defaultValue;
        }
    }
    // Determine whether deletion of an entity will result in
    // deletion of entities of other types
    function _IsCascadeDelete(type, field) {
        return _GetCascadeValue(type, field, "delete", false);
    }
    // Determine whether update of an entity will result in 
    // update of entities of other types
    function _IsCascadeUpdate(type, field) {
        return _GetCascadeValue(type, field, "update", false);
    }
    // Return a list of entities that will be impacted by 
    // cascade update or delete
    function _GetCascadeTarget(type, field) {
        return _GetCascadeValue(type, field, "target", null);
    }
    // Get cached user identity
    this.getCachedUser = function () {
        var user = null;
        try { user = JSON.parse(window.localStorage.getItem("LOCAL_USER")); } catch (err) { }
        return user;
    };
    // Save user identity to offline cache
    this.setCachedUser = function (user) {
        window.localStorage.setItem("LOCAL_USER", JSON.stringify(user));
    }
    // Load entities from offline cache
    this.init = function (user) {
        _user = user;
        try { _tables = JSON.parse(window.localStorage.getItem(_user.id + "_OFFLINE_STORAGE")); } catch (err) { }
        if (_tables == null) { _tables = new Object(); }
    };
    // Remove tombstones that have been uploaded to server and persist to offline cache
    this.commit = function () {
        for (var i in _tables) {
            for (var j in _tables[i].rows) {
                if (_tables[i].rows[j].__metadata.isDeleted) {
                    if (!_tables[i].rows[j].__metadata.isDirty) {
                        delete _tables[i].rows[j];
                    }
                }
            }
        }
        window.localStorage.setItem(_user.id + "_OFFLINE_STORAGE", JSON.stringify(_tables));
    };
    // Get cached server blob
    this.getServerBlob = function () {
        var serverBlob = "";
        try { serverBlob = window.localStorage.getItem(_user.id + "_SERVER_BLOB"); } catch (err) { }
        return serverBlob;
    };
    // Save server blob to offline cache
    this.setServerBlob = function (blob) {
        try { serverBlob = window.localStorage.setItem(_user.id + "_SERVER_BLOB", blob); } catch (err) { }
    };
    // Get table of a particular entity type
    this.getTable = function (name) {
        if (_tables[name] == null) {
            _tables[name] = new Object();
            _tables[name].name = name;
            _tables[name].rows = new Object();
        };
        return _tables[name];
    };
    // Get the schema object
    this.getSchema = function() {
        return _schema;
    }
    // Save entity and handle tempId and cascade update
    this.setEntity = function(entity) {
        var table = this.getTable(entity.__metadata.type);
        var id = entity.__metadata.uri ? entity.__metadata.uri : entity.__metadata.tempId;
        // Only handle cascade update when it's on existing entity and it's from local
        // Note:    Cascade update is only needed when corresponding relationship exists
        //          between tables on server side. It's an application choice but not 
        //          enforced by sync service implementation.
        if ((table.rows[id] != null) && (entity.__metadata.isDirty)) {
            for (var i in entity) {
                // Cascade update should only happen on updated key fields
                if (_IsKey(entity.__metadata.type, i)) {
                    if (_IsCascadeUpdate(entity.__metadata.type, i)) {
                        if (entity[i] != table.rows[id][i]) {
                            var target = _GetCascadeTarget(entity.__metadata.type, i);
                            // Walk through every cascade target and update corresponding fields
                            for (var j in target) {
                                var filter = new Object();
                                filter[target[j]] = entity[i];
                                var entities = this.getEntities(j, filter);
                                for (var k in entities) {
                                    k[target[j]] = entity[i];
                                    k.__metadata.isDirty = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        if (entity.__metadata.uri != null) {
            table.rows[entity.__metadata.uri] = entity;
            // Remove old entry keyed by tempId
            if (entity.__metadata.tempId != null) {
                delete table.rows[entity.__metadata.tempId];
            }
        }
        else if (entity.__metadata.tempId != null) {
            // Create an entry keyed by tempId, which will deleted when sync service
            // assigns an official uri to it.
            table.rows[entity.__metadata.tempId] = entity;
        }
        else {
            throw entity;
        }
    };
    // Delete an entity and handle cascade delete
    this.deleteEntity = function (entity) {
        var table = this.getTable(entity.__metadata.type);
        var id = entity.__metadata.uri ? entity.__metadata.uri : entity.__metadata.tempId;
        if (table.rows[id] != null) {
            entity = table.rows[id];
            entity.__metadata.isDeleted = true;
            entity.__metadata.isDirty = true;
            for (var i in entity) {
                if (i != "__metadata") {
                    // Handle cascade delete 
                    // Note:    Cascade delete is only needed when corresponding relationship exists
                    //          between tables on server side. It's an application choice but not 
                    //          enforced by sync service implementation.
                    if (_IsKey(entity.__metadata.type, i)) {
                        if (_IsCascadeDelete(entity.__metadata.type, i)) {
                            var target = _GetCascadeTarget(entity.__metadata.type, i);
                            for (var j in target) {
                                var filter = new Object();
                                filter[target[j]] = entity[i];
                                var entities = this.getEntities(j, filter);
                                for (var k in entities) {
                                    this.deleteEntity(entities[k]);
                                }
                            }
                        }
                    }
                    else {
                        delete entity[i];
                    }
                }
            }
            // Permenantly remove an entity if it's never uploaded to server
            if (id == entity.__metadata.tempId) {
                delete table.rows[entity.__metadata.tempId];
            }
        }
        else {
            throw entity;
        }
    };
    // Get a list of entities based on its type and filter
    this.getEntities = function (type, filter) {
        var table = _tables[type];
        var entities = new Object();
        if (table != null) {
            for (var i in table.rows) {
                // Ignore entities marked as deleted
                if (table.rows[i].__metadata.isDeleted) {
                    continue;
                }
                // Check for match
                var match = true;
                for (var j in filter) {
                    if (filter[j] != table.rows[i][j]) {
                        match = false;
                        break;
                    }
                }
                if (match) {
                    entities[i] = table.rows[i];
                }
            }
        }
        return entities;
    };
    // Enumerate changes from offline cache
    this.enumerateChanges = function () {
        var changes = new Array();
        for (var i in _tables) {
            for (var j in _tables[i].rows) {
                if (_tables[i].rows[j].__metadata.isDirty) {
                    changes.push(_tables[i].rows[j]);
                }
            }
        }
        return changes;
    };
    // Clear offline cache
    this.clearLocal = function () {
        _tables = new Object();
        this.setServerBlob("");
        this.commit();
        _user = null;
        this.setCachedUser(null);
    }
}
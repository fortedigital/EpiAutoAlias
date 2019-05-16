﻿define([
    // Dojo
    "dojo/_base/declare",
    //CMS
    "epi/_Module",
    "epi/routes"
], function (
    // Dojo
    declare,
    //CMS
    _Module,
    routes
) {
    return declare("redirects.UrlRedirectsModuleInitializer", [_Module], {

        initialize: function () {

            this.inherited(arguments);

            var registry = this.resolveDependency("epi.storeregistry");
            //Register the store
            registry.create("redirectsComponent.urlRedirectsStore", this._getRestPath("UrlRedirectsComponentStore"));
            registry.create("redirectsMenu.urlRedirectsStore", this._getRestPath("RedirectRuleStore"));
        },
        _getRestPath: function (name) {
            return routes.getRestPath({ moduleArea: "Redirects", storeName: name });
        }
   });
});
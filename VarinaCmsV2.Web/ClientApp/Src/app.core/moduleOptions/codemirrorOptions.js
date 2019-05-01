(function() {
    'use strict';

    angular
        .module('app.core')
        .service('codemirrorOptions', codemirrorOptions);

    codemirrorOptions.$inject = [];
    function codemirrorOptions() {
      angular.extend(this,{
            lineNumbers: true,
            mode: 'xml',
            theme:'material',
            autoCloseTags:true,
            styleActiveLine:true,
            extraKeys: {"Ctrl-Space": "autocomplete"},
           
      });
        
        ////////////////

        }
})();
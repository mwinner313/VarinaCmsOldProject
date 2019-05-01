(function() {
    'use strict';

    angular
        .module('app.core')
        .factory('arrayFlatter', arrayFlatter);

    arrayFlatter.$inject = [];
    function arrayFlatter() {

        function flat(root,childProp) {
            var stack = [], array = [], hashMap = {};
            stack=root;
        
            while(stack.length !== 0) {
                var node = stack.pop();
                if(node[childProp].length==0) {
                    visitNode(node, hashMap, array);
                } else {
                    visitNode(node, hashMap, array);
                    for(var i = node[childProp].length - 1; i >= 0; i--) {
                        stack.push(node[childProp][i]);
                    }
                }
            }

            function visitNode(node, hashMap, array) {
                if(!hashMap[node.id]) {
                    hashMap[node.id] = true;
                    array.push(node);
                }
            }
            
            angular.forEach(array,function(item,idx){
                array[idx][childProp]=[];
            });
            return array;
        }

        var service = {
            flat:flat
        };
        
        return service;

        ////////////////
        function exposedFn() { }
    }
})();
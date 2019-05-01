angular.element(document).ready(function () {
    var initInjector = angular.injector(['ng']);

    var $q = initInjector.get('$q');
    var $http = initInjector.get('$http');
    var $timeout = initInjector.get('$timeout');
    var currentUser = $http.get('/api/currentUser');
    var languages = $http.get('/api/language');
    var entitySchemes = $http.get('/api/entityScheme',{params:{type:5}});
    var formSchemes = $http.get('/api/formScheme');
    var notSeenForms = $http.get('/api/form', {
        params: {
            noPaginate: true,
            notSeen: true
        }
    });
    $q.all([currentUser, entitySchemes, formSchemes, notSeenForms, languages]).then(function (ress) {

        MobileDragDrop.polyfill({
            // use this to make use of the scroll behaviour
            dragImageTranslateOverride: MobileDragDrop.scrollBehaviourDragImageTranslateOverride
        });

        $(window).resize(function () {
            $('[ng-scrollbars]').css("height", window.innerHeight - 140)
        });

        function addToSessionStorage(key, data) {
            var cache = [];
            window.sessionStorage.setItem(key, JSON.stringify(data, function (key, value) {
                if (typeof value === 'object' && value !== null) {
                    if (cache.indexOf(value) !== -1) {
                        return;
                    }
                    cache.push(value);
                }
                return value;
            }))
            cache = null; // Enable garbage collection
        }
        addToSessionStorage('ngStorage-currentUser', ress[0].data);
        addToSessionStorage('ngStorage-entitySchemes', ress[1].data);
        addToSessionStorage('ngStorage-formSchemes', ress[2].data);
        addToSessionStorage('ngStorage-notSeenForms', ress[3].data);
        addToSessionStorage('ngStorage-languages', ress[4].data);
        angular.bootstrap(document, ['app']);
    });

});

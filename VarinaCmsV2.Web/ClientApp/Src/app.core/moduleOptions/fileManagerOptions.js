// angular.module('FileManagerApp').config(['fileManagerConfigProvider', function (config) {
//   var defaults = config.$get();
//   config.set({
//     // appName: 'varina',
//     pickCallback: function (item) {
//       if (window.tinyMceWating) {
//         window.mceEditor.insertContent("<img src='$path' class='img-responsive' alt='$alt'/>"
//           .replace('$path', item.fullPath())
//           .replace('$alt', item.name))
//         $('#fileManager').modal('hide');
//         window.tinyMceWating = false;
//         return true;
//       }
//     },


//     allowedActions: angular.extend(defaults.allowedActions, {

//     }),
//   });
// }]);

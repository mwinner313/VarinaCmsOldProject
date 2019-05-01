(function () {
    'use strict';

    angular
        .module('app')
        .service('tinyMceOptions', tinyMceOptions);

    tinyMceOptions.$inject = ['$mdDialog'];

    function tinyMceOptions($mdDialog) {
        return {
            directionality: 'rtl',
            language: 'fa_IR',
            font_formats: 'IRANSans=IRANSansUlt;',
            convert_urls: false,
            relative_urls: false,
            remove_script_host: false,
            height: "480",
            plugins: [
                "advlist autolink link image lists charmap preview hr anchor pagebreak",
                "searchreplace wordcount visualblocks visualchars code media nonbreaking",
                "table contextmenu directionality paste textcolor colorpicker imagetools fullscreen"
            ],
            toolbar1: "styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image media preview | forecolor backcolor table | code",
            toolbar2: "fullscreen | removeformat imageBtm",

            setup: function (editor) {
                editor.addButton('imageBtm', {
                    text: 'تصویر',
                    icon: false,
                    onclick: function () {
                        $mdDialog.show($mdDialog.multiImagePicker()).then(function (data) {
                            if (Array.isArray(data)) {
                                var startTag = '<div class="lightgallery">';
                                var endTag = '</div>';
                                var images = '';
                                angular.forEach(data, function (pic) {
                                    images += '<a class="gallery-item-wrapper" href="' + pic.path + '"><img class="gallery-item" alt="' + pic.path + '" src="' + pic.path + '" /></a>'
                                });
                                editor.insertContent(startTag + images + endTag);
                            } else {
                                editor.insertContent('<img class="img-fluid img-responsive" src="' + data.path + '" alt="' + data.path + '"/>')
                            }
                        });
                    }
                });
            }
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('app')
        .filter('fieldPreview', fieldPreview);

    function fieldPreview() {
        return fieldPreviewFilter;

        ////////////////

        function fieldPreviewFilter(field) {
            switch (field.fd.type) {
                case "datetime":
                case "date":
                    return field.rawValue.dateTimeString;
                case "number": return field.rawValue.number;
                case "text": return field.rawValue.text;
                case "email": return field.rawValue.emailAddress;
                case "url": return"<a class='field-preview' target='_blank' href='"+field.rawValue.url+""+"'>"+ " ... "+field.rawValue.url.slice(0,40)+ " </a>";
                case "file": return "<a target='_blank' href='" + field.rawValue.path + "'> " + field.rawValue.name + " </a>";
            }
        }
    }
})();
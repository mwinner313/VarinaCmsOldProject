(function () {
    'use strict';

    angular
        .module('app')
        .filter('orderStatus', orderStatus);

    function orderStatus() {
        return orderStatusFilter;

        function orderStatusFilter(status) {
            switch (status) {
                case 10:
                    return "در انتظار";
                case 20:
                    return "درحال پردازش";
                case 30:
                    return "تکمیل";
                case 40:
                    return "لغو شده";
            }
        }
    }
    angular
        .module('app')
        .filter('orderStatusColor', orderStatusColor);

    function orderStatusColor() {
        return orderStatusColorFilter;

        function orderStatusColorFilter(status) {
            switch (status) {
                case 10:
                    return "yellow";
                case 20:
                    return "blue";
                case 30:
                    return "green";
                case 40:
                    return "red";
            }
        }
    }

    angular
        .module('app')
        .filter('paymentStatus', paymentStatus);

    function paymentStatus() {
        return paymentStatusFilter;

        function paymentStatusFilter(status) {
            switch (status) {
                case 10:
                    return "در انتظار";
                case 20:
                    return "پرداخت شده";
            }
        }
    }

    angular
        .module('app')
        .filter('paymentStatusColor', paymentStatusColor);

    function paymentStatusColor() {
        return paymentStatusColorFilter;

        function paymentStatusColorFilter(status) {
            switch (status) {
                case 10:
                    return "yellow";
                case 20:
                    return "green";
            }
        }
    }

    angular
        .module('app')
        .filter('shippingStatus', shippingStatus);

    function shippingStatus() {
        return shippingStatusFilter;

        function shippingStatusFilter(status) {
            switch (status) {
                case 10:
                    return "به ارسال احتیاجی ندارد";
                case 20:
                    return "هنوز ارسال نشده";
                case 30:
                    return "ارسال شده";
                case 40:
                    return "رسیده به دست مشتری";
            }
        }
    }

    angular
        .module('app')
        .filter('shippingStatusColor', shippingStatusColor);

    function shippingStatusColor() {
        return shippingStatusColorFilter;

        function shippingStatusColorFilter(status) {
            switch (status) {
                case 10:
                    return "teal";
                case 20:
                    return "red";
                case 30:
                    return "blue";
                case 40:
                    return "green";
            }
        }
    }


})();
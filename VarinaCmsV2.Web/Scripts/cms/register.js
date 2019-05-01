(function () {
    $('#register-form')
        .submit(function (e) {
            e.preventDefault();

            var form = $(this);

            form.parsley().validate();

            if (!form.parsley().isValid()) {
                return;
            }

            var data = $(this).serialize();

            $.ajax({
                type: "POST",
                url: '/api/account/signup',
                data: data,
                error: error,
                success: success,
            });

            function success(res) {
                location.reload();
            }

            function error(err) {

            }

        });
}())

; (function () {
    $('#like-btn').click(function (e) {
        var id = $(this).attr('entity-id');
        e.preventDefault();
        $.ajax({
            url: "/entity/like/" + id,
            success: function (res) {
                $('#like-count').text(res.likesCount);
            },
            error: function (err) {
                alert('err');
            }
        });
    });
}());
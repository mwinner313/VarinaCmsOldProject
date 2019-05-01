(function () {
    $('#search-input').change(function(e){
        $('#search-place-holder').attr('href','/search/blog/'+ $('#search-input').val())
    })
}())

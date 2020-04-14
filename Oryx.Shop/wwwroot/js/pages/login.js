$('.message a').click(function () {
    $('form').animate({ height: "toggle", opacity: "toggle" }, "slow");
});

$("#btnLogin").click(function () {
    $.ajax({
        url: '/UserAdmin/Login',
        dataType: 'json',
        type: 'post',
        data: $('form').serialize(),
        success: function (res) {
            if (res.Success) {
                window.location = '/index';
            }
        }
    })
})
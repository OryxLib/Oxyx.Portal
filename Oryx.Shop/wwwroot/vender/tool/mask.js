/*
    <div id="MaskView" style="display:none;">
        <div class="mask-message">
            <div class="animation">
                <div></div>
                <div></div>
                <div></div>
            </div>
        </div>
        <div class="mask"></div>
    </div>
*/


function ShowMask() {
    $("#MaskView").show();
}

function HideMask() {
    $("#MaskView").fadeOut(300);
}

function SuccessMessage(content) {
    $("#success-content").text(content);
    $("#SuccessMessage").fadeIn(350);
    setInterval(function () {
        $("#SuccessMessage").fadeOut(1200);
    }, 3000);
}

function ErrorMessage(content,err) {
    $("#error-content").text(content);
    $("#ErrorMessage").fadeIn(350);
    if (err) {
        console.error(err);
    }
    setInterval(function () {
        $("#ErrorMessage").fadeOut(1200);
    }, 15000);
}

$(document).ready(function () {
    $("#closeErrorMessage").click(function () {
        $("#ErrorMessage").fadeOut(350);
    })

})

function DealWithFile(message, showImg) {
    $("#show-upload-file-info").fadeIn(250);
    var old = $("#show-upload-file-info").children().find("span");
    var oldFile = old.text();
    old.text('[' + new Date().Format("hh:mm:ss") + '] ' + message);
    if (showImg) {
        $("#show-upload-file-info").children().find("img").show();
    } else {
        $("#show-upload-file-info").children().find("img").hide();
    }
    $("#show-upload-file-info").children().eq(0).after('<p>' + oldFile + '</p>');

    while ($("#show-upload-file-info").find("p").length > 5) {
        $("#show-upload-file-info").find("p").last().remove();
    }
    setInterval(function () {
        $("#show-upload-file-info").fadeOut(1200);
    }, 5000);
}



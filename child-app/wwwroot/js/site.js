// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const URL_ID = 'parent-host';
var element = document.getElementById(URL_ID);

const PARENT_URL = element ? element.value : "http://localhost:5000";;

console.log("client ready");

window.enableEditMode = function(param) {
    var title = document.getElementById("page-title");
    var content = document.getElementById("page-content");

    // enable edit mode
    title.setAttribute("contenteditable", "true");
    content.setAttribute("contenteditable", "true");

    console.log("Edit mode enabled");
};


// TODO sync the names name-title

function receiver(e) {
    console.log("message received");
 //   console.log(e);

    if (e.origin == PARENT_URL) {
        switch (e.data) {
            case "enableEditMode":
                window.enableEditMode();
                break;
            case "save":
                var title = document.getElementById("page-title").innerHTML;
                var content = document.getElementById("page-content").innerHTML;
                var previewID = document.getElementById("previewID").value;

                console.log("saving content");
                $.ajax({
                    url: PARENT_URL + '/api/preview/' + previewID,
                    method: 'PUT',
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify({
                        name: title,
                        content: content
                    }),
                }).done(function () {
                    console.log('updated');
                }).fail(function (e) {
                    console.log('failed', e);
                });
                break;
            default:
                console.log("unknown message: ", e.data);
        }
    } else {
        console.log("Wrong origin! " + e.origin);
    }
}

window.addEventListener("message", receiver, false);
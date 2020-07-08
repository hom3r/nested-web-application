// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

const FRAME_ID = "client-iframe";
const CLIENT_URL = "https://localhost:5000/";

function sendMessage(message) {
    var o = document.getElementById(FRAME_ID);
    o.contentWindow.postMessage(message, CLIENT_URL);
    console.log("Message sent:", message);
}

window.sendEnableMessage = function (param) {
    sendMessage("enableEditMode");
}

window.sendSaveMessage = function (token) {
    console.log("token: ", token);
    sendMessage("save");
}
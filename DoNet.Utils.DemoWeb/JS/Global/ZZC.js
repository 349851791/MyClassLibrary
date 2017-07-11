//遮罩层
function closes() {
    $("#Loading").fadeOut("slow", function () {
        $(this).remove();
    });
}

var pc;

$.parser.onComplete = function () {
    if (pc) {
        clearTimeout(pc);
    }
    pc = setTimeout(closes, 500);
}
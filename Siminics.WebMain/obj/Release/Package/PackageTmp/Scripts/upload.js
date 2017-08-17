$(function () {
    if ($("#hidPath").val().length > 0) {
        $("#img_upload").after("<img src='http://img.kudongyi.com" + $("#hidPath").val() + "' width='230' height='230' id='upimg'>");
    }
    if ($("#hidFilePath").length > 0 && $("#hidFilePath").val().length>0) {
        $("#file_upload").after("<span id='upfile'>" + $("#hidFilePath").val() + "</span>");
    }
    $("#img_upload").uploadify({
        height: 20,
        swf: '/Plugs/uploadify/uploadify.swf',
        uploader: 'http://img.kudongyi.com/Handler/Upload.ashx?uptype=0',
        cancelImg: '/Plugs/uploadify/uploadify-cancel.png',
        buttonText: '选择',
        width: 80,
        multi: true,
        onUploadSuccess: function (file, data, response) {
            var json = eval('(' + data + ')');
            //上传成功
            if (json[0].state !== 0) {
                $("#hidPath").val(json[0].url);
                if ($("#upimg").length <= 0) {
                    $("#img_upload").after("<img src='http://img.kudongyi.com" + json[0].url + "' width='230' height='230' id='upimg'>");
                } else {
                    $("#upimg").attr("src", "http://img.kudongyi.com" + json[0].url);
                }
            }
        }
    });
    $("#file_upload").uploadify({
        height: 20,
        swf: '/Plugs/uploadify/uploadify.swf',
        uploader: 'http://img.kudongyi.com/Handler/Upload.ashx?uptype=1',
        cancelImg: '/Plugs/uploadify/uploadify-cancel.png',
        buttonText: '选择',
        width: 80,
        fileSizeLimit: '0',
        multi: true,
        onUploadSuccess: function (file, data, response) {
            var json = eval('(' + data + ')');
            //上传成功
            if (json[0].state !== 0) {
                $("#hidFilePath").val(json[0].url);
                if ($("#upfile").length <= 0) {
                    $("#file_upload").after("<span id='upfile'>" + json[0].url + "</span>");
                } else {
                    $("#upfile").html(json[0].url);
                }
            }
        }
    });
});
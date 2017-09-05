$(function () {
    //if (!$("#hidPath")&&$("#hidPath").val().length > 0) {
    //    $("#img_upload").after("<img src=" + $("#hidPath").val() + "' width='230' height='230' id='upimg'>");
    //}
    if ($("#hidFilePath").length > 0 && $("#hidFilePath").val().length>0) {
        $("#file_upload").after("<span id='upfile'>" + $("#hidFilePath").val() + "</span>");
    }
    $("#img_upload").uploadify({
        height: 20,
        swf: '/Plugs/uploadify/uploadify.swf',
        uploader: '/Handler/Upload.ashx?uptype=0',
        cancelImg: '/Plugs/uploadify/uploadify-cancel.png',
        buttonText: '选择',
        width: 80,
        multi: true,
        onUploadSuccess: function (file, data, response) {
            var json = eval('(' + data + ')');
            //上传成功
            if (json[0].state !== 0) {
                if ($("#hidPath").val().length>0)
                    $("#hidPath").val($("#hidPath").val() + "," + json[0].url);
                else
                    $("#hidPath").val(json[0].url);

                if ($("#upimg").length <= 0) {
                    var img1 = "<img src=" + json[0].url + " width='230' height='230' id='upimg'>";
                    $("#hidImageHtml").val($("#hidImageHtml").val()+img1);
                    $("#img_upload").after(img1);
                } else {
                    var img2 = "<img src=" + json[0].url + " width='230' height='230'>";
                    $("#upimg").after(img2);
                    $("#hidImageHtml").val( $("#hidImageHtml").val()+img2);
                    //$("#upimg").attr("src",json[0].url);
                }
            }
        }
    });

    $("#singleImg_upload").uploadify({
        height: 20,
        swf: '/Plugs/uploadify/uploadify.swf',
        uploader: '/Handler/Upload.ashx?uptype=0',
        cancelImg: '/Plugs/uploadify/uploadify-cancel.png',
        buttonText: '选择',
        width: 80,
        multi: false,
        onUploadSuccess: function (file, data, response) {
            var json = eval('(' + data + ')');
            //上传成功
            if (json[0].state !== 0) {

                $("#hidPath").val(json[0].url);

                var img = "<img src=" + json[0].url + " width='230' height='230' id='upimg'>";
                $("#hidImageHtml").val(img);

                if ($("#upimg").length <= 0) {
                    $("#singleImg_upload").after(img);
                } else {
                    $("#upimg").attr("src",json[0].url);
                }
            }
        }
    });

    $("#anotherImg_upload").uploadify({
        height: 20,
        swf: '/Plugs/uploadify/uploadify.swf',
        uploader: '/Handler/Upload.ashx?uptype=0',
        cancelImg: '/Plugs/uploadify/uploadify-cancel.png',
        buttonText: '选择',
        width: 80,
        multi: true,
        onUploadSuccess: function (file, data, response) {
            var json = eval('(' + data + ')');
            //上传成功
            if (json[0].state !== 0) {
                if ($("#hidAnotherPath").val().length > 0)
                    $("#hidAnotherPath").val($("#hidAnotherPath").val() + "," + json[0].url);
                else
                    $("#hidAnotherPath").val(json[0].url);

                if ($("#upAnotherimg").length <= 0) {
                    var img1 = "<img src=" + json[0].url + " width='230' height='230' id='upAnotherimg'>";
                    $("#hidAnotherImageHtml").val($("#hidAnotherImageHtml").val() + img1);
                    $("#anotherImg_upload").after(img1);
                } else {
                    var img2 = "<img src=" + json[0].url + " width='230' height='230'>";
                    $("#upAnotherimg").after(img2);
                    $("#hidAnotherImageHtml").val($("#hidAnotherImageHtml").val() + img2);
                    //$("#upimg").attr("src",json[0].url);
                }
            }
        }
    });

    $("#file_upload").uploadify({
        height: 20,
        swf: '/Plugs/uploadify/uploadify.swf',
        uploader: '/Handler/Upload.ashx?uptype=1',
        cancelImg: '/Plugs/uploadify/uploadify-cancel.png',
        buttonText: '选择',
        width: 80,
        fileSizeLimit: '0',
        multi: false,
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
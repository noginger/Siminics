
(function ($) {
    $.fn.jUploader = function (options) {
        var defaults = {
            width: 100,
            height: 100,
            fileType: 0, //0:图片；1：文件,2:广告
            url: '/Handler/upload.ashx?dt=' + new Date()
        };
        var options = $.extend(defaults, options);
        var uploader = new $.jUploader(this, options);
        uploader.init();
    };

    $.jUploader = function (obj, options) {
        this.init = function () {
            if (options.fileType == 0)
                obj.append('<div id="divFile" style="display:none"><img src="" alt="" width="' + options.width + '" height="' + options.height + '" /><div><a href="javascript:void(0)"  name="delete">重新上传</a></div></div>');
            else
                obj.append('<div id="divFile" style="display:none"><span>文件路径:</span><a href="" target="_blank"></a><br/><div><a href="javascript:void(0)"  name="delete">重新上传</a></div></div>');
            obj.append('<div id="divUpload" ><form name="uploadForm" action="" method="post" enctype="multipart/form-data"><input type="file" name="upfile" /><span class="uploading" style="display: none;">正在上传，请稍候...</span></form></div>');
            obj.find('div> form >input').bind('change', function () {
                var form = obj.find('form').eq(0);
                form.ajaxSubmit({
                    beforeSubmit: function () {
                        $('.uploading').show();
                    },
                    success: function (data, status) {
                        $('.uploading').hide();
                        if (data.state == 'SUCCESS') {
                            showFile(data);
                            obj.find('input[name=upfile]').val('');
                            var filePathControl = obj.find('input[type=hidden]').eq(0);
                            //var fileSizeControl = obj.find('input[type=hidden]').eq(1);
                            if (filePathControl) filePathControl.val(data.url);
                            //if (fileSizeControl) fileSizeControl.val(data.fileSize);
                        } else {
                            alert(data.state);
                        }
                    },
                    error: function (data, status, e) {
                        $('.uploading').hide();
                        alert(e);
                    },
                    dataType: 'json',
                    url: options.url + '&uptype=' + options.fileType
                });
            });

            var hidPathVal = obj.find('input[type=hidden]').eq(0).val();
            //var hidSizeVal = obj.find('input[type=hidden]').eq(1).val();
            if (hidPathVal)
                showFile({ url: hidPathVal});

            obj.find('a[name=delete]').bind('click', function () {
                showUploadPanel();
            });
        };
        var showFile = function (data) {
            if (options.fileType == 0)
                obj.find('#divFile').find('img').attr('src', data.url);
            else {
                obj.find('#divFile').find('a').eq(0).text(data.url);
                //obj.find('#fileSize').text(setFileSize(data.fileSize));
                obj.find('a').eq(0).attr('href', data.url);
            }

            obj.find('#divUpload').hide();
            obj.find('#divFile').show();
        };

        var showUploadPanel = function () {
            obj.find('#divUpload').show();
            obj.find('#divFile').hide();
        };

        //文件大小转换：默认为KB
        //var setFileSize = function (size) {
        //    var index = 0;
        //    while (size > 1024) {
        //        size /= 1024.0;
        //        index++;
        //        if (index == 3)
        //            break;
        //    }

        //    return Math.round(size * 100) / 100 + sizeArr[index];
        //};

        //var sizeArr = ["KB", "MB", "GB", "TB"];
    };
})(jQuery);


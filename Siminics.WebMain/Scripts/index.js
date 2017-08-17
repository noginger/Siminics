$(document).ready(function () {
    //全选 
    $('[name=chkAll]').bind('click', function () {
        if (($(this).attr('checked')))
            $('[name=chkItem]:enabled').attr('checked', $(this).attr('checked'));
        else
            $('[name=chkItem]').removeAttr('checked');
    });
    //列表移入变色
    $('.tablelist').find('tr').slice(1).hover(function () {
        $(this).addClass("on");
    },
        function () {
            $(this).removeClass("on");
        });

    //$('.tablelist').find('tr').each(function() {
    //    $(this).click(function() {
    //        if ($(this).find('[name=chkItem]').attr('checked'))
    //            $(this).find('[name=chkItem]').removeAttr('checked');
    //        else {
    //            $(this).find('[name=chkItem]').attr('checked', 'checked');
    //        }
    //    });
    //});

    //编辑
    $('.edit').bind('click', function () {
        var items = $('[name=chkItem]:checked');
        if (items.length != 1) {
            alert("请选择一项");
        } else {
            var url = getNoParamUrl();
            window.location.href = url + "/Edit/" + items.eq(0).attr('value');
        }
    });

    //删除单条
    $('[name=linkDelete]').bind('click', function () {
        if (!confirm('确实要' + $(this).text() + '项吗？')) {
            return false;
        }
        var localUrl = escape(encodeURI(window.location.href));
        var url = getNoParamUrl();
        var id = $(this).attr('value');
        window.location.href = url + "/delete/" + id + "?from=" + localUrl;
    });

    //激活
    $("[name=active]").bind('click', function () {
        var localUrl = escape(encodeURI(window.location.href));
        var url = getNoParamUrl();
        var id = $(this).attr('uid');
        window.location.href = url + "/active/" + id + "?from=" + localUrl;
    });

    //批量删除
    $('.delete').bind('click', function () {
        var items = $('[name=chkItem]:checked');
        if (!items || items.length < 1) {
            alert("至少选择一项");
        } else {
            if (confirm('确实要删除选中的项吗？')) {
                var localUrl = encodeURI(window.location.href);
                var url = getNoParamUrl();
                var ids = '';
                $.each(items, function (i) {
                    if (ids != '')
                        ids += ',';
                    ids += $(this).attr('value');
                });
                window.location.href = url + "/delete/" + ids+"?from="+localUrl;
            }
        }
    });

    //初始化密码
    $("[name=linkReset]").bind('click', function () {
        if (confirm("确认要进行重置密码操作吗？")) {
            var localUrl = escape(encodeURI(window.location.href));
            var url = getNoParamUrl();
            var id = $(this).attr('value');
            window.location.href = url + "/Reset/" + id + "?from=" + localUrl;
        }
    });

    //批量审核
    $('.pass').bind('click', function () {
        var items = $('[name=chkItem]:checked');
        var state = $(this).attr("state");
        if (!items || items.length < 1) {
            alert("至少选择一项");
        } else {
            if (confirm('确实选中的项全部通过审核吗？')) {
                var url = getNoParamUrl();
                var ids = '';
                $.each(items, function (i) {
                    if (ids != '')
                        ids += ',';
                    ids += $(this).attr('value');
                });
                window.location.href = url + "/Audit/" + ids + "?state=" + state;
            }
        }
    });

    //审核
    $("[name=linkAudit]").bind('click', function () {
        if (confirm("确认通过审核吗？")) {
            var url = getNoParamUrl();
            var id = $(this).attr("id");
            window.location.href = url + "/Audit/" + id;
        }
    });

    //批量拒绝通过
    $('.refuse').bind('click', function () {
        var items = $('[name=chkItem]:checked');
        var state = $(this).attr("state");
        if (!items || items.length < 1) {
            alert("至少选择一项");
        } else {
            if (confirm('确实选中的项全部审核不通过吗？')) {
                var url = getNoParamUrl();
                var ids = '';
                $.each(items, function (i) {
                    if (ids != '')
                        ids += ',';
                    ids += $(this).attr('value');
                });
                window.location.href = url + "/Audit/" + ids + "?state=" + state;
            }
        }
    });
    //物流收货
    $(".linkReceive").click(function () {
        if (confirm("确认收货吗？")) {
            var _this = $(this);
            var oid = _this.attr("oid");
            var href = _this.attr("href");
            if (oid.length != 15) {
                alert("参数错误！");
                return false;
            }
            _this.removeAttr("href");
            _this.html("正在处理...请等待...");
            $.ajax({
                type: 'post',
                url: '/Logistics/Receive/ConfirmReceipt/' + oid + "?date=" + new Date(),
                success: function (msg) {
                    if (msg == "1") {
                        if (confirm("收货成功！是否需要进行冲红补差填写？")) {
                            window.location.href = "/Logistics/Receive/Create/" + oid;
                        } else {
                            $(_this.parent().parent()).remove();
                            window.location.href = window.location.href;
                        }
                    } else {
                        alert(msg);
                        _this.attr("href", href);
                        _this.html("点击收货");
                    }
                }, error: function () {
                    _this.attr("href", href);
                    _this.html("点击收货");
                    alert("系统正忙，请稍后重试！");
                }
            });
            return false;
        }
    });


    $('.select_condition input').each(function () {
        if ($(this).attr('type') == 'text') {
            if ($(this).val() == '' && $(this).attr('tip') != '')
                $(this).val($(this).attr('tip'));

            $(this).focus(function () {
                if ($(this).val() == $(this).attr('tip'))
                    $(this).val('');
            });

            $(this).blur(function () {
                if ($(this).val() == '') {
                    $(this).val($(this).attr('tip'));
                }
            });
        }
    });

    $(".select_condition input[class*='submit']").live("click", function () {
        var str = "";
        $(".select_condition input[name][type!='checkbox']").each(function () {
            var type = $(this).attr("type");
            if ((type == "text" || type == "hidden") && $(this).val() != $(this).attr('tip')) {
                if (str.length > 0)
                    str += "&";
                str += $(this).attr("name") + "=" + $(this).val();
            }
        });

        $(".select_condition input[name][type=checkbox]:checked").each(function () {
            if (str.length > 0)
                str += "&";
            str += $(this).attr("name") + "=" + $(this).val();
        });

        $(".select_condition select[name]").each(function () {
            if (str.length > 0)
                str += "&";
            str += $(this).attr("name") + "=" + $(this).find("option:selected").val();
        });

        var url = window.location.href;
        if (url.indexOf('?') != -1)
            url = url.substring(0, url.indexOf('?'));

        if (str.length > 0)
            str = "?" + str;

        window.location.href = url + str;
    });
    //输入分页页码跳转
    $(".pageboxa").live("blur", (function () {
        var page = $(".pageboxa").val();
        replacePageNum(page);
    }));

    //获取无参Url
    var getNoParamUrl = function () {
        var url = window.location.href;
        if (url.indexOf('?') != -1)
            url = url.substring(0, url.indexOf('?'));

        url = url.replace('http://', '');

        if (url.split('/').length > 3)
            url = url.substring(0, url.lastIndexOf('/'));

        url = 'http://' + url;

        return url;
    };


    //显示更多信息
    $("[ref='ShowTipMsg']").next().hover(function () {
        $(this).removeClass("hide");
    }, function () {
        $(this).addClass("hide");
    });
    $("[ref='ShowTipMsg']").hover(function () {
        $(this).next().removeClass("hide");
    }, function () {
        $(this).next().addClass("hide");
    });


    //权限下拉
    $("[ref='ShowRoleDown']").live("mouseover", function () {
        var obj = $(this).parent().parent().parent();
        obj.addClass("zindex_top");
        obj.parent().addClass("zindex_up");
        obj.attr("ref", "hideRole");
        $(this).find("img").attr("src", "/Content/images/arrow_up.png");
        $(this).attr("ref", "ShowRoleUp");
    });
    //权限下拉
    $("[ref='ShowRoleUp']").live("click", function () {
        var obj = $(this).parent().parent().parent();
        obj.removeClass("zindex_top");
        obj.parent().removeClass("zindex_up");
        obj.removeAttr("ref", "hideRole");
        $(this).find("img").attr("src", "/Content/images/arrow_down.png");
        $(this).attr("ref", "ShowRoleDown");
    });

    $("[ref='hideRole']").live("mouseleave", function () {
        var obj = $(this);
        obj.find("img").attr("src", "/Content/images/arrow_down.png");
        obj.removeClass("zindex_top");
        obj.parent().removeClass("zindex_up");
        obj.removeAttr("ref", "hideRole");
        obj.find("[ref='ShowRoleUp']").attr("ref", "ShowRoleDown");
    });
});


//分析url 
function parseURL(url) {
    if (typeof (url) == "null" || url == '') url = window.location.href;
    var a = document.createElement('a');
    a.href = url;
    return {
        source: url,
        protocol: a.protocol.replace(':', ''),
        host: a.hostname,
        port: a.port,
        query: a.search,
        params: (function () {
            var ret = {},
            seg = a.search.replace(/^\?/, '').split('&'),
            len = seg.length, i = 0, s;
            for (; i < len; i++) {
                if (!seg[i]) { continue; }
                s = seg[i].split('=');
                ret[s[0]] = s[1];
            }
            return ret;

        })(),
        file: (a.pathname.match(/\/([^\/?#]+)$/i) || [, ''])[1],
        hash: a.hash.replace('#', ''),
        path: a.pathname.replace(/^([^\/])/, '/$1'),
        relative: (a.href.match(/tps?:\/\/[^\/]+(.+)/) || [, ''])[1],
        segments: a.pathname.replace(/^\//, '').split('/')
    };
}
//页面URL参数跳转
function urlRedirect(newParams) {
    var newUrl = replaceUrlParams(null, newParams);
    window.location.href = newUrl;
}

//替换myUrl中的同名参数值 
function replaceUrlParams(myUrl, newParams) {
    if (typeof (myUrl) == 'null' || myUrl == '' || myUrl == null) {
        myUrl = parseURL('');
    }

    for (var x in newParams) {
        var hasInMyUrlParams = false;
        for (var y in myUrl.params) {
            if (x.toLowerCase() == y.toLowerCase()) {
                myUrl.params[y] = newParams[x];
                hasInMyUrlParams = true;
                break;
            }
        }
        //原来没有的参数则追加 
        if (!hasInMyUrlParams) {
            myUrl.params[x] = newParams[x];
        }
    }
    var result = myUrl.protocol + "://" + myUrl.host + ":" + myUrl.port + myUrl.path + "?";

    for (var p in myUrl.params) {
        result += (p + "=" + encodeURIComponent(myUrl.params[p]) + "&");
    }

    if (result.substr(result.length - 1) == "&") {
        result = result.substr(0, result.length - 1);
    }

    if (myUrl.hash != "") {
        result += "#" + myUrl.hash;
    }
    return result;
}
//跳转到该页
function replacePageNum(values) {
    var _url = window.location.href;
    var _query = '';

    if (_url.indexOf('?') >= 0) {
        _url = _url.substring(_url.indexOf('?') + 1);
        var arrQuery = _url.split('&');
        for (var i = 0; i < arrQuery.length; i++) {
            if (arrQuery[i].indexOf('p=') == -1) {
                if (_query.length > 0)
                    _query += '&';
                _query += arrQuery[i];
            }
        }
    }
    window.location.href = window.location.href.split('?')[0] + '?' + _query + '&p=' + values;
}
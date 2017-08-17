
var printKey = 'jsprint_cookies';
var _cookieUrlRequestKey = '_cookieUrlRequestKey';


$(function () {
    
    ////返回上一页
    //$('.crumbs_right>.back_page').click(function () {
    //    //window.history.go(-1);
    //    var currentUrl = '', previousUrl = '';
    //    var strCookie = $.cookie(_cookieUrlRequestKey);
    //    if (strCookie != null && strCookie != '') {
    //        strCookie = strCookie.toLowerCase();
    //        var previousUrlIndex = strCookie.indexOf('&previousurl=');
    //        var currentUrlIndex = strCookie.indexOf('currenturl=');

    //        currentUrl = strCookie.substring(11, previousUrlIndex);
    //        previousUrl = strCookie.substring(previousUrlIndex + 13, strCookie.length);
    //    }
    //    if (previousUrl != '') {
    //        window.location.href = previousUrl;
    //    }

    //});
    
    ////绑定Select值
    //$('select[data]').each(function () {
    //    var _this = $(this);

    //    var _id = _this.attr('IdKey');
    //    if (typeof (_id) != 'string')
    //        _id = 'id';

    //    var _text = _this.attr('TextKey');
    //    if (typeof (_text) != 'string')
    //        _text = 'text';

    //    $.ajax({
    //        type: 'post',
    //        url: _this.attr('data'),
    //        dataType: 'json',
    //        success: function (data) {
    //            $.each(data, function (i) {
    //                _this.append('<option value="' + data[i][_id] + '">' + data[i][_text] + '</option>');
    //            });

    //            _this.val(_this.attr('DefaultVal'));
    //        }
    //    });
    //});
});

var printInsert = function(msgtitle, url, msgcss, callback) {
    var getCookie = $.cookie(printKey);
    if (getCookie != null) {
        $.cookie(printKey, null, { path: '/' });
    }
    var cookieValue = msgtitle + '|' + url + '|' + msgcss;
    $.cookie(printKey, cookieValue, { path: '/', expires: 0.03 });
};
//可以自动关闭的提示
var jsprint = function (msgtitle, url, msgcss, callback) {
    var cssname = "";
    switch (msgcss) {
        case "1":
            cssname = "up_box02";
            break;
        case "0":
            cssname = "up_box03";
            break;
        case "2":
            cssname = "up_box04";
            break;
        default:
            cssname = "up_box02";
            break;
    }
    var str = "<div class=\"" + cssname + "\"><div class=\"lbox\"></div><div class=\"rbox\">" + msgtitle + "</div></div>";
    $("body").append(str);
    $('.' + cssname).show();
    $('.' + cssname).stop(true).animate({ 'top': '40%', 'opacity': '1' }, 300);

    //2秒后清除提示
    setTimeout(function () {
        $.cookie(printKey, null, { path: '/' });  //删除cookie  
        $('.' + cssname).fadeOut(500);
        //如果动画结束则删除节点
        if (!$('.' + cssname).is(":animated")) {
            $('.' + cssname).remove();
        }
    }, 1500);
    
    //执行回调函数
    if (typeof (callback) == "function") callback();

    if (url != '') {
        var urlEvent = urlParse(url);
        var currentUrlEvent = urlParse(window.location.href);

        if (urlEvent.path.toLowerCase() != currentUrlEvent.path.toLowerCase()) {
            $.cookie(printKey, null, { path: '/' });  //删除cookie 
            window.location.href = url;
            return false;
        }
    }
};

//弹出提示层
var createLayer = function (ctrl, content, autoHide) {
    $('.fDiv').remove();
    var fDivId = "fdiv_" + getRandom(9999, 999999);
    //var ctrl$ = $('#' + ctrl);

    var ctrl$ = ctrl;
    if ($.type(ctrl) != 'object') {
        if ($.type(ctrl) == 'string') {
            ctrl$ = $('#' + ctrl);
            if (!ctrl$.attr('id')) {
                ctrl$ = $('[name=' + ctrl + ']');
            }
        }
    }


    var oLeft = ctrl$.offset().left;
    var oTop = ctrl$.offset().top;
    var ctrlW = ctrl$.outerWidth() / 2;

    var html$ = '<div style="position:absolute;float:none; font-size:12px;color:#333; z-index:999; left:' + oLeft + 'px; top:' + oTop + 'px; display:none;" id="' + fDivId + '" class="fDiv">' + '<div style="width:auto; background-color: #FDF2E3; border: 1px solid #DFC9B2; padding:0 8px; float:none; text-align:center; line-height:200%;">' + content + '</div>' + '<div style="background:url(/content/images/p_window_b.gif) no-repeat; font-size:0; height:6px; margin:-1px auto; overflow:hidden; width:9px; position:relative;"></div>' + '</div>';
    $('body').append(html$);
    var layerW = $('#' + fDivId).outerWidth() / 2;
    var layerH = $('#' + fDivId).outerHeight();
    oLeft = oLeft - layerW + ctrlW;
    oTop = oTop - layerH - 10;
    $('#' + fDivId).css('left', oLeft).css('top', oTop);
    $('#' + fDivId).fadeIn(500);

    if (autoHide) {
        setTimeout(function () { $('#' + fDivId).fadeOut(500); }, 2000);
        setTimeout(function () {
            $('#' + fDivId).remove();
        }, 2000);
    }
};

function urlParse(url) {
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
};

function getRandom(under, over) {
    switch (arguments.length) {
        case 1: return parseInt(Math.random() * under + 1);
        case 2: return parseInt(Math.random() * (over - under + 1) + under);
        default:
            return parseInt(Math.random() * (9999 - 100000 + 1) + 100000);
    }
};
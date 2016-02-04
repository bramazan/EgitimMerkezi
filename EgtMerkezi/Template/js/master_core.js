$(document).ready(function () {
    var piksel = document.body.clientHeight - 25;
    $('#page-content').css('min-height', piksel + 'px');

    //Dili Dinamik Olarak Değiştiren Fonksiyon
    $("[lang_key]").each(function (index, el) {
        var key = $(this).attr('lang_key');
        $(this).html(lang_array["tr"][key]);
    });

    //linke göre sol mendeki itemlere active classı veriyor
    $('ul.acc-menu a').each(function (index, el) {
        if ($(this).attr('href') == '@string.Format("http://"+Request.Url.Authority + "/" + ViewContext.RouteData.Values["Controller"] + "/" + ViewContext.RouteData.Values["Action"])') {
            $(this).parent().addClass('active');
            $(this).parent().parent('li').addClass('active');
        }
    });

    tooltip_tetikle(); //bootstrap's tooltip
    node_status_toogle(); // node status toogle

    function create_unique_id() { //unique bir id gerekirse diye 4 haneli random bir deger döndürür
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }

    //Modal Box Fonksiyonu
 $(document).on("click", "[data-toggle~='modal']", function (event) {
        $('#GlobalModalBoxFooterYesNo').show();
        $('#GlobalModalBoxFooterClose').show();
        $('#GlobalModalBoxTitle').html('');
        $('#GlobalModalBoxTitle').html($(this).attr('data-title'));
        $('#GlobalModalBoxBody').html('');
        $('#GlobalModalBoxBody').html($(this).attr('data-text'));
        var obj_id = create_unique_id() + create_unique_id();
        $(this).addClass(obj_id + '');
        var yes_click_function = $(this).attr('data-function') + "('." + obj_id + "','yes');";
        var no_click_function = $(this).attr('data-function') + "('." + obj_id + "','no');";
        var close_click_function = $(this).attr('data-function') + "('." + obj_id + "','close');";
        $('#GlobalModalBoxYes').attr('onclick', yes_click_function);
        $('#GlobalModalBoxNo').attr('onclick', no_click_function);
        $('.GlobalModalBoxClose').attr('onclick', close_click_function);
        var before_function = $(this).attr('data-before-function');
        if(before_function != undefined){
            var fn = window[before_function];
            if(typeof fn === 'function') {
                fn('.'+obj_id);
            }
        }
        var data_source = $(this).attr('data-source');
        if (data_source != undefined) {
            $('#GlobalModalBoxFooterYesNo').hide();
            var serialize = $(this).attr('data-serialize');
            if(serialize != undefined){
                var form = $(this).closest("form");
                var postData = form.serializeArray();
                if(serialize == 'true'){
                    $('#GlobalModalBoxBody').load(data_source,postData);
                }else if(serialize != 'false'){
                    $.ajax({
                        url: serialize,
                        type: 'POST',
                        data: postData,
                        success: function (data, textStatus, jqXHR) {
                            $('#GlobalModalBoxBody').load(data_source);
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log('error');
                        }
                    });
                }
            }else{
                $('#GlobalModalBoxBody').load(data_source);
            }


        } else {
            $('#GlobalModalBoxFooterClose').hide();
        }

        $('#GlobalModalBox').modal({ show: true, backdrop: 'static' });

        var width = $(this).attr('data-width');
        
        if (width == null || width == "" || width == 0)
        {
            width = 6;
        }
        
        var classes = "";
        var mdCLass = "col-md-" + width;
        var offsetClass = "col-md-offset-" + (Math.ceil((12 - width) / 2));
        var staticClass = "modal-content col-sm-8 col-sm-offset-2 col-xs-12";

        classes += staticClass + " " + mdCLass + " " + offsetClass;
        
        $(".modal-content").removeClass().addClass(classes);

        return false;
    });

    try {
        $('.mask').inputmask();
    } catch (e) {
        //console.log("inputmask çalışmadı");
    }


});

function dataTableRowRemove(obj) {
    oTable = $(obj).parent().parent().parent().parent().parent().dataTable();

    var anSelected = oTable.$(obj).parent().parent().parent();

    oTable.fnDeleteRow(anSelected[0]);
    return true;
}

function avantFormSubmit(SubmitButton, UseAjax) {
    CKupdate();
    var form = SubmitButton.closest("form");
    if (form.parsley('validate')) {
        if (!UseAjax) {
            form.submit();
        } else {

            var postData = form.serializeArray();
            var formURL = form.attr("action");
            var postMethod = form.attr("method");
            var ajaxcallback = form.attr("data-ajaxCallBack");

            console.log(postData);

            $.ajax({
                url: formURL,
                type: postMethod,
                data: postData,
                success: function (data, textStatus, jqXHR) {
                    if (ajaxcallback != "") {
                        var fn = window[ajaxcallback];
                        if (typeof fn === 'function') {
                            fn(data);
                        }
                    }
                    console.log('success')
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log('error')
                }
            });
        }
    }

    return false;
}



function CKupdate() {
    try {
        for (instance in CKEDITOR.instances)
            CKEDITOR.instances[instance].updateElement();
    }
    catch (err) {
    }

}

$.fn.exists = function () {
    return this.length !== 0;
}

var delay = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();


function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toGMTString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i].trim();
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}

function IsNumeric(input) {
    return (input - 0) == input && ('' + input).replace(/^\s+|\s+$/g, "").length > 0;
}

function tooltip_tetikle() { //bootstrap's tooltip
    $('.tooltips').tooltip();
    $('.tooltip').tooltip();
    $("[data-toggle~='tooltips']").tooltip();
    $("[data-toggle~='tooltip']").tooltip();
}

function node_status_toogle() { // node status toogle
    $('.node_status_toogle').each(function (index, el) {

        var obj = $(this);

        //yayında
        var yayinda_class = 'btn-success';
        var yayinda_icon = '<i class="fa fa-play"></i>';
        var yayinda_tooltip = 'Yayından Al';
        //yayında_degil
        var yayinda_degil_class = 'btn-default';
        var yayinda_degil_icon = '<i class="fa fa-pause"></i>';
        var yayinda_degil_tooltip = 'Yayınla';

        var durum_suan = $(obj).attr('data-status');
        var data_toogle = $(obj).attr('data-url');

        if (durum_suan == 1) {
            $(obj).removeClass(yayinda_degil_class);
            $(obj).addClass(yayinda_class);
            $(obj).attr('data-original-title', yayinda_tooltip);
            $(obj).html(yayinda_icon);
        } else {
            $(obj).removeClass(yayinda_class);
            $(obj).addClass(yayinda_degil_class);
            $(obj).attr('data-original-title', yayinda_degil_tooltip);
            $(obj).html(yayinda_degil_icon);
        }

        var durum_suan = $(obj).attr('data-status');
        var data_toogle = $(obj).attr('data-url');

        $(this).click(function (event) {
            $.ajax({ //jquery içindeki ajax fonksiyonunu çağırıyoruz
                type: "POST", //ajax kullanırkken kullanacağımız methodu belirliyoruz
                url: data_toogle, //ajax ile hangi dosyaya bağlanmak istediğimizi belirtiyoruz
                success: function (ajaxcevap) { //ajax.php dosyasından gelen cevabı ajaxcevap değişkenine atıyor
                    $(obj).attr('data-status', ajaxcevap);
                    node_status_toogle();
                }
            });
        });

    });

    tooltip_tetikle(); //bootstrap's tooltip
}

function DataTableNodeSil(obj, result) {
    if (result == 'yes') {
        var data_url = $(obj).attr('data-url');
        $.post(data_url, function (data, textStatus, xhr) {
            $.pnotify({
                title: 'Mesaj',
                text: data,
                type: 'info'
            });
            oTable = $(obj).closest('table').dataTable();

            var anSelected = oTable.$(obj).closest('tr')

            oTable.fnDeleteRow(anSelected[0]);
        });
    }
}

function DataTableDestekKapat(obj, result) {
    if (result == 'yes') {
        var data_url = $(obj).attr('data-url');
        $.post(data_url, function (data, textStatus, xhr) {
            $.pnotify({
                title: 'Mesaj',
                text: data,
                type: 'info'
            });
            oTable = $(obj).closest('table').dataTable();

            var anSelected = oTable.$(obj).closest('tr')

            oTable.fnDeleteRow(anSelected[0]);
        });
    }
}

function personelListeHover() {  //Personel Liste foto gösterimi çin tetikleme
    $('.personel_liste_hover').each(function (index, el) {
        var obj = $(this);
        var multi_control = $(obj).attr('multiple');

        if (typeof multi_control !== 'undefined' && multi_control !== false) {
            $(obj).parent().children('.ms-container').children('.ms-selectable').children('.ms-list').addClass('personel_liste_hover_ul');
            $(obj).parent().children('.ms-container').children('.ms-selectable').children('.ms-list').attr('data-popover-placement', 'left');
            $(obj).parent().children('.ms-container').children('.ms-selection').children('.ms-list').addClass('personel_liste_hover_ul');
            $(obj).parent().children('.ms-container').children('.ms-selection').children('.ms-list').attr('data-popover-placement', 'right');
        }

    });
    $('.personel_liste_hover_ul').on({

        mouseleave: function () {
            delay(function () {
                $(this).popover('hide');
            }, 350);
            $(this).children('li').removeClass('ms-hover');
        }

    });

    $('.personel_liste_hover_ul li').on({

        mouseenter: function () {
            var obj = $(this);
            var ul = $(obj).parent();
            var placement = $(ul).attr('data-popover-placement');
            var popover;

            delay(function () {
                var _id = $(obj).attr('id').replace("-selectable", "").replace("-selection", "");
                GlobalAjaxLoader = false;
                $.ajax({ //jquery içindeki ajax fonksiyonunu çağırıyoruz
                    type: "POST", //ajax kullanırkken kullanacağımız methodu belirliyoruz
                    url: SITE_URL + 'personel/foto/' + _id, //ajax ile hangi dosyaya bağlanmak istediğimizi belirtiyoruz
                    success: function (ajaxcevap) { //ajax.php dosyasından gelen cevabı ajaxcevap değişkenine atıyor
                        var img = '<img src="' + ajaxcevap + '" width="50">';
                        popover = ul.popover({ placement: placement, content: img, html: true });
                        popover.popover('show');
                    }
                });
                GlobalAjaxLoader = true;
            }, 100);
        },
        mouseleave: function () {
            var obj = $(this);
            var ul = $(obj).parent();
            ul.popover('hide');
        }

    });
}

var GlobalAjaxLoader = true; //otomatik ajax loadingin çıkmasını istemediğimiz zaman kullanıyoruz
$(document).ajaxStart(function (e) {
    var _target = e.currentTarget.activeElement.type;
    if (_target != 'text' && _target != 'textarea' && GlobalAjaxLoader) {
        $("html").showLoading();
    }
});

$(document).ajaxComplete(function (event, xhr, settings) {
    $("html").hideLoading();
    tooltip_tetikle(); //bootstrap's tooltip
    node_status_toogle(); // node status toogle
});
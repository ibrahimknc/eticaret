var vba = {
    parseFloat: function (n) {
        return parseFloat(n).toFixed(2);
    },
    settings: {

        serverDate: new Date()
    },
    loading: true,
    root: {
        user: null,
        signOut: function () {
            $.post("/api/user/logout", {})
                .done(function (res) {
                    if (res.type == "error") {
                        vba.alert({
                            message: res.message == "" ? "İşlem başarısız" : res.message,
                            classes: 'alert-danger',
                            duration: 5000
                        });
                    }
                    else {
                        vba.alert({
                            message: res.message == "" ? "İşlem Başarılı" : res.message,
                            classes: 'alert-danger'
                        });
                        window.location.href = '/';
                    }
                }).fail(function () {

                    vba.alert({
                        message: res.message == "" ? "İşlem başarısız" : res.message,
                        classes: 'alert-danger',
                        duration: 5000
                    });
                }
                );
        },
        checkLogin: function () {
            if (vba.root.user != null) {
                $.post("/api/user/check", {})
                    .done(function (res) {
                        if (res.type == "error") {
                            if (vba.root.user != null) {
                                window.location.href = window.location.href;
                            }
                        } else {
                            if (vba.root.user == null) {
                                window.location.href = window.location.href;
                            }
                            else {
                                vba.root.user = res.data;
                            }
                        }
                        setTimeout(function () {
                            vba.root.checkLogin();
                        }, 120000);
                    }).fail(function () {
                        window.location.href = window.location.href;
                    });
            }
        },
        changeLoading: function (obj, overlay) {
            if (obj != null) { vba.loading = obj; }
            if (vba.loading) {
                $("#page-preloader").fadeIn(500);
                //$("#page-preloader").show();
            }
            else {
                $("#page-preloader").fadeOut(500);
                //$("#page-preloader").hide();
            }
        },
        scrollTop: function () {
            $('html, body, .ui-page').animate({
                scrollTop: 0
            }, 0);
            return false;
        },
        search: function () {
            if (vba.route.ccnt.items.hasOwnProperty("filterForm") && vba.route.ccnt.funcs.hasOwnProperty("getItems")) {
                vba.route.ccnt.items.filterForm.page = 1;
                vba.route.ccnt.items.filterForm.type = -1;
                vba.route.ccnt.funcs.getItems();
            }
            return false;
        }
    },
    load: function () {
        $("#pages_placeholder [data-repeat]").each(function () {
            var verName = $(this).attr("data-repeat");
            $(this).removeAttr("data-repeat");
            vba.route.ccnt.items["bind" + verName] = $(this)[0].outerHTML;
            $(this).remove();
        });

        var path = vba.route.getPath(window.location.pathname);
        if (path != "") { vba.route.ccnt.name = path; vba.controller[path].load(); }
        else { vba.root.changeLoading(false); }

        setTimeout(function () {
            vba.root.checkLogin();
        }, 120000);

        setInterval(function () {
            vba.settings.serverDate.setMilliseconds(vba.settings.serverDate.getMilliseconds() + 1000);
        }, 1000);

    },
    ready: function () {
        vba.route.load();
    },
    compile: function (tpl) {
        return vba.compileTemp(tpl, [{}]);
    },
    modal: {
        close: function () {
            $('#myModal').modal('hide');
        },
        resetFuncs: function () {
            for (var key in vba.modal.funcs) {
                delete vba.modal.funcs[key];
            }
            for (var key in vba.modal.items) {
                delete vba.modal.items[key];
            }
        },
        name: "",
        html: "",
        items: {},
        funcs: {},
        init: function () {
            if (($("#myModal").data('bs.modal') || {}).isShown) {
                $('#myModal').modal('hide');
            }
            $('#myModal .modal-content').html("");
        },
        bind: function (html) {
            $('#myModal .modal-content').html(html);
            $('#myModal').modal({ show: true });
        }
    },
    modal: {
        close: function () {
            $('#myModal').modal('hide');
        },
        resetFuncs: function () {
            for (var key in vba.modal.funcs) {
                delete vba.modal.funcs[key];
            }
            for (var key in vba.modal.items) {
                delete vba.modal.items[key];
            }
        },
        name: "",
        html: "",
        items: {},
        funcs: {},
        init: function () {
            if (($("#myModal").data('bs.modal') || {}).isShown) {
                $('#myModal').modal('hide');
            }
            $('#myModal .modal-content').html("");
        },
        bind: function (html) {
            $('#myModal .modal-content').html(html);
            $('#myModal').modal({ show: true });
        }
    },
    route: {
        getPath: function (pathName) {
            if (pathName.indexOf('?') > -1) {
                pathName = pathName.substr(0, pathName.indexOf('?'));
            }
            if (vba.controller.hasOwnProperty(pathName))
                return pathName;
            else if (vba.controller.hasOwnProperty(pathName + "/"))
                return pathName + "/";
            else return "";
        },
        getController: function (href, hrefp) {
            var r = Math.random();
            if (vba.controller.hasOwnProperty(hrefp)) {
                window.history.pushState({ a: r }, document.title, href);
                vba.route.changeRoute(hrefp);
            }
            else {
                window.history.pushState({ a: r }, document.title, "/");
                vba.route.changeRoute("/");
            }
        },
        //current controller
        ccnt: {
            name: "", intervals: [], timeOuts: [], items: {}, funcs: {}
        },
        changeRoute: function (controller) {
            vba.root.changeLoading(true);
            $(window).unbind('scroll');
            if (vba.route.ccnt.name != "") {
                var i = vba.route.ccnt.intervals.length;
                while (i--) {
                    window.clearInterval(vba.route.ccnt.intervals[i]);
                    vba.route.ccnt.intervals.splice(i, 1);
                }
                for (var key in vba.route.ccnt.timeOuts) {
                    try { window.clearTimeout(vba.route.ccnt.timeOuts[key]); } catch (e) { }
                    delete vba.route.ccnt.items[key];
                }
            }
            for (var key in vba.route.ccnt.items) {
                delete vba.route.ccnt.items[key];
            }
            for (var key in vba.route.ccnt.funcs) {
                delete vba.route.ccnt.funcs[key];
            }
            vba.route.ccnt.name = controller;

            $("#pages_placeholder").load("/ajax" + window.location.pathname + window.location.search, function (responseTxt, statusTxt, xhr) {
                if (statusTxt == "success") {
                    $("#pages_placeholder [data-repeat]").each(function () {
                        var verName = $(this).attr("data-repeat");
                        $(this).removeAttr("data-repeat");
                        vba.route.ccnt.items["bind" + verName] = $(this)[0].outerHTML;
                        $(this).remove();
                    });
                    $("#pages_placeholder").html(vba.compile($("#pages_placeholder").html()));
                    vba.controller[controller].load();
                    $("html").scrollTop(0);
                }
                else {
                    window.location.href = window.location.href;
                }
            });

        },
        load: function () {
            $("a:not(.redirect)").click(function () {
                var href = $(this).attr('href');
                if (href && href.length && href[0] == '/' && href[1] != '#' && href[0] != '#' && href.indexOf(".") == -1) {
                    var hrefp = vba.route.getPath(href);
                    if (hrefp != "" && vba.controller.hasOwnProperty(hrefp)) {
                        vba.route.getController(href, hrefp);
                        return false;
                    }
                }
            });
            $(document).on('click', '#pages_placeholder a:not(.redirect)', function () {
                var href = $(this).attr('href');
                if (href && href.length && href[0] == '/' && href[1] != '#' && href[0] != '#' && href.indexOf(".") == -1) {
                    var hrefp = vba.route.getPath(href);
                    if (hrefp != "" && vba.controller.hasOwnProperty(hrefp)) {
                        vba.route.getController(href, hrefp);
                        return false;
                    }

                }
            });
        }
    },
    /*-----------------------CONTROLLER İŞLEMLERİ----------------------------*/
    controller: {
        "/": {
            load: function () {

                vba.root.changeLoading(false);
            }
        },
        "/contact": {
            load: function () {
                vba.root.changeLoading(false);
            }
        },
        "/login": {
            load: function () {
                vba.route.ccnt.funcs.updateItem = function (form) {
                    if (vba.root.user == null) {
                        vba.root.changeLoading(true);
                        var elem = vba.serializeForm(form);
                        console.log(elem);
                        $.post("/api/user/login", {
                            email: elem.email,
                            password: elem.password
                        },
                            function (res) {
                                if (res.type == "error") {
                                    vba.alert({
                                        message: res.message == "" ? "İşlem başarısız" : res.message,
                                        classes: 'alert-danger'  
                                    });
                                }
                                else {
                                    vba.alert({
                                        message: res.message == "" ? "İşlem Başarılı" : res.message,
                                        classes: 'alert-success'
                                    });
                                    window.location.href = '/';
                                }
                                vba.root.changeLoading(false);
                            }).fail(function () {
                                vba.alert({
                                    message: "İşlem başarısız",
                                    classes: 'alert-danger'
                                });
                                vba.root.changeLoading(false);
                            });
                    } else {
                        vba.alert({
                            message: "İşlem başarısız",
                            classes: 'alert-danger'
                        });
                        vba.root.changeLoading(false);
                    } 
                } 
                vba.root.changeLoading(false);
            }
        },
        "/register": {
            load: function () {
                vba.route.ccnt.funcs.updateItem = function (form) {
                    vba.root.changeLoading(true);
                    $.post("/api/user/register", $(form).serialize()
                    ).done(function (res) {
                        if (res.type == "error") {
                            vba.alert({
                                message: res.message == '' ? "İşlem Başarısız" : res.message,
                                classes: 'alert-danger'
                            });
                            vba.root.checkLogin();
                        } else {
                            vba.alert({
                                message: res.message == '' ? "İşlem Başarılı" : res.message,
                                classes: "alert-success"
                            });
                            vba.route.getController("/", "/");
                        }
                    }).fail(function () {
                        vba.alert({
                            message: "İşlem başarısız",
                            classes: 'alert-danger'
                        });
                    });
                }
                vba.root.changeLoading(false);
            }
        },
        "/update": {
            load: function () {
                vba.route.ccnt.funcs.updateItem = function (form) {
                    var frm = vba.serializeForm(form);
                    vba.root.changeLoading(true);
                    $.post("/api/default/updateSettings", frm
                    ).done(function (res) {
                        if (res.type == "error") {
                            vba.alert({
                                message: res.message == '' ? "İşlem başarısız" : res.message,
                                classes: 'alert-danger',
                                duration: 5000
                            });
                            vba.root.checkLogin();
                        } else {
                            vba.alert({
                                message: "İşlem Başarılı",
                                classes: "alert-success",
                                duration: 5000
                            });
                        }
                    }).fail(function () {
                        vba.alert({
                            message: "İşlem başarısız",
                            classes: 'alert-danger',
                            duration: 5000
                        });
                        vba.root.checkLogin();
                        vba.root.changeLoading(false);
                    });

                }
                vba.root.changeLoading(false);
            }
        },
        "/data/": {
            load: function () {
                vba.route.ccnt.items.filterForm = {};
                vba.route.ccnt.items.filterForm.page = 1;
                vba.route.ccnt.items.filterForm.itemsPerPage = 30;
                vba.route.ccnt.items.filterForm.totalItems = 0;
                vba.route.ccnt.items.filterForm.type = -1;

                vba.route.ccnt.funcs.getItems = function () {
                    $("#pages_placeholder .tableContainer").html("");
                    vba.route.ccnt.items.dataLoading = true;
                    var post = {
                        type: vba.route.ccnt.items.filterForm.type,
                        search: $("#searchForm input[type='text']").val(),
                        page: vba.route.ccnt.items.filterForm.page,
                        itemsPerPage: vba.route.ccnt.items.filterForm.itemsPerPage
                    };
                    $.post("/api/default/getUsers/", post).done(function (res) {
                        if (res.type == "error") {
                            vba.alert({
                                message: res.message == '' ? "İşlem başarısız" : res.message,
                                classes: 'alert-danger',
                                duration: 5000
                            });
                            vba.root.checkLogin();
                        }
                        else {
                            if (res.data.length > 0) {
                                var tmpl = "binditems";
                                $("#pages_placeholder .tableContainer").append(vba.compileTemp(vba.route.ccnt.items[tmpl], res.data));
                                if (res.data.length == 0) {
                                    $(".tableContainer tbody").append("<tr><td colspan=\"" + $("#pages_placeholder .tableContainer thead th").length + "\">" + vba.compileTemp(vba.noDataHtml, [{}]) + "</td></td>");
                                }

                                vba.route.ccnt.items.filterForm.totalItems = res.c;

                                $(".tabletfoot .totalItems").text(vba.route.ccnt.items.filterForm.totalItems);
                                vba.route.ccnt.items.dataLoading = false;
                                vba.pagination();
                            }
                            else {
                                $(".tabletfoot .totalItems").html("<b> Kayıt bulunmamaktadır.</b>");
                                vba.route.ccnt.items.dataLoading = false;
                                vba.root.changeLoading(false);
                            }
                        }

                        vba.route.ccnt.items.dataLoading = false;
                    }).fail(function () {
                        vba.route.ccnt.items.dataLoading = false;
                        $("#pages_placeholder .tableContainer").html("");
                        vba.root.checkLogin();
                    });
                }
                vba.route.ccnt.funcs.getItems();
                vba.root.changeLoading(false);
            }
        }

    },
    alert: function (obj) {
        if (obj.classes == "alert-success") {
            alertify.success(obj.message)
        }
        else if (obj.classes == "alert-danger") {
            alertify.error(obj.message)
        }
        else if (obj.classes == "alert-info") {
            alertify.info(obj.message)
        }
    },
    getData: function (str) {
        return JSON.parse(decodeURIComponent(str));
    },
    writeData: function (obj) {
        return encodeURIComponent(JSON.stringify(obj)).split("\"").join("\\\"");
    },
    compileTemp: function (html, obj) {
        var tmpl = html.split(/\{\{(.*?)\}\}/g);
        var send = obj.map(function (o) {
            return tmpl.map(vba.render(o)).join('');
        });

        return send;
    },
    render: function (item) { return function (tok, i) { if (i % 2 && tok.length) { var val = ""; try { tok = tok.split("&gt;").join(">"); val = eval(tok); } catch (e) { } return val; } else { return tok; }; }; },
    numbersOnly: function (e) {
        var inputValue = e.value;
        var transformedInput = inputValue.replace(/[^0-9\.\-]+/g, '');
        if (transformedInput != inputValue) {
            e.value = transformedInput;
        }
        return false;
    },
    toUpper: function (e) {
        var inputValue = e.value.replace('i', 'İ');
        var transformedInput = inputValue.toUpperCase();
        e.value = transformedInput;
        return false;
    },
    toLower: function (e) {
        var inputValue = e.value.replace('I', 'ı');
        var transformedInput = inputValue.toLowerCase();
        e.value = transformedInput;
        return false;
    },
    pagination: function () {
        $(".tabletfoot .pagination").hide();
        $(".tabletfoot .pagination .nav-page").remove();

        if (vba.route.ccnt.items.filterForm.totalItems > 0) {
            var totalPage = 1;
            if (vba.route.ccnt.items.filterForm.totalItems % vba.route.ccnt.items.filterForm.itemsPerPage == 0) {
                totalPage = vba.route.ccnt.items.filterForm.totalItems / vba.route.ccnt.items.filterForm.itemsPerPage;
            }
            else {
                totalPage = parseInt(vba.route.ccnt.items.filterForm.totalItems / vba.route.ccnt.items.filterForm.itemsPerPage) + 1;
            }
            vba.route.ccnt.items.filterForm.totalPage = totalPage;
            var startpage = 1;
            var endpage = totalPage;
            if (totalPage > 6) {
                startpage = vba.route.ccnt.items.filterForm.page - 2;
                endpage = vba.route.ccnt.items.filterForm.page + 2;
                if (startpage < 1) {
                    startpage = 1;
                    endpage += 3;
                }
                if (endpage > totalPage) {
                    endpage = totalPage;
                    startpage -= 3;
                }
            }
            var navHTML = "";
            for (var i = startpage; i <= endpage; i++) {
                navHTML += "<li class=\"nav-page" + (vba.route.ccnt.items.filterForm.page == i ? " active" : "") + "\"><a href=\"javascript:;\" onclick=\"vba.route.ccnt.items.filterForm.page = " + i + "; vba.route.ccnt.funcs.getItems(); return false;\" class=\"page-link\"><span>" + i + "</span></a></li>";
            }
            $(".tabletfoot .pagination li:eq(1)").after(navHTML);

            $(".tabletfoot .pagination").show();
        }
        else {
            vba.route.ccnt.items.filterForm.page = 1;
            vba.route.ccnt.items.filterForm.totalPage = 1;
        }

    },
    serializeForm: function (form) {
        var serialized = $(form).serializeArray();
        var data = {};
        for (var i = 0; i < serialized.length; i++) { var item = serialized[i]; data[item.name] = item.value; }
        $(form).find("input[type='checkbox']").each(function (ix, it) {
            data[$(it).attr("name")] = $(it)[0].checked;
        });
        return data;
    },
    cookie: {
        getCookie: function (c_name) {
            var i, x, y, ARRcookies = document.cookie.split(";");
            for (i = 0; i < ARRcookies.length; i++) {
                x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
                y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
                x = x.replace(/^\s+|\s+$/g, "");
                if (x == c_name) {
                    return unescape(y);
                }
            }
        },
        setCookie: function (c_name, value, exhour) {
            var exdate = new Date();
            exdate.setHours(exdate.getHours() + exhour);
            var c_value = escape(value) + ((exhour == null) ? "" : "; expires=" + exdate.toUTCString());
            document.cookie = c_name + "=" + c_value + "; path=/";
        }
    }
};

for (var i = 0; i < binderObj.length; i++) {
    binderObj[i]();
    binderObj.splice(i, 1);
    i--;
}

$(window).on('popstate', function (event) {
    var path = vba.route.getPath(window.location.pathname);
    if (path != "") { vba.route.changeRoute(path); }
    else {
        window.location.reload(1);
    }
});
vba.load();
$(document).ready(function () {
    vba.ready();
});


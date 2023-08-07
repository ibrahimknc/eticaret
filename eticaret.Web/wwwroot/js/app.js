var vba = {
    parseFloat: function (n) {
        return parseFloat(n).toFixed(2);
    },
    settings: {

        serverDate: new Date(),
        staticID: ""
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
            if (!window.location.pathname.includes("categories")) {
                vba.route.getController("/categories", "/");
            }

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
    route: {
        getPath: function (pathName) {
            //Path id query gizleme -------- //
            if (pathName.includes("favorites")) { vba.settings.staticID = pathName.split("favorites/")[1]; pathName = "/user/favorites"; }
            if (pathName.includes("profile")) { vba.settings.staticID = pathName.split("profile/")[1]; pathName = "/user/profile"; }
            if (pathName.includes("categories")) { vba.settings.staticID = pathName.split("categories/")[1]; pathName = "/categories"; }
            if (pathName.includes("products")) { vba.settings.staticID = pathName.split("products/")[1]; pathName = "/products"; }

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
                $.getScript("/js/main.js", function () { }); //Template CSS

                vba.route.ccnt.funcs.updateUserFavorite = function (elem) {
                    vba.root.changeLoading(true);
                    var post = {
                        productID: elem,
                        favoriteID: "00000000-0000-0000-0000-000000000000"
                    };
                    $.post("/api/user/updateUserFavorite", post).done(function (res) {
                        if (res.type == "error") {
                            vba.alert({
                                message: res.message == '' ? "İşlem Başarısız" : res.message,
                                classes: 'alert-danger'
                            });
                        } else {
                            vba.alert({
                                message: res.message == '' ? "İşlem Başarılı" : res.message,
                                classes: "alert-success"
                            });
                        }
                    }).fail(function () {
                        vba.alert({
                            message: "İşlem başarısız",
                            classes: 'alert-danger'
                        });
                    });
                    vba.root.changeLoading(false);
                }

                vba.root.changeLoading(false);
            }
        },
        "/contact": {
            load: function () {
                vba.root.changeLoading(false);
            }
        },
        "/aboutus": {
            load: function () {
                var titlebody = document.getElementById("titlebody");
                if (titlebody) {
                    titlebody.innerText = vba.settings.title;
                }
                vba.root.changeLoading(false);
            }
        },
        "/user/profile": {
            load: function () {
                vba.route.ccnt.funcs.updateItemProfile = function (form) {
                    $.post("/api/user/updateUser", $(form).serialize()
                    ).done(function (res) {
                        if (res.type == "error") {
                            vba.alert({
                                message: res.message == '' ? "İşlem Başarısız" : res.message,
                                classes: 'alert-danger'
                            });
                            vba.root.changeLoading(false);
                        } else {
                            vba.alert({
                                message: res.message == '' ? "İşlem Başarılı" : res.message,
                                classes: "alert-success"
                            });
                            vba.route.ccnt.funcs.getItems();
                        }
                    }).fail(function () {
                        vba.alert({
                            message: "İşlem başarısız",
                            classes: 'alert-danger'
                        });
                    });
                }
                vba.route.ccnt.funcs.updateItemProfilePass = function (form) {
                    var objForm = $(form).serializeArray();
                    var formObject = {};
                    objForm.forEach(function (item) {
                        formObject[item.name] = item.value;
                    });

                    if (formObject.password != null && formObject.newPassword != null && formObject.newPasswordRepeat != null) {
                        if (objForm.newPassword == objForm.newPasswordRepeat) {
                            $.post("/api/user/updateUserPassword", objForm
                            ).done(function (res) {
                                if (res.type == "error") {
                                    vba.alert({
                                        message: res.message == '' ? "İşlem Başarısız" : res.message,
                                        classes: 'alert-danger'
                                    });
                                    vba.root.changeLoading(false);
                                } else {
                                    vba.alert({
                                        message: res.message == '' ? "İşlem Başarılı" : res.message,
                                        classes: "alert-success"
                                    });
                                    $(form).trigger("reset");
                                    vba.route.ccnt.funcs.getItems();
                                }
                            }).fail(function () {
                                vba.alert({
                                    message: "İşlem başarısız",
                                    classes: 'alert-danger'
                                });
                            });
                        }
                        else {
                            vba.alert({
                                message: "Lütfen yeni şifreleri aynı giriniz.",
                                classes: 'alert-danger'
                            });
                        }
                    }
                    else {
                        vba.alert({
                            message: "Lütfen hiç boş bırakmayınız.",
                            classes: 'alert-danger'
                        });
                    }
                }
                vba.route.ccnt.funcs.getItems = function () {
                    vba.root.changeLoading(true);
                    $.post("/api/user/getUserProfile", {}).done(function (res) {
                        if (res.type == "error") {
                            vba.alert({
                                message: res.message == '' ? "İşlem başarısız" : res.message,
                                classes: 'alert-danger',
                                duration: 5000
                            });
                            vba.root.checkLogin();
                        }
                        else if (res.type == "inActive") {
                            vba.route.getController("/", "/");
                        }
                        else {
                            if (res.data.id !== null) {
                                $(".profile-firstName-lastName").text(res.data.firstName + " " + res.data.lastName);
                                $(".profileMail").html(res.data.email);
                                $('#profileForm input[name="firstName"]').val(res.data.firstName);
                                $('#profileForm input[name="lastName"]').val(res.data.lastName);
                                $('#profileForm input[name="email"]').val(res.data.email);
                                $('#profileForm input[name="phone"]').val(res.data.phone);
                                $('#profileForm textarea[name="address"]').val(res.data.address);

                            } else {
                                $("#pages_placeholder ").html(" <div class='col-lg-12 mb-3'> <div class='row g-0 bg-light py-4'><div class='col-md-12 d-flex align-items-center justify-content-center'> 😔 Üzgünüm, talebinizle eşleşen birşey bulamadık 😔</div></div></div>").hide();
                                $("#pages_placeholder ").fadeIn(500);
                                vba.route.getController("/", "/");
                            }
                        }
                    }).fail(function () {
                        vba.route.ccnt.items.dataLoading = false;
                        $("#pages_placeholder .dataContainer").html(" <div class='col-lg-12 mb-3'> <div class='row g-0 bg-light py-4'><div class='col-md-12 d-flex align-items-center justify-content-center'> 😔 Üzgünüm, talebinizle eşleşen birşey bulamadık 😔</div></div></div>");
                    });
                    vba.root.changeLoading(false);
                }
                vba.route.ccnt.funcs.getItems();
            }
        },
        "/user/favorites": {
            load: function () {
                vba.route.ccnt.items.filterForm = {};
                vba.route.ccnt.items.filterForm.id = vba.settings.staticID;
                vba.route.ccnt.items.filterForm.page = 1;
                vba.route.ccnt.items.filterForm.itemsPerPage = 3;
                vba.route.ccnt.items.filterForm.totalItems = 0;
                vba.route.ccnt.items.filterForm.listSorting = 0;
                vba.route.ccnt.funcs.updateUserFavorite = function (elem) {

                    var confirmDialog = alertify.confirm();
                    confirmDialog.setting('labels', { ok: "Evet", cancel: "Hayır" });
                    confirmDialog.setting('transition', 'zoom');
                    confirmDialog.setting('movable', true);
                    confirmDialog.setting('closable', false);
                    confirmDialog.setting('pinnable', true);
                    confirmDialog.setting('title', "İşlem Onay Penceresi");
                    confirmDialog.setting('message', "<i class=\"fa fa-exclamation-triangle text-danger\" aria-hidden=\"true\"></i> Favorilerden Çıkarmak İstediğinizden Eminmisiniz?");
                    confirmDialog.show();
                    confirmDialog.set('onok', function () {
                        vba.root.changeLoading(true);
                        var post = {
                            productID: "00000000-0000-0000-0000-000000000000",
                            favoriteID: elem
                        };
                        $.post("/api/user/updateUserFavorite", post)
                            .done(function (res) {
                                if (res.type == "error") {
                                    vba.alert({
                                        message: res.message == '' ? "İşlem Başarısız" : res.message,
                                        classes: 'alert-danger'
                                    });
                                    vba.root.changeLoading(false);
                                } else {
                                    vba.alert({
                                        message: res.message == '' ? "İşlem Başarılı" : res.message,
                                        classes: "alert-success"
                                    });
                                    vba.route.ccnt.funcs.getItems();
                                }
                            })
                            .fail(function () {
                                vba.alert({
                                    message: "İşlem başarısız",
                                    classes: 'alert-danger'
                                });
                                vba.root.changeLoading(false);
                            });
                    }).set('oncancel', function () {
                        vba.alert({
                            message: "Favori Çıkarmaktan Vazgeçildi.",
                            classes: 'alert-danger'
                        });
                    });
                }
                vba.route.ccnt.funcs.getItems = function () {
                    vba.root.changeLoading(true);
                    $("#pages_placeholder .dataContainer").html("");
                    vba.route.ccnt.items.dataLoading = true;

                    var post = {
                        listSorting: vba.route.ccnt.items.filterForm.listSorting,
                        search: $("#searchForm input[type='text']").val(),
                        page: vba.route.ccnt.items.filterForm.page,
                        itemsPerPage: vba.route.ccnt.items.filterForm.itemsPerPage,
                        userID: vba.route.ccnt.items.filterForm.id
                    };

                    $.post("/api/user/getUserFavorite", post).done(function (res) {
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
                                $("#pages_placeholder .dataContainer").append(vba.compileTemp(vba.route.ccnt.items[tmpl], res.data));

                            } else {
                                $("#pages_placeholder .dataContainer").html(" <div class='col-lg-12 mb-3'> <div class='row g-0 bg-light py-4'><div class='col-md-12 d-flex align-items-center justify-content-center'> 😔 Üzgünüm, talebinizle eşleşen birşey bulamadık 😔</div></div></div>").hide();
                                $("#pages_placeholder .dataContainer").fadeIn(500);
                            }

                            $(".total-products").html("Toplam <b><u>" + res.c + "</u></b> Ürün Var");
                            $(".showingTotalProducts").html("Toplam <b><u>" + res.c + "</u></b> Üründen <b><u>" + res.data.length + "</u></b> Adet Gösteriliyor");
                            vba.route.ccnt.items.filterForm.totalItems = res.c;
                            vba.pagination();
                        }
                        vba.route.ccnt.items.dataLoading = false;
                        vba.root.changeLoading(false);
                    }).fail(function () {
                        vba.route.ccnt.items.dataLoading = false;
                        $("#pages_placeholder .dataContainer").html("");
                    });
                    vba.root.changeLoading(false);
                }
                vba.route.ccnt.funcs.getItems();

                //----------------Filter-------------------// 
                $('#listSorting').change(function () {
                    vba.route.ccnt.items.filterForm.listSorting = this.value;
                    vba.route.ccnt.items.filterForm.page = 1;
                    vba.route.ccnt.funcs.getItems();
                });
                //----------------end Filter-------------------//

                vba.root.changeLoading(false);
            }
        },
        "/user/login": {
            load: function () {
                vba.route.ccnt.funcs.updateItem = function (form) {
                    if (vba.root.user == null) {
                        vba.root.changeLoading(true);
                        var elem = vba.serializeForm(form);
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
                                    vba.root.changeLoading(false);
                                }
                                else {
                                    vba.alert({
                                        message: res.message == "" ? "İşlem Başarılı" : res.message,
                                        classes: 'alert-success'
                                    });
                                    window.location.href = '/';
                                }

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
        "/user/register": {
            load: function () {
                vba.route.ccnt.funcs.updateItem = function (form) {
                    $.post("/api/user/register", $(form).serialize()
                    ).done(function (res) {
                        if (res.type == "error") {
                            vba.alert({
                                message: res.message == '' ? "İşlem Başarısız" : res.message,
                                classes: 'alert-danger'
                            });
                            vba.root.changeLoading(false);
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
                        vba.root.changeLoading(false);
                    });
                }
                vba.root.changeLoading(false);
            }
        },
        "/categories": {
            load: function () {
                vba.route.ccnt.items.filterForm = {};
                vba.route.ccnt.items.filterForm.id = vba.settings.staticID;
                vba.route.ccnt.items.filterForm.page = 1;
                vba.route.ccnt.items.filterForm.itemsPerPage = 5;
                vba.route.ccnt.items.filterForm.totalItems = 0;
                vba.route.ccnt.items.filterForm.price = "";
                vba.route.ccnt.items.filterForm.listSorting = 0;
                vba.route.ccnt.items.filterForm.defaultProductsGrid = "";

                vba.route.ccnt.funcs.getItems = function () {
                    vba.root.changeLoading(true);
                    $("#pages_placeholder .dataContainer").html("");
                    vba.route.ccnt.items.dataLoading = true;

                    //var url = new URLSearchParams(window.location.search);
                    //var cid = url.get("id");
                    //vba.route.ccnt.items.filterForm.id = cid;

                    var post = {
                        price: vba.route.ccnt.items.filterForm.price,
                        listSorting: vba.route.ccnt.items.filterForm.listSorting,
                        search: $("#searchForm input[type='text']").val(),
                        page: vba.route.ccnt.items.filterForm.page,
                        itemsPerPage: vba.route.ccnt.items.filterForm.itemsPerPage,
                        id: vba.route.ccnt.items.filterForm.id
                    };

                    $.post("/api/categories/getCategoriList", post).done(function (res) {
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
                                $("#pages_placeholder .dataContainer").append(vba.compileTemp(vba.route.ccnt.items[tmpl], res.data));

                                $.each(res.tags, function (index, item) {
                                    $("#tagsContainer").append("<li><a href=\"#\" title=\"" + item.split(",")[0] + "\">" + item.split(",")[0] + "</a></li> ")
                                });

                            } else {
                                $("#pages_placeholder .dataContainer").html(" <div class='col-lg-12 mb-3'> <div class='row g-0 bg-light py-4'><div class='col-md-12 d-flex align-items-center justify-content-center'> 😔 Üzgünüm, talebinizle eşleşen birşey bulamadık 😔</div></div></div>").hide();
                                $("#pages_placeholder .dataContainer").fadeIn(500);
                            }
                            $(".activePageName").text(res.name);
                            $(".total-products").html("Toplam <b><u>" + res.c + "</u></b> Ürün Var");
                            $(".showingTotalProducts").html("Toplam <b><u>" + res.c + "</u></b> Üründen <b><u>" + res.data.length + "</u></b> Adet Gösteriliyor");
                            vba.route.ccnt.items.filterForm.totalItems = res.c;
                            vba.pagination();
                        }
                        vba.route.ccnt.items.dataLoading = false;
                        vba.root.changeLoading(false);
                    }).fail(function () {
                        vba.route.ccnt.items.dataLoading = false;
                        $("#pages_placeholder .dataContainer").html("");
                    });
                    vba.root.changeLoading(false);
                }
                vba.route.ccnt.funcs.getItems();
                vba.route.ccnt.funcs.updateUserFavorite = function (elem) {
                    vba.root.changeLoading(true);
                    var post = {
                        productID: elem,
                        favoriteID: "00000000-0000-0000-0000-000000000000"
                    };
                    $.post("/api/user/updateUserFavorite", post).done(function (res) {
                        if (res.type == "error") {
                            vba.alert({
                                message: res.message == '' ? "İşlem Başarısız" : res.message,
                                classes: 'alert-danger'
                            });
                        } else {
                            vba.alert({
                                message: res.message == '' ? "İşlem Başarılı" : res.message,
                                classes: "alert-success"
                            });
                        }
                    }).fail(function () {
                        vba.alert({
                            message: "İşlem başarısız",
                            classes: 'alert-danger'
                        });
                    });
                    vba.root.changeLoading(false);
                }

                //----------------Filter-------------------//
                vba.route.ccnt.funcs.filterOperations = function () {
                    vba.route.ccnt.items.filterForm.price = $("#price-filter").val();
                    vba.route.ccnt.funcs.getItems();
                }
                vba.route.ccnt.funcs.getProductsGrid = function () {
                    if (vba.route.ccnt.items.filterForm.defaultProductsGrid != "") {
                        $("#gridProductButton").addClass("active");
                        $("#listProductButton").removeClass("active");
                        $("#products-grid").html(vba.route.ccnt.items.filterForm.defaultProductsGrid);
                    }
                }
                vba.route.ccnt.funcs.getProductsList = function () {
                    $("#gridProductButton").removeClass("active");
                    $("#listProductButton").addClass("active");
                    vba.route.ccnt.items.filterForm.defaultProductsGrid = $("#products-grid").html();
                    var data = $(".dataContainer").html();
                    $(".dataContainer").removeClass("row");
                    $(".products-block").addClass("layout-5");
                    $(".dataContainer").html("<div class=\"product-item\"> <div class=\"row\">" + data + "</div> </div>");
                }
                $('#listSorting').change(function () {
                    vba.route.ccnt.items.filterForm.listSorting = this.value;
                    vba.route.ccnt.items.filterForm.page = 1;
                    vba.route.ccnt.funcs.getItems();
                });
                //----------------end Filter-------------------//

                $.getScript("/js/main.js", function () { }); //Template CSS
                vba.root.changeLoading(false);
            }
        },
        "/products": {
            load: function () {
                vba.route.ccnt.items.filterForm = {};
                vba.route.ccnt.items.filterForm.id = vba.settings.staticID;
                vba.route.ccnt.funcs.shareProduct = function (elem) {
                    vba.modal.init();
                    if (vba.modal.name != "recommendsInfo") {
                        vba.modal.resetFuncs();
                        vba.modal.funcs.load = function () {
                            var html = vba.compileTemp(vba.modal.html, [{}]);
                            vba.modal.bind(html);
                        };

                        var url = window.location.href;;
                        var productName = $("h2.productTitle").text();
                        var productPrice = $(".productSalePrice").text();
                        $.get("/modals/share?title=" + productName + "&url=" + productName + " " + productPrice + " " + url).done(function (res) {
                            vba.modal.html = res;
                            vba.modal.funcs.load();
                        });
                    }

                }
                vba.route.ccnt.funcs.clipboardCopy = function () {
                    var url = window.location.href;
                    navigator.clipboard.writeText(url);
                    alert("Bağlantı panoya kopyalandı!");
                }
                vba.route.ccnt.funcs.windowPrint = function () {
                    window.print();
                }
                vba.route.ccnt.funcs.sendToMail = function () {
                    var emailSubject = "Bu sayfaya gözat";
                    var emailBody = "Bu sayfaya gözat: " + window.location.href;
                    var emailTo = prompt("Alıcı: ", "");
                    var emailCC = ""; // Opsiyonel, CC alanı için e-posta adresi
                    var emailBCC = ""; // Opsiyonel, BCC alanı için e-posta adresi

                    var mailtoLink = "mailto:" + emailTo + "?subject=" + encodeURIComponent(emailSubject) + "&body=" + encodeURIComponent(emailBody);

                    // CC ve BCC alanlarını eklemek için aşağıdaki gibidir.
                    // var mailtoLink = "mailto:" + emailTo + "?cc=" + emailCC + "&bcc=" + emailBCC + "&subject=" + encodeURIComponent(emailSubject) + "&body=" + encodeURIComponent(emailBody);

                    window.location.href = mailtoLink;

                }
                vba.route.ccnt.funcs.scrollToSection = function (resp) {
                    $("a.nav-link[data-toggle='tab'][href='#review']").click();
                    setTimeout(function () {
                        const element = document.querySelector(resp);
                        if (element) {
                            element.scrollIntoView({
                                behavior: "smooth",
                                block: "start"
                            });
                        }
                    }, 300);

                }
                vba.route.ccnt.funcs.getItems = function () {
                    vba.root.changeLoading(true);
                    $("#pages_placeholder .dataContainer").html("");
                    var post = {
                        id: vba.route.ccnt.items.filterForm.id
                    };
                    $.post("/api/products/getProduct", post).done(function (res) {
                        if (res.type == "error") {
                            vba.alert({
                                message: res.message == '' ? "İşlem başarısız" : res.message,
                                classes: 'alert-danger',
                                duration: 5000
                            });
                            vba.root.checkLogin();
                        }
                        else if (res.type == "inActive") {
                            vba.route.getController("/", "/");
                        }
                        else {
                            if (res.data.id !== null) {
                                $(".productTitle").text(res.title);
                                $(".categoryName").html(res.categoryName);
                                $(".categoryName").attr("title", res.categoryName);
                                $(".categoryName").attr("href", "/categories/" + res.categoryID);
                                $(".productSalePrice").text(res.data.salePrice + " ₺");
                                $(".productBasePrice").text(res.data.basePrice + " ₺");
                                $(".productStock").html(res.data.stock > 0 ? " <span class=\"availability\">Stok Durumu :</span> <i class=\"fa fa-check-square-o\" aria-hidden=\"true\"></i>Stokta var" : "<span class=\"availability\">Stok Durumu :</span><font color=\"red\" class=\"beniGoster-text\"> <i class=\"fa fa-times-circle-o\" aria-hidden=\"true\"></i>Stokta Yok</font>");
                                $(".productMainImage").html("<img class=\"img-responsive\" src=\"/uploads/products/" + res.data.image + "\" alt=\"Product Image\">");
                                $(".product-short-description").text(vba.toMinString(res.data.details, 20));
                                $("#descriptionDetails").text(res.data.details);
                                $("#additional-information").text(res.data.details);
                                $(".commetsCount").text("Toplam Yorum (" + res.comments.length + ")");
                                $(".productTags").text("#" + res.data.tags.replace(/,/g, ", #"));
                                $(".productView").text(res.productView);
                                $(".averageRating").html(vba.ratingChange(res.averageRating));


                                if (res.comments.length > 0) {
                                    $(".comments-list").empty();
                                    $.each(res.comments, function (index, item) {

                                        var ratingHtml = vba.ratingChange(item.rating);

                                        $(".comments-list").append(`<div class="item col-md-12">
                                                                <div class="comment-left pull-left">
                                                                    <div class="avatar">
                                                                        <img src="/img/profil_avatar.png" alt="" class="imgBoxShadow" width="70" height="70">
                                                                    </div>
                                                                    <div class="product-rating ml-5">
                                                                        ${ratingHtml}
                                                                    </div>
                                                                </div>
                                                                <div class="comment-body">
                                                                    <div class="comment-meta">
                                                                        <span class="author">${item.user.firstName + " " + item.user.lastName}</span> - <span class="time">${item.creatingTime}</span>
                                                                    </div>
                                                                    <div class="comment-content">${item.detail}</div>
                                                                </div>
                                                            </div>`);
                                    });
                                }
                                else {
                                    $(".comments-list").append(`<div class="item col-md-12">
                                                                <div class="comment-left pull-left">
                                                                <div class='col-md-12 d-flex align-items-center justify-content-center'>
                                                                    😔 Üzgünüm,Yorum Yapılmamıştır.
                                                                </div>
                                                                </div>
                                                            </div>`);
                                }

                                if (res.productImageList.length > 0) {
                                    var responseHTML = `<div class="thumb-images owl-theme owl-carousel">`;
                                    $.each(res.productImageList, function (index, item) {
                                        responseHTML += `<img class="img-responsive" src="/uploads/products/${item.url}" alt="${item.product.name} Image"> `;
                                    });
                                    responseHTML += `</div>`;
                                    $("#productImageList").append(responseHTML);
                                }

                                if (res.relatedProducts.length > 0) {
                                    var responseHTML = `<div class="products owl-theme owl-carousel"> `;
                                    $.each(res.relatedProducts, function (index, item) {

                                        var ratingHtml = vba.ratingChange(item.averageRating);
                                        responseHTML += `<div class="product-item">
                                                            <div class="product-image">
                                                                <a href="/products/${item.product.id}">
                                                                    <img src="/uploads/products/${item.product.image}" alt="${item.product.name}">
                                                                </a>
                                                            </div>

                                                            <div class="product-title">
                                                                <a href="/products/${item.product.id}">
                                                                    ${item.product.name}
                                                                </a>
                                                            </div>

                                                            <div class="product-rating">
                                                                ${ratingHtml}
                                                            </div>
                                                            <span class="review-count">(${item.commentCount})</span>

                                                            <div class="product-price">
                                                                <span class="sale-price">${item.product.salePrice}</span>
                                                                <span class="base-price">${item.product.basePrice}</span>
                                                            </div>

                                                            <div class="product-buttons">
                                                                <a class="add-to-cart" href="javascript:;" onclick="vba.route.ccnt.funcs.updateUserFavorite('${item.product.id}');">
                                                                    <i class="fa fa-shopping-basket" aria-hidden="true"></i>
                                                                </a>

                                                                <a class="add-wishlist" href="#">
                                                                    <i class="fa fa-heart" aria-hidden="true"></i>
                                                                </a>

                                                                <a class="quickview" href="/products/${item.product.id}">
                                                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                                                </a>
                                                            </div>
                                                        </div>`;
                                    });
                                    responseHTML += `</div>`;
                                    $(".relatedProducts").append(responseHTML);
                                }

                            } else {
                                $("#pages_placeholder ").html(" <div class='col-lg-12 mb-3'> <div class='row g-0 bg-light py-4'><div class='col-md-12 d-flex align-items-center justify-content-center'> 😔 Üzgünüm, talebinizle eşleşen birşey bulamadık 😔</div></div></div>").hide();
                                $("#pages_placeholder ").fadeIn(500);
                                vba.route.getController("/", "/");
                            }
                        }
                        vba.root.changeLoading(false);
                    }).fail(function () {
                        $("#pages_placeholder .dataContainer").html(" <div class='col-lg-12 mb-3'> <div class='row g-0 bg-light py-4'><div class='col-md-12 d-flex align-items-center justify-content-center'> 😔 Üzgünüm, talebinizle eşleşen birşey bulamadık 😔</div></div></div>");
                    });
                    $.getScript("/js/main.js", function () { }); //Template CSS   
                    vba.root.changeLoading(false);
                }
                vba.route.ccnt.funcs.getItems();

                vba.route.ccnt.funcs.updateUserFavorite = function (elem) {
                    vba.root.changeLoading(true);
                    var post = {
                        productID: elem,
                        favoriteID: "00000000-0000-0000-0000-000000000000"
                    };
                    $.post("/api/user/updateUserFavorite", post).done(function (res) {
                        if (res.type == "error") {
                            vba.alert({
                                message: res.message == '' ? "İşlem Başarısız" : res.message,
                                classes: 'alert-danger'
                            });
                        } else {
                            vba.alert({
                                message: res.message == '' ? "İşlem Başarılı" : res.message,
                                classes: "alert-success"
                            });
                        }
                    }).fail(function () {
                        vba.alert({
                            message: "İşlem başarısız",
                            classes: 'alert-danger'
                        });
                    });
                    vba.root.changeLoading(false);
                }
                vba.route.ccnt.funcs.updateComment = function (form) {
                    vba.root.changeLoading(true);

                    var elem = vba.serializeForm(form);
                    var post = {
                        productID: vba.route.ccnt.items.filterForm.id,
                        rating: elem.rating,
                        detail: elem.detail
                    };

                    if (elem.rating != null) {
                        $.post("/api/products/updateComment", post).done(function (res) {
                            if (res.type == "error") {
                                vba.alert({
                                    message: res.message == '' ? "İşlem Başarısız" : res.message,
                                    classes: 'alert-danger'
                                });
                                vba.root.changeLoading(false);
                            } else {
                                vba.alert({
                                    message: res.message == '' ? "İşlem Başarılı" : res.message,
                                    classes: "alert-success"
                                });
                                $(form).trigger("reset");
                                vba.route.ccnt.funcs.getItems();
                                vba.route.ccnt.funcs.scrollToSection('#review');
                            }
                        }).fail(function () {
                            vba.alert({
                                message: "İşlem başarısız",
                                classes: 'alert-danger'
                            });
                        });
                    }
                    else {
                        vba.alert({
                            message: "Lütfen Değerlendirme Seçiniz.",
                            classes: 'alert-danger',
                            duration: 5000
                        });
                    }

                }
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
    toMinString: function (str, limit) {
        return (str.split(" ").slice(0, limit).join(" ")) + "...";
    },
    ratingChange: function (rating) {
        var ratingHtml = "";
        for (var i = 0; i < 5; i++) {
            if (rating > i) {
                ratingHtml += '<div class="star on"></div>';
            }
            else {
                ratingHtml += '<div class="star off"></div>';
            }
        }
        return ratingHtml;
    },

    pagination: function () {
        $("#pages_placeholder .pagination ul").hide();
        $("#pages_placeholder .pagination ul .nav-page").remove();

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
                navHTML += "<li class=\"nav-page\" ><a href=\"javascript:;\" onclick=\"vba.route.ccnt.items.filterForm.page = " + i + "; vba.route.ccnt.funcs.getItems(); return false;\"  class=\"" + (vba.route.ccnt.items.filterForm.page == i ? "current" : "") + "\" >" + i + "</a></li>";
            }
            $("#pages_placeholder .pagination ul li:eq(1)").after(navHTML);

            $("#pages_placeholder .pagination ul").show();
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


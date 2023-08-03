var vba = {
    parseFloat: function (n) {
        return parseFloat(n).toFixed(2);
    },
    settings: {

        serverDate: new Date(),
        title: "",
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
        window.onload = function () {
            var Data = {};
            fetch("/api/default/getSettings", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({})
            })
                .then(res => res.json())
                .then(res => {
                    Data = res.data[0];
                    document.getElementById("title").innerText = Data.title;
                    document.getElementById("keywords").setAttribute("content", Data.keywords);
                    document.getElementById("description").setAttribute("content", Data.description);
                    document.getElementById("email").innerText = Data.email;
                    document.getElementById("email").setAttribute("href", "mailto:" + Data.email);
                    document.getElementById("phone").innerText = Data.phone;
                    document.getElementById("phone").setAttribute("href", "tel:" + Data.phone);
                    document.getElementById("address").innerText = Data.address;
                    document.getElementById("addressphone").innerText = Data.phone;
                    document.getElementById("addressphone").setAttribute("href", "tel:" + Data.phone);
                    document.getElementById("addressemail").innerText = Data.email;
                    document.getElementById("addressemail").setAttribute("href", "mailto:" + Data.email);

                    vba.settings.title = Data.title;
                    var titlebody = document.getElementById("titlebody");
                    if (titlebody) {
                        titlebody.innerText = vba.settings.title;
                    }
                })
                .catch(error => console.error(error));

        };
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
            //Path id query gizleme --------
            if (pathName.includes("categories")) {
                vba.settings.staticID = pathName.split("categories/")[1];
                pathName = "/categories";
            }
            if (pathName.includes("products")) {
                vba.settings.staticID = pathName.split("products/")[1];
                pathName = "/products";
            }
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
                /*
                vba.route.ccnt.funcs.getProductFavorites = function () {
                    var favlist = ["Today", "Yesterday", "General"];
                    var favlistID = ["#new-arrivals", "#best-seller", "#on-sale"];
                    $.each(favlist, function (index, item) {
                        $.post("/api/default/getProductFavorites", { whichDay: item }).done(function (res) {
                            if (res.type == "error") {
                                vba.alert({
                                    message: res.message == '' ? "İşlem başarısız" : res.message,
                                    classes: 'alert-danger',
                                    duration: 5000
                                });
                            }
                            else {
                                if (res.data.length > 0) {
                                    var tmpl = "binditems"; 
                                    $("#pages_placeholder #productFavorite " + favlistID[index] + " div").append(vba.compileTemp(vba.route.ccnt.items[tmpl], res.data)); 

                                } else { } 
                            } 
                        }).fail(function () {
                            $("#pages_placeholder #productFavorite").html("");
                        });
                    }); 
                }
                vba.route.ccnt.funcs.getProductFavorites();

                vba.route.ccnt.funcs.getSliders = function () {
                    $.post("/api/default/getSliders", {}).done(function (resSlide) {
                        if (resSlide.type == "error") {
                            vba.alert({
                                message: resSlide.message == '' ? "İşlem başarısız" : resSlide.message,
                                classes: 'alert-danger',
                                duration: 5000
                            });
                        }
                        else {
                            if (resSlide.data.length > 0) {
                                var tmpl = "binditems";
                                $("#pages_placeholder #tiva-slideshow").append(vba.compileTemp(vba.route.ccnt.items[tmpl], resSlide.data));

                            } else { }
                        }
                    }).fail(function () {
                        $("#pages_placeholder #tiva-slideshow").html("");
                    });

                }
                vba.route.ccnt.funcs.getSliders();
                setTimeout(function () {
                    $.getScript("/js/main2.js", function () { }); //Template CSS  
                    vba.root.changeLoading(false);
                }, 500);  
                */

                $.getScript("/js/main.js", function () { }); //Template CSS  
                var titlebody = document.getElementById("titlebody");
                if (titlebody) {
                    titlebody.innerText = vba.settings.title;
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

                vba.route.ccnt.funcs.filterOperations = function () {
                    vba.route.ccnt.items.filterForm.price = $("#price-filter").val();
                    vba.route.ccnt.funcs.getItems();
                }

                $('#listSorting').change(function () {
                    vba.route.ccnt.items.filterForm.listSorting = this.value;
                    vba.route.ccnt.items.filterForm.page = 1;
                    vba.route.ccnt.funcs.getItems();
                });

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
                    vba.route.ccnt.items.dataLoading = true; 
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
                                $(".productStock").html(res.data.stock > 0 ? " <span class=\"availability\">Stok Durumu :</span> <i class=\"fa fa-check-square-o\" aria-hidden=\"true\"></i>Stokta var" : "<span class=\"availability\">Stok Durumu :</span><font color=\"red\" class=\"beniGoster-text\"> <i class=\"fa fa-times-circle-o\" aria-hidden=\"true\"></i>Stokta Yok<font>");
                                $(".productMainImage").html("<img class=\"img-responsive\" src=\"/uploads/products/" + res.data.image + "\" alt=\"Product Image\">");
                                $(".product-short-description").text((res.data.details.split(" ").slice(0, 20).join(" ")) + "...");
                                $("#descriptionDetails").text(res.data.details);
                                $("#additional-information").text(res.data.details);
                                $(".commetsCount").text("Toplam Yorum (" + res.comments.length + ")");
                                $(".productTags").text("#" + res.data.tags.replace(/,/g, ", #"));


                                getComments();
                                function getComments() {
                                    if (res.comments.length > 0) {
                                        $.each(res.comments, function (index, item) {

                                            var raiting = "";
                                            for (var i = 0; i < 5; i++) {
                                                if (item.rating > i) {
                                                    raiting += '<div class="star on"></div>';
                                                }
                                                else {
                                                    raiting += '<div class="star off"></div>';
                                                }

                                            }

                                            $(".comments-list").append(`<div class="item col-md-12">
                                                                <div class="comment-left pull-left">
                                                                    <div class="avatar">
                                                                        <img src="/img/avatar.jpg" alt="" width="70" height="70">
                                                                    </div>
                                                                    <div class="product-rating">
                                                                        ${raiting}
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
                                        $(".comments-list").append(`<div class="item  col-md-12">
                                                                <div class="comment-left pull-left">
                                                                <div class='col-md-12 d-flex align-items-center justify-content-center'>
                                                                    😔 Üzgünüm,Yorum Yapılmamıştır.
                                                                </div>
                                                                </div>
                                                            </div>`);
                                    }
                                }

                                //res.productImageList
                                getProductImageList();
                                function getProductImageList() {
                                    if (res.productImageList.length > 0) {
                                        var responseHTML = `<div class="thumb-images owl-theme owl-carousel">`;
                                        $.each(res.productImageList, function (index, item) {
                                            responseHTML += `<img class="img-responsive" src="/uploads/products/${item.url}" alt="${item.product.name} Image"> `;
                                        });
                                        responseHTML += `</div>`;
                                        $("#productImageList").append(responseHTML);
                                    }
                                }
                            } else {
                                $("#pages_placeholder ").html(" <div class='col-lg-12 mb-3'> <div class='row g-0 bg-light py-4'><div class='col-md-12 d-flex align-items-center justify-content-center'> 😔 Üzgünüm, talebinizle eşleşen birşey bulamadık 😔</div></div></div>").hide();
                                $("#pages_placeholder ").fadeIn(500);
                                vba.route.getController("/", "/"); s
                            }
                        }
                        vba.route.ccnt.items.dataLoading = false;
                        vba.root.changeLoading(false);
                    }).fail(function () {
                        vba.route.ccnt.items.dataLoading = false;
                        $("#pages_placeholder .dataContainer").html(" <div class='col-lg-12 mb-3'> <div class='row g-0 bg-light py-4'><div class='col-md-12 d-flex align-items-center justify-content-center'> 😔 Üzgünüm, talebinizle eşleşen birşey bulamadık 😔</div></div></div>");
                    });
                    vba.root.changeLoading(false);
                }
                vba.route.ccnt.funcs.getItems();
                vba.route.ccnt.funcs.updateComment = function (form) {
                    vba.root.changeLoading(true);
                    var elem = vba.serializeForm(form);
                    if (elem.rating != null) {
                        console.log(elem);
                    }
                    else {
                        vba.alert({
                            message: "Lütfen Değerlendirme Seçiniz.",
                            classes: 'alert-danger',
                            duration: 5000
                        });
                    }

                    vba.root.changeLoading(false);
                }
                $.getScript("/js/main.js", function () { }); //Template CSS  
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


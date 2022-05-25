/***************************************SitecoreIntegrations-JS*****************************************/
var isInMobileView = isMobileViewEnabled();

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function CreateGuid() {
    function _p8(s) {
        var p = (Math.random().toString(16) + "000000000").substr(2, 8);
        return s ? "-" + p.substr(0, 4) + "-" + p.substr(4, 4) : p;
    }
    return _p8() + _p8(true) + _p8(true) + _p8();
}

appSettings = {
    fieldSettings: {
        invalidSpecialChars: "!@#$^&%*()+=[]\/{}|:<>?,~`;",
        invalidPhoneSpecialChars: "!@#$^&%*+=[]\/{}|:<>?,~`;."
    }
}

function sanitizeFields(value, specialChars) {
    if (specialChars) {
        for (var i = 0; i < specialChars.length; i++) {
            value = value.replace(new RegExp("\\" + specialChars[i], "gi"), "");
        }
    }
    return value.trim();
}

function toggleUtilityNavCaption(elemId) {
    if ($(elemId).length === 1) {
        if (isInMobileView) {
            if ($(elemId).hasClass("show")) {
                $(elemId).removeClass("show");
            }
            else {
                $(elemId).addClass("show");
            }
            $(".modal-backdrop").remove();
        }
        else {
            if ($(elemId).hasClass("show")) {
                $(elemId).parent("li").removeClass("active");
                $(elemId).removeClass("show").removeClass("caret-open").attr("aria-expanded", "false");
                $(elemId).next(".dropdown-menu").removeClass("show").removeAttr("data-bs-popper");
            }
            else {
                $(elemId).parent("li").addClass("active");
                $(elemId).addClass("show").addClass("caret-open").attr("aria-expanded", "true");
                $(elemId).next(".dropdown-menu").addClass("show").attr("data-bs-popper", "none");
            }
        }
    }
}

function closeUilityModal(elemId) {
    if ($(elemId).length === 1) {
        if (isInMobileView) {
            $(elemId).removeClass("show");
            $("#isi-main, #isi-inline").removeClass("d-none");
            $(window).scrollTop(0);
            sessionStorage.setItem("inidicationStatus", "true");
        }
    }
}

window.ScrollToTop = function (elementItem) {
    var pos = $(elementItem).position().top.toString();
    $("html,body", window.document).animate({
        scrollTop: pos
    }, "fast");
};

window.SmoothScrollToTop = function(elementItem, speed) {
    console.log(elementItem);
    if ($(elementItem)) {
        $("html,body", window.document).animate({
            scrollTop: $(elementItem).offset().top
        }, speed);
    }
};

$(window).resize(function () {
    isInMobileView = isMobileViewEnabled();
});

function isMobileViewEnabled() {
    if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)
        || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) {
        return true;
    }
    else {
        return false;
    }
}

String.prototype.replaceAll = function (strReplace, strWith) {
    // See http://stackoverflow.com/a/3561711/556609
    var esc = strReplace.replace(/[-\/\\^$*+?.()|[\]{}]/g, '\\$&');
    var reg = new RegExp(esc, 'ig');
    return this.replace(reg, strWith);
};

//Added dynamic events for select dropdowns open and close states
$("select").click(function (e) {
    toggleDropdownState(this);
});

function toggleDropdownState(elem) {
    if ($(elem).parent("div").hasClass("select-active")) {
        $(elem).parent("div").removeClass("select-active");
    }
    else {
        $(elem).parent("div").addClass("select-active");
    }
}

$("select").keypress(function (e) {
    //(keyCode === 13 == enter key) || (keyCode === 32 == space key)
    if (e.keyCode === 13 || e.keyCode === 32) {
        toggleDropdownState(this);
    }

    //(keyCode === 9 == tab key);
    if (e.keyCode === 9) {
        $(this).parent("div").removeClass("select-active");
    }
});

$("select").focusout(function (e) {
    $(this).parent("div").removeClass("select-active");
});
//Added dynamic events for select dropdowns open and close states

var alertMessage = function () {
    $(window).on("load resize", function () {
        alertMessage();
    });

    $(".site-alert--banner").each(function () {
        var statusBannerItemId = ("#" + $(this).attr("id"));
        if (sessionStorage.getItem(statusBannerItemId)) {
            $(this).hide();
        }
        else {
            $(this).show();
        }
    });

    $(".close-site-alert").on('click', function (e) {
        e.preventDefault();
        var statusBannerItemId = ("#" + $(this).attr("data-banner-id"));
        //Set session for close specified statusBanner to true
        sessionStorage.setItem(statusBannerItemId, true);
        //Hide the alert
        $(this).parents(".site-alert--banner").hide();
        alertMessage();
    });
};
alertMessage();

function onBodyScroll() {
    var position = 0;
    
    if (!isInMobileView) {
        if (scrollY > position) {
            $(".ul-header-main-links li a.dropdown-toggle").each(function () {
                if ($(this).attr("aria-expanded") === "true") {
                    var elemId = ("#" + $(this).attr("id"));
                    toggleUtilityNavCaption(elemId);
                }
            });
        }
    }
}

function getCookie(name) {
    var match = document.cookie.match(RegExp('(?:^|;\\s*)' + name + '=([^;]*)'));
    return match ? match[1] : null;
}
/***************************************SitecoreIntegrations-JS*****************************************/
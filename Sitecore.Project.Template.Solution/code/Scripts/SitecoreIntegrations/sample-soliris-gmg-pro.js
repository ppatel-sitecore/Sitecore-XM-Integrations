/***************************************SolirisGMGPro-JS*****************************************/
var isInMobileView = isMobileViewEnabled();
var formData = {
    signUpRequest: false,
    salesRequest: false,
    frmRequest: false,
    contactPreference: "",
    frmTopic1: false,
    frmTopic2: false,
    frmTopic3: false,
    frmTopicOther: "",
    repTopic1: false,
    repTopic2: false,
    repTopic3: false,
    repTopicOther: "",
    iama: "",
    firstName: "",
    lastName: "",
    optInCheckbox: false,
    phoneNumber: "",
    email: "",
    zipcode: "",
    npiNumber: "",
    mobileNumber: "",
    timeStamp: "",
    uniqueIdentifier: "",
    formUrl: "",
    requiredFieldsValidated: false,
}
var todayDate = new Date();
var ogBodyPos
var headerHeight

$(document).ready(function () {
    setTimeout(() => {
        ogBodyPos = $("body").position().top;
        headerHeight = $("header").height();
        $("#main").css("paddingTop", headerHeight + "px");
    }, 500);

    /******************************SETTING ACTIVE DROPDOWN MENU*********************************/
    $(".mega-menu-items .li-item").removeClass("active");
    var activePage = location.pathname.replace(/%20/g, " ").toString();
    $(".mega-menu-items .li-item a").each(function (i, obj) {
        if (activePage === $(this).attr("href")) {
            $(this).parent(".li-item").addClass("active");
            $(this).parents(".nav-item").addClass("active");
        }
    });
    /******************************SETTING ACTIVE DROPDOWN MENU*********************************/

    /******************************REGAIN OR OLE FUNCTIONALITY*********************************/
    var slug = location.hash
    if (slug === "#ole") {
        $('[data-tab="regain-tab"]').removeClass("active");
        $("#regain-tab.tab-pane").removeClass("show");
        $('[data-tab="ole-tab"]').addClass("active");
        $("#ole-tab.tab-pane").addClass("show");
    }
    /******************************REGAIN OR OLE FUNCTIONALITY*********************************/

    /***************************OPEN ACCORDIONS BASED ON ANCHOR LINK***************************/
    if (slug.length > 4) {

        if (slug === "#worn-out-nurse" || slug === "#forgotten-friend" || slug === "#mr-iiwii" || slug === "#woman-on-the-sidelines" || slug === "#marooned-matriarch") {
            OpenCharacterAccordion(slug);
        }
        else {
            return;
        }
    }
    /***************************OPEN ACCORDIONS BASED ON ANCHOR LINK***************************/

    InitializeInterstitial();
    InitializeIsiSection();
    InitializeGetInTouchForm();
    $("a.download-link, a.card-link, a[href*='.pdf'], a[href*='http://'], a[href*='https://']").each(function () {
        var hrefUrl = $(this).attr("href");

        if (hrefUrl !== "#") {
            $(this).attr("target", "_blank");
        }
    });

    //InitializeHamburgerCTASectionIOS_Safari();
});

function InitializeHamburgerCTASectionIOS_Safari() {
    var is_iphone_ipad = navigator.userAgent.match(/(iPhone|iPad)/);
    var is_safari = /^((?!chrome|android).)*safari/i.test(navigator.userAgent);
    if (is_iphone_ipad && is_safari) {
        $("#mobile-links-nav").addClass("overflow-y-hidden");
        $("#mobile-links-nav").addClass("overflow-y-hidden");
    }
}

function InitializeGetInTouchForm() {
    /***************************SITECORE FORMS INITIALIZATION***************************/
    if ($("#frm-row")) {
        $(".form-opt-row").append($("#frm-row"));
    }
    if ($("#sr-row")) {
        $(".sales-opt-row").append($("#sr-row"));
    }
    if ($(".get-in-touch-form-disclosures")) {
        $(".opt-in-col").append($(".get-in-touch-form-disclosures"));
    }
    if ($(".get-in-touch-form-submit")) {
        $(".form-submit-row").append($(".get-in-touch-form-submit"));
    }

    var currentDateStamp = (todayDate.getFullYear() + "-" + (todayDate.getMonth() + 1) + "-" + todayDate.getDate());
    formData.formUrl = location.href.toLowerCase().trim();
    formData.timeStamp = currentDateStamp;
    formData.uniqueIdentifier = CreateGuid().toLowerCase().trim() + "_" + currentDateStamp;
    if (queryStrings && queryStrings["sc_site"]) {
        $(".show-page").removeClass("show-page");
        $(".page-question").removeClass("show-question");
        $(".default-hero").hide();
        $("[data-question=thank-you]").addClass("show-page");
        SmoothScrollToTop("#main", 2000);
    }
    if ($(".sitecore-form")) {
        $(".sitecore-form").attr("novalidate", "novalidate");
    }
    /***************************SITECORE FORMS INITIALIZATION***************************/
}

function CreateGuid() {
    function _p8(s) {
        var p = (Math.random().toString(16) + "000000000").substr(2, 8);
        return s ? "-" + p.substr(0, 4) + "-" + p.substr(4, 4) : p;
    }
    return _p8() + _p8(true) + _p8(true) + _p8();
}

/******************************ISI & HEADER ON SCROLL*********************************/
var position = $(window).scrollTop();
function InitializeIsiSection() {
    var isiSessionOpened = sessionStorage.getItem("isiSessionOpened");
    if (isiSessionOpened) {
        $("#isi-main").addClass("window-scrolled");
    }
    else {
        $("#isi-main").removeClass("window-scrolled");
    }
}

$(window).on("scroll", function () {
    var scroll = $(window).scrollTop();
    var utilityNavToggleId = ("#ultilyNavModal, #desktop-indication-dropdown-link > .dropdown-menu");

    if (scroll > 0) {
        $("body").css("top", 0)

        if ($("#onetrust-banner-sdk").css("visibility") !== "hidden") {
            $("header").css("top", 0);
        }
        else {
            $("header").css("top", "unset");
        }
    }
    else {
        $("header").css("top", "unset");
        $("body").css("top", ogBodyPos + "px");
    }

    if (scroll > 200) {

        if (scroll > position) {
            if (isInMobileView) {
                $(".header-rows-1").addClass("d-none");
            }
            else {
                $("header").addClass("d-none");
            }

            updateStickyElement();
            $(utilityNavToggleId).addClass("d-none");
        }
        else {
            if (isInMobileView) {
                $(".header-rows-1").removeClass("d-none");
            }
            else {
                $("header").removeClass("d-none");
            }

            updateStickyElement();
            $(utilityNavToggleId).removeClass("d-none");
        }
    }

    if (scroll < 200) {
        $("header").removeClass("d-none");
        if (isInMobileView) {
            $(".header-rows-1").removeClass("d-none");
        }

        $(utilityNavToggleId).removeClass("d-none");
    }
    position = scroll;
    onBodyScroll();
});

function updateStickyElement() {
    if ($("header").hasClass("scroll-down")) {
        $(".sticky-top").css("top", "0");
    }
    else {
        var headerHeight = parseInt($("header").height(), 10);
        $(".sticky-top").css("top", headerHeight + "px");
        if ($(window).width() >= 768) {
            ScrollToResourcesSectionPos(false);
        }
    }
}

$("a[href='#isi-inline'").click(function (e) {
    e.preventDefault();
    SmoothScrollToTop("#isi-inline", 500)
})

$(".close-desktop-indication").click(function () {
    $("#desktop-indication-dropdown-link").removeClass("active");
    $("#desktop-indication-dropdown-link .dropdown-menu").removeClass("show");
    $("#desktop-indication-dropdown-link .dropdown-toggle").removeClass("show caret-open");
    sessionStorage.setItem("inidicationStatus", "true");
});

$(document).on("click", ".isi-linkrole", function (e) {
    e.preventDefault();
    if ($("#isi-main").hasClass("active")) {
        $("#isi-main").removeClass("active");
        $("header").removeClass("scroll-down");
        $("body").css("overflow-y", "unset");
        $("#isi-main").addClass("window-scrolled");
        sessionStorage.setItem("isiSessionOpened", true);
        $("#onetrust-banner-sdk").removeClass("z-index0");
    }
    else {
        $("#isi-main").addClass("active");
        $("header").addClass("scroll-down");
        $("body").css("overflow-y", "hidden");
        $("#onetrust-banner-sdk").addClass("z-index0");
    }
})

var observer = new IntersectionObserver(function (entries) {
    // isIntersecting is true when element and viewport are overlapping
    // isIntersecting is false when element and viewport don't overlap
    var inlineIsiTopPos = parseInt($("#isi-inline").position().top, 10);
    if (entries[0].isIntersecting === true || position >= inlineIsiTopPos) {
        $(".isi-main").addClass("hide-isi-main");
    }
    else if (entries[0].isIntersecting === false) {
        $(".isi-main").removeClass("hide-isi-main");
    }

}, { threshold: [0] });
observer.observe(document.getElementById("isi-inline"));
/******************************ISI & HEADER ON SCROLL*********************************/

/******************************UPDATE EXIT INTERSTITAL LINK*********************************/
function InitializeInterstitial() {
    if ($(".external, .external-link").length > 0) {
        $(".external, .external-link").each(function () {
            $(this).click(function (e) {
                e.preventDefault();

                if ($(this).attr("href")) {
                    $("#leavingSite .leaving-modal-yes").attr("href", $(this).attr("href"));
                }
                $("#leavingSite").modal("show");
            });
        });
    }
}
/******************************UPDATE EXIT INTERSTITAL LINK*********************************/

/**************************************SOLIRIS CARD JS*****************************************/
$(".card-toggle-btn").click(function () {
    var thisCard = $(this).closest(".soliris-card");
    var imgContainer = $(this).closest(".heading").siblings(".img-section_container")
    var windowHeight = $(window).height()
    var cardHeading = thisCard.find(".heading").outerHeight()
    var newCardHeight = windowHeight - cardHeading

    if (!thisCard.hasClass("expand")) {
        thisCard.addClass("expand")
        $(".isi-main").addClass("hide-isi-main");
        $("body").css("overflow-y", "hidden");
        imgContainer.css("height", newCardHeight);
        $("header").css("transform", "translatey(-200%)");

        if ($(".sticky-top").length > 0) {
            $(".sticky-top").css("transform", "translatey(-200%)");
        }
        if (thisCard.hasClass("additional-content")) {
            var additionalContent = $(this).closest(".heading").siblings(".content-section_container");
            imgContainer.css("height", newCardHeight - additionalContent.height());
        }
        else {
            imgContainer.css("height", newCardHeight);
        }
    }
    else {
        thisCard.removeClass("expand");
        $(".isi-main").removeClass("hide-isi-main");
        imgContainer.css("height", "fit-content");
        $("body").css("overflow-y", "scroll");
        $("header").css("transform", "translatey(0)");

        if ($(".sticky-top").length > 0) {
            $(".sticky-top").css("transform", "translatey(0)");
        }
    }

    var element = $(this).parents('.soliris-card');
    SmoothScrollToTop(element, 500);
});

$(".soliris-card .close-overlay").click(function () {
    $(this).closest(".overlay").fadeOut();
    $(this).closest(".img-section_container").addClass("scroll-active");
});

$(".soliris-card .overlay").on("touchend", function () {
    $(this).fadeOut();
    $(this).closest(".img-section_container").addClass("scroll-active");
});
/**************************************SOLIRIS CARD JS*****************************************/

/**************************************CHARACTER ACCORDION JS*****************************************/
$(".patient-identification .tab-link").click(function () {
    var character = "#" + $(this).attr("id");
    OpenCharacterAccordion(character);
});

function OpenCharacterAccordion(character) {
    var characterData = $("" + character).data("content")
    var characterContent = ("#" + characterData);
    var thisCard = $(".tab-link[data-content='" + characterData + "']");
    var thisCharacterWrapper = thisCard.parents(".tab-wrapper");

    if (thisCard.hasClass("active")) {
        return;
    }
    else {
        closeCharacterCards();
        SmoothScrollToTop(thisCard, 500);

        if ($('.tab-link').length > 0 && !thisCard.hasClass("active")) {
            $('.tab-link').removeClass("active");
            $(".tabbed-content").removeClass("active");
            thisCard.addClass("active");
        }
    }
    if ($(characterContent).hasClass("active")) {
        $(characterContent).removeClass("active");
    }
    else {
        $(characterContent).addClass("active");
    }

    $(".character-full").toggleClass("active");
    var newHeight = thisCard.height() + $(characterContent).outerHeight();
    thisCharacterWrapper.css("height", newHeight + "px");
}

$(".patient-identification .close-icon").click(function () {
    closeCharacterCards();
});

function closeCharacterCards() {
    $(".tab-link").removeClass("active");
    $(".tabbed-content").removeClass("active");
    $(".tab-wrapper").css("height", "auto");
}
/**************************************CHARACTER ACCORDION JS*****************************************/

$(".regain-ole-nav .nav-item").click(function () {
    var tabData = $(this).data("tab");
    var tabContent = $("#" + tabData);

    if ($("regain-ole-nav .nav-item").not(this).hasClass("active")) {
        $("regain-ole-nav .nav-item").removeClass("active");
    }
    if (!$(tabData).hasClass("active")) {
        $(".regain-ole-nav .nav-item").not(this).removeClass("active");
        $(this).addClass("active");
    }

    if (!$(tabContent).hasClass("show")) {
        $(".tab-full-page").not(tabContent).removeClass("show");
        $(tabContent).addClass("show");
    }
})

/*ACCESS SCROLL LINKS */
$(".scroll-to-anchor").click(function (e) {
    e.preventDefault();
    var anchor = ("#" + $(this).attr("href").toString());
    SmoothScrollToTop(anchor, 200);
})
/*ACCESS SCROLL LINKS */

/*RESOURCES DATA FILTER */
$(".resource-data-filter").click(function () {
    if ($(this).data("filter") === "all") {
        $(".resource-data-filter").removeClass("active");
        $(this).addClass("active");
        $(".resource-section").addClass("show");
    }
    else {
        hideAllResources();
        $(this).addClass("active");
        var resourceData = $(this).data("filter");
        $("[data-filter-content='" + resourceData + "']").addClass("show");
    }

    ScrollToResourcesSectionPos(true);
});

function ScrollToResourcesSectionPos(isScrollToTopSection) {
    var headerHeight = parseInt($("header").height(), 10);
    var filterHeight = parseInt($("#filters").height(), 10);
    var firstResourceSection = $(".resource-section.show").first();
    if (firstResourceSection.length === 1 && isScrollToTopSection) {
        $(firstResourceSection).css("padding-top", (headerHeight + filterHeight) + "px");
        SmoothScrollToTop(firstResourceSection, 1000);
    }
}

function hideAllResources() {
    $(".resource-data-filter").removeClass("active");
    $(".resource-section").removeClass("show");
}
/*RESOURCES DATA FILTER */

/******************************SITECORE FORMS CHANGING FUNCTIONALITY*********************************/
$("form .col-selection").click(function () {
    var inputElem = $(this).find("input");
    if ($(inputElem).prop("checked") === true) {
        $(inputElem).prop("checked", false);
    }
    else {
        $(inputElem).prop("checked", true);
    }

    var reqTypeFieldId = ("[data-sc-field-name=" + $(inputElem).attr("data-field-id") + "]");
    if ($(inputElem).attr("type") === "checkbox") {
        if ($(reqTypeFieldId) && reqTypeFieldId.indexOf("signup-request") > -1) {
            formData.signUpRequest = $(inputElem).prop("checked");
            $(reqTypeFieldId).prop("checked", formData.signUpRequest);
        }
        else if ($(reqTypeFieldId) && reqTypeFieldId.indexOf("frm-request") > -1) {
            formData.frmRequest = $(inputElem).prop("checked");
            $(reqTypeFieldId).prop("checked", formData.frmRequest);
        }
        else if ($(reqTypeFieldId) && reqTypeFieldId.indexOf("sales-request") > -1) {
            formData.salesRequest = $(inputElem).prop("checked");
            $(reqTypeFieldId).prop("checked", formData.salesRequest);
        }

        if (formData.frmRequest || formData.signUpRequest || formData.salesRequest) {
            $("[data-question=one]").find(".nextBtn").removeClass("disabled");
        }
        else {
            $("[data-question=one]").find(".nextBtn").addClass("disabled");
        }
    }
    else if ($(inputElem).attr("type") === "radio") {
        if ($(reqTypeFieldId)) {
            $(reqTypeFieldId).val($(inputElem).attr("value"));
            formData.contactPreference = $(reqTypeFieldId).val();
        }
    }
});

$(".frmr-col-selection, .sales-col-selection").click(function () {
    var inputElem = $(this).find("input");
    var controlWrapperElem = $(this).find(".control-wrapper");

    var reqTypeFieldId = ("[data-sc-field-name=" + $(inputElem).attr("data-field-id") + "]");
    if ($(inputElem).prop("checked") === true) {
        $(inputElem).prop("checked", false);
        $(reqTypeFieldId).prop("checked", false);
        $(controlWrapperElem).removeClass("active");
    }
    else {
        $(inputElem).prop("checked", true);
        $(reqTypeFieldId).prop("checked", true);
        $(controlWrapperElem).addClass("active");
    }

    if ($(inputElem).attr("type") === "checkbox") {
        if ($(inputElem).attr("id") === "access") {
            formData.frmTopic1 = $(reqTypeFieldId).is(":checked");
        }
        if ($(inputElem).attr("id") === "coding-billing") {
            formData.frmTopic2 = $(reqTypeFieldId).is(":checked");
        }
        if ($(inputElem).attr("id") === "frmr-health-insurance") {
            formData.frmTopic3 = $(reqTypeFieldId).is(":checked");
        }
        if ($(inputElem).attr("id") === "ongoing-support") {
            formData.repTopic1 = $(reqTypeFieldId).is(":checked");
        }
        if ($(inputElem).attr("id") === "starting-treatment") {
            formData.repTopic2 = $(inputElem).is(":checked");
        }
        if ($(inputElem).attr("id") === "sales-health-insurance") {
            formData.repTopic3 = $(reqTypeFieldId).is(":checked");
        }
    }
});

$("#chk-form-optIn-selected").click(function () {
    formData.optInCheckbox = $(this).is(":checked");
    $(this).prop("checked", formData.optInCheckbox);
    $("[data-sc-field-name=hidden-opt-in-field]").val(formData.optInCheckbox);

    if (formData.optInCheckbox) {
        $(".opt-in-error-msg").addClass("d-none");
    }
});

$("#frm-other, #rep-details").focusout(function () {
    var reqTypeFieldId = ("[data-sc-field-name=" + $(this).attr("data-field-id") + "]");
    var inputVal = $(this).val();
    if ($(reqTypeFieldId)) {

        $(this).val(inputVal);
        $(reqTypeFieldId).val(inputVal);
        if ($(this).attr("id") === "frm-other") {
            formData.frmTopicOther = $(reqTypeFieldId).val();
        }
        else if ($(this).attr("id") === "rep-details") {
            formData.repTopicOther = $(reqTypeFieldId).val();
        }
    }
});

$(".text-input.required-form-field, #frm-other, #rep-details, [data-sc-field-name=npi-number]").keyup(function () {
    var reqTypeFieldId = $(this).attr("data-sc-field-name");
    var elemId = $(this).attr("id");
    var inputVal = $(this).val().trim();

    if (reqTypeFieldId === "firstname" || reqTypeFieldId === "lastname" || elemId === "frm-other" || elemId === "rep-details") {
        inputVal = sanitizeFields($(this).val(), appSettings.fieldSettings.invalidSpecialChars);
        $(this).val(inputVal);
    }
    else if (reqTypeFieldId === "phone-number") {
        inputVal = sanitizeFields($(this).val(), appSettings.fieldSettings.invalidPhoneSpecialChars);
        $(this).val(inputVal);
    }
    else if (reqTypeFieldId === "npi-number") {
        inputVal = inputVal.replace(/\D/g, "").replace(".", "").substr(0, 10).trim();
        $(this).val(inputVal);
    }

    CheckFieldLength(this, inputVal);
});

$(".sitecore-form .form-control.text-input").focusout(function () {
    var inputVal = $(this).val().trim();
    var reqTypeFieldId = $(this).attr("data-sc-field-name");
    if ($(reqTypeFieldId)) {
        if (reqTypeFieldId === "firstname") {
            formData.firstName = inputVal;
        }
        else if (reqTypeFieldId === "lastname") {
            formData.lastName = inputVal;
        }
        else if (reqTypeFieldId === "phone-number") {
            formData.phoneNumber = $(this).val();
            if (formData.contactPreference === "text") {
                formData.mobileNumber = formData.phoneNumber;
            }
        }
        else if (reqTypeFieldId === "email-address") {
            formData.email = inputVal;
        }
        else if (reqTypeFieldId === "zip-code") {
            formData.zipcode = inputVal;
        }
        else if (reqTypeFieldId === "npi-number") {
            formData.npiNumber = inputVal;
        }
        else if (reqTypeFieldId === "email-address") {
            formData.email = inputVal;
        }

        CheckFieldLength(this, inputVal);
    }
});

function CheckFieldLength(elem, inputVal) {
    if (inputVal.length > 0) {
        $(elem).removeClass("input-error");
        $(elem).parent("div").find("span.field-validation-valid").last().html("");
    }
}

$("form .nextBtn").click(function (e) {
    e.preventDefault();
    updateFormValues();
    if (!$(this).hasClass("disabled")) {
        var slide = $(this).data('slide');
        $(".show-page").removeClass("show-page");
        $("[data-question='" + slide + "']").addClass("show-page");
        $(".page-question").removeClass("show-question");
        $("[data-question-hero='" + slide + "']").addClass('show-question');
    }
})

$(".backBtn").click(function (e) {
    e.preventDefault();

    if (!$(this).hasClass("disabled")) {
        var slide = $(this).data('slide');
        $('.show-page').removeClass("show-page");
        $("[data-question='" + slide + "']").addClass("show-page");
        $('.page-question').removeClass("show-question");
        $("[data-question-hero='" + slide + "']").addClass("show-question");
    }
})

$("#submitBtn").click(function (e) {
    var isValidatedFields = true;
    var isInvalidZipCode = false;
    $(".required-form-field").removeClass("select-error").removeClass("input-error");

    $(".required-form-field, .npi-num-form-control").each(function () {
        var reqTypeFieldId = $(this).attr("data-sc-field-name");
        if (reqTypeFieldId === "iama" && $(this).val().toLowerCase().trim() === "select one") {
            $(this).addClass("select-error");
        }
        else {
            var errorMessage = "";
            var inputVal = $(this).val().trim();
            var npiMaxLen = parseInt($(this).attr("maxlength"), 10);
            var regexPattern = $(this).attr("data-val-regex-pattern");
            var regex = new RegExp(regexPattern);

            if ((regexPattern && (!regex.test(inputVal)))) {
                errorMessage = $(this).attr("data-val-regex").trim();
                if (reqTypeFieldId === "zip-code") {
                    isInvalidZipCode = true;
                }
            }
            else if ((reqTypeFieldId === "firstname" || reqTypeFieldId === "lastname") && inputVal.length === 0) {
                errorMessage = $(this).attr("data-val-length").trim();
            }
            else if (reqTypeFieldId === "npi-number" && (inputVal.length > 0 && inputVal.length < npiMaxLen)) {
                errorMessage = $(this).attr("data-val-length").trim();
            }
            else if (reqTypeFieldId === "hidden-opt-in-field" && (inputVal.length === 0 || inputVal === "false")) {
                $(".opt-in-error-msg").removeClass("d-none");
            }

            if (errorMessage && (errorMessage.length > 0)) {
                $(this).parent("div").find("span.field-validation-valid").last().html(errorMessage.replace(" *", "").trim());
                $(this).addClass("input-error");
            }
        }
    });

    CheckFormData();
    isValidatedFields = formData.requiredFieldsValidated;
    if (isInvalidZipCode || !isValidatedFields) {
        e.preventDefault();
        SmoothScrollToTop(".sitecore-form", 2000);
        return false;
    }
    else {
        $(".form-submitBtn").click();
    }
});

function CheckFormData() {
    formData.firstName = $("[data-sc-field-name=firstname]").val();
    formData.lastName = $("[data-sc-field-name=lastname]").val();
    formData.phoneNumber = $("[data-sc-field-name=phone-number]").val();
    formData.mobileNumber = $("[data-sc-field-name=mobile-phone-number]").val();
    formData.email = $("[data-sc-field-name=email-address]").val();
    formData.zipcode = $("[data-sc-field-name=zip-code]").val();
    formData.npiNumber = $("[data-sc-field-name=npi-number]").val();

    if (formData.contactPreference === "text") {
        $("[data-sc-field-name=mobile-phone-number]").val(formData.phoneNumber);
        formData.requiredFieldsValidated = (formData.firstName.length > 0 && formData.lastName.length > 0 && formData.mobileNumber.length > 0
            && formData.email.length > 0 && formData.zipcode.length > 0 && formData.optInCheckbox === true
            && (formData.npiNumber.length === 0 || formData.npiNumber.length === 10));
    }
    else if (formData.contactPreference === "email") {
        formData.requiredFieldsValidated = (formData.firstName.length > 0 && formData.lastName.length > 0 && formData.email.length > 0
            && formData.zipcode.length > 0 && formData.optInCheckbox === true
            && (formData.npiNumber.length === 0 || formData.npiNumber.length === 10));
    }
    else {
        formData.requiredFieldsValidated = (formData.firstName.length > 0 && formData.lastName.length > 0 && formData.phoneNumber.length > 0
            && formData.email.length > 0 && formData.zipcode.length > 0 && formData.optInCheckbox === true
            && (formData.npiNumber.length === 0 || formData.npiNumber.length === 10));
    }
    console.log(formData);
}

function updateFormValues() {
    var qOneFRM = $("#field-reimbursement-manager").prop('checked');
    var qOneSR = $("#sales-representative").prop('checked');
    var frmDetails = $("#frm-row");
    var srDetails = $("#sr-row");
    var phoneFieldDiv = $("[data-sc-field-name=phone-number]").parent("div.col-12.col-sm-6");
    var emailFieldDiv = $("[data-sc-field-name=email-address]").parent("div.col-12.col-sm-6");
    var zipCodeFieldDiv = $("[data-sc-field-name=zip-code]").parent("div.col-12.col-sm-6");
    var npiNumberFieldDiv = $("[data-sc-field-name=npi-number]").parent("div.col-12.col-sm-6");

    if (qOneFRM === true) {
        frmDetails.show();
    }
    else {
        frmDetails.hide();
    }

    if (qOneSR === true) {
        srDetails.show();
    } else {
        srDetails.hide();
    }

    if (formData.contactPreference === "call" || formData.contactPreference === "text") {
        phoneFieldDiv.parent("div.row").append(emailFieldDiv);
        npiNumberFieldDiv.parent("div.row").prepend(zipCodeFieldDiv);
        phoneFieldDiv.show();
    }
    else if (formData.contactPreference === "email") {
        phoneFieldDiv.hide();
        emailFieldDiv.parent("div.row").append(zipCodeFieldDiv);
    }
}
/******************************SITECORE FORMS CHANGING FUNCTIONALITY*********************************/

$("#mobile-hamburger-menu").click(function() {
    $("body").css("overflow-y", "hidden");
    $("#mobile-links-nav").addClass("show")
    $(".isi-main").addClass("hide-isi-main");
});

$("#mobile-hamburger-menu-close").click(function() {
    $("#mobile-links-nav").removeClass("show")
    $(".isi-main").removeClass("hide-isi-main")
    $("body").css("overflow-y", "scroll");
});
/***************************************SolirisGMGPro-JS*****************************************/
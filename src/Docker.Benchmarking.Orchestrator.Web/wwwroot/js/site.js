// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function () {
    function showAlert(alert) {
        const alertContainer = $('.alert-container');

        const alertElement = $(`<div class="alert alert-${alert.type} alert-dismissible" role="alert">` +
            '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
            `<strong>${alert.title}</strong> ${alert.body}` +
            '</div>');

        alertContainer.append(alertElement);
        alertElement.alert();
    }

    $(document).ajaxComplete((event, xhr) => {
        if (xhr.getResponseHeader('x-alert-type')) {
            const alert = {
                type: xhr.getResponseHeader('x-alert-type'),
                title: xhr.getResponseHeader('x-alert-title'),
                body: xhr.getResponseHeader('x-alert-body')
            }

            showAlert(alert);
        }
    });
})();


function formatDateForGraphLabel(date) {
    var strTime = date.getHours() + ':' + date.getMinutes() + ':' + date.getSeconds();
    return strTime;
}

if (window.frameElement) {
    document.getElementById('top-nav').classList.add('d-none');
    document.getElementById('side-nav').setAttribute('style', 'display:none !important');

    var mainElement = document.getElementsByTagName("main")[0];
    mainElement.className = "col-md-12 ml-sm-auto col-lg-12 pt-3 px-4";
}


$(function () {

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "1000",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": 0,
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut",
        "tapToDismiss": false
    }
})

// In your Javascript (external .js resource or <script> tag)
$(document).ready(function () {
    $.fn.select2.defaults.set("theme", "bootstrap");
    $('.js-example-basic-single').select2({ theme: 'bootstrap' });

    $('.js-example-basic-multiple').select2({ theme: 'bootstrap', maximumSelectionLength: 6 });

});
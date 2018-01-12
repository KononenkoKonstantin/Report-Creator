var PaymentController = function () {
    var element;

    var init = function () {
        $("span.delete").click(onDelete);        
    };

    
    var onDelete = function (e) {        
        e.preventDefault();        
        element = $(e.target);
        var paymentId = element.attr("data-payment-id");

        bootbox.confirm({
            message: "Are you sure you want to delete this payment?",
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-default'
                }                
            },
            callback: function (result) {
                if (!result) return;
                deletePayment(paymentId);
            }
        });
    }
    var deletePayment = function (paymentId) {
        $.ajax({
            url: "/payment/delete/" + paymentId,
            method: "POST"
        })
            .done(function () {
                element.closest('tr')
                    .fadeOut(function () {
                        $(this).remove();
                    })
                window.
            })
            .fail(fail);
    }

    var fail = function () { alert("Error!"); };

    return {
        init
    };
}();
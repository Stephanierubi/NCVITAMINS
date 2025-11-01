function showLoadingProcess(loading, message) {

    if (message == undefined) {
        var message = 'Process'
    }

    if (loading) {
        jQuery.blockUI({
            message: message,
            css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#000',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                '-ms-border-radius': '10px',
                opacity: .5,
                color: '#fff',
                'z-index': 2000
            }
        });

        $(".blockMsg").append('<br><i class="fa fa-spinner fa-spin" style="font-size:24px"></i>');
    } else {
        jQuery.unblockUI();
    }

}
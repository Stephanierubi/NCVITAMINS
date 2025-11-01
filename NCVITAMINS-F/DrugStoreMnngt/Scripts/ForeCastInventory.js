function setCurrentYear() {
    $('#Year').val($('#HCurrentYear').val());
    var typeForm = $('#FormCreate').val();
   
    if (typeForm == "DCFlow") {
        ShowDataDC();
    }
    else {
        ShowDataFrameUnit();
    }  
}
function GetDataToSaveDC() {
    var FrameDCFlow = new Array();
    var FrameDCFlowUnit = {};

    FrameDCFlowUnit = {
        Month: '1',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        DC: $('#DC').val(),
        TransferIn: $('#divJanuary :input#TransferIn').val(),
        TransferOut: $('#divJanuary :input#TransferOut').val(),
        PlacementOrders: $('#divJanuary :input#PlacementOrders').val(),
        OutboundToStore: $('#divJanuary :input#OutboundToStore').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '2',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        DC: $('#DC').val(),
        TransferIn: $('#divFebraury :input#TransferIn').val(),
        TransferOut: $('#divFebraury :input#TransferOut').val(),
        PlacementOrders: $('#divFebraury :input#PlacementOrders').val(),
        OutboundToStore: $('#divFebraury :input#OutboundToStore').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '3',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        DC: $('#DC').val(),
        TransferIn: $('#divMarch :input#TransferIn').val(),
        TransferOut: $('#divMarch :input#TransferOut').val(),
        PlacementOrders: $('#divMarch :input#PlacementOrders').val(),
        OutboundToStore: $('#divMarch :input#OutboundToStore').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '4',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        DC: $('#DC').val(),
        TransferIn: $('#divApril :input#TransferIn').val(),
        TransferOut: $('#divApril :input#TransferOut').val(),
        PlacementOrders: $('#divApril :input#PlacementOrders').val(),
        OutboundToStore: $('#divApril :input#OutboundToStore').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '5',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        DC: $('#DC').val(),
        TransferIn: $('#divMay :input#TransferIn').val(),
        TransferOut: $('#divMay :input#TransferOut').val(),
        PlacementOrders: $('#divMay :input#PlacementOrders').val(),
        OutboundToStore: $('#divMay :input#OutboundToStore').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '6',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        DC: $('#DC').val(),
        TransferIn: $('#divJune :input#TransferIn').val(),
        TransferOut: $('#divJune :input#TransferOut').val(),
        PlacementOrders: $('#divJune :input#PlacementOrders').val(),
        OutboundToStore: $('#divJune :input#OutboundToStore').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '7',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        DC: $('#DC').val(),
        TransferIn: $('#divJuly :input#TransferIn').val(),
        TransferOut: $('#divJuly :input#TransferOut').val(),
        PlacementOrders: $('#divJuly :input#PlacementOrders').val(),
        OutboundToStore: $('#divJuly :input#OutboundToStore').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '8',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        DC: $('#DC').val(),
        TransferIn: $('#divAugust :input#TransferIn').val(),
        TransferOut: $('#divAugust :input#TransferOut').val(),
        PlacementOrders: $('#divAugust :input#PlacementOrders').val(),
        OutboundToStore: $('#divAugust :input#OutboundToStore').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '9',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        DC: $('#DC').val(),
        TransferIn: $('#divSeptember :input#TransferIn').val(),
        TransferOut: $('#divSeptember :input#TransferOut').val(),
        PlacementOrders: $('#divSeptember :input#PlacementOrders').val(),
        OutboundToStore: $('#divSeptember :input#OutboundToStore').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '10',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        DC: $('#DC').val(),
        TransferIn: $('#divOctober :input#TransferIn').val(),
        TransferOut: $('#divOctober :input#TransferOut').val(),
        PlacementOrders: $('#divOctober :input#PlacementOrders').val(),
        OutboundToStore: $('#divOctober :input#OutboundToStore').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '11',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        DC: $('#DC').val(),
        TransferIn: $('#divNovember :input#TransferIn').val(),
        TransferOut: $('#divNovember :input#TransferOut').val(),
        PlacementOrders: $('#divNovember :input#PlacementOrders').val(),
        OutboundToStore: $('#divNovember :input#OutboundToStore').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '12',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        DC: $('#DC').val(),
        TransferIn: $('#divDecember :input#TransferIn').val(),
        TransferOut: $('#divDecember :input#TransferOut').val(),
        PlacementOrders: $('#divDecember :input#PlacementOrders').val(),
        OutboundToStore: $('#divDecember :input#OutboundToStore').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    $.ajax({
        url: $('#hiddenurl').val(),
        dataType: "html",
        data: JSON.stringify({ frameDCFlow: FrameDCFlow }),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (Response) {
            $('#divloader').attr('hidden', 'hidden');
            toastr.success('Information saved', "Information", {
                closeButton: true, timeOut: 3000, positionClass: "toast-top-full-width", showMethod: "fadeIn", hideMethod: "fadeOut", hideEasing: "linear"
            });
        },
        error: function (data) {

        }
    });
}
function GetDataToSaveFramUnits() {
    var FrameDCFlow = new Array();
    var FrameDCFlowUnit = {};

    FrameDCFlowUnit = {
        Month: '1',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        REC: $('#divJanuary :input#REC').val(),
        FutureSales: $('#divJanuary :input#FutureSales').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '2',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        REC: $('#divFebraury :input#REC').val(),
        FutureSales: $('#divFebraury :input#FutureSales').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '3',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        REC: $('#divMarch :input#REC').val(),
        FutureSales: $('#divMarch :input#FutureSales').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '4',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        REC: $('#divApril :input#REC').val(),
        FutureSales: $('#divApril :input#FutureSales').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '5',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        REC: $('#divMay :input#REC').val(),
        FutureSales: $('#divMay :input#FutureSales').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '6',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        REC: $('#divJune :input#REC').val(),
        FutureSales: $('#divJune :input#FutureSales').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '7',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        REC: $('#divJuly :input#REC').val(),
        FutureSales: $('#divJuly :input#FutureSales').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '8',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        REC: $('#divAugust :input#REC').val(),
        FutureSales: $('#divAugust :input#FutureSales').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '9',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        REC: $('#divSeptember :input#REC').val(),
        FutureSales: $('#divSeptember :input#FutureSales').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '10',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        REC: $('#divOctober :input#REC').val(),
        FutureSales: $('#divOctober :input#FutureSales').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '11',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        REC: $('#divNovember :input#REC').val(),
        FutureSales: $('#divNovember :input#FutureSales').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);

    FrameDCFlowUnit = {
        Month: '12',
        Year: $('#Year').val(),
        Category: $('#Category').val(),
        REC: $('#divDecember :input#REC').val(),
        FutureSales: $('#divDecember :input#FutureSales').val()
    };
    FrameDCFlow.push(FrameDCFlowUnit);
    //console.log(FrameDCFlow);
    $.ajax({
        url: $('#hiddenurl').val(),
        dataType: "html",
        data: JSON.stringify({ frameUnitsFlow: FrameDCFlow }),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (Response) {
            toastr.success('Information saved', "Information", {
                closeButton: true, timeOut: 3000, positionClass: "toast-top-full-width", showMethod: "fadeIn", hideMethod: "fadeOut", hideEasing: "linear"
            });
        },
        error: function (data) {
        }
    });
}
function ShowDataDC() {
    $.ajax({
        url: $('#hiddenurlGet').val(),
        dataType: "json",
        data: JSON.stringify({ Year: $('#Year').val(), Category: $('#Category').val(), DC: $('#DC').val() }),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (Response) {
            if (Response.length == 0) {
                var container = document.getElementById('AllYear');
                var inputs = container.getElementsByTagName('input');
                for (index = 0; index < inputs.length; ++index) {
                        inputs[index].value = '0';
                }
            }
            $.each(Response, function (i, obj) {
                switch (obj.Month) {
                    case 1:
                        $('#Year').val(obj.Year);
                        $('#Category').val(obj.Category);
                        $('#DC').val(obj.DC);
                        $('#divJanuary :input#TransferIn').val(obj.TransferIn);
                        $('#divJanuary :input#TransferOut').val(obj.TransferOut);
                        $('#divJanuary :input#PlacementOrders').val(obj.PlacementOrders);
                        $('#divJanuary :input#OutboundToStore').val(obj.OutboundToStore);
                        break;
                    case 2:
                        $('#divFebraury :input#TransferIn').val(obj.TransferIn);
                        $('#divFebraury :input#TransferOut').val(obj.TransferOut);
                        $('#divFebraury :input#PlacementOrders').val(obj.PlacementOrders);
                        $('#divFebraury :input#OutboundToStore').val(obj.OutboundToStore);
                        break;
                    case 3:
                        $('#divMarch :input#TransferIn').val(obj.TransferIn);
                        $('#divMarch :input#TransferOut').val(obj.TransferOut);
                        $('#divMarch :input#PlacementOrders').val(obj.PlacementOrders);
                        $('#divMarch :input#OutboundToStore').val(obj.OutboundToStore);
                        break;
                    case 4:
                        $('#divApril :input#TransferIn').val(obj.TransferIn);
                        $('#divApril :input#TransferOut').val(obj.TransferOut);
                        $('#divApril :input#PlacementOrders').val(obj.PlacementOrders);
                        $('#divApril :input#OutboundToStore').val(obj.OutboundToStore);
                        break;
                    case 5:
                        $('#divMay :input#TransferIn').val(obj.TransferIn);
                        $('#divMay :input#TransferOut').val(obj.TransferOut);
                        $('#divMay :input#PlacementOrders').val(obj.PlacementOrders);
                        $('#divMay :input#OutboundToStore').val(obj.OutboundToStore);
                        break;
                    case 6:
                        $('#divJune :input#TransferIn').val(obj.TransferIn);
                        $('#divJune :input#TransferOut').val(obj.TransferOut);
                        $('#divJune :input#PlacementOrders').val(obj.PlacementOrders);
                        $('#divJune :input#OutboundToStore').val(obj.OutboundToStore);
                        break;
                    case 7:
                        $('#divJuly :input#TransferIn').val(obj.TransferIn);
                        $('#divJuly :input#TransferOut').val(obj.TransferOut);
                        $('#divJuly :input#PlacementOrders').val(obj.PlacementOrders);
                        $('#divJuly :input#OutboundToStore').val(obj.OutboundToStore);
                        break;
                    case 8:
                        $('#divAugust :input#TransferIn').val(obj.TransferIn);
                        $('#divAugust :input#TransferOut').val(obj.TransferOut);
                        $('#divAugust :input#PlacementOrders').val(obj.PlacementOrders);
                        $('#divAugust :input#OutboundToStore').val(obj.OutboundToStore);
                        break;
                    case 9:
                        $('#divSeptember :input#TransferIn').val(obj.TransferIn);
                        $('#divSeptember :input#TransferOut').val(obj.TransferOut);
                        $('#divSeptember :input#PlacementOrders').val(obj.PlacementOrders);
                        $('#divSeptember :input#OutboundToStore').val(obj.OutboundToStore);
                        break;
                    case 10:
                        $('#divOctober :input#TransferIn').val(obj.TransferIn);
                        $('#divOctober :input#TransferOut').val(obj.TransferOut);
                        $('#divOctober :input#PlacementOrders').val(obj.PlacementOrders);
                        $('#divOctober :input#OutboundToStore').val(obj.OutboundToStore);
                        break;
                    case 11:
                        $('#divNovember :input#TransferIn').val(obj.TransferIn);
                        $('#divNovember :input#TransferOut').val(obj.TransferOut);
                        $('#divNovember :input#PlacementOrders').val(obj.PlacementOrders);
                        $('#divNovember :input#OutboundToStore').val(obj.OutboundToStore);
                        break;
                    case 12:
                        $('#divDecember :input#TransferIn').val(obj.TransferIn);
                        $('#divDecember :input#TransferOut').val(obj.TransferOut);
                        $('#divDecember :input#PlacementOrders').val(obj.PlacementOrders);
                        $('#divDecember :input#OutboundToStore').val(obj.OutboundToStore);
                        break;
                    default:
                }
            });
        },
        error: function (data) {
            console.log('error');
        }
    });
}
function ShowDataFrameUnit() {
    $.ajax({
        url: $('#hiddenurlGet').val(),
        dataType: "json",
        data: JSON.stringify({ Year: $('#Year').val(), Category: $('#Category').val() }),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (Response) {
            if (Response.length == 0) {
                var container = document.getElementById('AllYear');
                var inputs = container.getElementsByTagName('input');
                for (index = 0; index < inputs.length; ++index) {
                    inputs[index].value = '0';
                }
            }
            $.each(Response, function (i, obj) {
               
                switch (obj.Month) {
                    case 1:
                        $('#Year').val(obj.Year);
                        $('#Category').val(obj.Category);
                        $('#divJanuary :input#REC').val(obj.REC);
                        $('#divJanuary :input#FutureSales').val(obj.FutureSales);
                        break;
                    case 2:
                        $('#divFebraury :input#REC').val(obj.REC);
                        $('#divFebraury :input#FutureSales').val(obj.FutureSales);
                        break;
                    case 3:
                        $('#divMarch :input#REC').val(obj.REC);
                        $('#divMarch :input#FutureSales').val(obj.FutureSales);
                        break;
                    case 4:
                        $('#divApril :input#REC').val(obj.REC);
                        $('#divApril :input#FutureSales').val(obj.FutureSales);
                        break;
                    case 5:
                        $('#divMay :input#REC').val(obj.REC);
                        $('#divMay :input#FutureSales').val(obj.FutureSales);
                        break;
                    case 6:
                        $('#divJune :input#REC').val(obj.REC);
                        $('#divJune :input#FutureSales').val(obj.FutureSales);
                        break;
                    case 7:
                        $('#divJuly :input#REC').val(obj.REC);
                        $('#divJuly :input#FutureSales').val(obj.FutureSales);
                        break;
                    case 8:
                        $('#divAugust :input#REC').val(obj.REC);
                        $('#divAugust :input#FutureSales').val(obj.FutureSales);
                        break;
                    case 9:
                        $('#divSeptember :input#REC').val(obj.REC);
                        $('#divSeptember :input#FutureSales').val(obj.FutureSales);
                        break;
                    case 10:
                        $('#divOctober :input#REC').val(obj.REC);
                        $('#divOctober :input#FutureSales').val(obj.FutureSales);
                        break;
                    case 11:
                        $('#divNovember :input#REC').val(obj.REC);
                        $('#divNovember :input#FutureSales').val(obj.FutureSales);
                        break;
                    case 12:
                        $('#divDecember :input#REC').val(obj.REC);
                        $('#divDecember :input#FutureSales').val(obj.FutureSales);
                        break;
                    default:
                }
            });
        },
        error: function (data) {
            console.log('error');
        }
    });
}

var loading = $('#divWait').hide();
$(document).ready(function () {
    setCurrentYear();
    $(document)
        .ajaxStart(function () {
            loading.show();
        })
        .ajaxStop(function () {
            loading.hide();
        });
});

$('#btnSaveDC').click(function (e) {
    GetDataToSaveDC();
});
$('#btnSaveFrameUnits').click(function (e) {
    GetDataToSaveFramUnits();
});
$('#Year').change(function () {
    var typeForm = $('#FormCreate').val();
    if (typeForm == "DCFlow") {
        ShowDataDC();
    }
    else {
        ShowDataFrameUnit();
    }   
});
$('#DC').change(function () {
    ShowDataDC();
});
$('#Category').change(function () {
    var typeForm = $('#FormCreate').val();
    if (typeForm == "DCFlow") {
        ShowDataDC();
    }
    else {
        ShowDataFrameUnit();
    }
});
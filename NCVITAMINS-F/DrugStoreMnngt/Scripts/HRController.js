$('#pendingrequests').click(function () {
    LoadInitalTable();
    $("#myModalSave").modal('show');
});

function LoadInitalTable() {
    if ($.fn.DataTable.isDataTable('#tbllistado')) {
        $('#tbllistado').DataTable().destroy();
    }
    var oTable = $('#tbllistado').DataTable({
        ajax: {
            url: $('#hiddenurl').val(),
            dataSrc: "",
        },
        paging: false,
        columns: [
            { data: "employeeid" },
            { data: "employeename" },
        ],
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf'
        ],
        initComplete: function () {
            this.api().columns().every(function () {
                var that = this;
                $('input', this.header()).on('keyup change', function () {
                    if (that.search() !== this.value) {
                        that
                            .search(this.value)
                            .draw();
                    }
                });
            });
        }
    });
}


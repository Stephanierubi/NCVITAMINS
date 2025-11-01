function GetStore(storenumber, urlmethod, table) {
    var a = {};
    a.url = urlmethod;
    a.type = "POST";
    a.data = JSON.stringify({ StoreNumber: storenumber });
    a.datatype = "json";
    a.contentType = "application/json";
    a.success = function (List) {
        table.row.add([List.StoreNumber, List.Storename]).draw(true);
    };
    a.error = function () { alert("Error Getting List"); };
    $.ajax(a);
}
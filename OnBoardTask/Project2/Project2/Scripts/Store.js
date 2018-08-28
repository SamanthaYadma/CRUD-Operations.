updateStoreTable();
function addStore() {
    var store = {
        Storename: $("#addStoreModal #StoreName").val(),
        StoreAddress: $("#addStoreModal #StoreAddress").val()
    };
    $.post("/Stores/AddStore", store, function (data, status) {
        $("#addStoreModal #StoreName").val('');
        $("#addStoreModal #StoreAddress").val('');

        updateStoreTable();
    });
}
function editStore() {
    var store = {
        StoreId: $("#StoreId").val(),
        StoreName: $("#editStoreModal #StoreName").val(),
        StoreAddress: $("#editStoreModal #StoreAddress").val()
    };

    $.post("/Stores/EditStore", store, function (data, status) {
        updateStoreTable();
    });
}
function deleteStore() {
    var data = {
        StoreId: $("#StoreId").val()
    }

    $.post("/Stores/DeleteStore", data, function (data, status) {
        updateStoreTable();
    });
}
function openEditStoreModal(store) {
    $("#editStoreModal").modal("toggle");
    $("#StoreId").val(store.StoreId);
    $("#editStoreModal #StoreName").val(store.StoreName);
    $("#editStoreModal #StoreAddress").val(store.StoreAddress);
}
function openDeleteStoreModal(store) {
    $("#deleteStoreModal").modal("toggle");
    $("#StoreId").val(store.StoreId);
    $("#deleteStoreModal #StoreName").text(store.StoreName);
    $("#deleteStoreModal #StoreAddress").text(store.StoreAddress);
}
function updateStoreTable() {
    $.get("/Stores/GetStore", function (data, status) {
        $("#storeTable tr td").remove();

        for (var i = 0; i < data.length; i++) {
            updateStoreRow(i, data[i]);
        }
    });
}
function updateStoreRow(index, store) {
    $("#storeTable").append("<tr><td>" + store.StoreName + "</td>" +
        "<td>" + store.StoreAddress + "</td>" +
        "<td><button type='button' class='btn btn-warning' onclick='openEditStoreModal(" + JSON.stringify(store) + ")'><span class= 'glyphicon glyphicon-edit'></span> Edit</button></td>" +
        "<td><button type='button' class='btn btn-danger' onclick='openDeleteStoreModal(" + JSON.stringify(store) + ")'><span class='glyphicon glyphicon-trash'></span> Delete</button></td></tr>");
}


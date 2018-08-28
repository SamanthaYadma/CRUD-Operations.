updateSaleTable();
function addSale() {
    var sale = {
        // Get data from the form
        CustomerId: $("#addSaleModal #CustomerId").val(),
        ProductId: $("#addSaleModal #ProductId").val(),
        StoreId: $("#addSaleModal #StoreId").val(),
        DateSold: $("#addSaleModal #DateSold").val()
    };
    $.ajax({
        type: "POST",
        url: "/ProductSolds/AddSale",
        data: sale,
        error: function (xhr, statusText, error) {
            alert("Error: " + statusText + " " + error);
        },
        success: function (data) {
            updateSaleTable();
        }
    });
}
function editSale() {
    var sale = {
        id: $("#Id").val(),
        customerId: $("#editSaleModal #CustomerId").val(),
        productId: $("#editSaleModal #ProductId").val(),
        storeId: $("#editSaleModal #StoreId").val(),
        dateSold: $("#editSaleModal #DateSold").val()
    };

    $.post("/ProductSolds/EditSale", sale, function (data, status) {
        updateSaleTable();
    });
}
function deleteSale() {
    var data = {
        id: $("#Id").val()
    }

    $.post("/ProductSolds/DeleteSale", data, function (data, status) {
        updateSaleTable();
    });
}
function openEditSaleModal(sale) {
    var dateObj = new Date(parseInt(sale.DateSold.substr(6)));
    var date = (dateObj.getMonth() + 1) + '/' + dateObj.getDate() + '/' + dateObj.getFullYear();
   $("#editSaleModal").modal("toggle");
    $("#Id").val(sale.Id);
    $("#editSaleModal #CustomerId").val(sale.CustomerId);
    $("#editSaleModal #ProductId").val(sale.ProductId);
    $("#editSaleModal #StoreId").val(sale.StoreId);
    $("#editSaleModal #DateSold").val(date);
}
function openDeleteSaleModal(sale) {
    var dateObj = new Date(parseInt(sale.DateSold.substr(6)));
    var date = (dateObj.getMonth() + 1) + '/' + dateObj.getDate() + '/' + dateObj.getFullYear();

    $("#deleteSaleModal").modal("toggle");
    $("#Id").val(sale.Id);
    $("#deleteSaleModal #CustomerId").val(sale.CustomerId);
    $("#deleteSaleModal #ProductId").val(sale.ProductId);
    $("#deleteSaleModal #StoreId").val(sale.StoreId);
    $("#deleteSaleModal #DateSold").val(date);
}
function updateSaleTable() {
    $.get("/ProductSolds/GetSales", function (data, status) {
        $("#saleTable tr td").remove();
        for (var i = 0; i < data.length; i++) {
            updateSaleRow(i, data[i]);
        }
    });
}

function updateSaleRow(index, productSolds) {
    var dateObj = new Date(parseInt(productSolds.DateSold.substr(6)));
    var date = (dateObj.getMonth()) + '/' + dateObj.getDate() + '/' + dateObj.getFullYear();

    $("#saleTable").append("<tr><td>" + date + "</td>" + "<td>" + productSolds.CustomerName + "</td>"
        + "<td>" + productSolds.ProductName + "</td>"
        + "<td>" + productSolds.StoreName + "</td>"
        + "<td><button type='button' class='btn btn-warning' onclick='openEditSaleModal(" + JSON.stringify(productSolds) + ")'><span class= 'glyphicon glyphicon-edit'></span> Edit</button></td>" +
        "<td><button type='button' class='btn btn-danger' onclick='openDeleteSaleModal(" + JSON.stringify(productSolds) + ")'><span class='glyphicon glyphicon-trash'></span> Delete</button></td></tr>");
}
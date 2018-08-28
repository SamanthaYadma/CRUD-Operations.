updateProductTable();
function addProduct() {
    var product = {
        ProductName: $("#addProductModal #ProductName").val(),
        price: $("#addProductModal #Price").val()
    };
    $.post("/Products/AddProduct", product, function (data, status) {
        $("#addProductModal #ProductName").val('');
        $("#addProductModal #Price").val('');

        updateProductTable();
    });
}
function editProduct() {
    var product = {
        Productid: $("#ProductId").val(),
        ProductName: $("#editProductModal #ProductName").val(),
        price: $("#editProductModal #Price").val()
    };
    $.post("/Products/EditProduct", product, function (data, status) {
        updateProductTable();
    });
}
function deleteProduct() {
    var data = {
        ProductId: $("#ProductId").val()
    }
    $.post("/Products/DeleteProduct", data, function (data, status) {
        updateProductTable();
    });
}
function openEditProductModal(product) {
    $("#editProductModal").modal("toggle");
    $("#ProductId").val(product.ProductId);
    $("#editProductModal #ProductName").val(product.ProductName);
    $("#editProductModal #Price").val(product.Price);
}
function openDeleteProductModal(product) {
    $("#deleteProductModal").modal("toggle");
    $("#ProductId").val(product.ProductId);
    $("#deleteProductModal #ProductName").text(product.ProductName);
    $("#deleteProductModal #Price").text(product.Price);
}
function updateProductTable() {
    $.get("/Products/GetProducts", function (data, status) {
        $("#productTable tr td").remove();

        for (var i = 0; i < data.length; i++) {
            updateProductRow(i, data[i]);
        }
    });
}
function updateProductRow(index, product) {
    $("#productTable").append("<tr><td>" + product.ProductName + "</td>" +
        "<td>" + product.Price + "</td>" +
        "<td><button type='button' class='btn btn-warning' onclick='openEditProductModal(" + JSON.stringify(product) + ")'><span class= 'glyphicon glyphicon-edit'></span> Edit</button></td>" +
        "<td><button type='button' class='btn btn-danger' onclick='openDeleteProductModal(" + JSON.stringify(product) + ")'><span class='glyphicon glyphicon-trash'></span> Delete</button></td></tr>");
}



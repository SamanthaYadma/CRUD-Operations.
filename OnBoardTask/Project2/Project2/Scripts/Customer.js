updateCustomer();
function addCustomer() {
    var customer = {
        CustomerName: $("#addCustomerModal #CustomerName").val(),
        CustomerAddress: $("#addCustomerModal #CustomerAddress").val()
    };
    $.ajax({
        type: "POST",
        url: "/Customers/addCustomer",
        data: customer,
        error: function (xhr, statusText, error) {
            alert("Error: " + statusText + " " + error);
        },
        success: function (data) {
            $("#addCustomerModal #CustomerName").val('');
            $("#addCustomerModal #CustomerAddress").val('');
            updateCustomer();
        }
    })
}
function editCustomer() {
    var customer = {
        CustomerId: $("#CustomerId").val(),
        CustomerName: $("#editCustomerModal #CustomerName").val(),
        CustomerAddress: $("#editCustomerModal #CustomerAddress").val()
    };
    $.post("/Customers/EditCustomer", customer, function (data, status) {
        updateCustomer();
    });   
}
function deleteCustomer() {
    var data = {
        CustomerId: $("#CustomerId").val()
    }
    $.post("/Customers/DeleteCustomer", data, function (data, status) {
        updateCustomer();
    });
}
function openEditCustomerModal(customer) {
    $("#editCustomerModal").modal("toggle");
    $("#CustomerId").val(customer.CustomerId);
    $("#editCustomerModal #CustomerName").val(customer.CustomerName);
    $("#editCustomerModal #CustomerAddress").val(customer.CustomerAddress);

}
function openDeleteCustomerModal(customer) {
    $("#deleteCustomerModal").modal("toggle");
    $("#CustomerId").val(customer.CustomerId);
    $("#deleteCustomerModal #CustomerName").text(customer.CustomerName);
    $("#deleteCustomerModal #CustomerAddress").text(customer.CustomerAddress);
}
function updateCustomer() {
    $.ajax({
        type: "GET",
        url: "/Customers/GetCustomers",
        contentType: "application/json; charset=utf-8",
        error: function (xhr, statusText, error) {
            alert("Error: " + statusText + " " + error);
        },
        success: function (data) {
            $("#Customer tr td").remove();
            for (var i = 0; i < data.length; i++) {
                updateCustomerRow(i, data[i]);
            }
        }

        })
}
function updateCustomerRow(index, customer) {
    $("#Customer").append("<tr><td>" + customer.CustomerName + "</td>" + "<td>" + customer.CustomerAddress + "</td>"
        + "<td><button type='button' class='btn btn-warning' onclick='openEditCustomerModal(" + JSON.stringify(customer) + ")'><span class= 'glyphicon glyphicon-edit'></span> Edit</button></td>" +
        "<td><button type='button' class='btn btn-danger' onclick='openDeleteCustomerModal(" + JSON.stringify(customer) + ")'><span class='glyphicon glyphicon-trash'></span> Delete</button></td></tr>");
}
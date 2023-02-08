
$(document).ready(function () {
    $("#parents").(function (e) { 
        $.ajax({
            type: "method",
            url: "/Admin/ProductCategory/GetParents",
            data: e,
            dataType: "int",
            success: function (response) {
                debugger;
            }
        });
    });
});
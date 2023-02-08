$(document).ready(function () {
  var AddChild = function (pid) {
    $.ajax({
      url: "/Admin/ProductCategory/GetParents",
      method: "get",
      data: { parentId: pid },
    }).done(function (res) {
      if ($.trim(res) != null) {
        $("#childs").attr("name", "");
        $("#childs").attr("id", "");
        $("#subCat").html(res);
        $("#childs").on("change", AddChild($(this).val()));
      } else {
        $("#subCat").empty();
      }
    });
  };

  $("#parents")
    .css("margin", "10px 0")
    .on("change", function () {
      var pId = $(this).val();
      // $("#subCat").empty();

      $.ajax({
        url: "/Admin/ProductCategory/GetParents",
        method: "get",
        data: { parentId: pId },
      }).done(function (res) {
        if ($.trim(res) != null) {
          // $("#childs").attr("name", "");
          // $("#childs").attr("id", "");
          $("#subCat").html(res);
          $("#childs").on("change", AddChild($(this).val()));
        } else {
          $("#subCat").empty();
        }
      });
    });

  // if ($.trim(res) != null) {
  //   $("#parents").attr("name", "");
  //   $("#subCat").html(res);
  // } else {
  //   $("#subCat").empty();
  // }

  // $("#subCat").on("click", function () {
  //   $("#childs")
  //     .css("margin", "10px 0")
  //     .on("change", function () {
  //       var id = $(this).val();

  //       $.ajax({
  //         url: "/Admin/ProductCategory/GetParents",
  //         method: "get",
  //         data: { parentId: id },
  //       }).done(function (res) {
  //         if ($.trim(res) != null) {
  //           $("#childs").attr("name", "");
  //           $("#childs").after(res);
  //         }
  //       });
  //     });
  // });

  // $("#submit").click(function (e) {
  //   if ($("#childs").val() == null) {
  //     $("#parents").attr("name", "ParentId");
  //     $("#childs").attr("name", "");
  //   }
  // });
});

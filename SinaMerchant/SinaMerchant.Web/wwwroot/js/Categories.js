$(document).ready(function () {
  $("#subCat").prev().hide();
  $("#parents")
    .css("margin", "10px 0")
    .on("change", function () {
      if ($("#childs").length != 0) $("#subCat").empty();
      var id = $(this).val();
      var list = (function () {
        return AddChild(id);
      })();

      $("#subCat").html(list);
      $("#subCat").prev().show();
      $("#childs").on("change", function () {
        debugger;
        $(".subCategory").remove();
        var id = $(this).val();
        var list = (function () {
          return AddChild(id);
        })();
        if (list) {
          $("#childs").attr("name", "");
          $("#childs").attr("id", "");
          $("#subCat").append(list);
          $("#childs").addClass("subCategory");
        }
        $("#childs").on("change", function () {
          debugger;
          $(".subCategory2").remove();
          var id = $(this).val();
          var list = (function () {
            return AddChild(id);
          })();
          if (list) {
            $("#childs").attr("name", "");
            $("#childs").attr("id", "");
            $("#subCat").append(list);
            $("#childs").addClass("subCategory subCategory2");
          }
        });
      });
    });

  function AddChild(pId) {
    var tmp = null;
    $.ajax({
      async: false,
      url: "/Admin/ProductCategory/GetParents",
      method: "get",
      data: { parentId: pId },
      success: function (res) {
        tmp = res;
      },
    });
    return tmp;
  }

  $("#submit").click(function (e) {
    debugger;
    if (
      $("#childs").val() == "none" &&
      $(".subCategory:eq(0)").val() == $("#childs").val()
    ) {
      $("#childs").attr("name", "");
      $("#parents").attr("name", "ParentId");
    } else if (
      $("#childs").val() == "none" &&
      $(".subCategory:eq(0)").val() != $("#childs").val()
    ) {
      $("#childs").attr("name", "");
      $("#childs").prev().attr("name", "ParentId");
    }
  });
});

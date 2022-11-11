//Tìm kiếm, hiển thị dữ liệu dưới dạng phân trang
function paginationSearch(page) {
    var link = $("#formSearch").prop("action");
    var data = $("#formSearch").serializeArray();
    data.push({ "name": "page", "value": page });
    $.ajax({
        url: link,
        type: "POST",
        data: data,
        async: false,
        error: function () {
            alert("Your request is not valid!");
        },
        success: function (data) {
            $("#searchResult").html(data);
        }
    });
}
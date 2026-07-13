$(document).ready(function () {

    var table = $("#mytable").DataTable({
        ajax: {
            url: "/Product/GetData",
            type: "GET",
            dataSrc: "data"
        },
        columns: [
            { data: "name" },
            { data: "description" },
            { data: "price" },
            { data: "categoryName" },
            {
                data: "id",
                render: function (id) {
                    return `
                        <a href="/Product/Edit/${id}" class="btn btn-success btn-sm">
                            <i class="fa-solid fa-pen"></i>
                        </a>

                        <button class="btn btn-danger btn-sm delete-btn" data-id="${id}">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    `;
                }
            }
        ]
    });

    $(document).on("click", ".delete-btn", function () {

        var id = $(this).data("id");

        $.ajax({
            url: "/Product/Delete/" + id,
            type: "DELETE",
            success: function (res) {

                if (res.success) {
                    table.ajax.reload(null, false);
                }

                alert(res.message);
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            }
        });

    });

});
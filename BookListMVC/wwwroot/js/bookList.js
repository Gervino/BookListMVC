var dataTable;

$(document).ready(function () {
    loadDataTable();
});



function loadDataTable() {
    dataTable = $("#DT_load").DataTable({
        "ajax": {
            "url": "/books/GetAll/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "20%" },
            { "data": "name", "width": "20%" },
            { "data": "author", "width": "20%" },
            { "data": "isbn", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center"> 
                            <a href="/Books/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width: 70px;'>
                                Edit
                            </a>
                            &nbsp;
                            <a class='btn btn-danger text-white' style='cursor:pointer; width: 70px;' onclick="Delete('/books/Delete?id='+${data})">
                                Delete
                            </a>
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "No Data Found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal.fire({
        icon: "warning",
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        showCancelButton: true,
        showCloseButton: true

    }).then((willDelete) => {
        console.log("will.delete ", willDelete);
        if (willDelete.value == true) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    console.log("data ", data);
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

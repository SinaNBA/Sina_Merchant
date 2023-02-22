function RemoveCategory(CategoryId) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.get("/Admin/ProductCategory/Delete/" + CategoryId).then(res => {
                console.log(res);

                $("#category-" + CategoryId).fadeOut();

                Swal.fire(
                    'Deleted!',
                    'Your file has been deleted.',
                    'success'
                );
            });
        }
    });
}

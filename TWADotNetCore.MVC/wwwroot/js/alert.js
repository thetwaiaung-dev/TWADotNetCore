function successMessage(message,url) {
    Swal.fire({
        title: "Success!",
        text: message,
        icon: "success",
        showDenyButton: false,
        showCancelButton: false,
        confirmButtonText: "Ok",
    }).then((result) => {
        if (result.isConfirmed && url != undefined) {
            location.href = url;
        }
    });
}

function errorMessage(message) {
    Swal.fire({
        title: "Error!",
        text: message,
        icon: "error",
    });
}

function confirmDelete(message, id) {
    return new Promise(function (resolve, reject) {
        Swal.fire({
            title: "Confirm",
            text: message,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes",
        }).then((result) => {
            if (result.isConfirmed) {
                resolve(id);
            } else {
                reject("User canceled the deletion.");
            }
        });
    });
}
window.ShowToastrMsg = (type, message) => {
    if (type === "error") {
        toastr.error(message, "Operation Failed", { timeOut: 6000 });
    }
}
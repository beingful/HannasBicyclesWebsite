$("#form").submit(function (event) {

    var vForm = $(this)

    if (vForm[0].checkValidity() === false) {
        console.log(3)
        event.preventDefault()
        event.stopPropagation()
    }

    vForm.addClass('was-validated');
});
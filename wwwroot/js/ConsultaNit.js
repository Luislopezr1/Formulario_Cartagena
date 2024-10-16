
$(document).ready(function () {
    $('#nitForm').submit(function (event) {
        event.preventDefault(); // Evitar el envío tradicional del formulario
        let nitValue = $('#input-basico-id').val();
        let formData = new FormData();
        formData.append("Nit", nitValue);
        $.ajax({
            url: "Home/GuardarDatos",
            type: 'POST',
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (Array.isArray(response)) {
                    $.each(response, function (index, item) {
                        $('#resultContainer').add('<li>' + item + '</li>');
                    });
                } else {
                    $('#resultContainer').html('<p>' + response + '</p>');
                }
            },
            error: function (xhr, status, error) {
                console.error("Error en la solicitud AJAX:", error);
                alert("Ocurrió un error al procesar los datos: " + xhr.responseText);
            }
        });
    });
});
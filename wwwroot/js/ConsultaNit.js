$(document).ready(function () {
    $('#nitForm').submit(function (event) {
        event.preventDefault(); // Evitar el envío tradicional del formulario
        let nitValue = $('#input-basico-id').val();
        let ccValue = $('#input-basico-cc').val();
        let formData = new FormData();
        formData.append("Nit", nitValue);
        formData.append("Cc", ccValue);
        $.ajax({
            url: "Home/GuardarDatos",
            type: 'POST',
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                if (Array.isArray(response)) {
                    let lista = '';
                    $.each(response, function (index, item) {
                        lista += '<li>' + item + '</li>'
                    });
                    $('#resultContainer').html(lista);
                } else {
                    $('#resultContainer').html('<p>' + response.message + '</p>');
                }
            },
            error: function (xhr, status, error) {
                console.error("Error en la solicitud AJAX:", error);
                alert("Ocurrió un error al procesar los datos: " + xhr.responseText);
            }
        });
    });
});
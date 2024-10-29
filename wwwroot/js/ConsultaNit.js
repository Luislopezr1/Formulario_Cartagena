
$(document).ready(function () {
    $('#nitForm').submit(function (event) {
        event.preventDefault(); // Evitar el envío tradicional del formulario
        let nitValue = $('#input-basico-id').val();
        let ccValue = $('#input-basico-cc').val();
        let formData = new FormData();
        formData.append("Nit", nitValue);
        formData.append("Cc", ccValue);

        let evento = event;
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
                    nextItemAdvanceLineIVertical(evento);
                } else {
                    openModal(response.message);
                }
            },
            error: function (xhr) {
                openModal("Ocurrió un error al procesar los datos: " + xhr.responseText);
            }
        });
    });
});

function openModal(modalMensaje) {
    let modalItem = document.getElementById('modal_error');
    modalItem.style.display = 'block'
    document.getElementById("mensajeModal").innerHTML = `${modalMensaje}`;
}

function cerrar() {
    document.getElementById('modal_error').style.display = 'none';
}

function verificar(e) {
}

function updateProgressAdvanceLine(headers, indexActive, elementParent, wh) {
    // porcentajes establecidos
    const porcent = [15, 50, 80, 100];
    const elementActive = headers[indexActive];
    const elementIndicator = elementActive.querySelector('.indicator-linea-avance-govco');
    const attributePorcentage = elementIndicator.getAttribute('percentage');
    const percentage = attributePorcentage ? attributePorcentage : porcent[indexActive];
    const elementProgress = elementParent.querySelector('.progress-bar');
    elementProgress.style[wh] = percentage + "%";
    elementProgress.setAttribute('aria-valuenow', percentage);
}

function nextItemAdvanceLineIVertical(e) {
    const bodyActive = e.target.closest('.body-linea-avance-govco');
    const idParent = bodyActive.getAttribute('data-la-parent');
    const elementParent = document.querySelector(idParent);
    const items = elementParent.querySelectorAll('.item-linea-avance-govco');

    const indexActive = getActiveItemAdvanceLine(items);
    items[indexActive].closest('.item-linea-avance-govco').classList.remove('active-linea-avance-govco');
    items[indexActive].closest('.item-linea-avance-govco').classList.add('checked-linea-avance-govco');
    items[indexActive + 1].closest('.item-linea-avance-govco').classList.add('active-linea-avance-govco');
    items[indexActive + 1].closest('.item-linea-avance-govco').classList.remove('checked-linea-avance-govco');

    updateProgressAdvanceLine(items, indexActive + 1, elementParent, 'height');

}
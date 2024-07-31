var datatable;

$(document).ready(function () {
    $("#contenedor").validate({
        rules: {
            txtlugarEvento: {
                required: true
            },
            txtDescripcionEvento: {
                required: true
            },
            txtValorEvento: {
                required: true,
                number: true
            }
        },
        messages: {
            txtlugarEvento: {
                required: "El lugar del evento es obligatorio"
            },
            txtDescripcionEvento: {
                required: "La descripción del evento es obligatoria"
            },
            txtValorEvento: {
                required: "El valor del evento es obligatorio",
                number: "Por favor ingrese un valor numérico válido"
            }
        },
        errorPlacement: function (error, element) {
            // Mostrar los errores en el lblError
            $("#lblError").html(error);
            $("#lblError").show();
        },
        submitHandler: function (form) {
            $("#lblError").hide();
        }
    });

    cargaDatos();
});

function abrirModalEventos(json) {
    try {
        $("#txtIdEvento").val('0');
        $("#txtFechaEvento").val('');
        $("#txtLugarEvento").val('');
        $("#txtDescripcionEvento").val('');
        $("#txtValorEvento").val('0');
        $("#lblError").hide();
        if (json != null) {
            var fechaEventoFormateada = moment(json.fechaEvento).format('DD/MM/YYYY HH:mm:ss');
            $("#txtIdEvento").val(json.idEvento);
            $("#txtFechaEvento").val(json.fechaEvento);
            $("#txtlugarEvento").val(json.lugarEvento);
            $("#txtDescripcionEvento").val(json.descripcionEvento);
            $("#txtValorEvento").val(json.precio);
        }
        $("#formModal").modal("show");
    } catch (error) {
        alert(error);
    }
}


function cargaDatos() {
    try {
        if (datatable != null) {
            datatable.destroy();
        }

        datatable = $('#tbl').DataTable({
            "ajax": {
                type: "GET",
                url: '/miControlador/GetEventos',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataSrc: "eventos",
            },
            "columns": [
                {
                    "targets": 0,
                    "className": "text-center",
                    "data": "fechaEvento",
                    "render": function (data) {
                        return moment(data).format('DD-MM-YYYY HH:mm:ss');
                    }
                },
                {
                    "targets": 1,
                    "className": "text-center",
                    "data": "lugarEvento"
                },
                {
                    "targets": 2,
                    "className": "text-center",
                    "data": "descripcionEvento"
                },
                {
                    "targets": 3,
                    "className": "text-center",
                    "data": "precio",
                    "render": function (data) {
                        return `$${data.toFixed(2)}`;
                    }
                },
                {
                    "targets": 4,
                    "className": "text-center",
                    "data": "fechaCreacion",
                    "render": function (data) {
                        return moment(data).format('DD-MM-YYYY HH:mm:ss');
                    }
                },
                {
                    "defaultContent": '<button type="button" class="btn btn-primary btn-sm btn-editar-evento"><i class="fas fa-pen"></i></button>' +
                        '<button type="button" class="btn btn-danger btn-sm ms-2 btn-eliminar-evento"><i class="fas fa-trash "></i></button>',
                    "orderable": false,
                    "searchable": false,
                    "width": "90px"
                }
            ],
            "language": {
                url: "/JSON/es_datatable.json"
            }
        });
    } catch (error) {
        alert(error);
    }
}

$("#tbl tbody").on("click", '.btn-editar-evento', function () {
    var filaSeleccionada = $(this).closest("tr");
    var data = datatable.row(filaSeleccionada).data();
    abrirModalEventos(data);
});

$("#tbl tbody").on("click", '.btn-eliminar-evento', function () {
    try {
        var filaSeleccionada = $(this).closest("tr");
        var data = datatable.row(filaSeleccionada).data();
        Swal.fire({
            title: data.descripcionEvento, // Asume que descripción es el nombre del evento
            text: "¿Desea eliminar el evento?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Sí",
            cancelButtonText: "No",
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "DELETE",
                    url: '/miControlador/EliminarEvento?id=' + data.idEvento, 
                    data: '',
                    contentType: "application/json",
                    dataType: "json",
                    success: function (response) {
                        if (response.resultado) {
                            cargaDatos();
                        } else {
                            $("#lblEliminadoEvento").show().text(response.mensaje);
                        }
                    },
                    error: function (error) {
                        alert(error.responseText);
                    }
                });
            }
        });
    } catch (error) {
        alert(error);
    }
});

function GuardarDatos() {
    try {
        var idEvento = $('#txtIdEvento').val();
        if ($("#contenedor").valid()) {
            const evento = {
                idEvento: idEvento,
                fechaEvento: $('#txtFechaEvento').val(),
                lugarEvento: $('#txtlugarEvento').val(),
                descripcionEvento: $('#txtDescripcionEvento').val(),
                precio: $('#txtValorEvento').val()
            };
            if (idEvento == 0) {
                //ES NUEVO
                jQuery.ajax({
                    type: "POST",
                    url: '/miControlador/CrearEvento',
                    data: JSON.stringify(evento),
                    contentType: "application/json",
                    dataType: "json",
                    success: function (data) {
                        $(".card").LoadingOverlay("hide");
                        if (data.resultado == true) {
                            Swal.fire({
                                title: "Registro correcto",
                                text: "",
                                icon: "success"
                            });
                            $("#formModal").modal("hide");
                            cargaDatos();
                        } else {
                            Swal.fire({
                                title: "Error",
                                text: data.mensaje,
                                icon: "error"
                            });
                        }


                    },
                    error: function (error) {
                        $(".card").LoadingOverlay("hide")
                        alert(error);
                    },
                    beforeSend: function () {
                        $(".card").LoadingOverlay("show", {
                            imageResizeFactor: 2,
                            Text: "GUARDANDO.....",
                            size: 14
                        })
                    }
                })
            } else {
                jQuery.ajax({
                    type: "PUT",
                    url: '/miControlador/actualizarEvento',
                    data: JSON.stringify(evento),
                    contentType: "application/json",
                    dataType: "json",
                    success: function (data) {
                        $(".card").LoadingOverlay("hide");
                        if (data.resultado == true) {
                            Swal.fire({
                                title: "Actualizacion correcta",
                                text: "",
                                icon: "success"
                            });
                            $("#formModal").modal("hide");
                            cargaDatos();
                        } else {
                            Swal.fire({
                                title: "Error",
                                text: data.mensaje,
                                icon: "error"
                            });
                        }


                    },
                    error: function (error) {
                        $(".card").LoadingOverlay("hide")
                        alert(error);
                    },
                    beforeSend: function () {
                        $(".card").LoadingOverlay("show", {
                            imageResizeFactor: 2,
                            Text: "GUARDANDO.....",
                            size: 14
                        })
                    }
                })
            }
        } 
        

      

    } catch (e) {
        alert(e);
    }
}

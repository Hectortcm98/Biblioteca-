$(document).ready(function () {

    // Llama a las funciones para obtener todos los libros y todas las editoriales al cargar la página
    LibroGetAll();
    EditorialGetAll();

    // Función para obtener todos los libros
    function LibroGetAll() {
        $.ajax({
            url: "https://localhost:7099/api/Libro/Libro/GetAll",
            type: "GET",
            dataType: "json",
            success: function (result) {
                $("#showLibros").empty(); // Limpia el contenedor antes de agregar nuevas tarjetas
                if (result && result.success) {
                    $.each(result.data, function (i, libro) {
                        var nombreEditorial = libro.editorial ? libro.editorial.nombre : 'Sin editorial';
                        var imagenUrl = libro.Imagen || 'https://via.placeholder.com/150'; // URL de imagen por defecto si no hay imagen
                        var fechaPublicacion = new Date(libro.añoPublicacion).toLocaleDateString('es-ES', { year: 'numeric', month: 'long', day: 'numeric' });
                        var cardHtml = `
                            <div class="col-md-4 mb-4">
                                <div class="card">
                                    <img src="${imagenUrl}" class="card-img-top" alt="...">
                                    <div class="card-body">
                                        <h5 class="card-title">${libro.tituloLibro}</h5>
                                        <p class="card-text"><strong>Autor:</strong> ${libro.autor}</p>
                                        <p class="card-text"><strong>Año de Publicación:</strong> ${fechaPublicacion}</p>
                                        <p class="card-text"><strong>Editorial:</strong> ${nombreEditorial}</p>
                                        <button type="button" class="btn btn-outline-secondary mr-2" onclick="Update(${libro.IdLibro})">Editar</button>
                                        <button type="button" class="btn btn-outline-danger" onclick="Delete(${libro.IdLibro})">Eliminar</button>
                                    </div>
                                </div>
                            </div>`;
                        $("#showLibros").append(cardHtml);
                    });
                } else {
                    $("#showLibros").html(
                        '<div class="col text-center">' +
                        '<p>NO HAY LIBROS</p>' +
                        '<img src="https://c0.klipartz.com/pngpicture/81/490/gratis-png-humano-al-lado-del-signo-de-interrogacion-icono-de-graficos-escalables-archivo-de-computadora-signo-de-interrogacion-thumbnail.png" width="150" height="150" />' +
                        '</div>'
                    );
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    // Función para obtener todas las editoriales
    function EditorialGetAll() {
        $.ajax({
            url: "https://localhost:7099/api/Editorial/Editorial/GetAll",
            type: "GET",
            dataType: "json",
            success: function (result) {
                if (result && result.success) {
                    var options = '<option value="" selected>Selecciona una opción</option>';
                    $.each(result.data, function (i, data) {
                        options += `<option value="${data.idEditorial}">${data.nombre}</option>`;
                    });
                    $("#ddlEditorial").html(options);
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    // Función para agregar un nuevo libro
    function AddNewLibro() {
        if (!ValidateForm()) {
            return; // Si la validación no pasa, salir de la función
        }

        var nuevoLibro = {
            Autor: $("#txtAutor").val(),
            TituloLibro: $("#txtTitulo").val(),
            AñoPublicacion: $("#txtAñoPublicacion").val(),
            Imagen: $("#txtImagen").val(),
            Editorial: {
                IdEditorial: parseInt($("#ddlEditorial").val()),
                Nombre: $("#ddlEditorial option:selected").text()
            }
        };

        $.ajax({
            url: "https://localhost:7099/api/Libro/Libro/Add",
            type: "POST",
            data: JSON.stringify(nuevoLibro),
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            success: function (result) {
                $("#alertForm").hide(); // Ocultar la alerta del formulario
                if (result.success) {
                    $("#modalForm").modal("hide"); // Ocultar el modal
                    $("#alertInfo").show().removeClass("alert-danger").addClass("alert-success").text("Nuevo libro agregado");
                    LibroGetAll(); // Actualizar la lista de libros
                } else {
                    $("#alertInfo").show().removeClass("alert-success").addClass("alert-danger").text("Error al guardar nuevo libro");
                }

                setTimeout(function () {
                    $("#alertInfo").hide();
                }, 6000);
            },
            error: function (error) {
                console.log(error);
                $("#alertForm").hide(); // Ocultar la alerta del formulario
                $("#alertInfo").show().removeClass("alert-success").addClass("alert-danger").text("Error al conectar con el servidor");
                setTimeout(function () {
                    $("#alertInfo").hide();
                }, 6000);
            }
        });
    }

    // Evento click para el botón "Guardar" del formulario modal
    $('#btnForm').on("click", function () {
        AddNewLibro();
    });

    // Evento click para el botón "Cancelar" del formulario modal
    $('#cancelForm').on("click", function () {
        $("#modalForm").modal("hide"); // Ocultar el modal
        setTimeout(function () {
            normalForm();
        }, 1000);
    },



        function Delete(id) {
            if (confirm("¿Estás seguro de eliminar el empleado de forma permanente?")) {
                $.ajax({
                    url: "https://localhost:7099/api/Libro/Libro/Delete/" + id,
                    type: "Delete",
                    success: function (result) {
                        if (result.success) {
                            EmpleadoGetAll();
                            Alert("success", "El empleado ha sido eliminado de forma permanente");
                        } else {
                            Alert("danger", "Error al eliminar empleado");
                        }
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
            }
        });








    // Función de validación de formulario
    function ValidateForm() {
        var isValid = true;

        // Ejemplo de validación básica, ajustar según tus requisitos
        if ($("#txtAutor").val().trim() === "") {
            $("#lblAutor").text("Campo requerido").show();
            isValid = false;
        } else {
            $("#lblAutor").hide();
        }

        if ($("#txtTitulo").val().trim() === "") {
            $("#lblTitulo").text("Campo requerido").show();
            isValid = false;
        } else {
            $("#lblTitulo").hide();
        }

        if ($("#txtAñoPublicacion").val().trim() === "") {
            $("#lblAñoPublicacion").text("Campo requerido").show();
            isValid = false;
        } else {
            $("#lblAñoPublicacion").hide();
        }

        // Agregar más validaciones según sea necesario

        return isValid;
    }

});

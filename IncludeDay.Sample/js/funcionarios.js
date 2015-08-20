var Funcionarios = function () {

    var handleCarregarFuncionarios = function () {

        $("#btn-filtrar").trigger("click");

    }

    var handleFiltrarFuncionarios = function () {

        $("#btn-filtrar").on("click", function () {

            var dadosFuncionarios = {
                Nome: $("#campo-filtro").val()
            };

            $.ajax({
                async: true,
                type: "GET",
                data: dadosFuncionarios,
                url: URL_SERVICO + '/Funcionario/',
                dataType: "JSON",
                processData: true,
                success: function (data) {
                    $('.table-result').empty();
                    console.log(data);

                    $.each(data, function (i, item) {
                        var tr = $('<tr/>');
                        tr.append("<td>" + item.Id + "</td>");
                        tr.append("<td>" + item.Nome + "</td>");
                        tr.append("<td>" + item.Cargo + item.Id + "</td>");
                        tr.append("<td>" + item.Email + "</td>");
                        $('.table-result').append(tr);
                    });
                },
                error: function (xhr) {
                    alert("Ocorreu um erro ao carregar a lista de funcionários.");
                }
            });

        });
    }

    var handleAplicarFiltro = function () {

        $('.container').on('click', '.panel-heading span.filter', function (e) {
            var $this = $(this),
                $panel = $this.parents('.panel');

            $panel.find('.panel-body').slideToggle();
            if ($this.css('display') != 'none') {
                $panel.find('.panel-body input').focus();
            }
        });
        $('[data-toggle="tooltip"]').tooltip();
    }

    return {
        //Função principal que inicializa o módulo
        inicializar: function () {
            handleFiltrarFuncionarios();
            handleCarregarFuncionarios();
            handleAplicarFiltro();
        }
    };
}();

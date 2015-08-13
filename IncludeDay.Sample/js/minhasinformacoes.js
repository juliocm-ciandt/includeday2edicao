﻿var MinhasInformacoes = function () {
    //var URL_SERVICO = "http://localhost/IncludeDay.Services/api";
    var URL_SERVICO = "http://localhost:61719/api";

    //Cuida do carregamento dos dados do funcionário da tela
    var handleCarregarDados = function () {
        var idFuncionario = $("#idFuncionario").val();

        $.ajax({
            async: true,
            type: "GET",
            url: URL_SERVICO + '/Funcionario/' + idFuncionario,
            dataType: "JSON",
            processData: true,
            success: function (data) {

                console.log(data);

                $("#nome").val(data.Nome);
                $("#cargo").val(data.Cargo);
                $("#email").val(data.Email);
                $("#predio").val(data.Departamento.Predio.Id);
                $("#predio").trigger("change");
                $("#departamento").val(data.Departamento.Id);

            },
            error: function (xhr) {
                alert("Ocorreu um erro ao carregar os dados do funcionário.");
            }
        });

    }

    //Gerencia o click do botão Cadastrar
    var handleCadastrar = function () {
        $(".btn-cadastrar").click(function () {

            var dadosFuncionario = {
                Id: $("#idFuncionario").val(),
                Nome: $("#nome").val(),
                Cargo: $("#cargo").val(),
                Email: $("#email").val(),
                Departamento: {
                    Id: $("#departamento").val()
                }
            };

            console.log(dadosFuncionario);

            $.ajax({
                async: false,
                type: "POST",
                url: URL_SERVICO + '/Funcionario',
                data: dadosFuncionario,
                //contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                processData: true,
                success: function (data) {
                    alert("Dados cadastrados com sucesso!");
                },
                error: function (xhr) {
                    alert(xhr.responseText);
                }
            });

        });
    }

    //Preenche o campo de Prédios
    var handlePredios = function () {
        $select = $('#predio');

        $.ajax({
            async: false,
            type: "GET",
            url: URL_SERVICO + '/Predio',
            dataType: 'JSON',
            success: function (data) {
                $select.html('');
                $select.append('<option value="0">Selecione</option>');
                $.each(data, function (key, val) {
                    $select.append('<option value="' + val.Id + '">' + val.Nome + '</option>');
                });
                handlePredioAlterado();
            },
            error: function () {
                $select.html('<option value="-1">Nenhum disponível</option>');
            }
        });
    }

    //Cuida da mudança no campo Prédio
    var handlePredioAlterado = function () {

        $('#predio').on("change", function () {
            var predioId = $(this).val();

            //TODO: Implementar o método
            if (predioId > 0) {
                $select = $('#departamento');

                var dadosPredio = {
                    Predio: {
                        Id: predioId
                    }
                };

                $.ajax({
                    async: false,
                    type: "GET",
                    url: URL_SERVICO + '/Departamento',
                    data: dadosPredio,
                    dataType: "JSON",
                    success: function (data) {
                        $select.html('');
                        $select.append('<option value="0">Selecione</option>');
                        $.each(data, function (key, val) {
                            $select.append('<option value="' + val.Id + '">' + val.Nome + '</option>');
                        });
                    },
                    error: function () {
                        $select.html('<option value="-1">Nenhum prédio disponível</option>');
                    }
                });
            }
        });

    }

    return {
        //Função principal que inicializa o módulo
        inicializar: function () {
            handlePredios();
            handleCarregarDados();
            handleCadastrar();
        }
    };
}();
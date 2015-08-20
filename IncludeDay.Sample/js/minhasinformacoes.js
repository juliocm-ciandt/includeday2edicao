var MinhasInformacoes = function () {

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

                $("#cargo").val(data.Nome);
                $("#nome").val(data.Cargo);
                $("#email").val(data.Email);
                $("#idade").val(data.Idade);
                $("#predio").val(data.Projeto.Predio.Id);
                CarregarProjetos(data.Projeto.Predio.Id);
                $("input[name='projeto'][value='" + data.Projeto.Id + "']").attr('checked', true);

                //ATENÇÃO: Não remover esta chamada
                handleRadios();
            },
            error: function (xhr) {
                alert("Ocorreu um erro ao carregar os dados do funcionário.");
            }
        });

    }

    //Função que gerencia o click do botão Cadastrar do formulário
    var handleCadastrar = function () {

        $(".btn-cadastrar").on("click", function (e) {
            //e.preventDefault();

            if (ValidacaoFormulario()) {
                var dadosFuncionario = {
                    Id: $("#idFuncionario").val(),
                    Nome: $("#nome").val(),
                    Cargo: $("#nome").val(),
                    Email: $("#email").val(),
                    Idade: $("#idade").val(),
                    Projeto: {
                        Id: $("input[name='projeto']:checked").val()
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
            }
        });
    }

    //Função que valida os dados do formulário antes de enviar ao serviço
    var ValidacaoFormulario = function () {

        var nome = $("#nome").val();
        var cargo = $("#cargo").val();
        var email = $("#email").val();
        var idade = $("#idade").val();
        var predio = $("#predio").val();
        var projeto = $("input[name='projeto']:checked").val();

        if (nome === "") {
            alert("Informe o campo nome.");
            return false;
        }

        if (cargo === "") {
            alert("Informe o campo cargo.");
            return false;
        }

        if (email === "") {
            alert("Informe o campo e-mail.");
            return false;
        }

        if (idade === "" || parseInt(idade) <= 0) {
            alert("Informe o campo idade.");
            return false;
        }

        if (typeof(predio) === 'undefined' || parseInt(predio) <= 0) {
            alert("Selecione um prédio.");
            return false;
        }

        if (typeof (projeto) === 'undefined' || parseInt(projeto) <= 0) {
            alert("Selecione um projeto no mapa.");
            return false;
        }

        return true;
    }

    //Função que preenche o campo de seleção de Prédios
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

    //Função que cuida da mudança de seleção no campo Prédio
    var handlePredioAlterado = function () {

        $('#predio').on("change", function () {
            CarregarProjetos($(this).val());

            //ATENÇÃO: Não remover esta chamada
            handleRadios();
        });

    }

    //Função que carrega os projetos passando como parâmetro o ID do prédio
    var CarregarProjetos = function (idPredio) {

        //TODO: Implementar o método
        if (idPredio > 0) {
            $select = $('#projeto');
            $mapa = $('#map-image');

            var dadosPredio = {
                Predio: {
                    Id: idPredio
                }
            };

            $.ajax({
                async: false,
                type: "GET",
                url: URL_SERVICO + '/Projeto',
                data: dadosPredio,
                dataType: "JSON",
                success: function (data) {
                    $mapa.empty();
                    $mapa.html(data);
                },
                error: function () {
                    $select.html('<option value="-1">Nenhum prédio disponível</option>');
                }
            });

        } else {
            $mapa.empty();
        }

    }

    //Função que faz aparecer os RadioButtons sobre o mapa para seleção
    var handleRadios = function () {

        $('.checkradios').checkradios({
            checkbox: {
                iconClass: 'fa fa-check-circle'
            },
            radio: {
                iconClass: 'fa fa-check-circle'
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
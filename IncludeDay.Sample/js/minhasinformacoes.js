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

        $("#btn-salvar").on("click", function (e) {

            if (ValidacaoFormulario()) {
                var dadosFuncionario = {
                    Id: $("#idFuncionario").val(),
                    Nome: $("#cargo").val(),
                    Cargo: $("#nome").val(),
                    Email: $("#idade").val(),
                    Idade: $("#email").val(),
                    Projeto: {
                        Id: $("input[name='projeto']:checked").val()
                    }
                };

                console.log(dadosFuncionario);

                //TODO: Implementar a chamada para enviar ao serviço de gravação

            }
        });
    }

    //Função que valida os dados do formulário antes de enviar ao serviço
    var ValidacaoFormulario = function () {

        var nome = $("#nome").val();
        var predio = $("#predio").val();
        var projeto = $("input[name='projeto']:checked").val();

        if (nome === "") {
            alert("Informe o campo nome.");
            return false;
        }

        if (typeof(predio) == undefined && parseInt(predio) <= 0) {
            alert("Selecione um prédio.");
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
                $select.empty();
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
            var predioId = $().val();

            CarregarProjetos(predioId);

            //ATENÇÃO: Não remover esta chamada
            handleRadios();
        });

    }

    //Função que carrega os projetos passando como parâmetro o ID do prédio
    var CarregarProjetos = function (idPredio) {

        //TODO: Implementar o método
        if (idPredio > 0) {



        } else {
            


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
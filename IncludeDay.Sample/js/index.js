var Index = function () {
    //var URL_SERVICO = "http://localhost/IncludeDay.Services/api";
    var URL_SERVICO = "http://localhost:61719/api";

    //Cuida do carregamento dos dados do funcionário da tela
    var handleCarregarUsuario = function () {
        var idFuncionario = $("#idFuncionario").val();

        $.ajax({
            async: true,
            type: "GET",
            url: URL_SERVICO + '/Funcionario/' + idFuncionario,
            dataType: "JSON",
            processData: true,
            success: function (data) {

                $("#nomeUsuario").text(data.Nome);

            },
            error: function (xhr) {
                alert("Ocorreu um erro ao carregar os dados do funcionário.");
            }
        });

    }

    return {
        //Função principal que inicializa o módulo
        inicializar: function () {
            handleCarregarUsuario();
        }
    };
}();
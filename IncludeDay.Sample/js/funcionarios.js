var Funcionarios = function () {

    //Função que carrega os dados dos funcionários na tela
    var handleCarregarFuncionarios = function () {

        //TODO: Implementar função

    }

    //Função que aplica o manipulador do botão de exibir/esconder o campo de filtro
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
            handleCarregarFuncionarios();
            handleAplicarFiltro();
        }
    };
}();

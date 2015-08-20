var Funcionarios = function () {

    var handleCarregarFuncionarios = function () {

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
            handleCarregarFuncionarios();
            handleAplicarFiltro();
        }
    };
}();

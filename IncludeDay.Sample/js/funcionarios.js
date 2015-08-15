var Funcionarios = function () {

    var handleCarregarFuncionarios = function () {

        $.ajax({
            async: true,
            type: "GET",
            url: URL_SERVICO + '/Funcionario/',
            dataType: "JSON",
            processData: true,
            success: function (data) {

                console.log(data);

                $.each(data, function (i, item) {
                    var tr = $('<tr/>');
                    tr.append("<td>" + item.Id + "</td>");
                    tr.append("<td>" + item.Nome + "</td>");
                    tr.append("<td>" + item.Cargo + "</td>");
                    tr.append("<td>" + item.Email + "</td>");
                    $('.table-result').append(tr);
                });
            },
            error: function (xhr) {
                alert("Ocorreu um erro ao carregar a lista de funcionários.");
            }
        });

    }

    var handleAplicarFiltro = function () {
        $.fn.extend({
            filterTable: function () {
                return this.each(function () {
                    $(this).on('keyup', function (e) {
                        $('.filterTable_no_results').remove();
                        var $this = $(this),
                            search = $this.val().toLowerCase(),
                            target = $this.attr('data-filters'),
                            $target = $(target),
                            $rows = $target.find('tbody tr');

                        if (search == '') {
                            $rows.show();
                        } else {
                            $rows.each(function () {
                                var $this = $(this);
                                $this.text().toLowerCase().indexOf(search) === -1 ? $this.hide() : $this.show();
                            })
                            if ($target.find('tbody tr:visible').size() === 0) {
                                var col_count = $target.find('tr').first().find('td').size();
                                var no_results = $('<tr class="filterTable_no_results"><td colspan="' + col_count + '">Resultados não encontrados.</td></tr>');
                                $target.find('tbody').append(no_results);
                            }
                        }
                    });
                });
            }
        });
        $('[data-action="filter"]').filterTable();

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

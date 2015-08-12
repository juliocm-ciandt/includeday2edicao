$(document).ready(function () {

    var URL_PREDIO = "http://localhost/IncludeDay.Services/api/Predio";

    $select = $('#predios');

    $.ajax({
        url: URL_PREDIO,
        dataType:'JSON',
        success:function(data){
            $select.html('');
            $.each(data, function(key, val) {
                $select.append('<option id="' + val.Id + '">' + val.Nome + '</option>');
            })
        },
        error:function(){
            $select.html('<option id="-1">Nenhum disponível</option>');
        }
    });
});
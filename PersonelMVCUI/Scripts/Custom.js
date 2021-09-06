$(function () { //ajax ile silme işlemi
    $("#tblDepartman").on("click", ".btnDepartmanSil", function () {  //id'si "#" olan  "." class'ına "click" işlemi olduğun da fonk. çalıştır. 
        var btn = $(this);  //btn değişkeninde butonun refaransını tutuyoruz.Aşağıda butonun olduğu satırı silmek için.
        bootbox.confirm("Silmek istediğinize emin misiniz", function (result) {
            if (result) {
                var id = btn.data("id");    //hangi butona tıklandı ise o butonun id'sini getir.ve id'ye ata.                
                $.ajax({    //obje tanımmlıyoruz
                    type: "GET",
                    url: "Departman/Sil/" + id,//controller/action/gerekli id
                    success: function () {  //başarılı ise bu fonk. gir
                        btn.parent().parent().remove(); //butonun üst parenti(td) bir üst parentı ise(tr)
                    }
                });
            }
        })
    });
});
$(document).ready(function () { //Jquery Datatable Plugin
    $('#tblDepartman').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        }
    });
});

function CheckDateTypeIsValid(dateElement) {    //Data type validation 
    var value = $(dateElement).val();
    if (value = '') {
        $(dateElement).valid("false");  
    }
    else {
        $(dateElement).valid("true");
    }
}
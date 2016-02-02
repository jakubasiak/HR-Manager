$(document).ready(function () {

    $('.jqte-test').jqte();




        $('.btn-file :file').on('fileselect', function (event, numFiles, label) {

            var input = $(this).parents('.input-group').find(':text'),
                log = numFiles > 1 ? numFiles + ' files selected' : label;

            if (input.length) {
                input.val(log);
            } else {
                if (log) alert(log);
            }

        });



});

$(document).on('change', '.btn-file :file', function () {
    var input = $(this),
        numFiles = input.get(0).files ? input.get(0).files.length : 1,
        label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
    input.trigger('fileselect', [numFiles, label]);
});

function clear() {
    $("#textInput").val("");
};
function clearInput(arg) {
    $("#"+arg).val("");
};
function panelRemove(arg) {
    $("#"+arg).remove();

};
function hide(arg) {
    $(arg).text("");
};
$(document).ready(function () {
    var formSteps = $('.form-step');
    var form = $('#formWizard');
    var progressBar = $('.progress-bar');
    var currentStep = 1;

    formSteps.hide().first().show();

    form.on('click', '.next-step', function () {
        currentStep++;
        formSteps.hide().eq(currentStep - 1).show();
        updateProgressBar();
    });

    form.on('click', '.prev-step', function () {
        currentStep--;
        formSteps.hide().eq(currentStep - 1).show();
        updateProgressBar();
    });

    function updateProgressBar() {
        var progress = (currentStep - 1) / (formSteps.length - 1) * 100;
        progressBar.css('width', progress + '%');
        progressBar.attr('aria-valuenow', progress);
    }
});
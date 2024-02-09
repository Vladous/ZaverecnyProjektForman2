var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl);
});
var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
    return new bootstrap.Popover(popoverTriggerEl);
});

function toggleFilterInput(inputId, hiddenInputId) {
    var input = document.getElementById(inputId);
    var filterIcon = input.previousElementSibling;
    var isFilterActive = filterIcon.classList.contains("active-filter");

    if (isFilterActive) {
        // Zrušit filtr
        document.getElementById(hiddenInputId).value = ''; // Reset hodnoty skrytého inputu
        filterIcon.classList.remove("active-filter"); // Změnit barvu ikony zpět
        document.getElementById('filterForm').submit(); // Odeslat formulář
    } else {
        // Zobrazit input, pokud předtím nebyl aktivní filtr
        input.style.display = "inline-block";
        input.focus();
    }
}

function applyFilter(inputElement, hiddenInputId) {
    var value = inputElement.value.trim();
    var hiddenInput = document.getElementById(hiddenInputId);
    var filterIcon = inputElement.previousElementSibling;

    if (value) {
        filterIcon.classList.add("active-filter");
    } else {
        filterIcon.classList.remove("active-filter");
    }
    hiddenInput.value = value; // Aktualizace hodnoty skrytého inputu
    inputElement.style.display = "none";

    var form = inputElement.closest('form');
    form.submit(); // Odeslat formulář
}



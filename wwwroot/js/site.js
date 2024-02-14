// Inicializace Bootstrap tooltipů pro všechny elementy s atributem data-bs-toggle="tooltip"
var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl);
});
// Inicializace Bootstrap popoverů pro všechny elementy s atributem data-bs-toggle="popover"
var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
    return new bootstrap.Popover(popoverTriggerEl);
});
// Skript pro automatické skrytí zprávy o úspěchu po 5 sekundách
window.addEventListener('DOMContentLoaded', (event) => {
    setTimeout(function () {
        var alert = document.getElementById('success-alert');
        if (alert != null) {
            alert.style.display = 'none';
        }
    }, 5000); // Zpráva se skryje po 5 sekundách
});
// Funkce pro přepínání viditelnosti vstupního pole filtru a aktualizace stavu filtru
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
// Funkce pro aplikaci filtru po vyplnění a odstranění focusu z inputu
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



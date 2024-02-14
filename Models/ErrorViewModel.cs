namespace ZaverecnyProjektForman2.Models
{
    /// <summary>
    /// Model pro zobrazení chybových hlášek.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Unikátní identifikátor požadavku, pøi kterém došlo k chybì.
        /// </summary>
        public string? RequestId { get; set; }
        /// <summary>
        /// Urèuje, zda se má zobrazit identifikátor požadavku.
        /// Vrací true, pokud <see cref="RequestId"/> není prázdný nebo null.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

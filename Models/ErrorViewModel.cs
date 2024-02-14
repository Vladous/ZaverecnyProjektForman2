namespace ZaverecnyProjektForman2.Models
{
    /// <summary>
    /// Model pro zobrazen� chybov�ch hl�ek.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Unik�tn� identifik�tor po�adavku, p�i kter�m do�lo k chyb�.
        /// </summary>
        public string? RequestId { get; set; }
        /// <summary>
        /// Ur�uje, zda se m� zobrazit identifik�tor po�adavku.
        /// Vrac� true, pokud <see cref="RequestId"/> nen� pr�zdn� nebo null.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

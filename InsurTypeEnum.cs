namespace ZaverecnyProjektForman2
{
    /// <summary>
    /// Definuje výčtový typ pro rozlišení mezi pojistníkem a pojistným.
    /// </summary>
    public static class InsurTypeEnum
    {
        /// <summary>
        /// Výčtový typ pro identifikaci pojistníka a pojistného.
        /// </summary>
        public enum InsurType
        {
            /// <summary>
            /// Reprezentuje pojistníka - osobu, která uzavírá pojištění.
            /// </summary>
            Pojistnik,
            /// <summary>
            /// Reprezentuje pojistného - osobu, na kterou se pojištění vztahuje.
            /// </summary>
            Pojisteny
        }
    }
}

namespace JellyButton.Exam.Core.Data
{
    /// <summary>
    /// Holds global variables that need only one instance
    /// and are accessed frequently by a lot of classes.
    /// </summary>
    public static class GlobalData
    {
        #region Speed

        public static float CurrentScrollSpeed = 20f;
        public static bool IsSpeeding = false;

        #endregion
    }
}
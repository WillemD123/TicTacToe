namespace WpfApp1
{

    /// <summary>
    /// The type of value a cell in the game is currently at
    /// </summary>
    public enum MarkType
    {
        /// <summary>
        /// The cell hasnt been clicked yet
        /// </summary>
        Free, 
        /// <summary>
        /// The cell is 0
        /// </summary>
        Nought,
        /// <summary>
        /// The cell is an x
        /// </summary>
        Cross
    }
}

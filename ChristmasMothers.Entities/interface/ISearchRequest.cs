namespace ChristmasMothers.Entities.@interface
{
    public interface ISearchRequest
    {
        /// <summary>
        /// Gets or sets Q, search criterias.
        /// </summary>
        string Q { get; set; }
        /// <summary>
        /// Gets or sets Skip.
        /// </summary>
        int Skip { get; set; }
        /// <summary>
        /// Gets or sets Take.
        /// </summary>
        int Take { get; set; }
    }
}

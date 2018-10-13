namespace ChristmasMothers.Entities.@interface
{
    public interface ISearchChildByAgeRequest
    {
        /// <summary>
        /// Gets or sets Q, search criterias.
        /// </summary>
        int MinAge { get; set; }
        int MaxAge { get; set; }
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

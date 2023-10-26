using System.Text.Json.Serialization;

namespace uk.tfl.apiclient.Models
{
    /// <summary>
    /// Tfl.Api.Presentation.Entities.RoadCorridor
    /// </summary>
    public class RoadCorridor
    {
        /// <summary>
        /// The Id of the Corridor e.g. "A406"
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
        /// <summary>
        /// The display name of the Corridor e.g. "North Circular (A406)". This may be identical to the Id.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        /// <summary>
        /// The group name of the Corridor e.g. "Central London". Most corridors are not grouped, in which case this field can be null.
        /// </summary>
        [JsonPropertyName("group")]
        public string Group { get; set; }
        /// <summary>
        /// Standard multi-mode status severity code
        /// </summary>
        [JsonPropertyName("statusSeverity")]
        public string StatusSeverity { get; set; }
        /// <summary>
        /// Description of the status severity as applied to RoadCorridors
        /// </summary>
        [JsonPropertyName("statusSeverityDescription")]
        public string StatusSeverityDescription { get; set; }
        /// <summary>
        /// The Bounds of the Corridor, given by the south-east followed by the north-west co-ordinate pair in geoJSON format e.g. "[[-1.241531,51.242151],[1.641223,53.765721]]"
        /// </summary>
        [JsonPropertyName("bounds")]
        public string Bounds { get; set; }
        /// <summary>
        /// The Envelope of the Corridor, given by the corner co-ordinates of a rectangular (four-point) polygon in geoJSON format e.g. "[[-1.241531,51.242151],[-1.241531,53.765721],[1.641223,53.765721],[1.641223,51.242151]]"
        /// </summary>
        [JsonPropertyName("envelope")]
        public string Envelope { get; set; }
        /// <summary>
        /// The start of the period over which status has been aggregated, or null if this is the current corridor status.
        /// </summary>
        [JsonPropertyName("statusAggregationStartDate")]
        public DateTime StatusAggregationStartDate { get; set; }
        /// <summary>
        /// The end of the period over which status has been aggregated, or null if this is the current corridor status.
        /// </summary>
        [JsonPropertyName("statusAggregationEndDate")]
        public DateTime StatusAggregationEndDate { get; set; }
        /// <summary>
        /// URL to retrieve this Corridor.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}

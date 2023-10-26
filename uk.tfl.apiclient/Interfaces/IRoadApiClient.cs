using uk.tfl.apiclient.Models;

namespace uk.tfl.apiclient.Interfaces
{
    public interface IRoadApiClient
    {
        /// <summary>
        /// Gets all roads managed by TfL
        /// </summary>
        /// <returns>list of RoadCorridor instance</returns>
        public List<RoadCorridor> GetCorridors();
        /// <summary>
        /// Gets all roads managed by TfL
        /// </summary>
        /// <returns>list of RoadCorridor instance</returns>
        Task<List<RoadCorridor>> GetCorridorsAsync();
        /// <summary>
        /// Gets the road with the specified id (e.g. A1)
        /// </summary>
        /// <param name="roadIdentifiers">list of road identifiers e.g. "A406, A2" (a full list of supported road identifiers can be found at the /Road/ endpoint)</param>
        /// <returns>list of RoadCorridor instance</returns>
        public List<RoadCorridor> GetCorridors(List<string> roadIdentifiers);
        /// <summary>
        /// Gets the road with the specified id (e.g. A1)
        /// </summary>
        /// <param name="roadIdentifiers">list of road identifiers e.g. "A406, A2" (a full list of supported road identifiers can be found at the /Road/ endpoint)</param>
        /// <returns>list of RoadCorridor instance</returns>
        Task<List<RoadCorridor>> GetCorridorsAsync(List<string> roadIdentifiers);
        /// <summary>
        /// Gets the road with the specified id (e.g. A1)
        /// </summary>
        /// <param name="roadIdentifiers">road identifiers e.g. "A406, A2"</param>
        /// <returns>RoadCorridor instance</returns>
        Task<RoadCorridor> GetCorridorAsync(string roadIdentifiers);
        /// <summary>
        /// Gets the road with the specified id (e.g. A1)
        /// </summary>
        /// <param name="roadIdentifiers">road identifiers e.g. "A406, A2"</param>
        /// <returns>RoadCorridor instance</returns>
        RoadCorridor GetCorridor(string roadIdentifiers);
    }
}

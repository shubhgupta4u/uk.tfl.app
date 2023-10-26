using uk.tfl.apiclient.Interfaces;
using uk.tfl.apiclient.Models;

namespace uk.tfl.apiclient.ApiClients
{
    public class RoadApiClient : IRoadApiClient
    {
        #region Private Members
        public const string ROUTE="road";
        private readonly IRestClient _restClient;
        #endregion

        #region Constructor
        public RoadApiClient()
        {
            this._restClient = DependencyResolver.Instance.Resolve<IRestClient>(); ;
        }
        public RoadApiClient(IRestClient restClient)
        {
            this._restClient = restClient;
        }
        #endregion
        #region Interfaces.IRoadApiClient Methods
        /// <inheritdoc/>
        public async Task<List<RoadCorridor>> GetCorridorsAsync()
        {
            return await this._restClient.GetAsync<List<RoadCorridor>>(ROUTE);
        }
        /// <inheritdoc/>
        public List<RoadCorridor> GetCorridors()
        {
            Task<List<RoadCorridor>> t = this.GetCorridorsAsync();
            return t.Result;
        }
        /// <inheritdoc/>
        public async Task<List<RoadCorridor>> GetCorridorsAsync(List<string> roadIdentifiers)
        {
            return await this._restClient.GetAsync<List<RoadCorridor>>(string.Format("{0}/{1}", ROUTE, string.Join(",", roadIdentifiers)));
        }
        /// <inheritdoc/>
        public List<RoadCorridor> GetCorridors(List<string> roadIdentifiers)
        {

            Task<List<RoadCorridor>> t = this.GetCorridorsAsync(roadIdentifiers);
            return t.Result;
        }
        /// <inheritdoc/>
        public async Task<RoadCorridor> GetCorridorAsync(string roadIdentifier)
        {
            List<string> roadIdentifiers = new List<string>();
            roadIdentifiers.Add(roadIdentifier);
            List<RoadCorridor> result = await this.GetCorridorsAsync(roadIdentifiers);
            if (result.Any())
            {
                return result.FirstOrDefault();
            }
            else
            {
                return default(RoadCorridor);
            }
               
        }
        /// <inheritdoc/>
        public RoadCorridor GetCorridor(string roadIdentifier)
        {

            Task<RoadCorridor> t = this.GetCorridorAsync(roadIdentifier);
            return t.Result;            
        }
        #endregion
    }
}

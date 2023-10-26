using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using uk.tfl.apiclient.Interfaces;
using uk.tfl.apiclient.Models;

namespace uk.tfl.apiclient.ApiClients.Tests
{
    [TestClass()]
    public class RoadApiClientTests
    {
        #region Private Members
        private IRoadApiClient _roadApiClient;
        private Mock<IRestClient> _restClientMock;
        #endregion

        [TestInitialize]
        public void Initialize()
        {
            Startup.ConfigureServices();
            this._restClientMock = new Mock<IRestClient>();
            this._roadApiClient = new RoadApiClient(this._restClientMock.Object);
        }

        [TestMethod]
        public void RoadApiClient_GetCorridors_Test()
        {
            //Arrange
            this._restClientMock.Setup(x => x.GetAsync<List<RoadCorridor>>(RoadApiClient.ROUTE)).Returns(this.GetRoadCorridorObjects());

            //Act
            List<RoadCorridor> results = this._roadApiClient.GetCorridors();

            //Assert
            this._restClientMock.Verify(x => x.GetAsync<List<RoadCorridor>>(RoadApiClient.ROUTE), Times.Once);
            Assert.IsTrue(results.Any());
          
        }
        [TestMethod]
        public void RoadApiClient_GetCorridorsAsync_Test()
        {
            //Arrange
            this._restClientMock.Setup(x => x.GetAsync<List<RoadCorridor>>(RoadApiClient.ROUTE)).Returns(this.GetRoadCorridorObjects());

            //Act
            Task<List<RoadCorridor>> results = this._roadApiClient.GetCorridorsAsync();
            List<RoadCorridor> roadCorridors = results.Result;

            //Assert
            this._restClientMock.Verify(x => x.GetAsync<List<RoadCorridor>>(RoadApiClient.ROUTE), Times.Once);
            Assert.IsTrue(roadCorridors.Any());

        }
        [TestMethod]
        public void RoadApiClient_GetSpecifiedCorridorsAsync_Success_Test()
        {
            List<string> data = new List<string>();
            data.Add("a2");
            string uri = string.Format("{0}/{1}", RoadApiClient.ROUTE, string.Join(",", data));

            //Arrange
            this._restClientMock.Setup(x => x.GetAsync<List<RoadCorridor>>(uri)).Returns(this.GetRoadCorridorObjects());

            //Act
           
            Task<List<RoadCorridor>> results = this._roadApiClient.GetCorridorsAsync(data);
            List<RoadCorridor> roadCorridors = results.Result;

            //Assert
            this._restClientMock.Verify(x => x.GetAsync<List<RoadCorridor>>(uri), Times.Once);
            Assert.IsTrue(roadCorridors.Any());
            Assert.IsTrue(roadCorridors.Count ==1);
            Assert.IsTrue(roadCorridors[0].DisplayName.Equals("a2", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(roadCorridors[0].StatusSeverity.Equals("good",StringComparison.InvariantCultureIgnoreCase));

        }
        [TestMethod]
        public void RoadApiClient_GetSpecifiedCorridorsAsync_Fail_Test()
        {
            List<string> data = new List<string>();
            data.Add("a233");
            string uri = string.Format("{0}/{1}", RoadApiClient.ROUTE, string.Join(",", data));

            //Arrange
            this._restClientMock.Setup(x => x.GetAsync<List<RoadCorridor>>(uri)).Returns(this.GetNotFoundRoadCorridorObjects());

            //Act
            Task<List<RoadCorridor>> results = this._roadApiClient.GetCorridorsAsync(data);
            List<RoadCorridor> roadCorridors = results.Result;

            //Assert
            this._restClientMock.Verify(x => x.GetAsync<List<RoadCorridor>>(uri), Times.Once);
            Assert.IsFalse(roadCorridors.Any());

        }
        [TestMethod]
        public void RoadApiClient_GetSpecifiedCorridors_Success_Test()
        {
            List<string> data = new List<string>();
            data.Add("a233");
            string uri = string.Format("{0}/{1}", RoadApiClient.ROUTE, string.Join(",", data));

            //Arrange
            this._restClientMock.Setup(x => x.GetAsync<List<RoadCorridor>>(uri)).Returns(this.GetRoadCorridorObjects());

            //Act
            List<RoadCorridor> roadCorridors = this._roadApiClient.GetCorridors(data);

            //Assert
            this._restClientMock.Verify(x => x.GetAsync<List<RoadCorridor>>(uri), Times.Once);
            Assert.IsTrue(roadCorridors.Any());
            Assert.IsTrue(roadCorridors.Count == 1);
            Assert.IsTrue(roadCorridors[0].DisplayName.Equals("a2", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(roadCorridors[0].StatusSeverity.Equals("good", StringComparison.InvariantCultureIgnoreCase));

        }
        [TestMethod]
        public void RoadApiClient_GetSpecifiedCorridors_Fail_Test()
        {
            List<string> data = new List<string>();
            data.Add("a233");
            string uri = string.Format("{0}/{1}", RoadApiClient.ROUTE, string.Join(",", data));

            //Arrange
            this._restClientMock.Setup(x => x.GetAsync<List<RoadCorridor>>(uri)).Returns(this.GetNotFoundRoadCorridorObjects());

            //Act
            List<RoadCorridor> roadCorridors = this._roadApiClient.GetCorridors(data);

            //Assert
            this._restClientMock.Verify(x => x.GetAsync<List<RoadCorridor>>(uri), Times.Once);
            Assert.IsFalse(roadCorridors.Any());

        }
        [TestMethod]
        public void RoadApiClient_GetSpecifiedCorridorAsync_Success_Test()
        {
            List<string> data = new List<string>();
            data.Add("a2");
            string uri = string.Format("{0}/{1}", RoadApiClient.ROUTE, string.Join(",", data));

            //Arrange
            this._restClientMock.Setup(x => x.GetAsync<List<RoadCorridor>>(uri)).Returns(this.GetRoadCorridorObjects());

            //Act
            Task<RoadCorridor> results = this._roadApiClient.GetCorridorAsync("a2");
            RoadCorridor roadCorridor = results.Result;

            //Assert
            this._restClientMock.Verify(x => x.GetAsync<List<RoadCorridor>>(uri), Times.Once);
            Assert.IsNotNull(roadCorridor);
            Assert.IsTrue(roadCorridor.DisplayName.Equals("a2", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(roadCorridor.StatusSeverity.Equals("good", StringComparison.InvariantCultureIgnoreCase));

        }
        [TestMethod]
        public void RoadApiClient_GetSpecifiedCorridorAsync_Fail_Test()
        {
            List<string> data = new List<string>();
            data.Add("a233");
            string uri = string.Format("{0}/{1}", RoadApiClient.ROUTE, string.Join(",", data));

            //Arrange
            this._restClientMock.Setup(x => x.GetAsync<List<RoadCorridor>>(uri)).Returns(this.GetNotFoundRoadCorridorObjects());

            //Act
            Task<RoadCorridor> results = this._roadApiClient.GetCorridorAsync("a233");
            RoadCorridor roadCorridor = results.Result;

            //Assert
            this._restClientMock.Verify(x => x.GetAsync<List<RoadCorridor>>(uri), Times.Once);
            Assert.IsNull(roadCorridor);

        }
        [TestMethod]
        public void RoadApiClient_GetSpecifiedCorridor_Success_Test()
        {
            List<string> data = new List<string>();
            data.Add("a2");
            string uri = string.Format("{0}/{1}", RoadApiClient.ROUTE, string.Join(",", data));

            //Arrange
            this._restClientMock.Setup(x => x.GetAsync<List<RoadCorridor>>(uri)).Returns(this.GetRoadCorridorObjects());

            //Act
            RoadCorridor roadCorridor = this._roadApiClient.GetCorridor("a2");

            //Assert
            this._restClientMock.Verify(x => x.GetAsync<List<RoadCorridor>>(uri), Times.Once);
            Assert.IsNotNull(roadCorridor);
            Assert.IsTrue(roadCorridor.DisplayName.Equals("a2", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(roadCorridor.StatusSeverity.Equals("good", StringComparison.InvariantCultureIgnoreCase));

        }
        [TestMethod]
        public void RoadApiClient_GetSpecifiedCorridor_Fail_Test()
        {
            List<string> data = new List<string>();
            data.Add("a233");
            string uri = string.Format("{0}/{1}", RoadApiClient.ROUTE, string.Join(",", data));

            //Arrange
            this._restClientMock.Setup(x => x.GetAsync<List<RoadCorridor>>(uri)).Returns(this.GetNotFoundRoadCorridorObjects());

            //Act
            RoadCorridor roadCorridor = this._roadApiClient.GetCorridor("a233");

            //Assert
            this._restClientMock.Verify(x => x.GetAsync<List<RoadCorridor>>(uri), Times.Once);
            Assert.IsNull(roadCorridor);

        }
        private Task<List<RoadCorridor>> GetRoadCorridorObjects()
        {
            List<RoadCorridor> roadCorridors = new List<RoadCorridor>();
            RoadCorridor roadCorridor = new RoadCorridor()
            {
                Id = "a2",
                DisplayName = "A2",
                StatusSeverity = "Good",
                StatusSeverityDescription = "No Exceptional Delays",
                Bounds = "[[-0.0857,51.44091],[0.17118,51.49438]]",
                Envelope = "[[-0.0857,51.44091],[-0.0857,51.49438],[0.17118,51.49438],[0.17118,51.44091],[-0.0857,51.44091]]",
                Url = "/Road/a2"
            };
            roadCorridors.Add(roadCorridor);
            return Task.Run(() =>
            {
                return roadCorridors;
            });

        }
        private Task<List<RoadCorridor>> GetNotFoundRoadCorridorObjects()
        {
            return Task.Run(() =>
            {
                return new List<RoadCorridor>();
            });
            
        }
    }
}
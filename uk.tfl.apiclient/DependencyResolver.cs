using Microsoft.Extensions.DependencyInjection;
using uk.tfl.apiclient.ApiClients;
using uk.tfl.apiclient.Interfaces;
using uk.tfl.apiclient.Services;

namespace uk.tfl.apiclient
{
    public sealed class DependencyResolver
    {
        #region Private Members
        private static readonly Lazy<DependencyResolver> lazy = new Lazy<DependencyResolver>(() => new DependencyResolver());
        private IServiceProvider _serviceProvider;
        #endregion

        #region Constructor
        private DependencyResolver()
        {
        }
        #endregion

        #region Properties
        public static DependencyResolver Instance
        {
            get
            {
                return lazy.Value;
            }
        }
        #endregion
        #region Methods
        public T Resolve<T>()
        {
            if(this._serviceProvider == null)
            {
                throw new TypeInitializationException(typeof(DependencyResolver).Name, new Exception("DependencyResolver is not initialized"));
            }
            return this._serviceProvider.GetService<T>();
        }
        public void Init(IServiceCollection serviceCollection)
        {
            this._serviceProvider = serviceCollection.BuildServiceProvider();
        }
        #endregion

        
    }
}

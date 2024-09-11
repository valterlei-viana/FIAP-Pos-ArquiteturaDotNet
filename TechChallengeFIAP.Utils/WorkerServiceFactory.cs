using System.Reflection;
using Microsoft.Extensions.Hosting;

namespace TechChallengeFIAP.Utils
{
    public abstract class WorkerServiceFactory<T> : IDisposable
    {
        private readonly HostApplicationBuilder _builder;

        protected WorkerServiceFactory()
        {
            var entryPoint = typeof(T).Assembly.EntryPoint!.DeclaringType!;
            var hostBuilderFactory = entryPoint.GetMethod(
                CreateBuilderMethodName,
                BindingFlags.Static | BindingFlags.NonPublic
            )!;
            var settings = new HostApplicationBuilderSettings
            {
                ApplicationName = entryPoint.Assembly.GetName().Name,
                EnvironmentName = Environment
            };
            _builder = (HostApplicationBuilder)
                hostBuilderFactory.Invoke(null, new object?[] { settings })!;
        }

        protected virtual void ConfigureHost(HostApplicationBuilder builder) { }

        public void Dispose()
        {
            _workerService?.Dispose();
            //ServiceScope.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Start()
        {
            _ = WorkerService;
        }

        protected virtual string CreateBuilderMethodName { get; } = "CreateHostBuilder";
        protected virtual string Environment { get; } = Environments.Development;

        public IServiceProvider Services => WorkerService.Host.Services;

        private TestWorkerService? _workerService;
        public TestWorkerService WorkerService =>
            _workerService ??= new TestWorkerService(_builder, ConfigureHost);

        // Optional: creates a service scope for you.
        /* public TService GetService<TService>()
            where TService : class => ServiceScope.ServiceProvider.GetRequiredService<TService>();

        private IServiceScope? _serviceScope;
        private IServiceScope ServiceScope => _serviceScope ??= Services.CreateScope();*/
    }
}

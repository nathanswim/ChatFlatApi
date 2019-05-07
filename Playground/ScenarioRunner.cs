using System.Threading.Tasks;

namespace Playground
{
    abstract class ScenarioRunner : IScenarioRunner
    {
        public void Run()
        {
            var t = Task.Run(async () => await RunAsync());
            t.Wait();
        }

        protected abstract Task RunAsync();
    }
}

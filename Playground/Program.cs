using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            IScenarioRunner runner = new Scenario1();
            var program = new Program(runner);
            program.Run();
            Console.ReadLine();
        }

        readonly IScenarioRunner runner;


        private Program(IScenarioRunner runner)
        {
            this.runner = runner;
        }

        void Run()
        {
            runner.Run();
        }

    }
}

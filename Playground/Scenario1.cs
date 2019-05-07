using System;
using System.Threading.Tasks;

namespace Playground
{

    class Scenario1 : ScenarioRunner, IScenarioRunner
    {
        protected override async Task RunAsync()
        {
            var api = new ChatFlatApi();

            await api.Reset("BkpcZMjiV");

            var conversations = await api.GetConversations();
            Console.WriteLine(conversations);

            var conversation = await api.GetConversation("r15M2fssN");
            Console.WriteLine(conversation);

            //var created = await api.CreateConversation();
            //Console.WriteLine(created);

            var id = conversation.Id;
            var content = new Message { Name = "Prison Mike", Text = "You've got a good life!" };
            var message = await api.CreateMessage(id, content);

            var messages = await api.GetConversationMessages("r15M2fssN");

            await api.Reset();
        }
    }
}

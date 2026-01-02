using System;
using System.Threading.Tasks;
using TimHanewich.Foundry.OpenAI.Responses;
using Spectre.Console;

namespace AgentForum
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainProgramAsync().Wait();
        }
        
        public static async Task MainProgramAsync()
        {
            
            //Welcome message!
            AnsiConsole.MarkupLine("[bold]Welcome to Agent Forum![/]");
            AnsiConsole.MarkupLine("[italic]https://github.com/TimHanewich/Agent-Forum[/]");
            Console.WriteLine();
            AnsiConsole.MarkupLine("This is an experimental project designed to test the interaction between two LLMs with custom instructions. This project uses [bold]Microsoft Foundry[/], specifically OpenAI models within Foundry, to serve for agent inference.");

            //Credentials needed for inference
            string Endpoint = "";       // i.e. https://my-project.services.ai.azure.com/openai/responses?api-version=2025-04-01-preview
            string ApiKey = "";         // the API key
            string ModelName = "";      // the name of the LLM deploy you want to use

            //Collect that detail if needed
            AnsiConsole.MarkupLine("[underline]Foundry Authentication[/]");
            if (Endpoint == "")
            {
                Endpoint = AnsiConsole.Ask<string>("Responses endpoint? [italic](i.e. https://my-project.services.ai.azure.com/openai/responses?api-version=2025-04-01-preview)[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[green]Endpoint received.[/]");
            }
            if (ApiKey == "")
            {
                ApiKey = AnsiConsole.Ask<string>("API key?");
            }
            else
            {
                AnsiConsole.MarkupLine("[green]API key received.[/]");
            }
            if (ModelName == "")
            {
                ModelName = AnsiConsole.Ask<string>("Model (deployment) name?");
            }
            else
            {
                AnsiConsole.MarkupLine("[green]Model name received.[/]");
            }
            Console.WriteLine();

            //Create the deployment
            Deployment d = new Deployment();
            d.Endpoint = Endpoint;
            d.ApiKey = ApiKey;

            //Info about agent A
            AnsiConsole.MarkupLine("[underline]Agent A System Prompt[/]");
            string SystemPromptAgentA = AnsiConsole.Ask<string>("> ");
            Console.WriteLine();

            //Info about agent B
            AnsiConsole.MarkupLine("[underline]Agent B System Prompt[/]");
            string SystemPromptAgentB = AnsiConsole.Ask<string>("> ");
            Console.WriteLine();

            //Construct agents
            Agent AgentA = new Agent(d, ModelName);
            AgentA.AddInput(new Message(Role.developer, SystemPromptAgentA));
            Agent AgentB = new Agent(d, ModelName);
            AgentB.AddInput(new Message(Role.developer, SystemPromptAgentB));

            //First dialog - what does agent a say?
            AnsiConsole.MarkupLine("[underline]First Dialog[/]");
            Console.WriteLine("Agent A speaks first. What do they say?");
            string response = AnsiConsole.Ask<string>("> "); //queue it up as the last response
            Console.WriteLine();

            while (true)
            {
                //Prompt Agent B
                AnsiConsole.Markup("[gray][italic]Agent B responding...[/][/]");
                AgentB.AddInput(new Message(Role.user, response));
                response = await AgentB.PromptAsync();
                Console.WriteLine();
                Console.WriteLine();
                AnsiConsole.MarkupLine("[bold][underline]AGENT B[/][/]");
                Console.WriteLine(response);
                Console.WriteLine();
                AnsiConsole.Markup("[gray][italic]Enter to continue...[/][/]");
                Console.ReadLine();

                //Prompt Agent A
                AnsiConsole.Markup("[gray][italic]Agent A responding...[/][/]");
                AgentA.AddInput(new Message(Role.user, response));
                response = await AgentA.PromptAsync();
                Console.WriteLine();
                Console.WriteLine();
                AnsiConsole.MarkupLine("[bold][underline]AGENT A[/][/]");
                Console.WriteLine(response);
                Console.WriteLine();
                AnsiConsole.Markup("[gray][italic]Enter to continue...[/][/]");
                Console.ReadLine();
            }
            

        }
    }
}
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

            //Create request we will use for entire thread with agent 1
            ResponseRequest rr_agent1 = new ResponseRequest();
            rr_agent1.Model = ModelName;

            //Create request we will use for entire thread with agent 2
            ResponseRequest rr_agent2 = new ResponseRequest();
            rr_agent2.Model = ModelName;

            //Create response objects we will always populate with latest response from each agent
            Response? r_agent1 = null;
            Response? r_agent2 = null;

            //Info about agent 1
            AnsiConsole.MarkupLine("[underline]Agent 1 System Prompt[/]");
            string SystemPromptAgent1 = AnsiConsole.Ask<string>("> ");
            Console.WriteLine();

            //Info about agent 2
            AnsiConsole.MarkupLine("[underline]Agent 2 System Prompt[/]");
            string SystemPromptAgent2 = AnsiConsole.Ask<string>("> ");
            Console.WriteLine();
            
            //CONVERSATION CYCLE!
            Message? ToInjectToAgent1 = new Message(Role.developer, SystemPromptAgent1);
            Message? ToInjectToAgent2 = new Message(Role.developer, SystemPromptAgent2);
            int InputTokensTally = 0;
            int OutputTokensTallky = 0;
            while (true)
            {


                ///// PROMPT AGENT 1 /////
                
                rr_agent1.Inputs.Clear(); //clear out old stuff

                //If there was a previous response from agent 1, add it back
                if (r_agent1 != null)
                {
                    rr_agent1.PreviousResponseID = r_agent1.Id;
                }

                //If there is a system prompt available, add it
                if (ToInjectToAgent1 != null)
                {
                    rr_agent1.Inputs.Add(ToInjectToAgent1);
                }

                //If there is a response from agent 2 it should know about, add it
                if (r_agent2 != null)
                {
                    foreach (Exchange ex in r_agent2.Outputs)
                    {
                        if (ex is Message msg)
                        {
                            if (msg.Text != null)
                            {
                                rr_agent1.Inputs.Add(new Message(Role.user, msg.Text));
                            }
                        }
                    }
                }

                //Call to service
                r_agent1 = await d.CreateResponseAsync(rr_agent1);

                //Increment trackers
                InputTokensTally = InputTokensTally + r_agent1.InputTokensConsumed;
                OutputTokensTallky = OutputTokensTallky + r_agent1.OutputTokensConsumed;

                //Print response
                AnsiConsole.MarkupLine("[bold][underline][blue]AGENT 1[/][/][/]");
                foreach (Exchange ex in r_agent1.Outputs)
                {
                    if (ex is Message msg)
                    {
                        if (msg.Text != null)
                        {
                            Console.WriteLine(msg.Text);
                        }
                    }
                }
                Console.ReadLine();








                ///// PROMPT AGENT 2 /////
                
                rr_agent2.Inputs.Clear(); //clear out old stuff

                //If there was a previous response from agent 2, add it back
                if (r_agent2 != null)
                {
                    rr_agent2.PreviousResponseID = r_agent2.Id;
                }

                //If there is a system prompt available, add it
                if (ToInjectToAgent2 != null)
                {
                    rr_agent2.Inputs.Add(ToInjectToAgent2);
                }

                //If there is a response from agent 1 it should know about, add it
                if (r_agent1 != null)
                {
                    foreach (Exchange ex in r_agent1.Outputs)
                    {
                        if (ex is Message msg)
                        {
                            if (msg.Text != null)
                            {
                                rr_agent2.Inputs.Add(new Message(Role.user, msg.Text));
                            }
                        }
                    }
                }

                //Call to service
                r_agent2 = await d.CreateResponseAsync(rr_agent2);

                //Increment trackers
                InputTokensTally = InputTokensTally + r_agent2.InputTokensConsumed;
                OutputTokensTallky = OutputTokensTallky + r_agent2.OutputTokensConsumed;

                //Print response
                AnsiConsole.MarkupLine("[bold][underline][blue]AGENT 2[/][/][/]");
                foreach (Exchange ex in r_agent2.Outputs)
                {
                    if (ex is Message msg)
                    {
                        if (msg.Text != null)
                        {
                            Console.WriteLine(msg.Text);
                        }
                    }
                }
                Console.ReadLine();


            }

        }
    }
}
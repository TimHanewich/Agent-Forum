using System;
using TimHanewich.Foundry.OpenAI.Responses;

namespace AgentForum
{
    public class Agent
    {
        private Deployment Deployment;
        private string Model;
        private string? PreviousResponseID;
        private List<Message> Inputs;

        public Agent(Deployment d, string model)
        {
            Deployment = d;
            Model = model;
            PreviousResponseID = null;
            Inputs = new List<Message>();
        }

        public void AddInput(Message msg)
        {
            Inputs.Add(msg);
        }

        public async Task<string> PromptAsync()
        {
            if (Inputs.Count == 0)
            {
                throw new Exception("You must specify at least one input.");
            }

            ResponseRequest rr = new ResponseRequest();
            rr.Model = Model;
            rr.PreviousResponseID = PreviousResponseID;
            rr.Inputs.AddRange(Inputs);

            //Call
            Response r = await Deployment.CreateResponseAsync(rr);
            PreviousResponseID = r.Id;

            //Clear out inputs for next time
            Inputs.Clear();

            //Extract response
            foreach (Exchange ex in r.Outputs)
            {
                if (ex is Message msg)
                {
                    if (msg.Text != null)
                    {
                        return msg.Text;
                    }
                }
            }
            throw new Exception("Agent did not return text!");
        }

    }
}
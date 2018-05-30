using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;

namespace FarmingBot.Services
{
    [Serializable]
    public class FarmingQuery
    {
        [Prompt("Please post your query below. Our expert will provide the resolution through email.")]
        public string Query { get; set; }
    }

}
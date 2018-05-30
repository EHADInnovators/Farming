using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FarmingBot.Dialogs
{
    // Model Id == App Id that you'll find in LUIS Portal
    // Find at: https://www.luis.ai/home/keys 
    // Subscription Key is one of the two keys from your Cognitive Services App.
    // Find at: https://portal.azure.com in the Resource Group where you've created
    // your Cognitive Services resource on the Keys blade.
    [LuisModel("49d2735c-cc60-483f-a203-d98509c17a6a", "0831d782457f4ab8a0222f5ce4233c6d")]
    [Serializable]

    public class AppRootDialog : LuisDialog<object>
    {
        private const string EntityFarming = "Farming";

        [LuisIntent("GetAssitance.Crops")]
        public async Task GetAssistanceForCrops(IDialogContext context,
                                 IAwaitable<IMessageActivity> activity,
                                 LuisResult result)
        {
            Trace.TraceInformation("AppRootDialog::GetAssistanceForCrops");

            var message = await activity;
            IAwaitable<object> awaitableMessage = await activity as IAwaitable<object>;

            if (!result.TryFindEntity(EntityFarming, out EntityRecommendation farmingRec)
                || farmingRec.Score <= .5)
            {
                Trace.TraceWarning("Low Confidence in Get Assitance for farms.");

                await context.PostAsync($"I'm sorry, I don't understand '{message.Text}'. I am a Virtual assistance for farmers. Please try asking me to 'I want to get assistance for farms related queries'.");
                context.Wait(this.MessageReceived);
                return;
            }

            await context.PostAsync("I see you want to get assistance for farms.");

            await context.Forward(new AppAuthDialog(),
                this.ResumeAfterFarmingServicesDialog, message, CancellationToken.None);
        }

        private async Task ResumeAfterFarmingServicesDialog(IDialogContext context, IAwaitable<object> result)
        {
            //await context.PostAsync("Thank you. We're all done. What else can I do for you?");

            context.Done<object>(null);
        }

        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            try
            {
                Trace.TraceInformation("MessagesController::Help");
                await context.PostAsync("Hi I don't understand this right now! I am a Virtual assistance for farmers. Please try asking me to 'I want to get assistance for farms related queries'.");
            }
            catch (Exception ex)
            {
                Trace.TraceInformation("MessagesController::HelException" + ex);
                throw ex;
            }

            //context.Wait(this.MessageReceived);
        }

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand '{result.Query}'. I am a Virtual assistance for farmers. Please try asking me to 'I want to get assistance for farms related queries'.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }
    }
}
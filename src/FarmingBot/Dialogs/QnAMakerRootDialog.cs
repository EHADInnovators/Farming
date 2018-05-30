using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace FarmingBot.Dialogs
{
    [Serializable]
    [QnAMaker("4e109f94f73f4701bcae61495b0737ae", "e544f56d-74eb-493f-996f-4bd1e5161a93", "Please hold on!!", 0.01, 1)]
    public class QnAMakerRootDialog : QnAMakerDialog
    {
        protected override async Task RespondFromQnAMakerResultAsync(IDialogContext context, IMessageActivity message, QnAMakerResults results)
        {
            try
            {
                Trace.TraceInformation("QnAMakerRootDialog::QnA" + results.Answers.Count.ToString());
                if (results.Answers.Count > 0)
                {
                    var response =
                        results.Answers.First().Answer;
                    await context.PostAsync(response);
                }
            }
            catch(Exception ex)
            {
                Trace.TraceInformation("QnAMakerRootDialog::Exception" + ex);
                throw ex;
            }
        }

        protected override async Task DefaultWaitNextMessageAsync(IDialogContext context, IMessageActivity message, QnAMakerResults results)
        {
            if (results.Answers.Count == 0)
            {
                var childFaq = new AppRootDialog();
                await context.Forward(childFaq, AfterFAQDialog, message, CancellationToken.None);
            }
            else
            {
                await base.DefaultWaitNextMessageAsync(context, message, results);
            }
        }

        private Task AfterFAQDialog(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);
            return Task.CompletedTask;
        }

        private Task AfterFAQDialog(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            context.Done<object>(null);
            return Task.CompletedTask;
        }
    }
}
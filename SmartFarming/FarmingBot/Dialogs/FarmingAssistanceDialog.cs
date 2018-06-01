using FarmingBot.Services;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FarmingBot.Dialogs
{
    [Serializable]
    public class FarmingAssistanceDialog : IDialog<object>
    {
        private const string UserSessionDataKey = "userdata";
        private string reservationChoice;
        /// <summary>
        /// Start method for chatbot
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task StartAsync(IDialogContext context)
        {
            Trace.TraceInformation("FarmingAssistanceDialog::StartAsync");

            await context.PostAsync("Welcome to Farm Assistance Services!");

            UserProfile userInfo = context.ConversationData.GetValue<UserProfile>(UserSessionDataKey);
            IFarmingOperations ihro = ServiceLocator.GetFarmingOperations();
            IList<string> menuOptions = await ihro.GetCropCategories(userInfo.Id);

            PromptDialog.Choice(context, OnOptionSelected, menuOptions,
                    "Please choose type of a farm that you looking for assistance:",
                    "Not a valid option", 2);
        }

        /// <summary>
        /// This method will get executed, once the category selected for assistance 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                Trace.TraceInformation("AppAuthDialog::OnOptionSelected");
                string optionSelected = await result;

                UserProfile userInfo = context.ConversationData.GetValue<UserProfile>(UserSessionDataKey);
                userInfo.CropCategory = optionSelected;
                reservationChoice = optionSelected;
                context.ConversationData.SetValue<UserProfile>(UserSessionDataKey, userInfo);

                IFarmingOperations ihro = ServiceLocator.GetFarmingOperations();

                IList<string> menuOptions = await ihro.GetCropSubCategories(optionSelected);

                if (menuOptions.Count > 0)
                {
                    PromptDialog.Choice(context, OnSubOptionSelected, menuOptions,
                            string.Format("Please choose type of a {0} that you looking for assistance:", userInfo.CropCategory),
                            "Not a valid option", 2);
                }
                else
                {
                    await context.PostAsync($"Sorry. I haven't trained in this area yet. ");
                    PostQuery(context);
                }

            }
            catch (TooManyAttemptsException ex)
            {
                string fullError = ex.ToString();
                Trace.TraceError(fullError);

                await context.PostAsync($"Sorry, I don't understand.");

                context.Done(true);
            }
        }

        /// <summary>
        /// Query builder
        /// </summary>
        /// <param name="context"></param>
        private void PostQuery(IDialogContext context)
        {
            var farmingQueryDialog = FormDialog.FromForm(this.BuildFarmingForm, FormOptions.PromptInStart);
            context.Call(farmingQueryDialog, this.CollectQuery);
        }

        /// <summary>
        /// This method will get executed once sub category will get selected.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task OnSubOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                Trace.TraceInformation("AppAuthDialog::OnSubOptionSelected");
                string optionSelected = await result;

                UserProfile userInfo = context.ConversationData.GetValue<UserProfile>(UserSessionDataKey);
                userInfo.CropSubCategory = optionSelected;
                reservationChoice = optionSelected;
                context.ConversationData.SetValue<UserProfile>(UserSessionDataKey, userInfo);

                IFarmingOperations ihro = ServiceLocator.GetFarmingOperations();

                IList<string> menuOptions = await ihro.GetSearchOptions(optionSelected);

                if (menuOptions.Count > 0)
                {
                    PromptDialog.Choice(context, OnSearchOptionSelected, menuOptions,
                            "Please choose Search options to serve you better",
                            "Not a valid option", 2);
                }
                else
                {
                    await context.PostAsync($"Sorry. I haven't trained in this area yet. ");
                    PostQuery(context);
                }


            }
            catch (TooManyAttemptsException ex)
            {
                string fullError = ex.ToString();
                Trace.TraceError(fullError);

                await context.PostAsync($"Sorry, I don't understand.");

                context.Done(true);
            }
        }

        /// <summary>
        /// This method will get executed once the search option get selected.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task OnSearchOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                Trace.TraceInformation("AppAuthDialog::OnSubOptionSelected");
                string optionSelected = await result;

                UserProfile userInfo = context.ConversationData.GetValue<UserProfile>(UserSessionDataKey);
                userInfo.SearchOption = optionSelected;
                reservationChoice = optionSelected;
                context.ConversationData.SetValue<UserProfile>(UserSessionDataKey, userInfo);

                IFarmingOperations ihro = ServiceLocator.GetFarmingOperations();

                IList<string> menuOptions = await ihro.GetInfo(optionSelected, userInfo.CropSubCategory);

                if (menuOptions.Count > 0)
                {
                    PromptDialog.Choice(context, OnSymptomsSelected, menuOptions,
                            "Please choose the below option",
                            "Not a valid option", 2);
                }
                else
                {
                    await context.PostAsync($"Sorry. I haven't trained in this area yet. ");
                    PostQuery(context);
                }

            }
            catch (TooManyAttemptsException ex)
            {
                string fullError = ex.ToString();
                Trace.TraceError(fullError);

                await context.PostAsync($"Sorry, I don't understand.");

                context.Done(true);
            }
        }

        /// <summary>
        /// This method will get selected once the symptom selected.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task OnSymptomsSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                Trace.TraceInformation("AppAuthDialog::OnSubOptionSelected");
                string optionSelected = await result;

                UserProfile userInfo = context.ConversationData.GetValue<UserProfile>(UserSessionDataKey);
                userInfo.SearchOption = optionSelected;
                reservationChoice = optionSelected;
                string subCategory = userInfo.CropSubCategory;
                context.ConversationData.SetValue<UserProfile>(UserSessionDataKey, userInfo);

                IFarmingOperations ihro = ServiceLocator.GetFarmingOperations();

                await context.PostAsync(ihro.GetSymptomResults(optionSelected, subCategory));

                IList<string> menuOptions = new List<string> { "Yes", "No" };

                    PromptDialog.Choice(context, OnResponseSelected, menuOptions,
                            "Did you get answer for your query?",
                            "Not a valid option", 2);

            }
            catch (TooManyAttemptsException ex)
            {
                string fullError = ex.ToString();
                Trace.TraceError(fullError);

                await context.PostAsync($"Sorry, I don't understand.");

                context.Done(true);
            }
        }

        /// <summary>
        /// This method will get executed if user is not satisfied for the response provided
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task OnResponseSelected(IDialogContext context, IAwaitable<string> result)
        {
            Trace.TraceInformation("AppAuthDialog::OnSubOptionSelected");
            string optionSelected = await result;

            UserProfile userInfo = context.ConversationData.GetValue<UserProfile>(UserSessionDataKey);
            userInfo.SearchOption = optionSelected;
            context.ConversationData.SetValue<UserProfile>(UserSessionDataKey, userInfo);

            if(optionSelected == "No")
            {
                await context.PostAsync($"Sorry for not helping you at this time. ");
                var farmingQueryDialog = FormDialog.FromForm(this.BuildFarmingForm, FormOptions.PromptInStart);
                context.Call(farmingQueryDialog, this.CollectQuery);
            }
            else
            {
                await context.PostAsync($"Thanks " + userInfo.Name + " for contacting us. Have a good day!!.");
                context.Done(true);
            }

        }

        /// <summary>
        /// Build farming form
        /// </summary>
        /// <returns></returns>
        private IForm<FarmingQuery> BuildFarmingForm()
        {
            return new FormBuilder<FarmingQuery>()
                .Field(nameof(FarmingQuery.Query))
                .AddRemainingFields()
                .Build();
        }

        /// <summary>
        /// To get query input from user.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task CollectQuery(IDialogContext context, IAwaitable<FarmingQuery> result)
        {
            try
            {

                FarmingQuery searchQuery = await result;
                string query = searchQuery.Query;

                UserProfile userInfo = context.ConversationData.GetValue<UserProfile>(UserSessionDataKey);
                userInfo.Query = query;
                context.ConversationData.SetValue<UserProfile>(UserSessionDataKey, userInfo);


                IList<string> menuOptions = new List<string> { "Yes", "No" };

                await context.PostAsync($"Please share if you have any images / media files for your query. It will be useful us to server you better");

                PromptDialog.Choice(context, OnQueryResponseSelected, menuOptions,
                        "Do you want to upload any media files?  ",
                        "Not a valid option", 2);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Query response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task OnQueryResponseSelected(IDialogContext context, IAwaitable<string> result)
        {
            Trace.TraceInformation("AppAuthDialog::OnSubOptionSelected");
            string query = await result;

            UserProfile userInfo = context.ConversationData.GetValue<UserProfile>(UserSessionDataKey);
            userInfo.Query = query;
            context.ConversationData.SetValue<UserProfile>(UserSessionDataKey, userInfo);

            if (query == "No")
            {
                IFarmingOperations ihro = ServiceLocator.GetFarmingOperations();
                ihro.UpdateQueries(userInfo.Name, userInfo.EMail, userInfo.Query, string.Empty);

                await context.PostAsync($"Thanks " + userInfo.Name + " for contacting us. Our expert team will reach you shortly.");
                context.Done<object>(null);
            }
            else
            {
                PromptDialog.Attachment(context, OnUploadImageResponse,
                        "Please upload any images / media files for your query.  ");
            }

        }

        /// <summary>
        /// To upload image responses from user.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task OnUploadImageResponse(IDialogContext context, IAwaitable<IEnumerable<Attachment>> result)
        {
            var attachments = await result;
            UserProfile userInfo = context.ConversationData.GetValue<UserProfile>(UserSessionDataKey);
            IFarmingOperations ihro = ServiceLocator.GetFarmingOperations();
            foreach (var attachment in attachments)
            {
                ihro.UpdateQueries(userInfo.Name, userInfo.EMail, userInfo.Query, attachment.ContentUrl);
            }

            await context.PostAsync($"Thanks " + userInfo.Name + " for contacting us. Our expert team will reach you shortly.");
            context.Done<object>(null);

        }
    }
}
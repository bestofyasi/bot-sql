using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_WH.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private const string addToStock = "Add items to stock";
        private const string deleteToStock = "Delete items from stock";
        private const string updateStock = "Add new item to stock";

        DBAccess dba = new DBAccess();

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            PromptDialog.Choice(
                 context,
                 this.AfterChoiceSelectedAsync,
                 new[] { addToStock, deleteToStock, updateStock },
                 "What you want to do?");
        }
        private async Task AfterChoiceSelectedAsync(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                var selection = await result;


                switch (selection)
                {
                    case addToStock:
                        await context.PostAsync("Adding to stock");
                        dba.addItems("sample", 33);
                        await this.StartAsync(context);
                        break;

                    case deleteToStock:
                        await context.PostAsync("Deleting to stock");
                        await this.StartAsync(context);
                        break;
                    case updateStock:
                        await context.PostAsync("Updating to stock");
                        await this.StartAsync(context);
                        break;
                }

            }
            catch (TooManyAttemptsException)
            {
                await this.StartAsync(context);
            }

        }
    }
}
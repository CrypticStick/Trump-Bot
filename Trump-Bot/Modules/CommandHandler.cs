using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Trump_Bot.Modules
{
    static class CommandHandler
    {
        private static DiscordSocketClient _client = Program.client;
        public static CommandService service;
        static MainForm _gui = new MainForm();

        public static CommandService Service
        {
            get { return service; }
        }

        public static async Task InitializeAsync()
        {
            service = new CommandService();
            service.Log += _gui.LogConsole;

            await service.AddModulesAsync(Assembly.GetEntryAssembly());

            _client.MessageReceived += HandleCommandAsync;
        }

        private static async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;

            int argPos = 0;
            if (!(msg.HasMentionPrefix(_client.CurrentUser, ref argPos) || msg.HasCharPrefix('$', ref argPos))) return;
            if (msg.Author.IsBot) return;

            var context = new SocketCommandContext(_client, msg);

            var result = await service.ExecuteAsync(context, argPos);

            if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
            service.Log += _gui.LogConsole;
        }
    }
}

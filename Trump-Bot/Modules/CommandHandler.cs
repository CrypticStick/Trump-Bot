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
    class CommandHandler
    {
        private DiscordSocketClient _client;
        public CommandService _service;
        MainForm _gui = new MainForm();

        public CommandService Service
        {
            get { return _service; }
        }

        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();
            _service.Log += _gui.LogConsole;

            await _service.AddModulesAsync(Assembly.GetEntryAssembly());
            await Commands.ImportClient(_service, _client);

            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;

            int argPos = 0;
            if (!(msg.HasMentionPrefix(_client.CurrentUser, ref argPos) || msg.HasCharPrefix('$', ref argPos))) return;
            if (msg.Author.IsBot) return;

            var context = new SocketCommandContext(_client, msg);

            var result = await _service.ExecuteAsync(context, argPos);

            if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
            _service.Log += _gui.LogConsole;
        }
    }
}

using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trump_Bot.Modules;
using static Trump_Bot.Modules.Commands;

namespace Trump_Bot
{
    static class Program
    {
        private static bool loopBegan = false;
        public static DiscordSocketClient _client;
        private static DiscordSocketClient _userClient;
        private static CommandHandler _handler;
        private static Commands _commands;
        private static Conditionals _conditionals;
        private static String _botToken = System.IO.File.ReadAllText("BotToken.txt");
        private static MainForm _gui;

        [STAThread]
         static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm _gui = new MainForm();
            new Thread(() => //Main GUI Thread
            {
                Application.Run(_gui);
            }).Start();
            StartAsync(_gui);
        }


        public static async Task StartAsync(MainForm gui)
        {
            new Thread(async () => //Trump Bot Thread
            {
                _gui = gui;
                _client = new DiscordSocketClient(new DiscordSocketConfig { MessageCacheSize = 10000 });
                await _gui.InitializeForm(_client);
                await _client.LoginAsync(Discord.TokenType.Bot, _botToken);
                await _client.StartAsync();

                _handler = new CommandHandler();
                await _handler.InitializeAsync(_client);

                _conditionals = new Conditionals();

                _client.Log += _gui.LogConsole;
                await _conditionals.InitializeConditionals(_client, _gui);

                _client.Ready += Client_Ready;

                await Task.Delay(-1);
                Environment.Exit(0);
            }).Start();
        }

        static async private Task RestoreConnect4Sessions()
        {
            _commands = new Commands();
            foreach (Connect4Session session in Connect4)
            {
                await _commands.StartConnect4Game(session.Channel);
            }
        }

        static public List<string> Array2List(string[] Array)
        {
            return Array.OfType<string>().ToList();
        }

        static public string[] List2Array(List<string> list)
        {
            return list.ToArray();
        }

        static private async Task Client_Ready()
        {
            var application = await _client.GetApplicationInfoAsync();
            await _gui.LogConsole(new LogMessage(LogSeverity.Info, "Program",
                $"Invite URL: <https://discordapp.com/oauth2/authorize?client_id={application.Id}&scope=bot&permissions=2146958591>"));
            await _client.SetGameAsync("build that wall (type $help)");
        }
    }
}

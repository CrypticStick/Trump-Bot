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
using static Trump_Bot.Modules.Connect4;

namespace Trump_Bot
{
    static class Program
    {
        public static DiscordSocketClient client;
        private static Commands _commands;
        private static Conditionals _conditionals;
        private static String _botToken = System.IO.File.ReadAllText("BotToken.txt");
        public static MainForm gui;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            gui = new MainForm();
            new Thread(() => //Main GUI Thread
            {
                Application.Run(gui);
            }).Start();
            StartAsync();
        }


        public static Task StartAsync()
        {
            new Thread(async () => //Trump Bot Thread
            {
                client = new DiscordSocketClient(new DiscordSocketConfig { MessageCacheSize = 10000 });
                await gui.InitializeForm(client);
                await client.LoginAsync(Discord.TokenType.Bot, _botToken);
                await client.StartAsync();

                await CommandHandler.InitializeAsync();

                _conditionals = new Conditionals();

                client.Log += gui.LogConsole;
                await _conditionals.InitializeConditionals();

                client.Ready += Client_Ready;

                await Task.Delay(-1);
                Environment.Exit(0);
            }).Start();

            return Task.CompletedTask;
        }

        static async private Task RestoreConnect4Sessions()
        {
            _commands = new Commands();
            foreach (Connect4Session session in Connect4SessionList)
            {
                await StartConnect4Game(session.Channel);
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
            var application = await client.GetApplicationInfoAsync();
            await gui.LogConsole(new LogMessage(LogSeverity.Info, "Program",
                $"Invite URL: <https://discordapp.com/oauth2/authorize?client_id={application.Id}&scope=bot&permissions=2146958591>"));
            await client.SetGameAsync("build that wall (type $help)");
        }
    }
}

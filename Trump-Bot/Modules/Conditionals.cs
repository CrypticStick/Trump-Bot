using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Trump_Bot.Modules.Connect4;

namespace Trump_Bot.Modules
{
    class Conditionals
    {
        private DiscordSocketClient _client = Program.client;
        private MainForm _gui = Program.gui;
        public static List<IMessage> DeletedMessages = new List<IMessage>();

        public async Task InitializeConditionals()
        {
            _client.MessageReceived += MessageReceived;
            _client.MessageDeleted += MessageDeleted;
            _client.MessageUpdated += MessageUpdated;
            _client.ReactionAdded += _client_ReactionAdded;
            _client.UserBanned += _client_UserBanned;
        }

        private async Task _client_UserBanned(SocketUser user, SocketGuild server)
        {
            await server.DefaultChannel.SendMessageAsync($"R.I.P. in pieces! <@{user.Id}> has been BAMMED!");
        }

        private async Task _client_ReactionAdded(Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (reaction.UserId == _client.CurrentUser.Id) return;
            var _Connect4Session = GrabConnect4Session(channel);
            if (_Connect4Session != null)
            {
                new Thread(async () =>
                {
                    if (!ReactionButtons.Contains(reaction.Emote) || (_Connect4Session.PlayerOne != reaction.User.Value || _Connect4Session.PlayerTwo != reaction.User.Value))
                    {
                        try { await reaction.Message.GetValueOrDefault().RemoveReactionAsync(reaction.Emote, reaction.User.Value); }
                        catch { }
                        return;
                    }
                    if (_Connect4Session.LastSentMessage.Id == message.Id)
                    {
                        if (_Connect4Session.PlayerTurn == 1 && _Connect4Session.PlayerOne == (SocketUser)reaction.User)
                            _Connect4Session.RequestedColumn = ReactionButtons.IndexOf((Emoji)reaction.Emote) + 1;
                        else if (_Connect4Session.PlayerTurn == 2 && _Connect4Session.PlayerTwo == (SocketUser)reaction.User)
                            _Connect4Session.RequestedColumn = ReactionButtons.IndexOf((Emoji)reaction.Emote) + 1;
                        _Connect4Session.Flags = "loading";
                        while (_Connect4Session.Flags == "loading") { }
                        try { await reaction.Message.GetValueOrDefault().RemoveReactionAsync(reaction.Emote, reaction.User.Value); }
                        catch { }
                    }
                }).Start();
            }
        }

        public Boolean badboy(string message)
        {
            char[] badLoops = { 'e', 'ė', 'ë', '3', 'е' };
            string[] words = message.ToLower().Split(' ');
            List<int> possible = new List<int>();
            foreach (string word in words)
            {
                var _word = word;
                var firstK = _word.IndexOf('k');
                var lastK = _word.LastIndexOf('k');
                var danger = _word.IndexOfAny(badLoops);
                while (danger != -1)
                {
                    possible.Add(danger);
                    _word = _word.Remove(danger, 1);
                    danger = _word.IndexOfAny(badLoops);
                }
                foreach (int suspect in possible)
                {
                    if (firstK < suspect && lastK > suspect)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public SocketGuild currentGuild(SocketMessage _message)
        {
            var currentChannel = _message.Channel as SocketGuildChannel;
            var _currentGuild = currentChannel.Guild;
            return _currentGuild;
        }

        public SocketGuildChannel logChannel(SocketMessage _message)
        {
            if (_message.Channel.Name == "log") return null; //Don't let it log a message in the log channel

            var allChannels = currentGuild(_message).Channels;
            SocketGuildChannel logChannel = null;
            foreach (SocketGuildChannel _channel in allChannels)
            {
                string _channelName = _channel.Name;
                if (_channelName == "log")
                {
                    logChannel = _channel;
                }
            }
            return logChannel;
        }

        public async Task MessageReceived(SocketMessage message)
        {
            if (message.Channel.Name == "log")
            {
                if (message.Author.Id == 319643595852349440 || message.Author.Id == 321097604131717120) return;
                else
                {
                    await message.DeleteAsync();
                    return;
                }
            }

            if (logChannel(message) != null && currentGuild(message).GetUser(319643595852349440).Status == UserStatus.Offline)
            {
                var _logChannel = (ITextChannel)logChannel(message);
                char empty = '\u2063';
                await _logChannel.SendMessageAsync(empty.ToString() + $" ```{message.Content}``` " +
                                                   $"  —{message.Author.Username} in <#{message.Channel.Id.ToString()}>");
            }

            var MuteList = System.IO.File.ReadAllLines("MutedUserList.txt");
            if (MuteList.Contains(message.Author.Id.ToString()))
            {
                await message.DeleteAsync();
            }

            var msg = message.Content.ToLower();

            if (message.Author.IsBot) return;

            if (System.IO.File.ReadAllLines("AutoBList.txt").Contains(message.Channel.Id.ToString()) && (message.Content.Contains('b') || message.Content.Contains('B')))
            {
                if (message.Content.Contains("`PROTECTED`")) return;
                var UndeleteList = System.IO.File.ReadAllLines("UndeleteUserList.txt");
                if (UndeleteList.Contains(message.Author.Id.ToString())) return;
                var convertedOnce = message.Content.Replace("b", ":b:");
                var convertedTwice = convertedOnce.Replace("B", ":b:");
                await message.DeleteAsync();
                var e = await message.Channel.SendMessageAsync("`" + message.Author.Username + "`: " + convertedTwice);
                Emoji b = new Emoji("🅱");
                await e.AddReactionAsync(b);
            }

            var allowedChannels = System.IO.File.ReadAllLines("AllowTriggersList.txt");
            if (!allowedChannels.Contains(message.Channel.Id.ToString())) return;


            string[,] TriggerList = new string[,]
            {
                {"smart", "My IQ is one of the highest — and you all know it! Please don’t feel so stupid or insecure; it’s not your fault."},
                {"humble", "I think I am actually humble. I think I’m much more humble than you would understand."},
                {"fnn", "https://www.youtube.com/watch?v=Ci4QZ82n2i8"},
                {"elect", "We should just cancel the election and just give it to Trump."},
                {"gun", "If she gets to pick her judges – nothing you can do, folks.Although, the Second Amendment people. Maybe there is.I don’t know."},
                {"nuke", "Why can’t we use nuclear weapons?"},
                {"baby", "I love babies ..." + Environment.NewLine + "Actually, I was only kidding. You can get that baby out of here. Don’t worry, I think she really believed me that I love having a baby crying while I’m speaking."},
                {"babies", "I love babies ..." + Environment.NewLine + "Actually, I was only kidding. You can get that baby out of here. Don’t worry, I think she really believed me that I love having a baby crying while I’m speaking."},
                {"covfefe", "Despite the constant negative press covfefe"},
                {"email", "Russia, if you’re listening, I hope you’re able to find the 30,000 emails that are missing. I think you will probably be rewarded mightily by our press."},
                {"wall", "I will build a great wall – and nobody builds walls better than me, believe me – and I’ll build them very inexpensively. I will build a great, great wall on our southern border, and I will make Mexico pay for that wall. Mark my words."},
                {"china", "I love China."},
                {"delete", "Did I hear \"delete\"? WE CAN'T DELETE THINGS. You remember Hillary's history, right?"},
                {"spin", "SPIN TO WIN! VSSSSSSSSSSSSSSSHHH I LOVE MY FIDGET SPINNER WEEEEEEEEEEEEEEEE!!!"},
                {"kim jong", "Kim Jong Un is 27 years old. His father dies, took over a regime. So say what you want but that is not easy, especially at that age."},
                {"9/11", "I was down there, and I watched our police and our firemen, down on 7-Eleven, down at the World Trade Center, right after it came down."},
                {"911", "40 Wall Street actually was the second-tallest building in downtown Manhattan... And now it’s the tallest."},
                {"lie", "I might lie to you like Hillary does all the time, but I'll never lie to Giacomo, okay?"},
                {"bing", "BING BING BONG BONG"}
            };

            string CheckForTrigger()
            {
                for (var i = 0; i <= TriggerList.Length / 2; i++)
                    if (msg.ToLower().Contains(TriggerList[i, 0])) return TriggerList[i, 1];
                return null;
            }
            if (CheckForTrigger() != null)
                await message.Channel.SendMessageAsync(CheckForTrigger());
        }

        public async Task MessageDeleted(Cacheable<IMessage, ulong> message, ISocketMessageChannel channel)
        {
            var x = await message.GetOrDownloadAsync();

            if (x.Author.Id == 322807144833351680)
            {
                await channel.SendMessageAsync("Thank you for deleting Hillary's message. I hate hearing her annoying voice.");
            }

            if (x.Content.Contains("`PROTECTED`"))
            {
                if (x.Content.StartsWith("$echo")) return;
                var newmessage = "";
                if (x.Content.Contains("Somebody attempted to delete a protected message. It said:"))
                {
                    newmessage = x.Content.Remove(0, 60);
                }
                else
                {
                    newmessage = ("`" + x.Author.Username + "`: " + x.Content);
                }
                var e = await channel.SendMessageAsync("Somebody attempted to delete a protected message. It said:" + Environment.NewLine + newmessage);
                Emoji smirk = new Emoji("😏");
                await e.AddReactionAsync(smirk);
            }

            if (x.Author.IsBot) return;

            DeletedMessages.Add(x);
            var UndeleteList = System.IO.File.ReadAllLines("UndeleteUserList.txt");
            if (UndeleteList.Contains(x.Author.Id.ToString()))
            {
                await channel.SendMessageAsync("`PROTECTED` - `" + x.Author.Username + "`: " + " just had a message deleted. It said:" + Environment.NewLine + x.Content);
            }
        }

        public async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage message, ISocketMessageChannel arg3)
        {
            if (logChannel(message) != null && currentGuild(message).GetUser(319643595852349440).Status == UserStatus.Offline)
            {
                var _logChannel = (ITextChannel)logChannel(message);
                char empty = '\u2063';
                await _logChannel.SendMessageAsync(empty.ToString() + $" ```EDITED MESSAGE: {message.Content}``` " +
                                                   $"  —{message.Author.Username} in <#{message.Channel.Id.ToString()}>");
            }
        }
    }
}

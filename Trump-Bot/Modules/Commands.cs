using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using static Trump_Bot.Modules.Connect4;

namespace Trump_Bot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        private static CommandService _service = CommandHandler.service;
        private static DiscordSocketClient _client = Program.client;
        public static List<IMessage> DeletedMessages = Conditionals.DeletedMessages;
        private static List<string> pics = new List<string>()
        {
            "https://m.popkey.co/160d81/k08O5.gif?c=popkey-web&p=popkey&i=hotlinebling&l=direct&f=.gif",
            "https://media.giphy.com/media/xTiTnHXbRoaZ1B1Mo8/giphy.gif",
            "https://media.giphy.com/media/VNF7YegY0DMwU/giphy.gif",
            "https://s3.amazonaws.com/wp-ag/wp-content/uploads/sites/72/2016/01/trump-gif-quotes.gif",
            "https://img.washingtonpost.com/blogs/the-fix/files/2015/11/3_TrumpFlag.gif",
            "https://img.washingtonpost.com/blogs/the-fix/files/2016/12/Trump_water.gif",
            "https://em.wattpad.com/f399c9a9d3dda0c8c459d18e73f507aa8e55f506/68747470733a2f2f73332e616d617a6f6e6177732e636f6d2f776174747061642d6d656469612d736572766963652f53746f7279496d6167652f573065412d4d4f343063334b6b773d3d2d3235353334353933372e313434643065653133653637303764362e676966?s=fit&w=1280&h=1280",
            "https://media.giphy.com/media/3oz8xLd9DJq2l2VFtu/giphy.gif",
            "https://i1.wp.com/media.boingboing.net/wp-content/uploads/2016/04/trumpwasphone-2MB.gif?fit=500%2C281",
            "https://i2.wp.com/fusion.net/wp-content/uploads/2016/03/cannotunsee7.gif?resize=480%2C480&quality=80&strip=all&ssl=1",
            "https://s23.postimg.org/wb6a1elm3/Yjky_ZGFh_ZDU0_ZSMv_Tk_FIRU9_CWU13_RGxy_WE5_CQmd_MSnox_LWtq.gif",
            "https://i2.wp.com/fusion.net/wp-content/uploads/2016/03/cannotunsee6.gif?resize=600%2C600&quality=80&strip=all&ssl=1",
            "https://i1.wp.com/fusion.net/wp-content/uploads/2016/03/cannotunsee1.gif?resize=740%2C988&quality=80&strip=all&ssl=1",
            "http://i.cdn.turner.com/cnn/interactive/2016/01/politics/trumpshade/media/False.gif",
            "https://media.giphy.com/media/l41lPd4rlOlw1lRqU/giphy.gif",
            "https://media.giphy.com/media/l41m5hkVQg20z10ys/giphy.gif",
            "http://storage.quebecormedia.com/v1/jdx-prod-images/photo/7b19323f-be50-450e-9ce0-c8fa80ce71b1_NEW01ab9d23-e3bc-4497-be33-48a765a31b98_TRUMP.gif",
            "https://img.washingtonpost.com/blogs/the-fix/files/2015/11/9_BingBong_CNN.gif"
        };

        private async Task temporaryMessage(string message, ISocketMessageChannel channel, int milliDelay)
        {
            new Thread(async () =>
            {
                var _message = await channel.SendMessageAsync(message);
                await Task.Delay(milliDelay);
                await _message.DeleteAsync();
            }).Start();
        }


        [Command("help")]
        [Summary("Lists information about available commands [`{CommandName}`]")]
        public async Task HalpM8ICantDoIt([Optional] string command)
        {
            var commands = _service.Commands;
            var message = "";

            if (command == null)
            {
                foreach (CommandInfo info in commands)
                {
                    message += ("`$" + info.Name + "` - " + info.Summary + Environment.NewLine);
                }
                await Context.Channel.SendMessageAsync(message);
            }
            else
            {
                foreach (CommandInfo info in commands)
                {
                    if (info.Name.ToLower() != command.ToLower()) continue;
                    message += ("`$" + info.Name + "` - " + info.Summary + Environment.NewLine);
                }
                if (message == "")
                {
                    await Context.Channel.SendMessageAsync("Command does not exist (check that you spelled correctly).");
                }
                else
                {
                    await Context.Channel.SendMessageAsync(message);
                }
            }

        }

        [Command("say")]
        [Alias("echo")]
        [Summary("Repeats the given text.")]
        public async Task IWillEchoYouMLad(params string[] repeat)
        {
            await Context.Message.DeleteAsync();

            var fullmessage = "";
            foreach (string word in repeat)
            {
                fullmessage += word + " ";
            }
            await Context.Channel.SendMessageAsync(fullmessage);
        }

        [Command("connect4")]
        [Summary("Play your favorite game, CONNECT 4! [`create`, `join`, `info`, `refresh`, `end`]")]
        public async Task OHMYGODITSMYFAVORITEGAMECONNECT4(string command)
        {
            Connect4Session _CurrentSession = null;
            foreach (Connect4Session session in Connect4SessionList)
                if (session.Channel == Context.Channel) _CurrentSession = session;

            if (command == "info")
            {
                if (Connect4SessionList.Count == 0)
                {
                    await Context.Channel.SendMessageAsync("There are no active Connect 4 games.");
                    return;
                }

                var e = Connect4SessionList.GetEnumerator();
                string listMessage = "";
                string currentLine = "There are currently no games on this channel." + Environment.NewLine;

                while (e.MoveNext())
                {
                    var session = e.Current;
                    if (session.Channel == null) continue;
                    if (session.Channel != Context.Channel) continue;

                    if (session.PlayerTurn > 0 && session.PlayerTwo != null)
                        currentLine = "<@" + session.PlayerOne.Id + "> " + "and <@" + session.PlayerTwo.Id + "> --> Player " + session.PlayerTurn + "'s turn.";
                    else if (session.PlayerTwo != null)
                        currentLine = "<@" + session.PlayerOne.Id + "> " + "and <@" + session.PlayerTwo.Id + "> ";
                    else
                        currentLine = "<@" + session.PlayerOne.Id + "> is waiting for player 2";

                    listMessage = listMessage + currentLine + Environment.NewLine;
                }

                listMessage = listMessage + "Total games started since last restart: " + Connect4SessionList.Count;
                await Context.Channel.SendMessageAsync(listMessage);
            }
            else if (command == "create")
            {
                bool ExistingGame = false;
                foreach (Connect4Session session in Connect4SessionList)
                    if (session.Channel == Context.Channel) ExistingGame = true;

                if (ExistingGame)
                {
                    await Context.Channel.SendMessageAsync("There is already a match in progress. Please start a match in another channel.");
                    return;
                }
                else
                {
                    var Player1 = Context.User.Id.ToString();
                    Connect4SessionList.Add(new Connect4Session
                    {
                        Channel = Context.Channel,
                        PlayerOne = Context.User
                    });
                    await StartConnect4Game(Context.Channel);
                }
            }
            else if (command == "join")
            {

                if (_CurrentSession != null)
                {
                    if (_CurrentSession.PlayerTwo == null)
                    {
                        _CurrentSession.PlayerTwo = Context.User;
                        await Context.Channel.SendMessageAsync($"{Context.User.Username} has successfully joined the match.");
                    }
                    else await temporaryMessage($"{Context.User.Username}, this match is already full!", Context.Channel, 5000);
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Username}, there is no game to join! Type '$connect4 create' if you would like to start a game.");
                }


            }
            else if (command == "refresh")
            {
                if (_CurrentSession != null) _CurrentSession.Flags = "refresh";
                else await Context.Channel.SendMessageAsync($"{Context.User.Username}, there's no match to refresh!");
            }
            else if (command == "end")
            {
                if (_CurrentSession == null) await temporaryMessage($"{Context.User.Username}, there's no match to end!", Context.Channel, 5000);
                else if (Context.User == _CurrentSession.PlayerOne || Context.User == _CurrentSession.PlayerTwo)
                {
                    _CurrentSession.Flags = "end";
                }
                else await temporaryMessage($"{Context.User.Username}, you are not in this match!", Context.Channel, 5000);
            }
            else
            {
                if (command != "info")
                {
                    await Context.Channel.SendMessageAsync("Invalid command. Please only create, join, info, refresh, or end.");
                }
            }
            await Context.Message.DeleteAsync();
        }

        [Command("profile")]
        [Summary("Provides a bunch of cool info about you (definitely not copying Bythos...)")]
        public async Task WowThatDoesLookLikeYou()
        {
            var user = Context.User;
            var embed = new EmbedBuilder();
            var footer = new EmbedFooterBuilder();

            embed.Color = new Color(Convert.ToUInt32("0061ff", 16));
            embed.ThumbnailUrl = user.GetAvatarUrl();
            embed.AddField("Requested user", user.Username);
            embed.AddField("ID", user.Id);
            embed.AddField("Status", user.Status.ToString());
            embed.AddField("Game", user.Game.ToString());
            embed.AddField("Date of account creation", user.CreatedAt.ToString());
            embed.Timestamp = new DateTime().ToLocalTime();
            embed.Footer = footer.WithText("© 2017 Trump's Amazing Profile Card");
            embed.Footer = footer.WithIconUrl("https://4.bp.blogspot.com/-7w3gf2hTARU/V0Js5ZZXCuI/AAAAAAAATYc/64Vtd4cdyecLQ2dIvehr8m2ZVILiUqWBwCLcB/s1600/Donald%2BTrump%252BFake%2BSmile%252BFalse%2BSmile%252BPseudoSmile%252BInsincere%252BDisgust%252BBody%2BLanguage%252BNonverbal%2BCommunication%2BExpert%252BSpeaker%252BKeynote%252BConsultant%252BLos%2BAngeles%252BLas%2BVegas%252BOrlando%252BNYC%252BChicago%252BFlorida%252BCalifornia.png");
            await Context.Channel.SendMessageAsync("", false, embed);
        }

        [Command("lenny")]
        [Summary("( ͡° ͜ʖ ͡°) M'lad")]
        public async Task WowICantBelieveItsNotLenny()
        {
            await Context.Message.DeleteAsync();
            await Context.Channel.SendMessageAsync("( ͡° ͜ʖ ͡°)");
        }

        [Command("trump")]
        [Summary("Trump does some neat stuff. [`image`,`respond` (`enable`,`disable`) (`guild`)]")]
        public async Task WowItsActuallyTrumpWow(string type, [Optional] string flag1, [Optional] string flag2)
        {
            if (type == "image")
            {
                var embed = new EmbedBuilder();

                Random rnd = new Random();
                embed.ImageUrl = pics.ElementAt(rnd.Next(0, pics.Count));
                await Context.Channel.SendMessageAsync("", false, embed);
            }
            else if (type == "respond")
            {
                var allowedChannels = Program.Array2List(System.IO.File.ReadAllLines("AllowTriggersList.txt"));
                if (flag1 == "disable")
                {
                    if (flag2 == "guild")
                    {
                        var alreadyDid = true;
                        foreach (var channel in Context.Guild.Channels)
                        {
                            if (channel.GetType().Name == "SocketVoiceChannel") continue;
                            if (allowedChannels.Contains(channel.Id.ToString()))
                            {
                                alreadyDid = false;
                                allowedChannels.Remove(channel.Id.ToString());
                            }
                        }
                        System.IO.File.WriteAllLines("AllowTriggersList.txt", allowedChannels);
                        if (alreadyDid)
                            await Context.Channel.SendMessageAsync("Trump has already been told to keep quiet in every channel.");
                        else
                            await Context.Channel.SendMessageAsync("Trump will try to shut his mouth in *every* channel now :(");
                    }
                    else
                    {
                        if (allowedChannels.Contains(Context.Channel.Id.ToString()))
                        {
                            allowedChannels.Remove(Context.Channel.Id.ToString());
                            await Context.Channel.SendMessageAsync("Trump will try to shut his mouth in this channel now :(");
                            System.IO.File.WriteAllLines("AllowTriggersList.txt", allowedChannels);
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("Trump has already agreed to keep quiet in this channel.");
                        }
                    }
                }
                else if (flag1 == "enable")
                {
                    if (flag2 == "guild")
                    {
                        var alreadyDid = true;
                        foreach (var channel in Context.Guild.Channels)
                        {
                            if (channel.GetType().Name == "SocketVoiceChannel") continue;
                            if (!allowedChannels.Contains(channel.Id.ToString()))
                            {
                                alreadyDid = false;
                                allowedChannels.Add(channel.Id.ToString());
                            }
                        }
                        System.IO.File.WriteAllLines("AllowTriggersList.txt", allowedChannels);
                        if (alreadyDid)
                            await Context.Channel.SendMessageAsync("Trump was already free to speak in every channel.");
                        else
                            await Context.Channel.SendMessageAsync("Trump will now feel free to speak in *every* channel :)");
                    }
                    else
                    {
                        if (!allowedChannels.Contains(Context.Channel.Id.ToString()))
                        {
                            allowedChannels.Add(Context.Channel.Id.ToString());
                            await Context.Channel.SendMessageAsync("Trump will now feel free to speak in this channel :)");
                            System.IO.File.WriteAllLines("AllowTriggersList.txt", allowedChannels);
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("Trump is already free to speak in this channel.");
                        }
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Please either type enable or disable at the end.");
                }
            }
        }

        [Command("test")]
        [Summary("A test command just for me :)")]
        public async Task TestingTesting123WOW()
        {
            if (Context.User.Id == 215507031375740928)
            {
                await Context.Channel.SendMessageAsync("this command actually doesn't do anything tbh");
            }
            else
            {
                await Context.Channel.SendMessageAsync("*You* aren't me!!!");
            }
        }

        [Command("random")]
        [Summary("Provides a random number between 1 and 100")]
        public async Task TestCommand()
        {
            Random rnd = new Random();
            int randomInt = rnd.Next(1, 100);
            await Context.Channel.SendMessageAsync(randomInt.ToString());
        }

        [Command("undelete")]
        [Summary("Recovers previously deleted messages from the current channel **(MODS ONLY)**  [`{UserID}` (`auto`), `all`, (`allGuild`)]")]
        public async Task UndoMyDeleteBoyo(string type, params string[] command)
        {
            if (_client.GetGuild(Context.Guild.Id).GetUser(Context.User.Id).GuildPermissions.ManageNicknames)
            {
                if (DeletedMessages.Count < 1 && !command.Contains("auto"))
                {
                    await Context.Channel.SendMessageAsync("There are no messages to recover.");
                    return;
                }

                if (type == "all")
                {
                    await Context.Channel.SendMessageAsync("Recovering recent messages...");
                    var messages = "";

                    if (command.Contains("allGuild"))
                    {
                        foreach (IMessage message in DeletedMessages)
                        {
                            var Msgchnl = message.Channel as SocketGuildChannel;
                            var MsgGuild = Msgchnl.Guild;

                            var Ctxtchnl = Context.Channel as SocketGuildChannel;
                            var CtxtGuild = Ctxtchnl.Guild;

                            if (MsgGuild != CtxtGuild) continue;
                            messages += ("`" + message.Author.Username + "`: " + message.Content + Environment.NewLine);
                        }
                        await Context.Channel.SendMessageAsync(messages);
                        if (command.Contains("auto"))
                        {
                            await Context.Channel.SendMessageAsync("Please only use the auto parameter on *specific* users.");
                        }
                    }
                    else
                    {
                        foreach (IMessage message in DeletedMessages)
                        {
                            if (message.Channel != Context.Channel) continue;
                            messages += ("`" + message.Author.Username + "`: " + message.Content + Environment.NewLine);
                        }
                        await Context.Channel.SendMessageAsync(messages);
                        if (command.Contains("auto"))
                        {
                            await Context.Channel.SendMessageAsync("Please only use the auto parameter on *specific* users.");
                        }
                    }
                }
                else if (type.ToString().Length == 18 || (type.StartsWith("<@!") && type.EndsWith(">")))
                {
                    if (type.StartsWith("<@!") && type.EndsWith(">")) type = type.Remove(0, 3).Remove(18, 1);

                    await Context.Channel.SendMessageAsync($"Recovering recent messages from <@{type}>...");
                    var messages = "";

                    if (command.Contains("allGuild"))
                    {
                        foreach (IMessage message in DeletedMessages)
                        {
                            var Msgchnl = message.Channel as SocketGuildChannel;
                            var MsgGuild = Msgchnl.Guild;

                            var Ctxtchnl = Context.Channel as SocketGuildChannel;
                            var CtxtGuild = Ctxtchnl.Guild;

                            if (MsgGuild != CtxtGuild) continue;
                            if (message.Author.Id != Convert.ToUInt64(type)) continue;
                            messages += ("`" + message.Author.Username + "`: " + message.Content + Environment.NewLine);
                        }
                        if (messages == "")
                        {
                            await Context.Channel.SendMessageAsync("This user ID is invalid / this user doesn't have any deleted messages.");
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync(messages);
                        }
                        if (command.Contains("auto"))
                        {

                            var UndeleteList = Program.Array2List(System.IO.File.ReadAllLines("UndeleteUserList.txt"));
                            if (UndeleteList.Contains(type))
                            {
                                UndeleteList.Remove(type);
                                await Context.Channel.SendMessageAsync("user has been removed from the auto-undelete list.");
                            }
                            else
                            {
                                UndeleteList.Add(type);
                                await Context.Channel.SendMessageAsync("user has been added to auto-undelete list.");
                            }
                            System.IO.File.WriteAllLines("UndeleteUserList.txt", UndeleteList);
                        }
                    }
                    else
                    {
                        foreach (IMessage message in DeletedMessages)
                        {
                            if (message.Channel != Context.Channel) continue;
                            if (message.Author.Id != Convert.ToUInt64(type)) continue;
                            messages += ("`" + message.Author.Username + "`: " + message.Content + Environment.NewLine);
                        }
                        if (messages == "")
                        {
                            await Context.Channel.SendMessageAsync("This user ID is invalid / this user doesn't have any deleted messages.");
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync(messages);
                        }
                        if (command.Contains("auto"))
                        {
                            var UndeleteList = Program.Array2List(System.IO.File.ReadAllLines("UndeleteUserList.txt"));
                            if (UndeleteList.Contains(type))
                            {
                                UndeleteList.Remove(type);
                                await Context.Channel.SendMessageAsync("user has been removed from the auto-undelete list.");
                            }
                            else
                            {
                                UndeleteList.Add(type);
                                await Context.Channel.SendMessageAsync("user has been added to auto-undelete list.");
                            }
                            System.IO.File.WriteAllLines("UndeleteUserList.txt", UndeleteList);
                        }
                    }
                }

                else
                {
                    await Context.Channel.SendMessageAsync("$help undelete");
                }

            }
            else
            {
                await Context.Channel.SendMessageAsync("You are not a moderator!");
            }
        }

        [Command("clear")]
        [Summary("Clears the defined messages **(MODS ONLY)** [`all`, `user` (`{UserID}`), `bot`, `undelete`] [`{# of messages 1-99}`]")]
        public async Task TestCommand(string type, int number, params string[] userID)
        {
            number++;
            if (_client.GetGuild(Context.Guild.Id).GetUser(Context.User.Id).GuildPermissions.ManageNicknames)
            {
                await Context.Message.DeleteAsync();
                if (type == "all")
                {
                    if (number > 99)
                    {
                        await Context.Channel.SendMessageAsync("Number is too large. Please enter a number between 1 and 99");
                        return;
                    }


                    var messages = await Context.Channel.GetMessagesAsync(number).Flatten();
                    await Context.Channel.DeleteMessagesAsync(messages);
                    await Context.Channel.SendMessageAsync($"{number} message(s) were deleted, as requested by {Context.User.Username}.");
                }

                if (type == "bot")
                {

                    if (number > 100)
                    {
                        await Context.Channel.SendMessageAsync("Number is too large. Please enter a number between 1 and 100");
                        return;
                    }
                    var messages = await Context.Channel.GetMessagesAsync(number).Flatten();
                    List<IMessage> botMessages = new List<IMessage>();
                    foreach (IMessage message in messages)

                    {
                        if (message.Author.IsBot)
                        {
                            botMessages.Add(message);
                        }
                    }

                    await Context.Channel.DeleteMessagesAsync(botMessages);
                    botMessages.Clear();
                }

                if (type == "user")
                {

                    if (number > 100)
                    {
                        await Context.Channel.SendMessageAsync("Number is too large. Please enter a number between 1 and 100");
                        return;
                    }
                    var messages = await Context.Channel.GetMessagesAsync(number).Flatten();
                    List<IMessage> userMessages = new List<IMessage>();
                    foreach (IMessage message in messages)

                    {
                        var FinalID = "";
                        foreach (string ID in userID)
                            if (ID.ToString().Length == 18 || (ID.StartsWith("<@!") && ID.EndsWith(">")))
                            {
                                if (ID.StartsWith("<@!") && ID.EndsWith(">"))
                                    FinalID = ID.Remove(0, 3).Remove(18, 1);
                                else FinalID = ID;
                                if (message.Author.Id == Convert.ToUInt64(FinalID))
                                {
                                    userMessages.Add(message);
                                }
                            }
                    }

                    await Context.Channel.DeleteMessagesAsync(userMessages);
                    userMessages.Clear();
                }

                if (type == "undelete")
                {
                    if (number > DeletedMessages.Count)
                    {
                        DeletedMessages.Clear();
                        await Context.Channel.SendMessageAsync("*All* messages have been removed from the recovery list");
                        return;
                    }

                    DeletedMessages.RemoveRange(0, number);
                    await Context.Channel.SendMessageAsync($"The oldest {number} message(s) have been removed from the recovery list");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("You are not a moderator!");
            }
        }

        [Command("mute")]
        [Summary("Restricts the specified user from speaking **(MODS ONLY)** [`{UserID}`]")]
        public async Task MutedMyDude(string id)
        {
            if (_client.GetGuild(Context.Guild.Id).GetUser(Context.User.Id).GuildPermissions.ManageNicknames)
            {
                if (id.ToString().Length == 18 || (id.StartsWith("<@!") && id.EndsWith(">")))
                {
                    if (id.StartsWith("<@!") && id.EndsWith(">")) id = id.Remove(0, 3).Remove(18, 1);

                    if (id == _client.CurrentUser.Id.ToString()) return;
                    var MuteList = Program.Array2List(System.IO.File.ReadAllLines("MutedUserList.txt"));
                    if (MuteList.Contains(id))
                        await Context.Channel.SendMessageAsync($"<@{id}> is already muted!");
                    else
                    {
                        MuteList.Add(id);
                        System.IO.File.WriteAllLines("MutedUserList.txt", MuteList);
                        await Context.Channel.SendMessageAsync($"<@{id}> has been muted!");
                    }
                }
                else await Context.Channel.SendMessageAsync("You are not a moderator!");
            }
        }

        [Command("unmute")]
        [Summary("Allows the muted user to speak again **(MODS ONLY)** [`{UserID}`]")]
        public async Task FreedMyDude(string id)
        {
            if (_client.GetGuild(Context.Guild.Id).GetUser(Context.User.Id).GuildPermissions.ManageNicknames)
            {
                if (id.ToString().Length == 18 || (id.StartsWith("<@") && id.EndsWith(">")))
                {
                    if (id.StartsWith("<@!") && id.EndsWith(">")) id = id.Remove(0, 3).Remove(18, 1);

                    if (id == "321097604131717120") return;
                    var MuteList = Program.Array2List(System.IO.File.ReadAllLines("MutedUserList.txt"));
                    if (MuteList.Contains(id))
                    {
                        MuteList.Remove(id);
                        System.IO.File.WriteAllLines("MutedUserList.txt", MuteList);
                        await Context.Channel.SendMessageAsync($"<@{id}> has been unmuted!");
                    }
                    else await Context.Channel.SendMessageAsync($"<@{id}> isn't muted!");
                }
                else await Context.Channel.SendMessageAsync("You are not a moderator!");
            }
        }

        [Command("b")]
        [Summary(":b:oi :b:e :b:lessed with the auto :b: converter **(MODS ONLY)**")]
        public async Task DIDSOMEBODYSAYB()
        {
            if (_client.GetGuild(Context.Guild.Id).GetUser(Context.User.Id).GuildPermissions.ManageNicknames)
            {
                var AutoBChannels = Program.Array2List(System.IO.File.ReadAllLines("AutoBList.txt"));
                if (AutoBChannels.Contains(Context.Channel.Id.ToString()))
                {
                    AutoBChannels.Remove(Context.Channel.Id.ToString());
                    System.IO.File.WriteAllLines("AutoBList.txt", AutoBChannels);
                    await Context.Channel.SendMessageAsync("The auto :b: converter has been disabled.");
                }
                else
                {
                    AutoBChannels.Add(Context.Channel.Id.ToString());
                    System.IO.File.WriteAllLines("AutoBList.txt", AutoBChannels);
                    await Context.Channel.SendMessageAsync("The auto :b: converter has been enabled.");
                }
            }
        }
    }
}

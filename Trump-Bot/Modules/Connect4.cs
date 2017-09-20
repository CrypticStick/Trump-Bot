using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Trump_Bot.Modules
{
    static class Connect4
    {
        public static List<Connect4Session> Connect4SessionList = new List<Connect4Session>();

        //Checks if there is a pre-existing session, and grabs it if there is.
        public static Connect4Session GrabConnect4Session(ISocketMessageChannel channel)
        {
            Connect4Session _CurrentSession = null;
            foreach (Connect4Session session in Connect4SessionList)
                if (session.Channel == channel) _CurrentSession = session;
            return _CurrentSession;
        }

        static string emptySlot = "⚪";
        static string playerPiece1 = "🔵";
        static string playerPiece2 = "🔴";

        public static List<Emoji> ReactionButtons = new List<Emoji>{
                    new Emoji("1⃣"),
                    new Emoji("2⃣"),
                    new Emoji("3⃣"),
                    new Emoji("4⃣"),
                    new Emoji("5⃣"),
                    new Emoji("6⃣"),
                    new Emoji("7⃣")
                };

        public class Connect4Session
        {
            public ISocketMessageChannel Channel { get; set; }
            public SocketUser PlayerOne { get; set; }
            public SocketUser PlayerTwo { get; set; }
            public Int32 PlayerTurn { get; set; }
            public List<List<Int32>> Gameboard { get; set; }
            public RestUserMessage LastSentMessage { get; set; }
            public Int32 RequestedColumn { get; set; }
            public string Flags { get; set; }
        }

        public static Task StartConnect4Game(ISocketMessageChannel _channel)
        {
            new Thread(async () =>
            {
                //Terminates the game cleanly
                async Task terminateGame(Connect4Session session, bool silent)
                {
                    if (!silent)
                    {
                        try { await session.LastSentMessage.DeleteAsync(); }
                        catch { }
                        await session.Channel.SendMessageAsync("The match has been terminated.");
                    }
                    Connect4SessionList.Remove(session);
                    return;
                }

                //Creates a brand new board (Connect4 needs a 7x6 board)
                List<List<Int32>> createNewBoard(int x, int y)
                {
                    List<List<Int32>> boardPositions = new List<List<Int32>>();
                    for (int i = 0; i < y; i++)
                    {
                        boardPositions.Add(new List<Int32>());
                        for (int e = 0; e < x; e++)
                        {
                            boardPositions[i].Add(0);
                        }
                    }
                    return boardPositions;
                }

                //Draws current session's gameboard
                string drawGameboard(Connect4Session session)
                {
                    string _gameboard = "≋ 𝟙      𝟚      𝟛     𝟜      𝟝      𝟞     𝟟 ≋" + Environment.NewLine;
                    for (int i = 0; i < 6; i++)
                    {
                        string _gameboardRow = "|";
                        for (int e = 0; e < 7; e++)
                        {
                            _gameboardRow = _gameboardRow + session.Gameboard[i][e] + "|";
                        }
                        _gameboard = _gameboard + _gameboardRow + Environment.NewLine;
                    }
                    _gameboard = _gameboard.Replace("0", emptySlot).Replace("1", playerPiece1).Replace("2", playerPiece2);
                    return _gameboard;
                }



                if (GrabConnect4Session(_channel) == null) return;
                if (GrabConnect4Session(_channel).PlayerTwo == null) await WaitForPlayerTwo(GrabConnect4Session(_channel));
                await playGame(GrabConnect4Session(_channel));

                //Game waits for player 2, and sets up game once ready
                async Task WaitForPlayerTwo(Connect4Session session)
                {
                    await session.Channel.SendMessageAsync("Connect4™ match will begin when another player joins...");

                    //Waits for player 2, closes game if desired
                    while (session.PlayerTwo == null)
                        if (session.Flags == "end")
                        {
                            await terminateGame(session, false);
                            return;
                        }
                        else await Task.Delay(1000);

                    session.PlayerTurn = 1;
                    session.Gameboard = createNewBoard(7, 6);
                    session.Flags = "incomplete";
                    session.RequestedColumn = 0;
                    Console.WriteLine("A game of Connect 4 has begun!");
                }

                //Attempts to update the gameboard, returns true if successful
                bool UpdateGameboard(Connect4Session session)
                {
                    var success = false;
                    for (int i = session.Gameboard.Count - 1; i >= 0; i--)
                    {
                        if (session.Gameboard[i][session.RequestedColumn - 1] == 0)
                        {
                            session.Gameboard[i][session.RequestedColumn - 1] = session.PlayerTurn;
                            success = true;
                            break;
                        }
                    }
                    return success;
                }

                string FourInARow(Connect4Session session)
                {
                    //CODE TO CHECK FOR 4 IN A ROW
                    //    ≋ 𝟙   𝟚   𝟛   𝟜   𝟝   𝟞   𝟟 ≋ (Spots in horizontal rows (2nd input))
                    // 1  | 2 | 2 | 0 | 0 | 0 | 0 | 0 |
                    // 2  | 0 | 2 | 0 | 0 | 0 | 0 | 0 |
                    // 3  | 0 | 2 | 2 | 0 | 0 | 1 | 0 |
                    // 4  | 0 | 2 | 0 | 2 | 1 | 0 | 0 |
                    // 5  | 0 | 0 | 0 | 1 | 0 | 0 | 0 |
                    // 6  | 0 | 0 | 1 | 1 | 1 | 1 | 1 |
                    // ^ (Horizontal rows (1st input))

                    // boardPositions[1st][2nd]
                    session.Flags = "tie";
                    for (int i = 0; i < 6; i++)
                    {
                        for (int e = 0; e < 7; e++)
                        {
                            if (session.Gameboard[i][e] == 0)
                            {
                                session.Flags = "incomplete";
                            }

                            if ((session.Gameboard[i][e] == 1 || session.Gameboard[i][e] == 2) && (i <= 2 && e <= 3))
                            {
                                if (session.Gameboard[i][e] == session.Gameboard[i + 1][e + 1] && session.Gameboard[i + 1][e + 1] == session.Gameboard[i + 2][e + 2] && session.Gameboard[i + 2][e + 2] == session.Gameboard[i + 3][e + 3])
                                {
                                    session.Flags = "complete";
                                    return session.Flags;
                                }
                            }

                            if ((session.Gameboard[i][e] == 1 || session.Gameboard[i][e] == 2) && (i >= 3 && e <= 3))
                            {
                                if (session.Gameboard[i][e] == session.Gameboard[i - 1][e + 1] && session.Gameboard[i - 1][e + 1] == session.Gameboard[i - 2][e + 2] && session.Gameboard[i - 2][e + 2] == session.Gameboard[i - 3][e + 3])
                                {
                                    session.Flags = "complete";
                                    return session.Flags;
                                }
                            }

                            if ((session.Gameboard[i][e] == 1 || session.Gameboard[i][e] == 2) && (i < 6 && e <= 3))
                            {
                                if (session.Gameboard[i][e] == session.Gameboard[i][e + 1] && session.Gameboard[i][e + 1] == session.Gameboard[i][e + 2] && session.Gameboard[i][e + 2] == session.Gameboard[i][e + 3])
                                {
                                    session.Flags = "complete";
                                    return session.Flags;
                                }
                            }

                            if ((session.Gameboard[i][e] == 1 || session.Gameboard[i][e] == 2) && (i <= 2 && e < 6))
                            {
                                if (session.Gameboard[i][e] == session.Gameboard[i + 1][e] && session.Gameboard[i + 1][e] == session.Gameboard[i + 2][e] && session.Gameboard[i + 2][e] == session.Gameboard[i + 3][e])
                                {
                                    session.Flags = "complete";
                                    return session.Flags;
                                }
                            }
                        }
                    }
                    return session.Flags;
                }

                async Task EndGame(Connect4Session session)
                {
                    var FinalBoard = drawGameboard(session) + Environment.NewLine;
                    await session.LastSentMessage.RemoveAllReactionsAsync();

                    if (session.Flags == "tie")
                    {
                        try { await session.LastSentMessage.ModifyAsync((m => m.Content = FinalBoard + "It was a tie! BOOOOO!!!")); }
                        catch { session.LastSentMessage = await session.Channel.SendMessageAsync(FinalBoard + "It was a tie! BOOOOO!!!"); }
                        await terminateGame(session, true);
                        return;
                    }
                    else if (session.Flags == "complete")
                    {
                        if (session.PlayerTurn == 1)
                            try { await session.LastSentMessage.ModifyAsync((m => m.Content = FinalBoard + $"{playerPiece1} <@!{session.PlayerOne.Id}> WINS!")); }
                            catch { session.LastSentMessage = await session.Channel.SendMessageAsync(FinalBoard + $"{playerPiece1} <@!{session.PlayerOne.Id}> WINS!"); }
                        else
                            try { await session.LastSentMessage.ModifyAsync((m => m.Content = FinalBoard + $"{playerPiece2} <@!{session.PlayerTwo.Id}> WINS!")); }
                            catch { session.LastSentMessage = await session.Channel.SendMessageAsync(FinalBoard + $"{playerPiece2} <@!{session.PlayerTwo.Id}> WINS!"); }
                        await terminateGame(session, true);
                        return;
                    }
                    else
                    {
                        await terminateGame(session, false);
                        return;
                    }
                }

                //The game itself!!
                async Task playGame(Connect4Session session)
                {
                    while (session.Flags != "complete" || session.Flags != "tie")
                    {
                        var MessageToSend = "";
                        if (session.Flags == "full")
                            MessageToSend += $"Column {session.RequestedColumn + 1} is full! Please choose another." + Environment.NewLine + Environment.NewLine;

                        MessageToSend += drawGameboard(session) + Environment.NewLine;

                        if (session.PlayerTurn == 1)
                            MessageToSend += $"{playerPiece1} <@!{session.PlayerOne.Id}>, it's your turn. Type $connect4 (Number between 1-7)";
                        else
                            MessageToSend += $"{playerPiece2} <@!{session.PlayerTwo.Id}>, it's your turn. Type $connect4 (Number between 1-7)";

                        try { await session.LastSentMessage.ModifyAsync((m => m.Content = MessageToSend)); }
                        catch
                        {
                            session.LastSentMessage = await session.Channel.SendMessageAsync(MessageToSend);
                            foreach (Emoji reaction in ReactionButtons)
                                await session.LastSentMessage.AddReactionAsync(reaction);
                        }

                        while (session.RequestedColumn == 0)
                        {
                            if (session.Flags == "end")
                            {
                                await terminateGame(session, false);
                                return;
                            }
                            else if (session.Flags == "refresh") break;
                            else await Task.Delay(1000);
                        }

                        if (session.Flags == "refresh")
                        {
                            try { await session.LastSentMessage.DeleteAsync(); }
                            catch { }
                            session.Flags = "incomplete";
                            continue;
                        }

                        var success = UpdateGameboard(session);
                        session.RequestedColumn = 0;
                        if (!success) { session.Flags = "full"; continue; }


                        var result = FourInARow(session);
                        if (result == "incomplete")
                        {
                            if (session.PlayerTurn == 1) session.PlayerTurn = 2;
                            else session.PlayerTurn = 1;
                            continue;
                        }
                        else
                        {
                            await EndGame(session);
                            return;
                        }
                    }
                }
            }).Start();
            return Task.CompletedTask;
        }
    }
}

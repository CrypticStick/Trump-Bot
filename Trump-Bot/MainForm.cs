using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trump_Bot
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private static DiscordSocketClient _client;
        private string guildName;
        private string guildID;
        private string channelName;
        private string channelID;
        private string userName;
        private string userID;
        private string roleName;
        private string roleID;
        private string userRoleName;
        private string userRoleID;
        private bool usernameSwap = false;
        private SocketChannel selectedChannel;
        private SocketGuildUser selectedUser;
        private SocketGuild selectedGuild;
        private SocketRole selectedGuildRole;
        private SocketRole selectedUserRole;
        private ITextChannel messageChannel;
        List<List<string>> guildNames = new List<List<string>>();
        List<List<string>> channelNames = new List<List<string>>();
        List<List<string>> userNames = new List<List<string>>();
        List<List<string>> roleNames = new List<List<string>>();
        List<List<string>> userRoleNames = new List<List<string>>();

        public Task InitializeForm(DiscordSocketClient client)
        {
            _client = client;

            guildNames.Add(new List<string>());
            guildNames.Add(new List<string>());

            channelNames.Add(new List<string>());
            channelNames.Add(new List<string>());

            userNames.Add(new List<string>());
            userNames.Add(new List<string>());

            roleNames.Add(new List<string>());
            roleNames.Add(new List<string>());

            userRoleNames.Add(new List<string>());
            userRoleNames.Add(new List<string>());

            //while (_client.ConnectionState != Discord.ConnectionState.Connected) { }
            updateStatus();
            Rainbow();

            LogConsole(new LogMessage(LogSeverity.Info, "Program",
        "Form is ready!"));
            return Task.CompletedTask;
        }

        private void updateStatus()
        {
            new Thread(async () =>
            {
                while (true)
                {
                    LblStatus.Text = _client.ConnectionState.ToString();
                    try { LblBotName.Text = _client.CurrentUser.Username; }
                    catch { LblBotName.Text = "N/A"; }
                    await Task.Delay(1000);
                }
            }).Start();
        }

        public Task LogConsole(LogMessage msg)
        {
            if (Console.InvokeRequired)
                Console.Invoke((MethodInvoker)delegate ()
                {
                    LogConsole(msg);
                });
            else
            Console.Text = Console.Text + Environment.NewLine + msg.ToString();
            return Task.CompletedTask;
        }

        private void Refresh_Guilds(object sender, EventArgs e)
        {
            LstGuilds.Items.Clear();
            guildNames[0].Clear();
            guildNames[1].Clear();

            foreach (SocketGuild guild in _client.Guilds)
            {
                guildName = guild.Name.ToString();
                if (guildNames[0].Contains(guildName))
                {
                    var _guildNames = new List<string>();
                    _guildNames = guildNames[0];
                    var _group = _guildNames.GroupBy(i => i);
                    foreach (var word in _group)
                    {
                        if (word.Key == guildName)
                        {
                            guildName = guildName + $"({word.Count().ToString()})";
                        }
                    }
                }

                guildID = guild.Id.ToString();

                guildNames[0].Add(guildName);
                guildNames[1].Add(guildID);
                LstGuilds.Items.Add(guildName);
            }
        }

        private void statusInfo(string message)
        {
            new Thread(async () =>
            {
                LblCommandInfo.Text = message;
                await Task.Delay(4000);
                LblCommandInfo.Text = "Waiting for action...";
            }).Start();
        }

        private void Refresh_Channels()
        {
            LstChannels.Items.Clear();
            LstChannels.Text = null;
            channelNames[0].Clear();
            channelNames[1].Clear();

            foreach (SocketGuildChannel channel in _client.GetGuild(selectedGuild.Id).Channels)
            {

                if (channel.GetType().Name == "SocketVoiceChannel") continue;
                try
                {
                    channelName = channel.Name.ToString();
                }
                catch
                {
                    continue;
                }
                if (channelNames[0].Contains(channelName))
                {
                    var _channelNames = new List<string>();
                    _channelNames = channelNames[0];
                    var _group = _channelNames.GroupBy(i => i);
                    foreach (var word in _group)
                    {
                        if (word.Key == channelName)
                        {
                            channelName = channelName + $"({word.Count().ToString()})";
                        }
                    }
                }
                channelID = channel.Id.ToString();
                channelNames[0].Add(channelName);
                channelNames[1].Add(channelID);
                LstChannels.Items.Add(channelName);
            }
        }

        private void Refresh_Users()
        {
            var oldUserID = "empty";
            if (usernameSwap && LstUsers.Text != "") { oldUserID = selectedUser.Id.ToString(); }
            LstUsers.Items.Clear();
            LstUsers.Text = null;
            userNames[0].Clear();
            userNames[1].Clear();

            foreach (SocketGuildUser _user in _client.GetGuild(selectedGuild.Id).Users)
            {
                if (ChkUsername.Checked)
                {
                    if (_user.Nickname == null)
                    {
                        userName = _user.Username + " #" + _user.Discriminator;
                    }
                    else { userName = _user.Nickname + " #" + _user.Discriminator; }
                }
                else { userName = _user.Username + " #" + _user.Discriminator; }
                userID = _user.Id.ToString();

                userNames[0].Add(userName);
                userNames[1].Add(userID);
                LstUsers.Items.Add(userName);
            }

            if (usernameSwap && oldUserID != "empty")
            {
                var _index = userNames[1].IndexOf(oldUserID);
                var _name = userNames[0].ElementAt(_index);
                LstUsers.Text = _name;
            }
        }

        private void Refresh_Roles()
        {
            LstGuildRoles.Items.Clear();
            LstGuildRoles.Text = null;
            roleNames[0].Clear();
            roleNames[1].Clear();

            foreach (SocketRole _role in _client.GetGuild(selectedGuild.Id).Roles)
            {
                if (_role.IsEveryone) continue;
                roleID = _role.Id.ToString();
                if (userRoleNames[1].Contains(roleID)) continue;
                roleName = _role.Name;
                if (roleNames[0].Contains(roleName))
                {
                    var _roleNames = new List<string>();
                    _roleNames = roleNames[0];
                    var _group = _roleNames.GroupBy(i => i);
                    foreach (var word in _group)
                    {
                        if (word.Key == roleName)
                        {
                            roleName = roleName + $"({word.Count().ToString()})";
                        }
                    }
                }

                roleNames[0].Add(roleName);
                roleNames[1].Add(roleID);
                LstGuildRoles.Items.Add(roleName);
            }
        }

        private void Refresh_UserRoles()
        {
            LstUserRoles.Items.Clear();
            LstUserRoles.Text = null;
            userRoleNames[0].Clear();
            userRoleNames[1].Clear();

            foreach (SocketRole _role in _client.GetGuild(selectedGuild.Id).GetUser(selectedUser.Id).Roles)
            {
                if (_role.IsEveryone) continue;
                userRoleName = _role.Name;
                if (userRoleNames[0].Contains(userRoleName))
                {
                    var _userRoleNames = new List<string>();
                    _userRoleNames = userRoleNames[0];
                    var _group = _userRoleNames.GroupBy(i => i);
                    foreach (var word in _group)
                    {
                        if (word.Key == userRoleName)
                        {
                            userRoleName = userRoleName + $"({word.Count().ToString()})";
                        }
                    }
                }

                userRoleID = _role.Id.ToString();
                userRoleNames[0].Add(userRoleName);
                userRoleNames[1].Add(userRoleID);
                LstUserRoles.Items.Add(userRoleName);
            }
        }

        private void LstGuilds_Click(object sender, EventArgs e)
        {
            BtnSendMessage.Enabled = false;

            var _index = guildNames[0].IndexOf(LstGuilds.SelectedItem.ToString());
            var _guildID = Convert.ToUInt64(guildNames[1].ElementAt(_index));
            selectedGuild = _client.GetGuild(_guildID);
            Refresh_Channels();
            Refresh_Users();
            Refresh_Roles();
            selectedChannel = null;
            selectedUser = null;
            selectedUserRole = null;
            selectedGuildRole = null;
            LstUserRoles.Items.Clear();
            LstUserRoles.Text = null;
        }

        private void LstChannels_Click(object sender, EventArgs e)
        {
            BtnSendMessage.Enabled = true;

            var _index = channelNames[0].IndexOf(LstChannels.SelectedItem.ToString());
            var _channelID = Convert.ToUInt64(channelNames[1].ElementAt(_index));
            selectedChannel = _client.GetChannel(_channelID);
        }

        private void LstUsers_Click(object sender, EventArgs e)
        {
            if (usernameSwap) return;
            var _index = userNames[0].IndexOf(LstUsers.SelectedItem.ToString());
            var _userID = Convert.ToUInt64(userNames[1].ElementAt(_index));
            selectedUser = _client.GetGuild(selectedGuild.Id).GetUser(_userID);
            Refresh_UserRoles();
            Refresh_Roles();
        }

        private void GuildRoles_Click(object sender, EventArgs e)
        {
            var _index = roleNames[0].IndexOf(LstGuildRoles.SelectedItem.ToString());
            var _roleID = Convert.ToUInt64(roleNames[1].ElementAt(_index));
            selectedGuildRole = _client.GetGuild(selectedGuild.Id).GetRole(_roleID);
        }

        private void UserRoles_Click(object sender, EventArgs e)
        {
            if (LstUserRoles.SelectedItem == null) return;
            var _index = userRoleNames[0].IndexOf(LstUserRoles.SelectedItem.ToString());
            var _roleID = Convert.ToUInt64(userRoleNames[1].ElementAt(_index));
            selectedUserRole = _client.GetGuild(selectedGuild.Id).GetRole(_roleID);
        }

        private void BtnSendMessage_Click(object sender, EventArgs e)
        {
            messageChannel = (ITextChannel)selectedChannel;
            messageChannel.SendMessageAsync(TxtChannels.Text);
        }

        public async Task Rainbow()
        {
            new Thread(async () =>
            {
                do
                {
                    try
                    {
                        await Task.Delay(1200);
                        await _client.SetStatusAsync(Discord.UserStatus.DoNotDisturb);
                        await Task.Delay(1200);
                        await _client.SetStatusAsync(Discord.UserStatus.Idle);
                        await Task.Delay(1200);
                        await _client.SetStatusAsync(Discord.UserStatus.Online);
                    }
                    catch { }
                } while (true);
            }).Start();
        }

        private void BtnRainbow_Click(object sender, EventArgs e)
        {
            Rainbow();
        }

        private async void BtnReset_Click(object sender, EventArgs e)
        {
            await _client.StopAsync();
            await Task.Delay(2000);
            await _client.StartAsync();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            _client.StopAsync();
            _client.LogoutAsync();
            Close();
        }

        private void BtnRemoveRole_Click(object sender, EventArgs e)
        {
            if (selectedUserRole != null)
            {
                if (selectedUser.Hierarchy < _client.GetGuild(selectedGuild.Id).CurrentUser.Hierarchy)
                {
                    selectedUser.RemoveRoleAsync(selectedUserRole);
                    while (selectedUser.Roles.Contains(selectedUserRole)) { }
                    Refresh_UserRoles();
                    Refresh_Roles();
                    selectedUserRole = null;
                }
                else
                {
                    statusInfo("Not enough permissions.");
                    return;
                }
            }
            else
            {
                statusInfo("No user role selected.");
                return;
            }
        }

        private void BtnAddRole_Click(object sender, EventArgs e)
        {
            if (selectedGuildRole != null)
            {
                if (selectedUser == null)
                {
                    statusInfo("Not enough permissions.");
                    return;
                }
                if (selectedUser.Hierarchy < _client.GetGuild(selectedGuild.Id).CurrentUser.Hierarchy && selectedGuildRole.Position < _client.GetGuild(selectedGuild.Id).CurrentUser.Hierarchy)
                {
                    selectedUser.AddRoleAsync(selectedGuildRole);
                    while (!selectedUser.Roles.Contains(selectedGuildRole)) { }
                    Refresh_UserRoles();
                    Refresh_Roles();
                    selectedGuildRole = null;
                }
                else
                {
                    statusInfo("Not enough permissions.");
                    return;
                }
            }
            else
            {
                statusInfo("No guild role selected.");
                return;
            }
        }

        private void ChkUsername_CheckedChanged(object sender, EventArgs e)
        {
            usernameSwap = true;
            Refresh_Users();
            usernameSwap = false;
        }
    }
}

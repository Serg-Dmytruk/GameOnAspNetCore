using Blazored.SessionStorage;
using Game.Common.ModelsDto;
using GameClient.Common.Options;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameClient.Common.Pages
{
    [Route("chatroom")]
    public partial class ChatRoom
    {
        [Inject] private ISessionStorageService _sessionStorageService { get; set; }
        [Inject] private IOptions<ChatOptions> _options { get; set; }

        private bool _isChatting = false;

        // name of the user who will be chatting
        private string _username;

        // on-screen message
        private string _message;

        // new message input
        private string _newMessage;

        // list of messages in chat
        private List<Message> _messages = new List<Message>();

        private HubConnection _hubConnection;

        public async Task Chat()
        {
            _username = (await _sessionStorageService.GetItemAsync<LoginModelDto>("User")).Login;

            try
            {
                // Start chatting and force refresh UI.
                _isChatting = true;
                await Task.Delay(1);

                // remove old messages if any
                _messages.Clear();

                // Create the chat client

                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(_options.Value.BaseUrl)
                    .Build();

                _hubConnection.On<string, string>("Broadcast", BroadcastMessage);

                await _hubConnection.StartAsync();

                await SendAsync($"[Notification] {_username} join to chat.");
            }
            catch (Exception e)
            {
                _message = $"ERROR: Failed to start chat client: {e.Message}";
                _isChatting = false;
            }
        }

        private void BroadcastMessage(string name, string message)
        {
            bool isMine = name.Equals(_username, StringComparison.OrdinalIgnoreCase);

            _messages.Add(new Message(name, message, isMine));

            // Inform blazor the UI needs updating
            StateHasChanged();
        }

        private async Task DisconnectAsync()
        {
            if (_isChatting)
            {
                await SendAsync($"[Notification] {_username} leave chat.");

                await _hubConnection.StopAsync();
                await _hubConnection.DisposeAsync();

                _hubConnection = null;
                _isChatting = false;
            }
        }

        private async Task SendAsync(string message)
        {
            if (_isChatting && !string.IsNullOrWhiteSpace(message))
            {
                await _hubConnection.SendAsync("Broadcast", _username, message);

                _newMessage = string.Empty;
            }
        }

        private class Message
        {
            public Message(string username, string body, bool mine)
            {
                Username = username;
                Body = body;
                Mine = mine;
            }

            public string Username { get; set; }
            public string Body { get; set; }
            public bool Mine { get; set; }

            public bool IsNotice => Body.StartsWith("[Notice]");

            public string CSS => Mine ? "sent" : "received";
        }
    }
}

using Playground;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopChatFlat
{
    public class ConversationHistoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly ChatFlatApi api;
        readonly List<string> userNames = new List<string>
        {
            "Prison Mike",
            "Dwight",
            "Jim",
            "Pam",
            "Andy"
        };

        public ConversationHistoryViewModel()
        {
            api = new ChatFlatApi();
            Task.Run(async () => await LoadDataAsync());
            OnPropertyChanged("UserNames");
            SelectedUserName = userNames[0];
        }

        public IEnumerable<string> UserNames => userNames;

        string selectedUserName;
        public string SelectedUserName
        {
            get { return selectedUserName; }
            set
            {
                selectedUserName = value;
                OnPropertyChanged("SelectedUserName");
            }
        }


        public IEnumerable<ConversationViewModel> Conversations
        {
            get; set;
        }

        ConversationViewModel selectedConversation;
        public ConversationViewModel SelectedConversation
        {
            get
            {
                return selectedConversation;
            }
            set
            {
                selectedConversation = value;
                OnPropertyChanged("SelectedConversation");
                OnPropertyChanged("ConversationText");
            }
        }

        public void SelectConversation(string conversationId)
        {
            Task.Run(async () => await SelectConversationAsync(conversationId));
        }

        public async Task SelectConversationAsync(string conversationId)
        {
            var conversation = await api.GetConversation(conversationId);
            SelectedConversation = new ConversationViewModel(conversation);
        }

        public string ConversationText
        {
            get
            {
                if (SelectedConversation == null) return string.Empty;
                return SelectedConversation.Text;
            }
        }

        public bool IsLoading { get; private set; }


        public async Task LoadDataAsync()
        {
            IsLoading = true;
            try
            {

                var conversations = await api.GetConversations();
                Conversations = new List<ConversationViewModel>(conversations.Select(c => new ConversationViewModel(c)));
                OnPropertyChanged("Conversations");

            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task CreateMessage(string messageText)
        {
            var conversationId = SelectedConversation.Id;
            var message = new Message { Name = SelectedUserName, Text = messageText };
            await api.CreateMessage(conversationId, message);
            await SelectConversationAsync(conversationId);
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ConversationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly Conversation conversation;

        public ConversationViewModel(Conversation conversation)
        {
            this.conversation = conversation;
            OnPropertyChanged("Text");
        }

        public string Id => conversation.Id;
        public DateTime? CreatedAt => conversation.CreatedAt;

        public string Text
        {
            get
            {
                var result = new StringBuilder();
                foreach (var each in conversation.Messages)
                {
                    result.AppendLine($"{each.Name} ({each.CreatedAt}):");
                    result.AppendLine($"{each.Text} ");
                    result.AppendLine();
                }
                return result.ToString();

            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

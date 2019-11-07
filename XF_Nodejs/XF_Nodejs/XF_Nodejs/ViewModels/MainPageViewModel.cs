using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XF_Nodejs.Models;

namespace XF_Nodejs.ViewModels
{
    public class MainPageViewModel : ModelPropertyChanged
    {
        public Quobject.SocketIoClientDotNet.Client.Socket ConnectSocket;
        private ObservableCollection<Messenger_Model> _lstMessenger;
        public ObservableCollection<Messenger_Model> LstMessenger { get => _lstMessenger; set => SetProperty(ref _lstMessenger, value); }
        private string _messenger;
        public string MessengerSend { get => _messenger; set => SetProperty(ref _messenger, value); }
        private string _nameSend;
        public string NameSend
        {
            get => _nameSend; 
            set
            {
                SetProperty(ref _nameSend, value);
                if(value!=_nameSend)
                    ConnectSocket.Emit("ChatMessenger", "GetAll");
            }
        }
        private Messenger_Model Messenger_Model;

        public Command SendMessenger => new Command(() =>
          {
              var obj = new Messenger_Model { Name = NameSend, Messenger = MessengerSend, createDate = DateTime.Now };
              var data = JsonConvert.SerializeObject(obj);
              ConnectSocket.Emit("ChatMessenger", data);
              MessengerSend = "";
          });
           public Command LockName => new Command(() =>
          {
              ConnectSocket.Emit("ChatMessenger", "GetAll");
          });

        public MainPageViewModel()
        {
            LstMessenger = new ObservableCollection<Messenger_Model>();
            ConnectSocket = Quobject.SocketIoClientDotNet.Client.IO.Socket("http://192.168.100.6:5000/");
            ConnectSocket.On(Quobject.SocketIoClientDotNet.Client.Socket.EVENT_CONNECT, () =>
            {
                //ConnectSocket.Emit("ChatMessenger", "ConnectTion......");
            });

            ConnectSocket.On("ChatMessenger", (data) =>
            {
                try
                {
                    var aaa = JsonConvert.DeserializeObject<List<Messenger_Model>>(data.ToString().Replace(@"\", "")).ToArray();
                    foreach (var item in aaa)
                    {
                        if (item.Name == this.NameSend)
                        {
                            item.isInputChat = true;
                        }
                        LstMessenger.Add(item);
                        MainPage.GoScrollTo.Invoke(item, new EventArgs());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(data);
                }
            });
        }
    }
}

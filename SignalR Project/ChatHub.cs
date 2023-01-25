using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;
using System.Linq;

namespace SignalR_Project
{
    public class ChatHub : Hub
    {
        bool isWork;

        static MongoClient client = new MongoClient();
        static IMongoDatabase database = client.GetDatabase("AnecdoteDB");
        static IMongoCollection<Anecdote> collection = database.GetCollection<Anecdote>("Anecdotes");
        static IMongoCollection<Message> collection1 = database.GetCollection<Message>("Messages");

        public async Task Send(string message, string userName, string Email)
        {
            if (message == "/start")
            {
                isWork = true;
                await Clients.All.SendAsync("Receive", "Привет! Я готов к работе!", "Bot", "(Johan)");
            }

            if (isWork = true)
            {
                if (message == "/end")
                {
                    isWork = false;
                    await Clients.All.SendAsync("Receive", "До встречи!", "Bot", "(Johan)");
                }

                else if (message == "/time")
                {
                    await Clients.All.SendAsync("Receive", $"{DateTime.Now}", "Bot", "(Johan)");
                }

                else if (message == "/Anecdote")
                {
                    List<Anecdote> AnecdotesList = collection.AsQueryable().ToList<Anecdote>();
                    Random rnd = new Random();
                    int num = rnd.Next(AnecdotesList.Count);
                    await Clients.All.SendAsync("Receive", $"{AnecdotesList[num].Anecdote_Text}", "Bot", "(Johan)");
                }

                else if (message == "/download")
                {
                    List<Message> list = collection1.Find(x => true).ToList();
                    foreach (var i in list)
                    {
                        await Clients.All.SendAsync("Receive", i.message, i.userName, i.Email);
                    }
                    //for (int i = 0; i < list.Count; i++)
                    //{
                    //    await Clients.All.SendAsync("Receive", list[i].message, list[i].userName, list[i].Email);
                    //}
                }

                else if (message != "/start")
                {
                    await Clients.All.SendAsync("Receive", message, userName, Email);
                }

                
            }

            else if (isWork = false)
            {
                await Clients.All.SendAsync("Receive", message, userName, Email);
            }
            Message mes = new Message();
            mes.message = message;
            mes.userName = userName;
            mes.Email = Email;
            collection1.InsertOne(mes);
        }
    }
}

using System;
using System.Threading.Tasks;

namespace Lab2_2
{
    public class Program
    {
        static async Task Main()
        {
            Console.WriteLine("UserName?");
            string username = Console.ReadLine();
            //string username = "qwe";
            var messenger = new Messenger(@"Data Source=KOTLAWAN\SQLEXPRESS;Initial Catalog=ChatBD;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
            
            Console.Clear();
            _ = Task.Run(async () =>
              {
                  while (true)
                  {
                      if (Console.CursorLeft == 0) await ShowMessages(messenger, username);
                      await Task.Delay(1000);
                  }
              });
            while (true)
            {
                string text = Console.ReadLine();
                if (text != "")
                {
                    await messenger.SendMessage(new Message
                    {
                        Name = username,
                        Text = text,
                    });
                }
                int currLine = Console.CursorTop - 1;
                Console.SetCursorPosition(0, currLine);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currLine);
                Console.CursorLeft = 0;
                await ShowMessages(messenger, username);
            }
        }

        static async Task ShowMessages(Messenger messenger, string username)
        {
            var messages = await messenger.GetMessages();
            foreach (var message in messages)
            {
                if (message.Name != username) Console.WriteLine(message);
                else Console.WriteLine(message.Text);
            }
        }
    }
}
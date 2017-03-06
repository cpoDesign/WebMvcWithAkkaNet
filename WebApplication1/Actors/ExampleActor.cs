using System;
using System.Threading.Tasks;
using Akka.Actor;

namespace WebApplication1.Actors
{
    public class ExampleActor : ReceiveActor
    {
        public ExampleActor()
        {
            ReceiveAsync<string>(async str =>
            {
                try
                {
                    var result = await SayHi(str);
                    Sender.Tell(result, Self);
                }
                catch (Exception e)
                {
                    Sender.Tell(new Failure { Exception = e }, Self);
                }
            });
        }

        static async Task<string> SayHi(string messageString)
        {
            return $"{DateTime.Now.ToString("O")} - hi -  {messageString}";
        }

        protected override void PreStart()
        {
        }
    }
}
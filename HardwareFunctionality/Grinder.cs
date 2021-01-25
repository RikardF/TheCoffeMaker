using System;
using System.Threading.Tasks;
using IO;

namespace HardwareFunctionality
{
    public class Grinder : IFunctionality
    {
        public event EventHandler<EventArgs> OnComplete;
        public CoffebeanContainer cc = new CoffebeanContainer();
        public int TimeDelay { get; set; }
        public async Task Run(float factor)
        {
            this.TimeDelay = Convert.ToInt32(4000 * factor);
            await cc.ContaierHandler();
            cc.UsesSinceRefill++;
            await Task.Delay(TimeDelay);
            if (OnComplete != null)
            {
                OnComplete(this, EventArgs.Empty);  
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using IO;

namespace HardwareFunctionality
{
    public class SugarDispenser : IFunctionality
    {
        public event EventHandler<EventArgs> OnComplete;
        public SugarContainer sc = new SugarContainer();
        public int TimeDelay { get; set; }
        public async Task Run(float factor)
        {
            this.TimeDelay = Convert.ToInt32(1000 * factor);
            await sc.ContaierHandler();
            sc.UsesSinceRefill++;
            await Task.Delay(TimeDelay);
            if (OnComplete != null)
            {
                OnComplete(this, EventArgs.Empty);
            }
        }
    }
}
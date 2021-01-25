using System;
using System.Threading.Tasks;
using IO;

namespace HardwareFunctionality
{
    public class WaterHeater : IFunctionality
    {
        public event EventHandler<EventArgs> OnComplete;
        public WaterContainer wc = new WaterContainer();
        public int TimeDelay { get; set; }
        public async Task Run(float factor)
        {
            this.TimeDelay = Convert.ToInt32(7000 * factor);
            await wc.ContaierHandler();
            wc.UsesSinceRefill++;
            await Task.Delay(TimeDelay);
            if (OnComplete != null)
            {
                OnComplete(this, EventArgs.Empty);
            }
        }
    }
}
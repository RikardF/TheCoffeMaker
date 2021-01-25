using System;
using System.Threading.Tasks;
using IO;

namespace HardwareFunctionality
{
    public class MilkFoamer : IFunctionality
    {
        public event EventHandler<EventArgs> OnComplete;
        public MilkContainer mc = new MilkContainer();
        public int TimeDelay { get; set; }
        public async Task Run(float factor)
        {
            this.TimeDelay = Convert.ToInt32(5000 * factor);
            await mc.ContaierHandler();
            mc.UsesSinceRefill++;
            await Task.Delay(TimeDelay);
            if (OnComplete != null)
            {
                OnComplete(this, EventArgs.Empty);  
            }
        }
    }
}
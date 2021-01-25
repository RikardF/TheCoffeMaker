using System;
using System.Threading.Tasks;

namespace IO
{
    public class WaterContainer : IContainer
    {
        public int UsesSinceRefill { get; set; }
        public int MaxUsesPerRefill { get; set; }
        public event EventHandler<ContainerArgs> OnContainerEmpty;
        public event EventHandler<EventArgs> OnRefillWater;
        private ContainerArgs ca = new ContainerArgs();
        public WaterContainer()
        {
            this.UsesSinceRefill = 0;
            this.MaxUsesPerRefill = 3;
        }
        public async Task ContaierHandler()
        {
            if (UsesSinceRefill == MaxUsesPerRefill)
            {
                ca.waterIsEmpty = true;
                if (OnContainerEmpty != null)
                {
                    OnContainerEmpty(this, ca);
                }
                await Refill();
                if (OnRefillWater != null)
                {
                    OnRefillWater(this, EventArgs.Empty);
                }
            }
        }
        public async Task Refill()
        {
            await Task.Delay(2000);
            ca.waterIsEmpty = false;
            this.UsesSinceRefill = 0;
        }
    }
}
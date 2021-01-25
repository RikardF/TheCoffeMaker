using System;
using System.Threading.Tasks;

namespace IO
{
    public class MilkContainer : IContainer
    {
        public int UsesSinceRefill { get; set; }
        public int MaxUsesPerRefill { get; set; }
        public event EventHandler<ContainerArgs> OnContainerEmpty;
        private ContainerArgs ca = new ContainerArgs();
        public MilkContainer()
        {
            this.UsesSinceRefill = 0;
            this.MaxUsesPerRefill = 4;
        }
        public async Task ContaierHandler()
        {
            if (UsesSinceRefill == MaxUsesPerRefill)
            {
                ca.milkIsEmpty = true;
                if (OnContainerEmpty != null)
                {
                    OnContainerEmpty(this, ca);
                }
            }
        }
        public async Task Refill()
        {
            ca.milkIsEmpty = false;
            this.UsesSinceRefill = 0;
        }
    }
}
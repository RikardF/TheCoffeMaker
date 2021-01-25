using System;
using System.Threading.Tasks;

namespace IO
{
    public class CoffebeanContainer : IContainer
    {
        public int UsesSinceRefill { get; set; }
        public int MaxUsesPerRefill { get; set; }
        public event EventHandler<ContainerArgs> OnContainerEmpty;
        private ContainerArgs ca = new ContainerArgs();
        public CoffebeanContainer()
        {
            this.UsesSinceRefill = 0;
            this.MaxUsesPerRefill = 5;
        }
        public async Task ContaierHandler()
        {
            if (UsesSinceRefill == MaxUsesPerRefill)
            {
                ca.coffeIsEmpty = true;
                if (OnContainerEmpty != null)
                {
                    OnContainerEmpty(this, ca);
                }
            }
        }
        public async Task Refill()
        {
            ca.coffeIsEmpty = false;
            this.UsesSinceRefill = 0;
        }
    }
}
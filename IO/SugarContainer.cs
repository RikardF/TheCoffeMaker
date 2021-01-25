using System;
using System.Threading.Tasks;

namespace IO
{
    public class SugarContainer : IContainer
    {
        public int UsesSinceRefill { get; set; }
        public int MaxUsesPerRefill { get; set; }
        public event EventHandler<ContainerArgs> OnContainerEmpty;
        private ContainerArgs ca = new ContainerArgs();
        public SugarContainer()
        {
            this.UsesSinceRefill = 0;
            this.MaxUsesPerRefill = 10;
        }
        public async Task ContaierHandler()
        {
            if (UsesSinceRefill == MaxUsesPerRefill)
            {
                ca.sugarIsEmpty = true;
                if (OnContainerEmpty != null)
                {
                    OnContainerEmpty(this, ca);
                }
            }
        }
        public async Task Refill()
        {
            ca.sugarIsEmpty = false;
            this.UsesSinceRefill = 0;
        }
    }
}
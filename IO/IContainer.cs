using System;
using System.Threading.Tasks;

namespace IO
{
    internal interface IContainer
    {
        int UsesSinceRefill { get; set; }
        int MaxUsesPerRefill { get; set; }
        public event EventHandler<ContainerArgs> OnContainerEmpty;
        public Task ContaierHandler();
        public Task Refill();
    }
}
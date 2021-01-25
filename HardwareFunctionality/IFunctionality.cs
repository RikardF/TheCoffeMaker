using System;
using System.Threading.Tasks;

namespace HardwareFunctionality
{
    internal interface IFunctionality
    {
    public event EventHandler<EventArgs> OnComplete;
    public int TimeDelay { get; set; }

        public Task Run(float factor);
    }
}
using FluentScheduler;
using StructureMap;

namespace VarinaCmsV2.Common
{
    public abstract class InjectibleJob:IJob
    {
        public static IContainer Container { get; set; }
        public abstract void Execute();
    }
}
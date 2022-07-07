using Domain.Models;

namespace Domain.Factories.Abstract_factory
{
    public abstract class OutFileFactory
    {
        public abstract IOutFile GetOutFile();

        public abstract string GetInfo();
    }
}

namespace DesignPatterns.ChainOfResponsibility.Impl
{
    public abstract class AbstractStringMutator : IStringMutator
    {
        private IStringMutator _mutator;
        public IStringMutator SetNext(IStringMutator next)
        {
            _mutator = next;
            return next;
        }

        public virtual string Mutate(string str)
        {
            return _mutator != null ? _mutator.Mutate(str) : str;
        }
    }
}
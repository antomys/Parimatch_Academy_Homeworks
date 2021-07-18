namespace DesignPatterns.ChainOfResponsibility.Impl
{
    public class TrimMutator : AbstractStringMutator
    {
        public override string Mutate(string str)
        {
            return base.Mutate(str.Trim());
        }
    }
}
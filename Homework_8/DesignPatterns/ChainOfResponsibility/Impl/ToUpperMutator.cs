namespace DesignPatterns.ChainOfResponsibility.Impl
{
    public class ToUpperMutator : AbstractStringMutator
    {
        public override string Mutate(string str)
        {
            return base.Mutate(str.ToUpper());
        }
    }
}
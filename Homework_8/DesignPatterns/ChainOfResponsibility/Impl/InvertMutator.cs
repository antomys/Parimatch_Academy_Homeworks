namespace DesignPatterns.ChainOfResponsibility.Impl
{
    using System.Linq;
    public class InvertMutator : AbstractStringMutator
    {
        public override string Mutate(string str)
        {
            return base.Mutate(new string(Enumerable.Range(1, str.Length).Select(i => str[^i]).ToArray()));
        }
    }
}
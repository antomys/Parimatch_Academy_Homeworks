namespace DesignPatterns.ChainOfResponsibility.Impl
{
    using System.Text.RegularExpressions;
    public class RemoveNumbersMutator : AbstractStringMutator
    {
        public override string Mutate(string str)
        {
            return base.Mutate(Regex.Replace(str, @"[\d]", ""));
        }
    }
}
namespace DesignPatterns.IoC.Impl
{
    public class SomeSecondTransient
    {
        private readonly SomeSingleton _singleton;

        public SomeSecondTransient(SomeSingleton singleton)
        {
            _singleton = singleton;
        }

        public int Counter => _singleton.Counter;
    }
}
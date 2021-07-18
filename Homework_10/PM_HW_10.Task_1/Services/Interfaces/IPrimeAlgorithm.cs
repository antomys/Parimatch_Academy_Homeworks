using System.Threading.Tasks;

namespace Task_1.Services.Interfaces
{
    public interface IPrimeAlgorithm
    {
        Task<Result> GetPrimes(string primeFrom, string primeTo);

        Task<bool> IsPrime(int number);
    }
}
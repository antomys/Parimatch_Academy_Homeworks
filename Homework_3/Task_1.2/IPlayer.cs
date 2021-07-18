using System.Collections.Generic;

namespace Task_1._2
{
    public interface IPlayer
    {
        public int Age { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public PlayerRank Rank { get; }
    }

    public enum PlayerRank
    {
        Private = 2,
        Lieutenant = 21,
        Captain = 25,
        Major = 29,
        Colonel = 33,
        General = 39,
    }
}
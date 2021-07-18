#nullable enable
using System;
using System.Collections.Generic;

namespace Task_1._2
{
    public class Player : IPlayer
    {
        public Player(int age, string firstName, string lastName, PlayerRank rank)
        {
            Age = age;
            FirstName = firstName;
            LastName = lastName;
            Rank = rank;
        }

        public int Age { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public PlayerRank Rank { get; }

        private sealed class PlayerUniqueComparer : IEqualityComparer<IPlayer>
        {

            public bool Equals(IPlayer? x, IPlayer? y)
            {
                var nameFirst = x?.LastName + x?.FirstName;
                var nameSecond = y?.LastName + y?.FirstName;
                return nameFirst == nameSecond;
            }

            public int GetHashCode(IPlayer obj)
            {
                return obj.FirstName.GetHashCode() ^ obj.LastName.GetHashCode();
            }
        }

        public sealed class Sorting : IComparer<IPlayer>
        {
            private const string Dictionary = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            //private IComparer<IPlayer> _comparerImplementation;
            public int Compare(IPlayer? x, IPlayer? y)
            {
                var namesFirst = x?.FirstName + x?.LastName;
                var namesSecond = y?.FirstName + y?.LastName;
                if (namesFirst == null || namesFirst.Trim() == "")
                {
                    throw new Exception(namesFirst);
                    //return (namesSecond == null || namesSecond.Trim() =="") ? 0 : -1;
                }

                if (namesSecond == null || namesSecond.Trim() == "")
                {
                    return 1;
                }

                int minLength = Math.Min(namesFirst.Length, namesSecond.Length);
                for (int i = 0; i < minLength; i++)
                {
                    int i1 = Dictionary.IndexOf(namesFirst[i]);
                    int i2 = Dictionary.IndexOf(namesSecond[i]);

                    if (i1 == -1)
                    {
                        throw new Exception(namesFirst);
                    }

                    if (i2 == -1)
                    {
                        throw new Exception(namesSecond);
                    }


                    int cmp = i1.CompareTo(i2);
                    if (cmp != 0)
                    {
                        return cmp;
                    }
                }

                //return _comparerImplementation.Compare(x, y);
                return namesFirst.Length.CompareTo(namesSecond.Length);
            }
        }

        public sealed class SortByAge : IComparer<IPlayer>
        {
            public int Compare(IPlayer? x, IPlayer? y)
            {
                var ageFirst = x?.Age;
                var ageSecond = y?.Age;
                if (ageFirst == 0 || x?.Age == null)
                {
                    return 1;
                }

                if (ageSecond == 0 || y?.Age == null)
                {
                    return -1;
                }

                if (ageFirst == 0 && ageSecond == 0)
                {
                    return 0;
                }

                return x.Age.CompareTo(y.Age);
            }
        }

        public sealed class SortByRank : IComparer<IPlayer>
        {
            public int Compare(IPlayer? x, IPlayer? y)
            {
                if (x?.Rank == null)
                {
                    return 1;
                }

                if (y?.Rank == null)
                {
                    return -1;
                }
                return x!.Rank.CompareTo(y!.Rank);
            }
        }
        public override string ToString()
        {
            return $"Age:{Age} ; First Name : {FirstName} ; Last Name : {LastName} ; Rank: {Rank}";
        }

        public static IEqualityComparer<IPlayer> Comparer { get; } = new PlayerUniqueComparer();
    }

    class Players : List<IPlayer>
    {
        public override string ToString() => $"({String.Join(",", this)})";
    }
}
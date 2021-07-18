namespace Level2_3.ArrayOperations
{
    using System;
    using System.Linq;
    internal class ArrayOptions : IArrayOptions
    {
        private int[] _sortedArray;
        private int _rangeLength;

        public void InputRange(int rangeLength)
        {
            _sortedArray = new int[rangeLength];

            _rangeLength = rangeLength;
            
           for (var i = 1; i <= _rangeLength; i++) 
           {
               Console.Write($"{i}: ");
               
               if (!int.TryParse(Console.ReadLine(), out var input))
                   throw new ArgumentOutOfRangeException($"{input}");
               
               _sortedArray[i - 1] = input;
           }
        }

        public int Calculate()
        {
            _sortedArray = ArraySort();
            
            Console.WriteLine( $"Minimal: {MinValue()}");
            Console.WriteLine( $"Maximal: {MaxValue()}");
            
            Console.WriteLine( $"Average: {Average()}");
            Console.WriteLine( $"Sum of Elements: {SumElement()}");
            
            Console.WriteLine( $"Standard Deviation: {StDer()}");
            Console.WriteLine(  "Sorted array:");
            
            foreach (var number in _sortedArray)
            {
                Console.WriteLine(number);
            }

            return 0;
        }

        #region Operations
        private int[] ArraySort()
        {
            for (var i = 0; i < _sortedArray.Length - 1; i++)
                for (var j = i + 1; j < _sortedArray.Length; j++)
                    if (_sortedArray[i] < _sortedArray[j])
                    {

                        var temp = _sortedArray[i];
                        _sortedArray[i] = _sortedArray[j];
                        _sortedArray[j] = temp;
                    }
            return _sortedArray;
        }

        private int MinValue() 
        {
            return _sortedArray[^1];
        }

        private int MaxValue()
        {
           return _sortedArray[0];
        }

        private int SumElement()
        {
           return _sortedArray.Sum();
        }

        private double Average()
        {
            var output = SumElement() / _sortedArray.Length;
            return output;
        }

        private double StDer()
        {
            var mean = Average();
            var variance = _sortedArray.Sum(number => (float) Math.Pow(number - mean, 2));
            
            return Math.Sqrt(variance / _sortedArray.Length);;
        }
        #endregion
    }
}
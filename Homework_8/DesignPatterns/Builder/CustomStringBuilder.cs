namespace DesignPatterns.Builder
{
    using System.Collections.Generic;
    using System.Linq;
    public class CustomStringBuilder : ICustomStringBuilder
    {
        private char[] _chars;
        public CustomStringBuilder()
        {
            _chars = new char[16];
        }

        public CustomStringBuilder(string text)
        {
            _chars = new char[text.Length];
            _chars = text.ToCharArray();
        }

        public ICustomStringBuilder Append(string str)
        {
            if (IsEmpty(_chars))
            {
                _chars = new char[str.Length];
                _chars = str.ToCharArray();
            }
            else
            {
                var copyOfList = _chars;

                var inputCharArray = str.ToCharArray();
            
                _chars = new char[copyOfList.Length + inputCharArray.Length];
                
                copyOfList.CopyTo(_chars,0);
                inputCharArray.CopyTo(_chars,copyOfList.Length);
            }
            return this;
        }

        public ICustomStringBuilder Append(char ch)
        {
            if (IsEmpty(_chars))
            {
                _chars = new char[1];
                _chars[^1] = ch;
            }
            else
            {
                var copyOfChars = _chars;
                _chars = new char[_chars.Length + 1];
                
                copyOfChars.CopyTo(_chars,0);
                _chars[^1] = ch;
            }

            return this;
        }

        public ICustomStringBuilder AppendLine()
        {
            return Append('\n');
        }

        public ICustomStringBuilder AppendLine(string str)
        {
            return Append(str + '\n');
        }

        public ICustomStringBuilder AppendLine(char ch)
        {
            return Append(ch.ToString()+'\n');
        }

        public string Build()
        {
            return IsEmpty(_chars) ? string.Empty : new string(_chars);
        }

        private static bool IsEmpty(IReadOnlyCollection<char> array)
        {
            var arrayLength = array.Count;
            var counter = array.Count(element => element == (char) 0);

            return arrayLength == counter;
        }

    }
}
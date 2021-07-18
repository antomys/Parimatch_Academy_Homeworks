using System;
using FluentAssertions;
using Xunit;
using NSubstitute;
using NotesApp.Tools;

namespace NotesApp.Tests
{
    public class StringGeneratorTests
    {
        [Fact]
        public void Method_GenerateNumberString_returns_empty_when_length_is_zero()
        {
            var toGenerateWithZeros = StringGenerator.GenerateNumbersString(0,true);
            var toGenerateWithoutZeros = StringGenerator.GenerateNumbersString(0,false);
            toGenerateWithZeros.Should().Be(string.Empty);
            toGenerateWithoutZeros.Should().Be(string.Empty);
        }

        [Fact]
        public void Method_GenerateNumberString_throws_exception_with_invalid_inputs()
        {
            var negativeToInput = -4000;
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                StringGenerator.GenerateNumbersString(negativeToInput, true));
        }

        [Fact]
        public void Method_GenerateNumberString_generates_string_without_zeros()
        {
            var zero = 0;
            var toGenerate = StringGenerator.GenerateNumbersString(17, false);
            toGenerate.Should().NotStartWith(zero.ToString());
        }

        [Fact]
        public void Method_GenerateNumberString_returns_string_with_input_length()
        {
            var expected = 14;
            var generated = StringGenerator.GenerateNumbersString(expected, true);
            generated.Length.Should().Be(expected);
        }

        [Fact]
        public void Method_GenerateNumberString_returns_string_could_be_converted_to_number()
        {
            var randomLength = new Random();
            var length = randomLength.Next(1, 20);
            var generated = Convert.ToUInt64(StringGenerator.GenerateNumbersString(length, false));
            var convertedLength = Math.Floor(Math.Log10(generated) + 1);
            convertedLength.Should().Be(length);
        }
        
    }
}
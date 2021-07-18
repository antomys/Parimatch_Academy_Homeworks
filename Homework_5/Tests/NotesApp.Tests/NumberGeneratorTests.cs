using System;
using FluentAssertions;
using Xunit;
using NotesApp.Tools;

namespace NotesApp.Tests
{
    public class NumberGeneratorTests
    {
        [Fact]
        public void Method_GeneratePositiveLong_throws_exception_when_invalid()
        {
            const int length = -12;
            const int otherLength = 48;
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                NumberGenerator.GeneratePositiveLong(length));
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                NumberGenerator.GeneratePositiveLong(otherLength));
        }

        [Fact]
        public void Method_GeneratePositiveLong_returns_with_written_length()
        {
            const int length = 14;
            var result = NumberGenerator.GeneratePositiveLong(length);
            Math.Floor(Math.Log10(result) + 1).Should().Be(length);
        }

        
    }
}
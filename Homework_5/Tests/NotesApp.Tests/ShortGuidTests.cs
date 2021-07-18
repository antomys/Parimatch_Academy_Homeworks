using System;
using System.Linq;
using FluentAssertions;
using NotesApp.Tools;
using Xunit;

namespace NotesApp.Tests
{
    public class ShortGuidTests
    {
        [Fact]
        public void Methods_correctly_transforms_guid_to_shortGuid_to_guid()
        {
            var guid = Guid.NewGuid();
            var shortGuid = ShortGuid.ToShortId(guid);
            var fromShortToNormal = ShortGuid.FromShortId(shortGuid);
            fromShortToNormal.Should().Be(guid);
        }

        [Fact]
        public void Method_FromShortId_correct_work_with_equals()
        {
            var generatedGuid = Guid.NewGuid();
            var guid = ShortGuid.FromShortId(generatedGuid.ToShortId() + "==");
            guid.Should().Be(generatedGuid);
        }
        [Fact]
        public void Method_FromShortId_correct_converts_from_string()
        {
            var generatedGuidToString = Guid.NewGuid().ToString();
            var guid = generatedGuidToString.FromShortId();
            guid.Should().Be(generatedGuidToString);
        }

        [Fact]
        public void Method_FromShortId_throws_exception_if_not_valid_shortGuid()
        { 
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var generateShortGuid =  new string(Enumerable.Repeat(chars, 30)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            Assert.Throws<FormatException>(() => generateShortGuid.FromShortId());
        }

        [Fact]
        public void Method_FromShortId_returns_null_if_input_is_null()
        {
            var generated = ShortGuid.FromShortId(null);
            generated.Should().Be(null);
        }
    }
}
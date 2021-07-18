using System;
using FluentAssertions;
using Moq;
using Moq.Language.Flow;
using NotesApp.Services.Abstractions;
using NotesApp.Services.Models;
using NotesApp.Services.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace NotesApp.Tests
{
    public class NotesServiceTests
    {
        [Fact]
        public void Method_AddNote_throws_exception_if_note_is_null()
        {
            var storage = Substitute.For<INotesStorage>();
            var events = Substitute.For<INoteEvents>();
            var classEntity = new NotesService(storage, events);
            Assert.Throws<ArgumentNullException>(() => classEntity.AddNote(null, 1));
        }
        [Fact]
        public void Moq_Method_AddNote_throws_exception_if_note_is_null()
        {
            var storage = new Mock<INotesStorage>().Object;
            var events = new Mock<INoteEvents>().Object;
            var classEntity = new NotesService(storage, events);
            Assert.Throws<ArgumentNullException>(() => classEntity.AddNote(null, 1));
        }

        [Fact]
        public void If_INotesStorage_AddNote_success_message_through_INoteEvents_NotifyAdded()
        {
            var mockEvents = new Mock<INoteEvents>();
            var mockStorage = new Mock<INotesStorage>();
            var sut = new NotesService(mockStorage.Object, mockEvents.Object);
            sut.AddNote(new Note(),new int());
            mockEvents.Verify(x=>x.NotifyAdded(It.IsAny<Note>(),It.IsAny<int>()));
        }

        [Fact]
        public void If_INotesStorage_AddNote_fail_no_message_through_INoteEvents_NotifyAdded()
        {
            var mockEvents = new Mock<INoteEvents>();
            var mockStorage = new Mock<INotesStorage>();
            var sut = new NotesService(mockStorage.Object, mockEvents.Object);
            try
            {
                sut.AddNote(null, new int());
            }
            catch
            {
                
            }
            finally
            {
                mockEvents.Verify(x=>
                    x.NotifyAdded(It.IsAny<Note>(), It.IsAny<int>()),Times.Never);
            }
            //todo: this is real shit. To redo
            /*mockEvents.Setup(x =>
                x.NotifyAdded(null, It.IsAny<int>())).Throws(new Exception());*/
            
        }

        [Fact]
        public void If_Deleted_by_INotesStorage_Publish_Message_through_INoteEvents()
        {
            var mockEvents = new Mock<INoteEvents>();
            var mockStorage = new Mock<INotesStorage>();
            mockStorage.Setup(x => x.DeleteNote(It.IsAny<Guid>())).Returns(true);
            var sut = new NotesService(mockStorage.Object, mockEvents.Object);
            sut.DeleteNote(new Guid(), new int());
            mockEvents.Verify(x=>x.NotifyDeleted(It.IsAny<Guid>(),It.IsAny<int>()),Times.Exactly(1));
        }

        [Fact]
        public void If_Not_Deleted_by_INotesStorage_Not_Published_Message_through_INoteEvents()
        {
            var mockEvents = new Mock<INoteEvents>();
            var mockStorage = new Mock<INotesStorage>();
            mockStorage.Setup(x => x.DeleteNote(It.IsAny<Guid>())).Returns(false);
            var sut = new NotesService(mockStorage.Object, mockEvents.Object);
            sut.DeleteNote(new Guid(), new int());
            mockEvents.Verify(x =>
                x.NotifyDeleted(It.IsAny<Guid>(), It.IsAny<int>()),Times.Never());
        }
    }
}
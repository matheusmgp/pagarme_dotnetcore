

using System.Net;
using Application.Errors;

namespace PAGAR.ME.Tests.errors
{
    public class ExceptionsUnitTests
    {
        [Fact]
        public async void Should_Be_InstanceOf_BadRequestException()
        {
            var exceptionResult = await Assert.ThrowsAsync<BadRequestException>(() => throw new BadRequestException("Mock Message"));
            Assert.Equal("Mock Message", exceptionResult.Message);
            Assert.Equal(HttpStatusCode.BadRequest, exceptionResult.StatusCode);
            Assert.True(typeof(BadRequestException).IsInstanceOfType(exceptionResult));
        }
        [Fact]
        public async void Should_Be_InstanceOf_ConflictException()
        {
            var exceptionResult = await Assert.ThrowsAsync<ConflictException>(() => throw new ConflictException("Mock Message"));
            Assert.Equal("Mock Message", exceptionResult.Message);
            Assert.Equal(HttpStatusCode.Conflict, exceptionResult.StatusCode);
            Assert.True(typeof(ConflictException).IsInstanceOfType(exceptionResult));
        }
        [Fact]
        public async void Should_Be_InstanceOf_DatabaseException()
        {
            var exceptionResult = await Assert.ThrowsAsync<DatabaseException>(() => throw new DatabaseException("Mock Message"));
            Assert.Equal("Mock Message", exceptionResult.Message);
            Assert.Equal(HttpStatusCode.ServiceUnavailable, exceptionResult.StatusCode);
            Assert.True(typeof(DatabaseException).IsInstanceOfType(exceptionResult));
        }
        [Fact]
        public async void Should_Be_InstanceOf_InternalServerException()
        {
            var exceptionResult = await Assert.ThrowsAsync<InternalServerException>(() => throw new InternalServerException("Mock Message"));
            Assert.Equal("Mock Message", exceptionResult.Message);
            Assert.Equal(HttpStatusCode.InternalServerError, exceptionResult.StatusCode);
            Assert.True(typeof(InternalServerException).IsInstanceOfType(exceptionResult));
        }
        [Fact]
        public async void Should_Be_InstanceOf_NotFoundException()
        {
            var exceptionResult = await Assert.ThrowsAsync<NotFoundException>(() => throw new NotFoundException("Mock Message"));
            Assert.Equal("Mock Message", exceptionResult.Message);
            Assert.Equal(HttpStatusCode.NotFound, exceptionResult.StatusCode);
            Assert.True(typeof(NotFoundException).IsInstanceOfType(exceptionResult));
        }
        [Fact]
        public async void Should_Be_InstanceOf_UnauthorizedAccessException()
        {
            var exceptionResult = await Assert.ThrowsAsync<UnauthorizedException>(() => throw new UnauthorizedException("Mock Message"));
            Assert.Equal("Mock Message", exceptionResult.Message);
            Assert.Equal(HttpStatusCode.Unauthorized, exceptionResult.StatusCode);
            Assert.True(typeof(UnauthorizedException).IsInstanceOfType(exceptionResult));
        }
    }
}
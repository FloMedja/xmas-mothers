using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ChristmasMothers.Entities;
using ChristmasMothers.Exceptions;
using ChristmasMothers.Web.Api.Middlewares;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ChristmasMothers.Exceptions;
using Shouldly;

namespace ChristmasMothers.Web.Api.Tests.Middlewares
{
    [TestClass]
    public class ErrorHandlingMiddlewareTests
    {
        private MemoryStream _bodyStream;
        private HttpResponse _httpResponse;
        private HttpContext _httpContext;
        private ILoggerFactory _loggerFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            _bodyStream = new MemoryStream();
            _httpResponse = Substitute.For<HttpResponse>();
            _httpResponse.Body.Returns(_bodyStream);
            _httpContext = Substitute.For<HttpContext>();
            _httpContext.Response.Returns(_httpResponse);
            _loggerFactory = Substitute.For<ILoggerFactory>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _bodyStream.Dispose();
        }

        #region Invoke

        [TestMethod]
        public async Task Invoke_HandlesNotFoundExceptions()
        {
            // Arrange            
            var exception = new NotFoundException(Guid.NewGuid(), typeof(Requisition));
            var sut = new ErrorHandlingMiddleware(ctx => throw exception, _loggerFactory);
            
            // Act
            await sut.Invoke(_httpContext);

            // Assert
            _httpResponse.Received(1).ContentType = "application/json";
            _httpResponse.Received(1).StatusCode = (int)HttpStatusCode.NotFound;

            _bodyStream.Length.ShouldBeGreaterThan(0);
            AssertStreamContains(_bodyStream, exception.Message);
        }

        [TestMethod]
        public async Task Invoke_HandlesUnauthorizedExceptions()
        {
            // Arrange            
            var exception = new UnauthorizedException("unauthorized exception message");
            var sut = new ErrorHandlingMiddleware(ctx => throw exception, _loggerFactory);

            // Act
            await sut.Invoke(_httpContext);

            // Assert
            _httpResponse.Received(1).ContentType = "application/json";
            _httpResponse.Received(1).StatusCode = (int)HttpStatusCode.Unauthorized;

            _bodyStream.Length.ShouldBeGreaterThan(0);
            AssertStreamContains(_bodyStream, exception.Message);
        }

        [TestMethod]
        public async Task Invoke_HandlesConflictExceptions()
        {
            // Arrange            
            var exception = new ConflictException("conflict exception message");
            var sut = new ErrorHandlingMiddleware(ctx => throw exception, _loggerFactory);

            // Act
            await sut.Invoke(_httpContext);

            // Assert
            _httpResponse.Received(1).ContentType = "application/json";
            _httpResponse.Received(1).StatusCode = (int)HttpStatusCode.Conflict;

            _bodyStream.Length.ShouldBeGreaterThan(0);
            AssertStreamContains(_bodyStream, exception.Message);
        }

        [TestMethod]
        public async Task Invoke_HandlesChristmasMotherExceptions()
        {
            // Arrange            
            var exception = new ChristmasMotherException("ChristmasMother exception message");
            var sut = new ErrorHandlingMiddleware(ctx => throw exception, _loggerFactory);

            // Act
            await sut.Invoke(_httpContext);

            // Assert
            _httpResponse.Received(1).ContentType = "application/json";
            _httpResponse.Received(1).StatusCode = (int)HttpStatusCode.BadRequest;

            _bodyStream.Length.ShouldBeGreaterThan(0);
            AssertStreamContains(_bodyStream, exception.Message);
        }

        [TestMethod]
        public async Task Invoke_HandlesException()
        {
            // Arrange            
            var exception = new Exception("exception message");
            var sut = new ErrorHandlingMiddleware(ctx => throw exception, _loggerFactory);

            // Act
            await sut.Invoke(_httpContext);

            // Assert
            _httpResponse.Received(1).ContentType = "application/json";
            _httpResponse.Received(1).StatusCode = (int)HttpStatusCode.InternalServerError;

            _bodyStream.Length.ShouldBeGreaterThan(0);
            AssertStreamContains(_bodyStream, exception.Message);
        }

        #endregion

        private void AssertStreamContains(Stream stream, string text)
        {
            stream.Position = 0;
            using (var reader = new StreamReader(stream))
            {
                var content = reader.ReadToEnd();
                content.ShouldContain(text);
            }
        }
    }
}

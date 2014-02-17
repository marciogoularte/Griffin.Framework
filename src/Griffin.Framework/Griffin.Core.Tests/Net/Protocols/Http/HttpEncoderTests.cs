﻿using System.IO;
using System.Text;
using FluentAssertions;
using Griffin.Net.Protocols.Http;
using Xunit;

namespace Griffin.Core.Tests.Net.Protocols.Http
{
    public class HttpEncoderTests
    {
        [Fact]
        public void request_in_its_simplest_form()
        {
            var frame = new BasicHttpRequest("POST", "/", "HTTP/1.1");
            var expected = "POST / HTTP/1.1\r\n\r\n";
            var buffer = new SocketBufferFake();

            var encoder = new HttpMessageEncoder();
            encoder.Prepare(frame);
            encoder.Send(buffer);
            var actual = Encoding.ASCII.GetString(buffer.Buffer, 0, buffer.Count);

            actual.Should().Be(expected);
        }

        [Fact]
        public void request_with_body()
        {
            var frame = new BasicHttpRequest("POST", "/?abc", "HTTP/1.1");
            frame.AddHeader("server", "Griffin.Networking");
            frame.AddHeader("X-Requested-With", "XHttpRequest");
            frame.ContentType = "text/plain";
            frame.Body = new MemoryStream(Encoding.ASCII.GetBytes("hello queue a"));
            var expected = "POST /?abc HTTP/1.1\r\nserver:Griffin.Networking\r\nX-Requested-With:XHttpRequest\r\nContent-Type:text/plain\r\ncontent-length:13\r\n\r\nhello queue a";
            var buffer = new SocketBufferFake();

            var encoder = new HttpMessageEncoder();
            encoder.Prepare(frame);
            encoder.Send(buffer);
            var actual = Encoding.ASCII.GetString(buffer.Buffer, 0, buffer.Count);

            actual.Should().Be(expected);
        }

        [Fact]
        public void basic_response()
        {
            var frame = new BasicHttpResponse(404, "Failed to find it dude", "HTTP/1.1");
            var expected = "HTTP/1.1 404 Failed to find it dude\r\n\r\n";
            var buffer = new SocketBufferFake();

            var encoder = new HttpMessageEncoder();
            encoder.Prepare(frame);
            encoder.Send(buffer);
            var actual = Encoding.ASCII.GetString(buffer.Buffer, 0, buffer.Count);

            actual.Should().Be(expected);
        }

        [Fact]
        public void response_with_body()
        {
            var frame = new BasicHttpResponse(404, "Failed to find it dude", "HTTP/1.1");
            frame.AddHeader("server", "Griffin.Networking");
            frame.AddHeader("X-Requested-With", "XHttpRequest");
            frame.ContentType = "text/plain";
            frame.Body = new MemoryStream(Encoding.ASCII.GetBytes("hello queue a"));
            var expected = "HTTP/1.1 404 Failed to find it dude\r\nserver:Griffin.Networking\r\nX-Requested-With:XHttpRequest\r\nContent-Type:text/plain\r\ncontent-length:13\r\n\r\nhello queue a";
            var buffer = new SocketBufferFake();

            var encoder = new HttpMessageEncoder();
            encoder.Prepare(frame);
            encoder.Send(buffer);
            var actual = Encoding.ASCII.GetString(buffer.Buffer, 0, buffer.Count);

            actual.Should().Be(expected);
        }
    }
}

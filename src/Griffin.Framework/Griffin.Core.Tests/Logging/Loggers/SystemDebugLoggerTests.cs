﻿using System;
using Griffin.Logging.Loggers;
using Xunit;

namespace Griffin.Core.Tests.Logging.Loggers
{
    public class SystemDebugLoggerTests
    {
        [Fact]
        public void Just_verify_the_regular_write()
        {

            var sut = new SystemDebugLogger(GetType());
            sut.Debug("Hello world");

        }

        [Fact]
        public void Just_verify_formatted_Write()
        {

            var sut = new SystemDebugLogger(GetType());
            sut.Debug("Hello {0}", "world");

        }

        [Fact]
        public void Just_verify_exception_Write()
        {

            var sut = new SystemDebugLogger(GetType());
            sut.Debug("Hello world", new Exception());

        }
    }
}

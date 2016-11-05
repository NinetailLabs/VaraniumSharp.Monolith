using FluentAssertions;
using NUnit.Framework;
using Owin;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using VaraniumSharp.Monolith.HostSetup;

namespace VaraniumSharp.Monolith.Tests.HostSetup
{
    public class OwinStartupTest
    {
        [Test]
        public void RegisterNancyForStartup()
        {
            // arrange
            var appBuilderDummy = new AppBuilderFixture();
            var sut = new OwinStartup();

            // act
            sut.Configuration(appBuilderDummy);

            // assert
            appBuilderDummy.MiddleWareRegistrationInvocations.Should().Be(1);
        }

        public class AppBuilderFixture : IAppBuilder
        {
            public int MiddleWareRegistrationInvocations { get; private set; }

            public AppBuilderFixture()
            {
                Properties = new ConcurrentDictionary<string, object>();
            }

            public IAppBuilder Use(object middleware, params object[] args)
            {
                MiddleWareRegistrationInvocations++;
                return this;
            }

            public IAppBuilder New()
            {
                return this;
            }

            public IDictionary<string, object> Properties { get; }

            public object Build(Type returnType)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
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
        #region Public Methods

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

        #endregion

        private class AppBuilderFixture : IAppBuilder
        {
            #region Constructor

            public AppBuilderFixture()
            {
                Properties = new ConcurrentDictionary<string, object>();
            }

            #endregion

            #region Properties

            public int MiddleWareRegistrationInvocations { get; private set; }

            public IDictionary<string, object> Properties { get; }

            #endregion

            #region Public Methods

            public object Build(Type returnType)
            {
                throw new NotImplementedException();
            }

            public IAppBuilder New()
            {
                return this;
            }

            public IAppBuilder Use(object middleware, params object[] args)
            {
                MiddleWareRegistrationInvocations++;
                return this;
            }

            #endregion
        }
    }
}
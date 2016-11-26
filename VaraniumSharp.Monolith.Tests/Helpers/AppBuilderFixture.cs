using Owin;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace VaraniumSharp.Monolith.Tests.Helpers
{
    public class AppBuilderFixture : IAppBuilder
    {
        #region Constructor

        public AppBuilderFixture()
        {
            var tokenSource = new CancellationTokenSource();
            Properties = new ConcurrentDictionary<string, object>();
            Properties.Add("server.OnDispose", tokenSource.Token);
        }

        #endregion

        #region Properties

        public int MiddleWareRegistrationInvocations { get; private set; }

        public IDictionary<string, object> Properties { get; }

        #endregion

        #region Public Methods

        public object Build(Type returnType)
        {
            return this;
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
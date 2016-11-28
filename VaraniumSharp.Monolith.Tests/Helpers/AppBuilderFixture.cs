using Moq;
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
            //See http://sourcebrowser.io/Browse/jchannon/katanaproject/src/Microsoft.Owin/Extensions/AppBuilderExtensions.cs#78
            Properties.Add("builder.AddSignatureConversion", DelegateAction);
        }

        #endregion

        #region Properties

        public Action<Delegate> DelegateAction => EmptyDelegateMethod;

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

        #region Private Methods

        private void EmptyDelegateMethod(Delegate @delegate)
        {
            //Just does nothing
        }

        #endregion
    }
}
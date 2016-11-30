using DryIoc;
using Hangfire;
using Hangfire.MemoryStorage;
using Moq;
using System;
using VaraniumSharp.Monolith.Tests.Helpers;

namespace VaraniumSharp.Monolith.Tests
{
    public static class HangfireHelperFixture
    {
        #region Constructor

        static HangfireHelperFixture()
        {
            ContainerDummy = new Mock<IContainer>();

            var appBuilderDummy = new AppBuilderFixture();
            GlobalConfiguration.Configuration.UseMemoryStorage();
            GlobalConfiguration.Configuration.UseActivator(new JobActivatorFixture(ContainerDummy.Object));
            appBuilderDummy.UseHangfireServer();
        }

        #endregion

        #region Public Methods

        public static void RegisterType<T>(Type type, ref T instanceToReturn)
        {
            ContainerDummy.Setup(t => t.Resolve(type, false)).Returns(instanceToReturn);
        }

        #endregion

        #region Variables

        private static readonly Mock<IContainer> ContainerDummy;

        #endregion

        private class JobActivatorFixture : JobActivator
        {
            #region Constructor

            public JobActivatorFixture(IContainer container)
            {
                _container = container;
            }

            #endregion

            #region Public Methods

            public override object ActivateJob(Type jobType)
            {
                return _container.Resolve(jobType);
            }

            #endregion

            #region Variables

            private readonly IContainer _container;

            #endregion
        }
    }
}
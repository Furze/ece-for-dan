using Xunit;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    [CollectionDefinition(Collections.IntegrationTestCollection)]
    public class IntegrationTestSetUpCollection : ICollectionFixture<RunOnceBeforeAllTests>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    public static class Collections
    {
        public const string IntegrationTestCollection = "IntegrationTestCollection";
    }
}
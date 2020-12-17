using Events.Integration.Protobuf.Eli;
using Events.Integration.Protobuf.Workflow;
using MoE.ECE.Web.Infrastructure.ServiceBus;
using Moe.Library.ServiceBus;
using Shouldly;
using Xunit;

namespace MoE.ECE.Unit.Tests
{
    public class MessageResolverTests
    {
        public MessageResolverTests() => _classUnderTest = new MessageResolver();

        private readonly MessageResolver _classUnderTest;

        [Fact]
        public void Approved_message_type_should_resolve_correctly()
        {
            var type = _classUnderTest.DetermineMessageType(nameof(Approved), MessageFormat.Proto);

            type.ShouldBe(typeof(Approved));
        }
        
        [Fact]
        public void Rs7Received_message_type_should_resolve_correctly()
        {
            var type = _classUnderTest.DetermineMessageType(nameof(Rs7Received), MessageFormat.Proto);

            type.ShouldBe(typeof(Rs7Received));
        }
        
        [Fact]
        public void Returned_message_type_should_resolve_correctly()
        {
            var type = _classUnderTest.DetermineMessageType(nameof(Returned), MessageFormat.Proto);

            type.ShouldBe(typeof(Returned));
        }
        
        [Fact]
        public void Declined_message_type_should_resolve_correctly()
        {
            var type = _classUnderTest.DetermineMessageType(nameof(Declined), MessageFormat.Proto);

            type.ShouldBe(typeof(Declined));
        }
    }
}
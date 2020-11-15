// using System;
// using Bard;
// using Microsoft.EntityFrameworkCore;
// using MoE.ECE.Domain.Event;
// using MoE.ECE.Domain.Infrastructure;
// using MoE.ECE.Domain.Read.Model.Rs7;
// using MoE.ECE.Integration.Tests.Chapter;
// using MoE.ECE.Integration.Tests.Infrastructure;
// using Moe.Library.Cqrs;
// using Shouldly;
// using Xunit;
// using Xunit.Abstractions;
//
// namespace MoE.ECE.Integration.Tests.Rs7.GET.For_a_rs7_that_has_been_peer_approved
// {
//     // ReSharper disable once InconsistentNaming
//     public class Rs7_has_been_submitted_for_peer_approval : SpeedyIntegrationTestBase
//     {
//         private Exception Exception
//         {
//             get => TestData.Exception;
//             set => TestData.Exception = value;
//         }
//
//         private Rs7Model Rs7Model
//         {
//             get => TestData.ArrangeResult;
//             set => TestData.ArrangeResult = value;
//         }
//
//         protected override void Arrange()
//         { 
//             Given
//                 .A_rs7_has_been_created()
//                 .The_rs7_has_been_submitted_for_peer_approval()
//                 .The_rs7_has_been_peer_approved()
//                 .GetResult(storyData => Rs7Model = storyData.Rs7Model);
//         }
//
//         protected override void Act()
//         {
//             try
//             {
//                 RejectPeerReview();
//             }
//             catch (Exception e)
//             {
//                 Exception = e;
//             }
//
//             When.Get($"{Constants.Url}/{Rs7Model.Id}");
//         }
//
//         private void RejectPeerReview()
//         {
//             var command = DbLoggerCategory.Database.Command.Rs7PeerReject((Guid) ArrangeResult.BusinessEntityId!);
//             var cqrs = Services.GetService<ICqrs>();
//             AsyncHelper.RunSync(() => cqrs.ExecuteAsync(command));
//         }
//         
//         [Fact]
//         public void Exception_should_be_thrown_when_processing_message()
//         {
//             this.Exception.ShouldNotBeNull();
//         }
//         
//         [Fact]
//         public void Roll_status_should_not_transition_to_rejected_state()
//         {
//           Then.Response.Content<Rs7Model>().RollStatus.ShouldBe(RollStatus.InternalReadyForReview);
//         }
//
//         [Fact]
//         public void Rejected_domain_event_should_not_be_raised()
//         {
//             A_domain_event_should_not_be_fired<Rs7PeerRejected>();
//         }
//
//         protected Rs7_has_been_submitted_for_peer_approval(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
//         {
//         }
//     }
// }


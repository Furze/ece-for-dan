using MoE.ECE.Domain.Event;

namespace MoE.ECE.Integration.Tests.Chapter
{
    public class ECEStoryData
    {
        public Rs7Created Rs7Created { get; set; } = new Rs7Created();
        public Rs7Updated Rs7Updated { get; set; } = new Rs7Updated();
    }
}
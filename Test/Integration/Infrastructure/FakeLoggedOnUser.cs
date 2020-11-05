﻿using MoE.ECE.Domain.Abstractions;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    public class FakeLoggedOnUser : ILoggedOnUser
    {
        public const string LoggedOnUserId = "burgundyr";

        public bool IsAuthenticated => true;

        public string UserName => LoggedOnUserId;
    }
}
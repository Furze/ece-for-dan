using System;
using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;

namespace MoE.ECE.Domain.Read.Model.Services
{
    public class SessionTimeModelMapper : Profile
    {
        public SessionTimeModelMapper()
        {
            CreateMap<DateTimeOffset?, SessionTimeModel>()
                .ConvertUsing<SessionTimeModelConverter>();
        }
    }

    public class SessionTimeModelConverter : ITypeConverter<DateTimeOffset?, SessionTimeModel>
    {
        public SessionTimeModel Convert(DateTimeOffset? source, SessionTimeModel destination, ResolutionContext context)
        {
            var nzDateTime = source.ToNzDateTimeOffSet();
            return new SessionTimeModel
            {
                Hour = nzDateTime?.Hour ?? 0,
                Minute = nzDateTime?.Minute ?? 0,
            };
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.ReferenceData;

namespace MoE.ECE.Domain.Read.Model.Services
{
    public class DailySessionModelMapper : Profile
    {
        public DailySessionModelMapper()
        {
            CreateMap<string?, int?>()
                .ConvertUsing<DayOfWeekConverter>();
            
            CreateMap<EceOperatingSession, DailySessionModel>()
                .Map(d => d.DayOfWeek, s => s.SessionDayDescription)
                .Map(d => d.Day, s => s.SessionDayDescription)
                .Ignore(d => d.OperatingTimes);
        }
    }

    public class OperatingSessionsConverter : ITypeConverter<ICollection<EceOperatingSession>, List<DailySessionModel>>
    {
        public List<DailySessionModel> Convert(ICollection<EceOperatingSession> source, List<DailySessionModel> destination, ResolutionContext context)
        {
            var sessionsForDay = source.GroupBy(session => session.SessionDayDescription);

            List<DailySessionModel> operatingSessionModels = new List<DailySessionModel>();
            
            // In our domain model we can have multiple sessions for the same day. with different operating times.
            // We want to merge duplicate sessions into one operating sessions with a collection of operating times.
            foreach (var sessionForDayGroup in sessionsForDay)
            {
                var first = sessionForDayGroup.First();

                var model = context.Mapper.Map<DailySessionModel>(first);
                model.OperatingTimes = context.Mapper.Map<List<OperatingSessionModel>>(sessionForDayGroup);
                operatingSessionModels.Add(model);    
            }

            return operatingSessionModels;
        }
    }

    public class DayOfWeekConverter : ITypeConverter<string?, int?> 
    {
        public int? Convert(string? source, int? destination, ResolutionContext context)
        {
            return source switch
            {
                SessionDay.Monday => 1,
                SessionDay.Tuesday => 2,
                SessionDay.Wednesday => 3,
                SessionDay.Thursday => 4,
                SessionDay.Friday => 5,
                SessionDay.Saturday => 6,
                SessionDay.Sunday => 7,
                var _ => null
            };
        }
    }
}
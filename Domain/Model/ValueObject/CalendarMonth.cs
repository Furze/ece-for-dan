using System.Linq;

namespace MoE.ECE.Domain.Model.ValueObject
{
    public class CalendarMonth : Enumeration
    {
        public static readonly CalendarMonth January = new CalendarMonth(1, "January");
        public static readonly CalendarMonth February = new CalendarMonth(2, "February");
        public static readonly CalendarMonth March = new CalendarMonth(3, "March");
        public static readonly CalendarMonth April = new CalendarMonth(4, "April");
        public static readonly CalendarMonth May = new CalendarMonth(5, "May");
        public static readonly CalendarMonth June = new CalendarMonth(6, "June");
        public static readonly CalendarMonth July = new CalendarMonth(7, "July");
        public static readonly CalendarMonth August = new CalendarMonth(8, "August");
        public static readonly CalendarMonth September = new CalendarMonth(9, "September");
        public static readonly CalendarMonth October = new CalendarMonth(10, "October");
        public static readonly CalendarMonth November = new CalendarMonth(11, "November");
        public static readonly CalendarMonth December = new CalendarMonth(12, "December");

        public static CalendarMonth GetById(int id)
        {
            return GetAll<CalendarMonth>().Single(enumeration => enumeration.Id == id);
        }

        private CalendarMonth(int id, string name) : base(id, name)
        {
        }
    }
}
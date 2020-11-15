using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MoE.ECE.Domain.Model.ValueObject
{
    public abstract class Enumeration : IComparable, IEqualityComparer<Enumeration>
    {
        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; }

        public int Id { get; }

        public int CompareTo(object? other) => Id.CompareTo(((Enumeration)other!).Id);

        public bool Equals(Enumeration? x, Enumeration? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null))
            {
                return false;
            }

            if (ReferenceEquals(y, null))
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            return x.Name == y.Name && x.Id == y.Id;
        }

        public int GetHashCode(Enumeration obj) => HashCode.Combine(obj.Name, obj.Id);

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            FieldInfo[]? fields = typeof(T).GetFields(BindingFlags.Public |
                                                      BindingFlags.Static |
                                                      BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is Enumeration otherValue))
            {
                return false;
            }

            bool typeMatches = GetType() == obj.GetType();
            bool valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        protected bool Equals(Enumeration other) => Name == other.Name && Id == other.Id;

        public override int GetHashCode() => HashCode.Combine(Name, Id);
    }
}
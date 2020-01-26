using FluentAssertions;
using NewTaxi.Infrastructure.Attributes;
using NewTaxi.Infrastructure.Extensions;
using Xunit;

namespace NewTaxi.Infrastructure.Test
{
    public class DocumentEntity_Test
    {
        public class Person
        {
            [PartitionKey]
            public string Sin { get; set; }

            public string Fullname { get; set; }
        }

        [Fact]
        public void Should_Get_Sin_PartitionKey()
        {
            var person = new Person();
            string partitionKeyName = person.GetPartitionKeyName();

            partitionKeyName.Should().Be("Sin", partitionKeyName);
        }
    }
}

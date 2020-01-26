using System;
using System.Collections.Generic;
using System.Text;

namespace NewTaxi.Infrastructure.Attributes
{
    /// <summary>
    /// This attribute is used for reflection
    /// to determine partitionkey for CosmosDB
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PartitionKeyAttribute : Attribute
    {
    }
}

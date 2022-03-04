﻿using System;

namespace Sb.Data.Models.Mongo
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
    public class MongoCollectionAttribute : Attribute
    {
        public string Name { get; private set; }
        public MongoCollectionAttribute(string name)
        {
            Name = name;
        }
    }
}

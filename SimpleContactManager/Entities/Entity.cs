using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Threading;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Threading;

namespace ContactManager.Entities
{
    [NotifyPropertyChanged]
    [ReaderWriterSynchronized]
    [Serializable]
    public abstract class Entity
    {
      

    }
}

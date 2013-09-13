using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PostSharp.Aspects;

namespace ContactManager
{
    [Serializable]
    class TraceAttribute : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            Trace.TraceInformation("Entering {0}.{1}", args.Method.DeclaringType.Name, args.Method.Name);
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            Trace.TraceInformation("Leaving {0}.{1}", args.Method.DeclaringType.Name, args.Method.Name);
        }

        public override void OnException(MethodExecutionArgs args)
        {
            Trace.TraceInformation("Method {0}.{1} failed with {2}", args.Method.DeclaringType.Name, args.Method.Name, args.Exception.GetType().Name);
        }
    }
}

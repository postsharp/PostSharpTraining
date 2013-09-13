using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Extensibility;
using PostSharp.Reflection;
using PostSharp.Serialization;

namespace FreezableSample
{
    [IntroduceInterface(typeof(IFreezable))]
    [PSerializable]
    class FreezableAttribute : InstanceLevelAspect, IFreezable
    {
        [IntroduceMember]
        public bool IsFrozen { get; private set; }

        [IntroduceMember]
        public void Freeze()
        {
            this.IsFrozen = true;
        }

        [OnLocationSetValueAdvice, MulticastPointcut(Targets = MulticastTargets.Field, Attributes = MulticastAttributes.Instance)]
        public void OnFieldSet(LocationInterceptionArgs args)
        {
            if ( this.IsFrozen )
                throw new InvalidOperationException();

            args.ProceedSetValue();
        }
    }

    [PSerializable]
    class RequireFreezableAttribute : LocationLevelAspect, ILocationValidationAspect<object>
    {
        public Exception ValidateValue(object value, string locationName, LocationKind locationKind)
        {
            IFreezable freezable = value as IFreezable;
            if ( freezable != null ) freezable.Freeze();

            return null;
        }
    }
}

using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;
using PostSharp.Serialization;

namespace ContactManager
{
    [PSerializable]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.After, StandardRoles.Threading)]
    public class StatusAttribute : OnMethodBoundaryAspect
    {
        private string text;

        public StatusAttribute(string text)
        {
            this.text = text;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            args.MethodExecutionTag = MainWindow.Instance.SetStatusText(this.text);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            MainWindow.Instance.SetStatusText((string) args.MethodExecutionTag);
        }
    }
}
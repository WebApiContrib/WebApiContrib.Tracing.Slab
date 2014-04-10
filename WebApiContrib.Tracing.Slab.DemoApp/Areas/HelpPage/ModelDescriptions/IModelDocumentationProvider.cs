using System;
using System.Reflection;

namespace WebApiContrib.Tracing.Slab.DemoApp.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}
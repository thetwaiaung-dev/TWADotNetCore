using System.Reflection;

namespace TWADotNetCore.RestApi
{
    public static class Dev
    {
        public static string GetProjectName()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string projectName = assembly.GetName().Name!;

            return projectName;
        }
    }
}

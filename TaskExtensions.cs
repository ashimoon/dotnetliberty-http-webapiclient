using System;
using System.Threading.Tasks;

namespace DotNetLiberty.Http
{
    public static class TaskExtensions
    {
        public static void WaitOrUnwrapException(this Task task)
        {
            try
            {
                task.Wait();
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }
    }
}
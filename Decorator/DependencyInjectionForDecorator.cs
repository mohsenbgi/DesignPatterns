using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core.Resolving.Pipeline;

namespace Decorator
{
    public interface IReportingService
    {
        void Report();
    }

    public class ReportingService : IReportingService
    {
        public void Report()
        {
            Console.WriteLine("Here is your report");
        }
    }

    public class ReportingServiceWithLogging : IReportingService
    {
        private IReportingService decorated;

        public ReportingServiceWithLogging(IReportingService decorated)
        {
            if (decorated == null)
            {
                throw new ArgumentNullException(paramName: nameof(decorated));
            }
            this.decorated = decorated;
        }

        public void Report()
        {
            Console.WriteLine("Commencing log...");
            decorated.Report();
            Console.WriteLine("Ending log...");
        }
    }

    class Program
    {
        void Main()
        {

            var builder = new ContainerBuilder();

            string reportingServiceName = "ReportingService";
            builder.RegisterType<ReportingService>().Named<IReportingService>(reportingServiceName);
            builder.RegisterDecorator<IReportingService>((context, service) => 
                new ReportingServiceWithLogging(service), reportingServiceName
            );

        }

    }
}

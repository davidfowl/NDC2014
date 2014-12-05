using System;
using Microsoft.Framework.OptionsModel;

namespace WebApplication11
{
    public class HelloService : IHelloService
    {
        private readonly SiteOptions _options;
        public HelloService(IOptions<SiteOptions> options)
        {
            _options = options.Options;
            Value++;
        }

        public static int Value { get; set; }

        public string GetHello()
        {
            return "Hello " + _options.Message + " => " + Value;
        }
    }
}
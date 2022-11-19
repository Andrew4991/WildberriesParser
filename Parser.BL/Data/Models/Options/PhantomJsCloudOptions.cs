using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.BL.Data.Models.Options
{
    public class PhantomJsCloudOptions
    {
        public string Url { get; set; }

        public string ApiKey { get; set; }

        public string FullUrl => Url + ApiKey + "/";
    }
}

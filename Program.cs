using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using dashboard.Persistence;
using last_fm_now_playing;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace dashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var playingNow = new NowPlaying("test");
            //var test = playingNow.GetTrackInfo("hafizmakten");

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:9000")
                .Build();
    }
}

using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarantiVp.Test
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class SelfHost : IDisposable
    {
        private static IDisposable wa = null;
        private static SelfHost instance;
        private List<Process> processList = new List<Process>();
        private Dictionary<string, Action<IOwinContext>> MapPathDic = new Dictionary<string, Action<IOwinContext>>();

        public SelfHost()
        {

        }

        public SelfHost(string url)
        {
            URL = url;
        }

        public string URL { get; private set; }

        public static SelfHost Run(string url)
        {
            instance = new SelfHost(url);
            instance.Start();
            return instance;
        }

        public SelfHost Start()
        {
            if (SelfHost.wa != null)
            {
                throw new Exception("Already running.");
            }
            if (string.IsNullOrWhiteSpace(URL))
                throw new ArgumentNullException("URL");
            wa = WebApp.Start<SelfHost>(URL);
            return this;
        }

        public SelfHost Stop()
        {
            wa.Dispose();
            wa = null;
            return this;
        }

        public void Dispose()
        {
            foreach (Process item in processList)
            {
                item.Close();
                item.Dispose();
            }
            processList.Clear();
            Stop();
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();
            //app.UseWelcomePage("/");
            app.Use(new Func<AppFunc, AppFunc>(next => (async env =>
            {
                //Console.WriteLine("Begin Request");
                System.Net.HttpListenerContext c = env.Values.OfType<System.Net.HttpListenerContext>().ToArray().FirstOrDefault();
                var path = c.Request.Url.LocalPath;
                await next.Invoke(env);
                //Console.WriteLine("End Request");
            })));
            app.Use(typeof(DynamicMethodMiddleware));
        }

        public class DynamicMethodMiddleware : OwinMiddleware
        {
            public DynamicMethodMiddleware(OwinMiddleware next) : base(next)
            {
            }

            public async override Task Invoke(IOwinContext context)
            {
                var Path = context.Request.Path.HasValue ? context.Request.Path.Value : "";
                if (SelfHost.instance.MapPathDic.ContainsKey(Path))
                {
                    SelfHost.instance.MapPathDic[Path](context);
                }
                //Console.WriteLine("Begin Request");
                await Next.Invoke(context);
                //Console.WriteLine("End Request");
            }
        }

        public SelfHost Listen(string mapPath, Action<IOwinContext> method)
        {
            if (instance == null)
                throw new Exception("Must be first Run method.");
            if(!instance.MapPathDic.ContainsKey(mapPath))
            {
                instance.MapPathDic.Add(mapPath, method);
            }
            return instance;
        }

        public SelfHost OpenWebClient(string mapPath, bool WaitForExit = true)
        {
            try
            {
                //Edge process is "recycled", therefore no new process is returned.
                Process.Start("microsoft-edge:" + (new Uri(new Uri(URL), mapPath)).ToString());

                //We need to find the most recent MicrosoftEdgeCP process that is active
                Process[] edgeProcessList = Process.GetProcessesByName("MicrosoftEdgeCP");
                Process newestEdgeProcess = null;
                var TimeOut = DateTime.Now.AddSeconds(30);
                while (DateTime.Now < TimeOut)
                {
                    foreach (Process theprocess in edgeProcessList)
                    {
                        if (newestEdgeProcess == null || theprocess.StartTime > newestEdgeProcess.StartTime)
                        {
                            newestEdgeProcess = theprocess;
                            break;
                        }
                    }
                    if (newestEdgeProcess != null)
                        break;
                }
                if (WaitForExit && (newestEdgeProcess != null))
                    newestEdgeProcess.WaitForExit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR OpenWebClient :" + ex.ToString());
                throw;
            }
            return this;
        }

        public static string CreateWebContent(string HTMLContent, string PageTitle)
        {
            var HTML = "<!DOCTYPE>\n"
                + "<html>\n"
                + " <head>\n"
                + "     <title>" + System.Net.WebUtility.HtmlEncode(PageTitle) + "</title>\n"
                + "     <meta charset=\"UTF-8\">\n"
                + "     <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">"
                + "     <link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css\" integrity=\"sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u\" crossorigin=\"anonymous\">"
                + "     <link rel=\"stylesheet\" href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css\" integrity=\"sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp\" crossorigin=\"anonymous\">"
                + "     <script src=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js\" integrity=\"sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa\" crossorigin=\"anonymous\"></script>"
                //+ "     <style type=\"text/css\" >\n"
                //+ "         *, html, body\n"
                //+ "         {\n"
                //+ "             margin:0;\n"
                //+ "             padding:0;\n"
                //+ "             font-family:Tahoma;\n"
                //+ "             font-size:10pt;\n"
                //+ "         }\n"
                //+ "     </style>\n"
                + " </head>\n"
                + " <body style=\"background:white;padding:20px;\">\n"
                + "         <h1 style=\"font-weight:normal;font-size:18pt;margin-top:5pt;border-bottom:1px solid gray;\">" + System.Net.WebUtility.HtmlEncode(PageTitle) + "</h1>\n"
                + "         <div class=\"contaioner\">\n" 
                + HTMLContent + "\n"
                + "         </div>\n"
                + " </body>\n"
                + "</html>\n";
            return HTML;
        }

        public static string CreateWebContent(IDictionary<string, string[]> formDataDic)
        {
            var ret = "";
            if (formDataDic == null)
                throw new ArgumentNullException("formData");
            Debug.WriteLine("INCOMING POSTED DATA");
            foreach (var item in formDataDic)
            {
                Debug.WriteLine("{0}={1}", item.Key, item.Value.FirstOrDefault());
                ret += "\n<div class=\"row\">";
                ret += "\n<div class=\"col-md-2\">";
                ret += System.Net.WebUtility.HtmlEncode(item.Key);
                ret += "\n</div>";
                ret += "\n<div class=\"col-md-10\">";
                ret += System.Net.WebUtility.HtmlEncode(string.Format("{0}", string.Join("\n", item.Value))).Replace("\n", "</br>");
                ret += "\n</div>";
                ret += "\n</div>";
            }
            return ret;
        }
    }
}

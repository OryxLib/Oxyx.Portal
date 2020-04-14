using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tamir.SharpSsh;

namespace Oryx.Linux.Operation
{
    class Program
    {
        static void Main(string[] args)
        {
            var serconfigPaht = AppDomain.CurrentDomain.BaseDirectory + "/config/server.config.json";

            var configPath = AppDomain.CurrentDomain.BaseDirectory + "/config/dbCluster.config.cnf";
            var configStr = File.ReadAllText(configPath);
            

            var serverConfigStr = File.ReadAllTextAsync(serconfigPaht).Result;
            var serverConfigJson = JObject.Parse(serverConfigStr);
            var taskList = new List<Task>();
            foreach (var host in serverConfigJson["host"])
            {
                var taskResult = Task.Run(() =>
                {
                    var shell = new SshShell(host.ToString(), "root", "Adminqwer!@#$");
                    shell.Connect();

                    foreach (var cmd in serverConfigJson["cmd"])
                    {
                        var cmdTxt = cmd.ToString();
                        //Console.WriteLine(cmdTxt);
                        switch (cmdTxt)
                        {
                            case "[writeConfig]":
                                shell.WriteLine($"echo '{configStr}' > /var/lib/mysql-cluster/config.ini");
                                break;
                            default:
                                shell.WriteLine(cmdTxt);
                                break;
                        }

                        //shell.
                    }
                    //shell.WriteLine("rm -rf ./*");
                });
                taskList.Add(taskResult);
            }
            Task.WaitAll(taskList.ToArray());
            Console.Read();
        }
    }
}

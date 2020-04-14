using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Oryx.DbBackup;
using Oryx.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.DataPlatform.Portal.Controllers
{
    public class DbBackupController : Controller
    {
        IConfiguration config;
        private readonly string backupFileDir;
        public DbBackupController(IConfiguration _config)
        {
            config = _config;
            backupFileDir = AppDomain.CurrentDomain.BaseDirectory + "dbbackup";
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Table(DataTableAjaxPostModel model)
        {
            // action inside a standard controller
            var totalResultsCount = 0;
            var filteredResultsCount = 0;

            var fileList = GetBackupFiles().Select(x => new
            {
                path = x
            });
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = fileList
            });
        }


        private string GenerateBackupFileName()
        {
            CreateBackupDir();
            var fileName = Path.Combine(backupFileDir, DateTime.Now.ToString("yyyyMMddhhssmm.sql"));
            return fileName;
        }

        private void CreateBackupDir()
        {
            if (!Directory.Exists(backupFileDir))
            {
                Directory.CreateDirectory(backupFileDir);
            }
        }

        private string[] GetBackupFiles()
        {
            if (!Directory.Exists(backupFileDir))
            {
                return null;
            }
            return Directory.GetFiles(backupFileDir);
        }

        public async Task<IActionResult> SaveBackup()
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                string constring = config["dbConn"];

                string file = GenerateBackupFileName();

                DbBackupTool dbBackupTool = new DbBackupTool();
                dbBackupTool.CreateBackup(constring, file);
            });
            return Json(apiMsg);

        }

        public async Task<IActionResult> Restore(string path)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                string constring = config["dbConn"]; 
                DbBackupTool dbBackupTool = new DbBackupTool();
                dbBackupTool.RestoreDB(constring, path);
            });
            return Json(apiMsg);
        }
        public async Task<IActionResult> Delete(string path)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            });
            return Json(apiMsg);
        }
    }
}
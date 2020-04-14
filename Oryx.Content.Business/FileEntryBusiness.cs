using Microsoft.AspNetCore.Http;
using Oryx.CommonDbOperation;
using Oryx.Content.DataAccessor;
using Oryx.Content.Model;
using Oryx.ViewModel.FileEntry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Content.Business
{
    public class FileEntryBusiness : BaseBusiness<FileEntry>
    {
        public DataFileEntryAccessor dataFileEntryAccessor;

        public const string PublishKey = "文件管理";

        public FileEntryBusiness(DataFileEntryAccessor commonAccessor) : base(commonAccessor)
        {
            dataFileEntryAccessor = commonAccessor;
        }

        public async Task<List<FileEntry>> SaveLocal(IFormFileCollection files, string Tag, string Description)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory + "/Upload/" + Tag;
            CreateDir(basePath);
            var fileEntryList = new List<FileEntry>();
            try
            {
                foreach (var file in files)
                {
                    var localId = Guid.NewGuid();
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = Path.Combine(basePath, localId + extension);

                    using (var stream = new FileStream(fileName, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    fileEntryList.Add(new FileEntry
                    {
                        PhysicalPath = fileName,
                        ActualPath = "/Pic/Get?Id=" + localId,/// Upload/" + Tag + "/" + localId + ".jpg",
                        Id = localId,
                        CreateTime = DateTime.Now,
                        Description = Description,
                        Extension = extension,
                        Tag = Tag,
                        Name = file.Name
                    });
                }
                await dataFileEntryAccessor.AddRange(fileEntryList);
            }
            catch (Exception exc)
            {

            }

            return fileEntryList;
        }

        public async Task<int> GetFileEntryAcount()
        {
            return await dataFileEntryAccessor.Count<FileEntry>(x => x.Tag == PublishKey);
        }

        public async Task<IList<FileEntry>> GetFileEntryList(int pageIndex, int pageSize)
        {
            return await dataFileEntryAccessor.ListOrderByDescendingAsync<FileEntry>(x => x.Tag == PublishKey, pageIndex, pageSize);
        }

        public async Task<byte[]> GetFile(Guid Id)
        {
            var fileEntry = await dataFileEntryAccessor.OneAsync<FileEntry>(x => x.Id == Id);
            if (!File.Exists(fileEntry.PhysicalPath))
            {
                return new byte[] { 0 };
            }
            try
            {
                var fileBytes = await File.ReadAllBytesAsync(fileEntry.PhysicalPath);

                return fileBytes;
            }
            catch (Exception exc)
            {

            }
            return new byte[] { 0 };
        }

        public async Task<FileEntry> AddOrUpdateFileEntry(FileEntry fileEntry)
        {
            var _file = await dataFileEntryAccessor.OneAsync<FileEntry>(x => x.Id == fileEntry.Id);
            if (_file == null)
            {
                _file = new FileEntry
                {
                    Id = fileEntry.Id,
                    PhysicalPath = fileEntry.PhysicalPath,
                    CreateTime = DateTime.Now,
                    Description = fileEntry.Description,
                    Name = fileEntry.Name,
                    Tag = "文件管理"
                };
                await dataFileEntryAccessor.Add(_file);
            }
            else
            {
                _file.Description = fileEntry.Description;
                _file.Name = fileEntry.Name;
                _file.Tag = "文件管理";
                if (!string.IsNullOrEmpty(fileEntry.PhysicalPath))
                {
                    _file.PhysicalPath = fileEntry.PhysicalPath;
                }
                await dataFileEntryAccessor.Update(_file);
            }
            return _file;
        }

        public async Task DeleteFile(Guid id)
        {
            await dataFileEntryAccessor.Delete<FileEntry>(x => x.Id == id);
        }

        public async Task<FileEntry> GetFileEntry(Guid Id)
        {
            return await dataFileEntryAccessor.OneAsync<FileEntry>(x => x.Id == Id);
        }

        private void CreateDir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}

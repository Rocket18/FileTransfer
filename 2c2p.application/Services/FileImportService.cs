using _2c2p.application.Contracts;
using _2c2p.application.Enumerations;
using _2c2p.application.Helpers;
using _2c2p.persistence;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace _2c2p.application.Services
{
    public class FileImportService : IFileImportService
    {
        private readonly DiBiContext _context;

        private readonly Func<FileType, IFileService> _importServiceResolver;

        private readonly IValidationService _validationService;

        public FileImportService(DiBiContext context, Func<FileType, IFileService> importServiceResolver, IValidationService validationService)
        {
            _context = context;
            _importServiceResolver = importServiceResolver;
            _validationService = validationService;
        }

        public async Task Import(IFormFile file)
        {
            if (file == null)
            {
                throw new Exception("No file to import");

            }

            var fileType = FileHelper.GetFileType(file.ContentType);

            var fileService = _importServiceResolver(fileType);

            if (fileService == null)
            {
                throw new Exception("Unknown format");
            }

            var model = await fileService.ExportToModel(file);

            var validationResult = await _validationService.Validate(model);

            if (validationResult.Success) 
            {
                _context.AddRange(model);

                await _context.SaveChangesAsync();
            }
        }  
    }
}

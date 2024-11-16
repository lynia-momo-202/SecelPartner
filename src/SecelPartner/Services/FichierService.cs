using SecelPartner.Core.Entities;

namespace SecelPartner.infrastructure.Services
{
    public class FichierService
    {
        private readonly PathService _pathService;

        public FichierService(PathService pathService)
        {
            _pathService = pathService;
        }

        public async Task<string> UploadAsync(IFormFile? File)
        {
            var uploadsPath = _pathService.GetUpLoadPath();

            var fichierFileName = GetRandomFileName(File.FileName);
            var fichierUploadPath = Path.Combine(uploadsPath, fichierFileName);
            var fs = new FileStream(fichierUploadPath, FileMode.Create);

            await File.CopyToAsync(fs);

            string path = _pathService.GetUpLoadPath(fichierFileName, withWebRootPath: false);

            return path;
        }

        //methode pour la suppression du fichier de lfichier
        public void DeleteUploadFile(string FilePath)
        {
            if (FilePath == null)
                return;
            var fichierPath = _pathService.GetUpLoadPath(Path.GetFileName(FilePath));

            if (File.Exists(fichierPath))
            {
                File.Delete(fichierPath);
            }
        }

        private string GetRandomFileName(string fileName)
        {
            return Guid.NewGuid() + Path.GetExtension(fileName);
        }
    }
}

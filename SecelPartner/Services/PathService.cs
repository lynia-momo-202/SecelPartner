using SecelPartner.infrastructure.Options;

namespace SecelPartner.infrastructure.Services
{
    public class PathService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public PathService(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public string GetUpLoadPath(string? filename = null, bool withWebRootPath = true)
        {
            var pathOptions = new PathOption();
            _configuration.GetSection(PathOption.Path).Bind(pathOptions);
            var uploadPath = pathOptions.File;
            if (null != filename)
                uploadPath = Path.Combine(uploadPath, filename);
            return withWebRootPath ? Path.Combine(_env.WebRootPath, uploadPath) : uploadPath;
        }
    }
}

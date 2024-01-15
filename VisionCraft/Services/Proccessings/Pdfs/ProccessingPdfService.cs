
using VisionCraft.Services.Foundations.Pdfs;

namespace VisionCraft.Services.Proccessings.Pdfs
{
    public class ProccessingPdfService : IProccessingPdfService
    {
        private readonly IPdfService pdfService;

        public ProccessingPdfService(IPdfService pdfService) =>
            this.pdfService = pdfService;

        public async ValueTask<string> ProcReadExtracPdfAsync(Stream pdfFile) =>
            await this.pdfService.ReadExtracPdfAsync(pdfFile);
    }
}

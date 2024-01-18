
using VisionCraft.Services.Foundations.Pdfs;

namespace VisionCraft.Services.Proccessings.Pdfs
{
    public class PdfProccessingService : IPdfProccessingService
    {
        private readonly IPdfService pdfService;

        public PdfProccessingService(IPdfService pdfService) =>
            this.pdfService = pdfService;

        public async ValueTask<string> ProcReadExtracPdfAsync(Stream pdfFile) =>
            await this.pdfService.ReadExtracPdfAsync(pdfFile);
    }
}

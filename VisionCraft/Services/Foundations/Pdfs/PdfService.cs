
using System.Text;
using iText.Kernel.Pdf.Canvas.Parser;
using VisionCraft.Brokers.Loggings;
using VisionCraft.Brokers.Pdfs;

namespace VisionCraft.Services.Foundations.Pdfs
{
    public class PdfService : IPdfService
    {
        private readonly IPdfBroker pdfBroker;
        private readonly ILoggingBroker loggingBroker;

        public PdfService(IPdfBroker pdfBroker, ILoggingBroker loggingBroker)
        {
            this.pdfBroker = pdfBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<string> ReadExtracPdfAsync(Stream pdfFile)
        {
            var pdfDocument = await this.pdfBroker.ReadExtracPdfAsync(pdfFile);

            var pdfText = new StringBuilder();

            for (int pageNum = 1; pageNum <= pdfDocument.GetNumberOfPages(); pageNum++)
            {
                pdfText.Append(PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(pageNum)));
            }

            return pdfText.ToString();
        }
    }
}

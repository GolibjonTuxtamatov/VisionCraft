using iText.Kernel.Pdf;

namespace VisionCraft.Brokers.Pdfs
{
    public class PdfBroker : IPdfBroker
    {
        public async ValueTask<PdfDocument> ReadExtracPdfAsync(Stream pdfFile)
        {
            PdfReader pdfReader = new PdfReader(pdfFile);

            PdfDocument pdfDocument = new PdfDocument(pdfReader);

            return pdfDocument;
        }
    }
}

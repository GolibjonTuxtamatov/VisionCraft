using iText.Kernel.Pdf;
using System.IO;
using OpenAI_API.Chat;
using iText.Kernel.Pdf.Canvas.Parser;

namespace VisionCraft.Brokers.Pdfs
{
    public class PdfBroker : IPdfBroker
    {
        public async ValueTask<PdfDocument> ReadExtracPdfAsync(Stream pdfFile)
        {
            using (PdfReader pdfReader = new PdfReader(pdfFile))
            {
                using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
                {
                    return pdfDocument;
                }
            }
        }
    }
}

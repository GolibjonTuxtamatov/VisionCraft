using iText.Kernel.Pdf;
using OpenAI_API.Chat;

namespace VisionCraft.Brokers.Pdfs
{
    public interface IPdfBroker
    {
        ValueTask<PdfDocument> ReadExtracPdfAsync(Stream pdfFile);
    }
}

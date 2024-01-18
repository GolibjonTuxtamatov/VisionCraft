using iText.Kernel.Pdf;

namespace VisionCraft.Brokers.Pdfs
{
    public interface IPdfBroker
    {
        ValueTask<PdfDocument> ReadExtracPdfAsync(Stream pdfFile);
    }
}

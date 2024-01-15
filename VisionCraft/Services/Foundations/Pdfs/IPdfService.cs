namespace VisionCraft.Services.Foundations.Pdfs
{
    public interface IPdfService
    {
        ValueTask<string> ReadExtracPdfAsync(Stream pdfFile);
    }
}

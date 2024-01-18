namespace VisionCraft.Services.Proccessings.Pdfs
{
    public interface IPdfProccessingService
    {
        ValueTask<string> ProcReadExtracPdfAsync(Stream pdfFile);
    }
}

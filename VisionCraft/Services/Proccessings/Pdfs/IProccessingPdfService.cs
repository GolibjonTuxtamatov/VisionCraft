namespace VisionCraft.Services.Proccessings.Pdfs
{
    public interface IProccessingPdfService
    {
        ValueTask<string> ProcReadExtracPdfAsync(Stream pdfFile);
    }
}

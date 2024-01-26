using VisionCraft.Models.Pdf.Exceptions;

namespace VisionCraft.Services.Foundations.Pdfs
{
    public partial class PdfService
    {
        private static void ValidatePdfNotNull(Stream pdf)
        {
            if (pdf is null)
                throw new NullPdfException();
        }
    }
}

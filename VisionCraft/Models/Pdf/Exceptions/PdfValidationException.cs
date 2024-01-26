using Xeptions;

namespace VisionCraft.Models.Pdf.Exceptions
{
    public class PdfValidationException: Xeption
    {
        public PdfValidationException(Xeption innerException)
            :base("Pdf validation exception error occured,fix the error and try again.",innerException)
        { }
    }
}

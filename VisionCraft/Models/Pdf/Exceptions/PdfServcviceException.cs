using Xeptions;

namespace VisionCraft.Models.Pdf.Exceptions
{
    public class PdfServcviceException : Xeption
    {
        public PdfServcviceException(Xeption innerException)
            : base("Pdf service error occured, contact support.", innerException)
        { }
    }
}

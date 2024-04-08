using Xeptions;

namespace VisionCraft.Models.Pdf.Exceptions
{
    public class NullPdfException : Xeption
    {
        public NullPdfException()
            : base("Pdf is null.")
        { }
    }
}

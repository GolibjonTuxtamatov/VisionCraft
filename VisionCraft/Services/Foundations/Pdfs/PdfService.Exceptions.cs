using VisionCraft.Models.Pdf.Exceptions;
using Xeptions;

namespace VisionCraft.Services.Foundations.Pdfs
{
    public partial class PdfService
    {
        private delegate ValueTask<string> UploadPdfFunction();

        private ValueTask<string> TryCatch(UploadPdfFunction uploadPdfFunction)
        {
            try
            {
                return uploadPdfFunction();
            }
            catch (NullPdfException nullpdfException)
            {
                throw CreateAndLogValidationException(nullpdfException);
            }
        }

        private PdfValidationException CreateAndLogValidationException(Xeption exception)
        {
            var pdfValidationException = new PdfValidationException(exception);
            this.loggingBroker.LogError(pdfValidationException);

            return pdfValidationException;
        }
    }
}

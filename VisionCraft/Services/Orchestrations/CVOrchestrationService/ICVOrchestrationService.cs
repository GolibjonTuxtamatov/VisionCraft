namespace VisionCraft.Services.Orchestrations.CVOrchestrationService
{
    public interface ICVOrchestrationService
    {
        ValueTask UploadCv(Stream pdfFile, string fileName, Guid vacacyId);
    }
}

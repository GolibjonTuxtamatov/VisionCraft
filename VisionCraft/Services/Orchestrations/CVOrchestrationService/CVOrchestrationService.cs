using VisionCraft.Models.CVs;
using VisionCraft.Models.Vacancies;
using VisionCraft.Services.Proccessings.CVs;
using VisionCraft.Services.Proccessings.OpenAIs;
using VisionCraft.Services.Proccessings.Pdfs;
using VisionCraft.Services.Proccessings.Vacancies;

namespace VisionCraft.Services.Orchestrations.CVOrchestrationService
{
    public class CVOrchestrationService : ICVOrchestrationService
    {
        private readonly ICVProccessingService cvProccessingService;
        private readonly IPdfProccessingService pdfProccessingService;
        private readonly IVacancyProccessingService vacancyProccessingService;
        private readonly IOpenAIProccessingService openAIProccessingService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CVOrchestrationService(
            ICVProccessingService cvProccessingService,
            IPdfProccessingService pdfProccessingService,
            IOpenAIProccessingService openAIProccessingService,
            IWebHostEnvironment webHostEnvironment,
            IVacancyProccessingService vacancyProccessingService)
        {
            this.cvProccessingService = cvProccessingService;
            this.pdfProccessingService = pdfProccessingService;
            this.openAIProccessingService = openAIProccessingService;
            this.webHostEnvironment = webHostEnvironment;
            this.vacancyProccessingService = vacancyProccessingService;
        }

        public async ValueTask UploadCv(Stream pdfFile, string fileName, Guid vacancyId)
        {
            string pdfCVText =
                await this.pdfProccessingService.ProcReadExtracPdfAsync(pdfFile);

            Vacancy vacancy =
                await this.vacancyProccessingService.ProcRetrieveVacancyByIdAsync(vacancyId);

            List<string> requests =
                vacancy.Requirements.Select(request => request.Request).ToList();

            requests.Reverse();

            requests.Add(pdfCVText);

            string aiCunclusion =
                await this.openAIProccessingService.ProcEvaluateCVAsync(requests.ToArray());

            var cvId = Guid.NewGuid();
            fileName = cvId + "_" + fileName;
            string filePath = await WriteCVAsync(pdfFile, fileName);

            var cv = new CV
            {
                Id = cvId,
                Name = fileName,
                Conclusion = aiCunclusion,
                CVPath = filePath,
                VacancyId = vacancyId
            };

            await this.cvProccessingService.ProcAddCVAsync(cv);
        }

        public async ValueTask<string> WriteCVAsync(Stream pdfFile, string fileName)
        {

            string webRootPath = webHostEnvironment.WebRootPath;
            string uploadsFolder = Path.Combine(webRootPath, "PDFs");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string filePath = Path.Combine(uploadsFolder, fileName);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                pdfFile.CopyTo(fs);
                fs.Flush();
            }

            return filePath;
        }
    }
}

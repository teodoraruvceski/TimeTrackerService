using TimeTrackerService.Repository.Interfaces;
using TimeTrackerService.Services.Interfaces;
using TimeTrackerService.Models;
using Task = TimeTrackerService.Models.Task;
using System.Reflection.Metadata;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Document = iTextSharp.text.Document;
using System.Globalization;

namespace TimeTrackerService.Services.Implementations
{
    public class TasksService : ITasksService
    {
        IRepository<Task> _repository;
        public TasksService(IRepository<Task> repository)
        {
            _repository = repository;
        }

        public async Task<Task> AddTask(Models.Task task)
        {
            if(task.StartTime != null && task.EndTime != null)
            {
                var startTime2 = new DateTime(task.StartTime.Value.Year, task.StartTime.Value.Month, task.StartTime.Value.Day, task.StartTime.Value.Hour + 2, task.StartTime.Value.Minute, task.StartTime.Value.Second);
                var endTime2 = new DateTime(task.EndTime.Value.Year, task.EndTime.Value.Month, task.EndTime.Value.Day, task.EndTime.Value.Hour + 2, task.EndTime.Value.Minute, task.EndTime.Value.Second);
                task.StartTime = startTime2;
                task.EndTime = endTime2;
            }
            if (task.Name != "undefined")
               return  _repository.Update(task);

            Task newTask = _repository.Create(task);
            newTask.Name = $"Task{newTask.Id}";
            return _repository.Update(newTask);
        }

        private void AddCell(PdfPTable table, string text, bool isHeader = false)
        {
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font font = new Font(bfTimes, 12, isHeader ? Font.BOLD : Font.NORMAL, BaseColor.BLACK);
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.Padding = 5;
            cell.HorizontalAlignment = isHeader ? Element.ALIGN_CENTER : Element.ALIGN_LEFT;
            table.AddCell(cell);
        }

        public async System.Threading.Tasks.Task GenerateReport()
        {
            List<Task> tasks = await  GetAllTasks();
            Document doc = new Document();
            FileStream fs = new FileStream($"Reports/ReportTime_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.pdf", FileMode.Create);
            var writer = PdfWriter.GetInstance(doc, fs);
            
                writer.Open();
                doc.Open();

                Paragraph title = new Paragraph("Task Report", new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                PdfPTable toDoTable = new PdfPTable(4);
                PdfPTable finishedTable = new PdfPTable(4);

                // Define column widths
                float[] columnWidths = new float[] { 2f, 2f, 2f, 1f };
                toDoTable.SetWidths(columnWidths);
                finishedTable.SetWidths(columnWidths);

            //Add headers to both tables
            AddCell(toDoTable, "Task", true);
            AddCell(toDoTable, "Project", true);
            AddCell(toDoTable, "Start Time", true);
            AddCell(toDoTable, "Duration", true);

            AddCell(finishedTable, "Task", true);
            AddCell(finishedTable, "Project", true);
            AddCell(finishedTable, "Start Time", true);
            AddCell(finishedTable, "Duration", true);

            var finishedTasksByDate = tasks
                    .Where(task => task.StartTime != null)
                    .GroupBy(task => task.StartTime.Value.Date)
                    .OrderBy(group => group.Key);

                doc.Add(new Paragraph("Finished Tasks:"));
                doc.Add(new Paragraph(" "));
                foreach (var group in finishedTasksByDate)
                {
                    // Add a section header with the date
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph($"Date: {group.Key.ToShortDateString()}"));
                    doc.Add(new Paragraph(" "));

                    // Create a new table for this date's tasks
                    PdfPTable finishedTableForDate = new PdfPTable(4);
                    finishedTableForDate.SetWidths(columnWidths);
                    AddCell(finishedTableForDate, "Task", true);
                    AddCell(finishedTableForDate, "Project", true);
                    AddCell(finishedTableForDate, "Start Time", true);
                    AddCell(finishedTableForDate, "Duration", true);

                // Add tasks for this date to the table
                foreach (var task in group)
                    {
                        AddCell(finishedTableForDate, task.Name);
                        AddCell(finishedTableForDate, task.ProjectName);
                        AddCell(finishedTableForDate, $"{task.StartTime.Value.ToString("hh:mm tt", CultureInfo.InvariantCulture)} - { task.EndTime?.ToString("hh:mm tt" , CultureInfo.InvariantCulture)}");
                        AddCell(finishedTableForDate, $"{(int)(task.Duration / 3600):00}:{(int)((task.Duration % 3600) / 60):00}");
                    }

                    // Add the table for this date to the document
                    doc.Add(finishedTableForDate);
                }

                // Add tasks to "To Do" section
                foreach (var task in tasks)
                {
                    if (task.StartTime == null)
                    {
                        // To Do task
                        AddCell(toDoTable, task.Name);
                        AddCell(toDoTable, task.ProjectName);
                        AddCell(toDoTable, " ");
                        AddCell(toDoTable, " ");
                    }
                }
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph("To Do Tasks:"));
                doc.Add(new Paragraph(" "));
                doc.Add(toDoTable);
                doc.Close();
                writer.Close();
        }

        public async Task<List<Task>> GetAllTasks()
        {
            return _repository.Get();
        }

        public async Task<Task> StartTask(int id)
        {
            Task task = _repository.Get(id);
            if(task != null)
            {
                task.StartTime = DateTime.Now;
                return _repository.Update(task);
            }
            return task;
        }

    }
}

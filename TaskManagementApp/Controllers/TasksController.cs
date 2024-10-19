using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManagementApp.Models.DataContext;
using TaskManagementApp.Models.Entities;

namespace TaskManagementApp.Controllers
{
    /* Görev kontrolünü sağlayan Controller sınıfı:
     * Fields:
     *          Database: TaskItem tablosunu çeken field
     * 
     * Methods:
     *             Index(TaskItemStatus? statusFilter):   Tüm görevleri veya seçili görevleri belirlenen(varsa) duruma göre gösteren eylem
     *             Create():                              Görev oluşturma sayfasını gösteren eylem
     *   [HttpPost]Create(TaskItemDto taskItemDto):       Yeni görev oluşturma işlemini gerçekleştiren eylem
     *             Edit(int id):                          Görev düzenleme sayfasını gösteren eylem
     *   [HttpPost]Edit(int id, TaskItemDto taskItemDto): Mevcut görevi güncelleme işlemini gerçekleştiren eylem
     *   [HttpPost]ArchiveTaskById(int id, 
     *              TaskItemStatus? statusFilter):        Görevi arşivleme/arşivden çıkarma işlemini gerçekleştiren eylem
     *             Delete(int id):                        Mevcut görevi silen eylem
    */

    [Authorize]
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext context;

        public TasksController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        // Implicitly [HttpGet] is added
        public IActionResult Index(TaskItemStatus? statusFilter)
        {
            // Authenticated user's name info to be viewed
            ViewBag.Name = HttpContext.User.Identity?.Name;
            ViewBag.Status = statusFilter;

            // all tasks or selected task by status.
            var tasks = statusFilter.HasValue
                ? context.Tasks.Where(t => t.Status == statusFilter).ToList()
                : context.Tasks.ToList();

            // options of the task statuses
            var statuses = Enum.GetValues(typeof(TaskItemStatus)).Cast<TaskItemStatus>().ToList();
            var options = new List<SelectListItem>
            {
                new() {
                    Text = "Hepsi",
                    Value = Url.Action("Index", "Tasks", new { statusFilter = "all" }),
                    Selected = (!statusFilter.HasValue || statusFilter.ToString() == "all")
                }
            };
            options.AddRange(statuses.Select(status => new SelectListItem
            {
                Text = status.ToString(),
                Value = Url.Action("Index", "Tasks", new { statusFilter = status.ToString() }),
                Selected = statusFilter?.ToString() == status.ToString()
            }));
            ViewBag.StatusOptions = options;

            return View(tasks);
        }

        // Implicitly [HttpGet] is added
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskItemDto taskItemDto)
        {

            if (string.IsNullOrEmpty(taskItemDto.Name))
            {
                ModelState.AddModelError("Name", "İsim zorunlu");
            }

            if (taskItemDto.DueDate != null && taskItemDto.DueDate < DateTime.Now)
            {
                ModelState.AddModelError("DueDate", "Geçmiş bir tarih olamaz. Lütfen bugünden sonraki bir tarih giriniz.");
            }

            if (!ModelState.IsValid)
            {
                return View(taskItemDto);
            }

            var task = new TaskItem()
            {
                Name = taskItemDto.Name,
                Status = taskItemDto.Status,
                Description = taskItemDto.Description,
                DueDate = taskItemDto.DueDate
            };

            context.Tasks.Add(task);
            context.SaveChanges();

            return RedirectToAction("Index", "Tasks");
        }

        // Implicitly [HttpGet] is added
        public IActionResult Edit(int id)
        {
            var taskFromDb = context.Tasks.Find(id);
            if (taskFromDb == null)
            {
                return RedirectToAction("Index", "Tasks");
            }

            var taskItemDto = new TaskItemDto()
            {
                Name = taskFromDb.Name,
                Description = taskFromDb.Description,
                Status = taskFromDb.Status,
                DueDate = taskFromDb.DueDate
            };

            ViewData["TaskId"] = taskFromDb.Id;

            return View(taskItemDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, TaskItemDto taskItemDto)
        {
            var taskFromDb = context.Tasks.Find(id);
            if (taskFromDb == null)
            {
                return RedirectToAction("Index", "Tasks");
            }

            if (string.IsNullOrEmpty(taskItemDto.Name))
            {
                ModelState.AddModelError("Name", "İsim zorunlu");
            }

            if (taskItemDto.DueDate != null && taskItemDto.DueDate < DateTime.Now)
            {
                ModelState.AddModelError("DueDate", "Geçmiş bir tarih olamaz. Lütfen bugünden sonraki bir tarih giriniz.");
            }

            if (!ModelState.IsValid)
            {
                ViewData["TaskId"] = taskFromDb.Id;
                return View(taskItemDto);
            }

            // update the taskFromDb properties with the new taskItemDto properties
            taskFromDb.Name = taskItemDto.Name;
            taskFromDb.Description = taskItemDto.Description;
            taskFromDb.Status = taskItemDto.Status;
            taskFromDb.DueDate = taskItemDto.DueDate;

            context.SaveChanges();

            return RedirectToAction("Index", "Tasks");
        }

        [HttpPost]
        public IActionResult ArchiveTaskById(int id, TaskItemStatus? statusFilter)
        {
            var taskFromDb = context.Tasks.Find(id);
            if (taskFromDb != null)
            {
                taskFromDb.IsArchived = !taskFromDb.IsArchived;
                context.SaveChanges();
            }

            return RedirectToAction("Index", "Tasks", new { statusFilter });
        }

        public IActionResult Delete(int id)
        {
            var taskFromDb = context.Tasks.Find(id);
            if (taskFromDb == null)
            {
                return RedirectToAction("Index", "Tasks");
            }

            context.Tasks.Remove(taskFromDb);
            context.SaveChanges(true);
            return RedirectToAction("Index", "Tasks");
        }

    }
}

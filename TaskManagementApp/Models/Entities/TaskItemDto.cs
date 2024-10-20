using System.ComponentModel.DataAnnotations;

namespace TaskManagementApp.Models.Entities
{
    /* Görev veri aktarım nesnesi (Data to Object) sınıfı(Exposed Task Object):
     * Fields(Props):
     *              Görev adı(zorunlu-Hata mesajı eklendi), 
     *              Görev açıklaması(isteğe bağlı)
     *              Görev durumu(zorunlu)
     *              Görev bitiş tarihi(isteğe bağlı)
     *              Görevin arşivlenmiş özelliğini gösteren veri(zorunlu)
    */
    public class TaskItemDto
    {

        [Required(ErrorMessage = "İsim Zorunlu"), MaxLength(100)]
        public string Name { get; set; } = "";

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public TaskItemStatus Status { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        public bool IsArchived { get; set; }
    }
}

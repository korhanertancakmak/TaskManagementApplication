using System.ComponentModel.DataAnnotations;

namespace TaskManagementApp.Models.Entities
{
    /* Görev sınıfı(Internal Task object):
     * Fields(Props): 
     *              Eşsiz(unique) kimlik, 
     *              Görev adı(zorunlu), 
     *              Görev açıklaması(isteğe bağlı)
     *              Görev durumu(zorunlu)
     *              Görev bitiş tarihi(isteğe bağlı)
     *              Görevin arşivlenmiş özelliğini gösteren veri(zorunlu)
    */
    public class TaskItem
    {

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = "";

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public TaskItemStatus Status { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        public bool IsArchived { get; set; }

    }

    // Görev durumları enum sınıfı
    public enum TaskItemStatus
    {
        Bekliyor,
        DevamEdiyor,
        Tamamlandi
    }
}

using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        
        [Required (ErrorMessage = "Bu Alan Boş Bırakılamaz")]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }
        
        [Display(Name = "Görüntüleme Sırası")]
        [DeniedValues(0, ErrorMessage = "Sıfır Değeri Kullanılamaz")]
        public int DisplayOrder { get; set; }
    }
}

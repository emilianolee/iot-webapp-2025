using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;     // DB스키마 정의 클래스

namespace WebApiApp01.Models
{
    public class Todoitem
    {
        [Key]
        public int Id { get; set; }

        [Required]  // Not Null 일 경우 string에 ?(Nullable)을 삭제할 것
        [Column(TypeName = "VARCHAR(100)")] // 이거 사용안하면 컬럼이 LONGTEXT로 생성됨
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "CHAR(8)")]  // 20250610
        public string TodoDate { get; set; }

        // boolean
        public bool IsComplete { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
namespace L012020MR602.Models
{
    public class motoristas
    {
        [Key]
        public int motoristaId { get; set; }
        public string nombreMotoristo { get; set; }

    }
}

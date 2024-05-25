using System.ComponentModel.DataAnnotations;

namespace ModalBlazorX.Models;

public class Video
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string YTId { get; set; }
    public string Group { get; set; }
    public DateTime CreateDate { get; set; }
}

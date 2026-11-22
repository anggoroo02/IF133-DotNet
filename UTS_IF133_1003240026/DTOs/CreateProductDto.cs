using System.ComponentModel.DataAnnotations;

namespace UTS_IF133_1003240026.DTOs;

public class CreateProductDto
{
    [Required(ErrorMessage = "Code wajib diisi.")]
    [MaxLength(20, ErrorMessage = "Code maksimal 20 karakter.")]
    public string Code { get; set; } = string.Empty;

    [Required(ErrorMessage = "Name wajib diisi.")]
    [MaxLength(100, ErrorMessage = "Name maksimal 100 karakter.")]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Price harus lebih besar dari 0.")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock tidak boleh negatif.")]
    public int Stock { get; set; }

    public bool IsActive { get; set; } = true;
}
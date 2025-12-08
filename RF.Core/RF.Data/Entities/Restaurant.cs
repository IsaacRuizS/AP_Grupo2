using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RF.Data
{
    [MetadataType(typeof(RestaurantMetadata))]
    public partial class Restaurant
    {
        public class RestaurantMetadata
        {
            [Required(ErrorMessage = "El usuario es obligatorio.")]
            [Display(Name = "Usuario")]
            public int UserID { get; set; }

            [Required(ErrorMessage = "El nombre es obligatorio.")]
            [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
            [Display(Name = "Nombre")]
            public string Name { get; set; }

            [Required(ErrorMessage = "La descripción es obligatoria.")]
            [StringLength(300)]
            [Display(Name = "Descripción")]
            public string Description { get; set; }

            [Required(ErrorMessage = "La dirección es obligatoria.")]
            [StringLength(200)]
            [Display(Name = "Dirección")]
            public string Address { get; set; }

            [Required(ErrorMessage = "El teléfono es obligatorio.")]
            [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "El teléfono debe tener 8 dígitos.")]
            [Display(Name = "Teléfono")]
            public string Phone { get; set; }

            [Required(ErrorMessage = "El correo es obligatorio.")]
            [EmailAddress(ErrorMessage = "Debe ingresar un correo válido.")]
            [Display(Name = "Correo electrónico")]
            public string Email { get; set; }

            [Display(Name = "Sitio web")]
            [RegularExpression(@".*", ErrorMessage = "Debe ingresar una URL válida.")]
            public string Website { get; set; }

            [Display(Name = "Enlace de Waze")]
            public string WazeLink { get; set; }

            [Display(Name = "Enlace de Google Maps")]
            public string GoogleMapsLink { get; set; }

            [Display(Name = "Latitud")]
            public decimal? Latitude { get; set; }

            [Display(Name = "Longitud")]
            public decimal? Longitude { get; set; }

            [Range(0, 5, ErrorMessage = "La calificación debe estar entre 0 y 5.")]
            [Display(Name = "Calificación")]
            public decimal? Rating { get; set; }

            [Display(Name = "Activo")]
            public bool? IsActive { get; set; }

            [Display(Name = "Creado en")]
            public DateTime? CreatedAt { get; set; }

            [Display(Name = "Actualizado en")]
            public DateTime? UpdatedAt { get; set; }

            [Display(Name = "Imagen del restaurante")]
            [RegularExpression(@".*", ErrorMessage = "Debe ingresar una URL válida.")]
            public string ImageUrl { get; set; }
        }
    }
}

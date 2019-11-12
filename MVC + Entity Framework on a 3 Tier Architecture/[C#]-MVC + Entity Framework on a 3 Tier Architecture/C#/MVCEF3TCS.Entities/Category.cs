//-----------------------------------------------------------------------
// <copyright file="Category.cs" company="Edson Castañeda">
//     Copyright © Edson Castañeda. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MVCEF3TCS.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Category")]
        public string Description { get; set; }
    }
}
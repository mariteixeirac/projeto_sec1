﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sec.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        public Usuario()
        {
            Preferencias = new List<Preferencia>();
        }
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo e-mail é obrigatório!")]
        [MaxLength(120, ErrorMessage = "O e-mail não poderá ultrapassar 120 caracteres!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Nick é obrigatório!")]
        [MaxLength(50, ErrorMessage = "O nick não poderá ultrapassar 50 caracteres!")]
        public string Nick { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório!")]
        public string Senha { get; set; }

        public byte[] Foto { get; set; }


        public virtual List<Preferencia> Preferencias { get; set; }
    }
}
﻿using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adı boş geçilemez");
            RuleFor(x => x.WriterSurName).NotEmpty().WithMessage("Yazar soyadı boş geçilemez");
            RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("En az iki karakter olmalıdır");
            RuleFor(x => x.WriterSurName).MaximumLength(50).WithMessage("En fazla elli karakter olmalıdır");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkında kısmını boş bırakamazsınız.");
            RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Ünvan kısmını boş bırakamazsınız.");

            //Ödev
            RuleFor(x=>x.WriterAbout).Must(x=> x!=null && x.Contains('a')).WithMessage("Hakkında kısmında a harfi zorunludur.");
        }

    }
}

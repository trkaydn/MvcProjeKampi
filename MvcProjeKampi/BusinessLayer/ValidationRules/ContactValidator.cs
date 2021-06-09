using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.UserMail).NotEmpty().WithMessage("Mail adresini boş geçemezsiniz");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu adı boş geçilemez");
            RuleFor(x => x.UserMail).NotEmpty().WithMessage("Kullanıcı adı boş geçilemez");
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("En az üç karakter olmalıdır");
            RuleFor(x => x.UserMail).MinimumLength(3).WithMessage("En az üç karakter olmalıdır");
            RuleFor(x => x.Subject).MaximumLength(50).WithMessage("En fazla elli karakter olmalıdır");
        }
    }
}

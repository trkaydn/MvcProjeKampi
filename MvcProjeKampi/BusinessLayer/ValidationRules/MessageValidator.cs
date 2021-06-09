using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Alıcı adresi boş geçilemez");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu boş geçilemez");
            RuleFor(x => x.MessageContent).NotEmpty().WithMessage("Mesaj boş geçilemez");
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("En az 3 karakter olmalıdır");
            RuleFor(x => x.Subject).MaximumLength(100).WithMessage("En fazla 100 karakter olmalıdır");

            //ödev
            RuleFor(x => x.ReceiverMail).EmailAddress().WithMessage("Doğru bir mail adresi girin");

        }
    }
}

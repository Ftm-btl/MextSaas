using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Common.Translations
{
    public static class CommonTranslationKeys
    {
        //General Keys
        public static string GeneralValidationExceptionMessage => nameof(GeneralValidationExceptionMessage);
        public static string GeneralServerExceptionMessage => nameof(GeneralServerExceptionMessage);

        //UserAuth Key
        public static string UserAuthRegisterSucceededMessage => nameof(UserAuthRegisterSucceededMessage);
        public static string UserAuthVerifyEmailSucceededMessage => nameof(UserAuthVerifyEmailSucceededMessage);

        public static string EmailVerificationSubject => nameof(EmailVerificationSubject);
        public static string EmailVerificationButtonText => nameof(EmailVerificationButtonText);
        public static string EmailVerificationContent => nameof(EmailVerificationContent);
    }
}

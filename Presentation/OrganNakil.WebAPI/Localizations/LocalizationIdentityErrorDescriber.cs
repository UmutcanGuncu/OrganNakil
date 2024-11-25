using System;
using Microsoft.AspNetCore.Identity;

namespace OrganNakil.WebAPI.Localizations
{
	public class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
	{
        public override IdentityError DuplicateUserName(string userName){ return new() { Code = "DuplicateUserName", Description = $"{userName} Kullanılmaktadır. Lütfen Başka Bir Tc Kimlik Numarası Giriniz" };}
        public override IdentityError DuplicateEmail(string email) {return new() { Code = "DuplicateEmail", Description = $"{email} Kullanılmaktadır. Lütfen Başka Bir E Posta Adresi Seçiniz" };}
        public override IdentityError DefaultError() { return new () { Code = nameof(DefaultError), Description = $"Bilinmeyen Bir Hata Oluştu" }; }
        public override IdentityError ConcurrencyFailure() { return new() { Code = nameof(ConcurrencyFailure), Description = "Nesne Üzerinde Değişiklik Yapılmış" }; }
        public override IdentityError PasswordMismatch() { return new() { Code = nameof(PasswordMismatch), Description = "Hatalı Şifre" }; }
        public override IdentityError InvalidToken() { return new() { Code = nameof(InvalidToken), Description = "Invalid token." }; }
        public override IdentityError LoginAlreadyAssociated() { return new() { Code = nameof(LoginAlreadyAssociated), Description = "Kullanıcı Halihazırda Oturum Açmış" }; }
        public override IdentityError InvalidUserName(string userName) { return new() { Code = nameof(InvalidUserName), Description = $"'{userName}' Geçersiz. Kullanıcı Adınız Yalnızca Harf veya Rakam İçerebilir" }; }
        public override IdentityError InvalidEmail(string email) { return new() { Code = nameof(InvalidEmail), Description = $"'{email}' Geçersiz." }; }
        public override IdentityError InvalidRoleName(string role) { return new() { Code = nameof(InvalidRoleName), Description = $"Role name '{role}' is invalid." }; }
        public override IdentityError DuplicateRoleName(string role) { return new () { Code = nameof(DuplicateRoleName), Description = $" '{role}' Kullanılmaktadır. Lütfen Başka Bir Rol Adı Seçiniz." }; }
        public override IdentityError UserAlreadyHasPassword() { return new () { Code = nameof(UserAlreadyHasPassword), Description = "User already has a password set." }; }
        public override IdentityError UserLockoutNotEnabled() { return new () { Code = nameof(UserLockoutNotEnabled), Description = "Lockout is not enabled for this user." }; }
        public override IdentityError UserAlreadyInRole(string role) { return new () { Code = nameof(UserAlreadyInRole), Description = $"Kullanıcının Halihazırda'{role}' Sahip." }; }
        public override IdentityError UserNotInRole(string role) { return new () { Code = nameof(UserNotInRole), Description = $"User is not in role '{role}'." }; }
        public override IdentityError PasswordTooShort(int length) { return new () { Code = nameof(PasswordTooShort), Description = $"Passwords must be at least {length} characters." }; }
        public override IdentityError PasswordRequiresNonAlphanumeric() { return new () { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "Şifreniz En Az Bir Tane Özel Karakter (!, @, # vb...) İçermelidir" }; }
        public override IdentityError PasswordRequiresDigit() { return new () { Code = nameof(PasswordRequiresDigit), Description = "Şifreniz En Az Bir Tane Rakam ('0'-'9')  İçermelidir" }; }
        public override IdentityError PasswordRequiresLower() { return new () { Code = nameof(PasswordRequiresLower), Description = "Şifreniz En Az Bir Tane Küçük Harf ('a'-'z') İçermelidir." }; }
        public override IdentityError PasswordRequiresUpper() { return new () { Code = nameof(PasswordRequiresUpper), Description = "Şifreniz En Az Bir Tane Büyük Harf ('A'-'Z') İçermelidir" }; }
    }

}


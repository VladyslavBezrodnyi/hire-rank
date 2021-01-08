export class PhoneRegex {
  public static Regex = new RegExp(/^([0-9]{3})([ -]?)([0-9]{3})\2([0-9]{4})$/, 'i');
}

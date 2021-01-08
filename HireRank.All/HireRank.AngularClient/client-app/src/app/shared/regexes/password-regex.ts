export class PasswordRegex {
  public static Regex = new RegExp(/^(.*\d.*[a-z].*)|(.*[a-z].*\d.*)$/, 'i');
}

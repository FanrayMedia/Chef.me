using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Chef
{
    /// <summary>
    /// String Extension Methods.
    /// 
    /// Guideline:
    /// 1. All methods should be generic use only, carries little or no biz logic.
    /// 2. The method is used at lease twice somewhere in the project.
    /// 2. Methods must not have any side effects.
    /// 
    /// All other please put into WebHelper.
    /// </summary>
	public static class StringExtensionMethods
	{
		// -------------------------------------------------------------------- returns string

		public static string FormatWith(this String s, params object[] ps)
		{
			return string.Format(s, ps);
		}

		/// <summary>
		/// "àåáâäãåą" returns "aaaaaaaa".  Used by FormatSlug
		/// </summary>
		/// <remarks>
		/// The normalization to FormD splits accented letters in accents+letters 
		/// the rest removes those accents (and other non-spacing characters). 
        /// 
        /// One difference between form C and form D is how letters with accents 
        /// are represented: form C uses a single letter-with-accent codepoint, 
        /// while form D separates that into a letter and an accent.
        /// http://stackoverflow.com/questions/3288114/what-does-nets-string-normalize-do/// </remarks>
		public static string RemoveAccents(this String s)
		{
			if (s.IsNullOrWhiteSpace()) return s;

			return new string(s
				.Normalize(NormalizationForm.FormD)
				.ToCharArray()
				.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
				.ToArray());
		}

		public static string RemoveExtraHyphen(this string text)
		{
			if (text.Contains("--"))
			{
				text = text.Replace("--", "-");
				return RemoveExtraHyphen(text);
			}

			return text;
		}

        /// <summary>
        /// force string to be maxlen or smaller
        /// </summary>
        public static string Truncate(this string s, int maxLength)
        {
            if (s.IsNullOrEmpty()) return s;
            return (s.Length > maxLength) ? s.Remove(maxLength) : s;
        }

        public static string TruncateWithEllipsis(this string s, int maxLength)
        {
            if (s.IsNullOrEmpty()) return s;
            if (s.Length <= maxLength) return s;

            return string.Format("{0}...", Truncate(s, maxLength - 3));
        }

		// -------------------------------------------------------------------- returns bool


        /// <summary>
        /// Same as - 
        /// return String.IsNullOrEmpty(value) || value.Trim().Length == 0;
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
		public static bool IsNullOrWhiteSpace(this String s)
		{
			return String.IsNullOrWhiteSpace(s);
		}

		public static bool LengthOver(this String s, int len)
		{
			return s.Length > len;
		}

		public static bool LengthBelow(this String s, int len)
		{
			return s.Length < len;
		}

		public static bool NonAlphanumericCharBelow(this String s, int count)
		{
			int c = NonAlphanumericCharCount(s);
			return c < count;
		}

		public static bool IsAllowedFileExtension(this string fileName)
		{
			if (!fileName.Contains(".")) return false;

			if (Regex.IsMatch(Path.GetExtension(fileName), ".jpg|.jpeg|.png|.gif",
				RegexOptions.IgnoreCase))
				return true;
			return false;
		}

        /// <summary>
        /// returns true if this looks like a semi-valid email address
        /// </summary>
        public static bool IsEmailAddress(this string s)
        {
            return !s.IsNullOrWhiteSpace()
                       ? Regex.IsMatch(s, @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.IgnoreCase)
                       : false;
        }

        /// <summary>
        /// returns true if this looks like a semi-valid openid string; it starts with "=@+$!(" or contains a period.
        /// </summary>
        public static bool IsOpenId(this string s)
        {
            return !s.IsNullOrWhiteSpace() ? Regex.IsMatch(s, @"^[=@+$!(]|.*?\.") : false;
        }

		// -------------------------------------------------------------------- private

		/// <summary>
		/// Returns the number of non-alphanumerica chars found in the string.
		/// </summary>
		private static int NonAlphanumericCharCount(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return 0;
			}

			int count = 0;
			for (int i = 0; i < str.Length; i++)
			{
				if (!char.IsLetterOrDigit(str, i))
				{
					count++;
				}
			}

			return count;
		}


	}
}
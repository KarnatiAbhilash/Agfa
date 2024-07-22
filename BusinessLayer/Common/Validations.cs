using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CSharp;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Common
{
    public class Validations
    {
        public enum Validation
        {
            NoBlank = 0,
            NoSpaceSpecialChar = 1,
            NoNumeric = 2,
            NoSpecialChar = 3,
            NoNumAllowSpecialChar = 4,
            NoChar = 5,
            Decimal = 6,
            Email = 7,
            StarSpecialCharAllowed = 8,
            HyphenSpecialCharAllowed = 9,
            HyphenHashSpecialCharAllowed = 10,
            Length = 11,
            PhoneNumber = 12,
            Name = 13,
            Address = 14,
            City = 15,
            ZipCode = 16,
            Latitude = 17,
            FaxNumber = 18,
            Hours = 19,
            CommaSpecialCharAllowed = 20,
            DotSpecialCharAllowed = 21,
            UnderScoreAllowed = 22,
            JobNumber = 23,
            FirstCharCap=24
        };

        /// <summary>
        /// Validate the inputes based on the conditions.
        /// </summary>
        /// <param name="strLabelName"></param>
        /// <param name="strInputText"></param>
        /// <param name="intCondition"></param>
        /// <param name="strOption"></param>
        /// <returns></returns>
        public static string ValidateInput(String labelName, string inputText, int[] condition, params string[] option)
        {
            int noofCondition;
            string result = string.Empty;

            for (noofCondition = 0; noofCondition <= condition.Length - 1; noofCondition++)
            {
                switch (condition[noofCondition])
                {

                    case (int)Validation.NoSpaceSpecialChar:
                        if (inputText.Trim() == "")
                        {
                            result = "Please Enter " + labelName + ".";
                        }
                        else if (!new Regex("^[a-zA-Z0-9]*$").IsMatch(inputText))
                        {
                            result = labelName + " Cannot Have Space Or Special Character(s).";
                        }
                        break;
                    case (int)Validation.NoSpecialChar:

                        if (!new Regex("^[0-9a-zA-Z\\s][a-zA-Z0-9\\s]*$").IsMatch(inputText) && inputText != "")
                        {
                            result = "Please Remove Special Character(s) From " + labelName + ".";
                        }
                        break;
                    case (int)Validation.NoBlank:
                        if (inputText.Trim() == "")
                        {
                            result = "Please Enter " + labelName + ".";
                        }
                        break;


                    case (int)Validation.NoChar:
                        if (!new Regex("^[0-9][0-9\\s]*$").IsMatch(inputText) && inputText != "" || inputText != "" && !(new Regex("^[0-9]*$").IsMatch(inputText)))
                        {
                            result = "Please Enter Only Numeric Values For " + labelName + ".";
                        }
                        break;

                    case (int)Validation.NoNumAllowSpecialChar:
                        if (!new Regex("^[a-zA-Z\\s]*$").IsMatch(inputText))
                        {
                            result = "Please Remove Number(s) From " + labelName + ".";
                        }
                        break;
                    case (int)Validation.Decimal:
                        if (!new Regex("^[0-9\\-][0-9.]*$").IsMatch(inputText) && (!new Regex("^[0-9][0-9\\s]*$").IsMatch(inputText)))
                        {
                            result = labelName + " Should Have Only Numeric Data.";
                        }
                        else if (inputText.IndexOf(".") != -1)
                            if (inputText.Substring(inputText.IndexOf(".") + 1).Length > 2)
                            {
                                result = labelName + "  Can Have Only Two Decimal Digits.";
                            }
                        break;


                    case (int)Validation.Email:
                        if (!new Regex("^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$").IsMatch(inputText))
                        {
                            result = "Please Enter Valid " + labelName + ".";
                        }
                        break;

                    case (int)Validation.NoNumeric:
                        if (!new Regex("^[a-zA-Z*&%$#@!)(\\s]*$").IsMatch(inputText))
                        {
                            result = "Please Remove Number(s) From " + labelName + ".";
                        }
                        break;

                    case (int)Validation.StarSpecialCharAllowed:
                        if (!new Regex("^[0-9a-zA-Z*\\s][a-zA-Z0-9*\\s]*$").IsMatch(inputText))
                        {
                            result = "Please Remove Special Character(s) From " + labelName + ".";
                        }
                        break;

                    case (int)Validation.HyphenSpecialCharAllowed:
                        if (!new Regex("^[0-9a-zA-Z-\\s][a-zA-Z0-9-\\s]*$").IsMatch(inputText))
                        {
                            result = "Please Remove Special Character(s) From " + labelName + ".";
                        }
                        break;

                    case (int)Validation.HyphenHashSpecialCharAllowed:
                        if (!new Regex("^[0-9|0-9-0-9][0-9a-zA-Z-#\\s][a-zA-Z0-9-#\\s]*$").IsMatch(inputText))
                        {
                            result = "Please Enter Valid " + labelName + ".";
                        }
                        break;

                    case (int)Validation.Length:
                        if (inputText.Length > Int32.Parse(option[0]))
                        {
                            result = labelName + " Length Cannot Be Greater Than " + option[0] + " .";
                        }
                        break;

                    case (int)Validation.PhoneNumber:
                        //if (inputText != "" && (!new Regex("^(\\+[1-9][0-9]*(\\([0-9]*\\)|-[0-9]*-))?[0]?[1-9][0-9\\- ]*$").IsMatch(inputText)))
                        //{
                        //    result = "Please Enter Valid " + labelName + ".";
                        //}
                        if (inputText != "" && (!new Regex("^((\\+\\d{1,3}(-| )?\\(?\\d\\)?(-| )?\\d{1,3})|(\\(?\\d{2,3}\\)?))(-| )?(\\d{3,4})(-| )?(\\d{4})(( x| ext)\\d{1,5}){0,1}$").IsMatch(inputText)))
                        {
                            result = "Please Enter Valid " + labelName + ".";
                        }
                        break;

                    case (int)Validation.Name:
                        if (!new Regex("^[a-zA-Z-'\\.\\s]{2,128}$").IsMatch(inputText))
                        {
                            result = "Please Enter Valid " + labelName + ".";
                        }
                        break;

                    case (int)Validation.Address:
                        if (!new Regex("^[0-9a-zA-Z-#\\s][a-zA-Z0-9-#\\s]*$").IsMatch(inputText))
                        {
                            result = "Please Enter Valid " + labelName + ".";
                        }
                        break;

                    case (int)Validation.ZipCode:
                        if (!new Regex("^(\\d{5}-\\d{4}|\\d{5}|\\d{9})$").IsMatch(inputText))
                        {
                            result = "Please Enter Valid " + labelName + ".";
                        }
                        break;

                    case (int)Validation.City:
                        if (!new Regex("^[a-zA-Z-'\\.\\s]{2,128}$").IsMatch(inputText))
                        {
                            result = "Please Enter Valid " + labelName + ".";
                        }
                        break;

                    case (int)Validation.Latitude:
                        if (!new Regex("^-{0,1}\\d*\\.{0,1}\\d+$").IsMatch(inputText))
                        {
                            result = "Please Enter Valid " + labelName + ".";
                        }
                        break;
                    case (int)Validation.FaxNumber:
                        if (!new Regex("^[0-9-()+\\s][0-9-()\\s]*$").IsMatch(inputText))
                        {
                            result = "Please Enter Valid " + labelName + ".";
                        }
                        break;
                    case (int)Validation.Hours:
                        if (!new Regex("^[0-9a-zA-Z-:\\.\\s][a-zA-Z0-9-:\\.\\s]*$").IsMatch(inputText))
                        {
                            result = "Please Remove Special Character(s) From " + labelName + ".";
                        }
                        break;
                    case (int)Validation.CommaSpecialCharAllowed:
                        if (!new Regex("^[0-9a-zA-Z\\,\\s][a-zA-Z0-9\\,\\s]*$").IsMatch(inputText))
                        {
                            result = "Please Remove Special Character(s) From " + labelName + ".";
                        }
                        break;
                    case (int)Validation.DotSpecialCharAllowed:
                        if (!new Regex("^[a-zA-Z\\.\\s]*$").IsMatch(inputText))
                        {
                            result = "Please Remove Special Character(s) From " + labelName + ".";
                        }
                        break;
                    case (int)Validation.UnderScoreAllowed:
                        if (!new Regex("^[a-zA-Z\\ _ \\s]*$").IsMatch(inputText))
                        {
                            result = "Please Remove Special Character(s) From " + labelName + ".";
                        }
                        break;
                    case (int)Validation.JobNumber:
                        int i = 0;
                        if (inputText.Length < 7)
                        {
                            result = labelName + " Cannot Be Less Than Seven Characters. ";
                        }
                        if (inputText.Length > 7)
                        {
                            result = labelName + " Cannot Be Greater Than Seven Characters. ";
                        }
                        if (inputText.Length == 7)
                        {
                            for (i = 0; i <= 2; i++)
                            {
                                if (!new Regex("[a-zA-Z]+").IsMatch(inputText[i].ToString()))
                                {
                                    result = labelName + " Please Enter Starting Three Characters Has Alphabet. ";
                                    return result;
                                }
                            }
                            if (i == 3)
                            {
                                for (i = 3; i <= 6; i++)
                                {
                                    if (!new Regex("[0-9]+").IsMatch(inputText[i].ToString()))
                                    {
                                        result = labelName + " Please Enter Last Four Characters Has Number. ";
                                        return result;
                                    }

                                }
                            }
                        }
                        break;
                    case (int)Validation.FirstCharCap:
                        if (inputText.Trim() == "")
                        {
                            result = "Please Enter " + labelName + ".";
                            break;
                        }
                        if (!new Regex("/^[A - Z][a - z] * (?: [A - Z][a - z] *) *$/").IsMatch(inputText))
                        {
                            result = labelName + " Cannot Have First  Letter Small (0r) Starting CHaracter must Captialize(s).";
                            break;
                        }
                        break;

                }

                if (result != "") return result; else continue;

            }
            return result;

        }
    }

}





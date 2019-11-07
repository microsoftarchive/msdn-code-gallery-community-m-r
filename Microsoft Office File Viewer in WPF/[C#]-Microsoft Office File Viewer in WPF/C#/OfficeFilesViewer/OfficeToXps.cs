using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Word = Microsoft.Office.Interop.Word;
namespace OfficeFilesViewer
{
    public class OfficeToXps
    {
        #region Properties & Constants
        private static List<string> wordExtensions = new List<string>
        {
            ".doc",
            ".docx"
        };

        private static List<string> excelExtensions = new List<string>
        {
            ".xls",
            ".xlsx"
        };

        private static List<string> powerpointExtensions = new List<string>
        {
            ".ppt",
            ".pptx"
        };

        #endregion

        #region Public Methods
        public static OfficeToXpsConversionResult ConvertToXps(string sourceFilePath, ref string resultFilePath)
        {
            var result = new OfficeToXpsConversionResult(ConversionResult.UnexpectedError);

            // Check to see if it's a valid file
            if (!IsValidFilePath(sourceFilePath))
            {
                result.Result = ConversionResult.InvalidFilePath;
                result.ResultText = sourceFilePath;
                return result;
            }



            var ext = Path.GetExtension(sourceFilePath).ToLower();

            // Check to see if it's in our list of convertable extensions
            if (!IsConvertableFilePath(sourceFilePath))
            {
                result.Result = ConversionResult.InvalidFileExtension;
                result.ResultText = ext;
                return result;
            }

            // Convert if Word
            if (wordExtensions.Contains(ext))
            {
                return ConvertFromWord(sourceFilePath, ref resultFilePath);
            }

            // Convert if Excel
            if (excelExtensions.Contains(ext))
            {
                return ConvertFromExcel(sourceFilePath, ref resultFilePath);
            }

            // Convert if PowerPoint
            if (powerpointExtensions.Contains(ext))
            {
                return ConvertFromPowerPoint(sourceFilePath, ref resultFilePath);
            }

            return result;
        }
        #endregion

        #region Private Methods
        public static bool IsValidFilePath(string sourceFilePath)
        {
            if (string.IsNullOrEmpty(sourceFilePath))
                return false;

            try
            {
                return File.Exists(sourceFilePath);
            }
            catch (Exception)
            {
            }

            return false;
        }

        public static bool IsConvertableFilePath(string sourceFilePath)
        {
            var ext = Path.GetExtension(sourceFilePath).ToLower();

            return IsConvertableExtension(ext);
        }
        public static bool IsConvertableExtension(string extension)
        {
            return wordExtensions.Contains(extension) ||
                   excelExtensions.Contains(extension) ||
                   powerpointExtensions.Contains(extension);
        }

        private static string GetTempXpsFilePath()
        {
            return Path.ChangeExtension(Path.GetTempFileName(), ".xps");
        }


        private static OfficeToXpsConversionResult ConvertFromWord(string sourceFilePath, ref string resultFilePath)
        {
            object pSourceDocPath = sourceFilePath;
            string pExportFilePath = string.IsNullOrWhiteSpace(resultFilePath) ? GetTempXpsFilePath() : resultFilePath;

            try
            {
                var pExportFormat = Word.WdExportFormat.wdExportFormatXPS;
                bool pOpenAfterExport = false;
                var pExportOptimizeFor = Word.WdExportOptimizeFor.wdExportOptimizeForOnScreen;
                var pExportRange = Word.WdExportRange.wdExportAllDocument;
                int pStartPage = 0;
                int pEndPage = 0;
                var pExportItem = Word.WdExportItem.wdExportDocumentContent;
                var pIncludeDocProps = true;
                var pKeepIRM = true;
                var pCreateBookmarks = Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
                var pDocStructureTags = true;
                var pBitmapMissingFonts = true;
                var pUseISO19005_1 = false;


                Word.Application wordApplication = null;
                Word.Document wordDocument = null;

                try
                {
                    wordApplication = new Word.Application();
                }
                catch (Exception exc)
                {
                    return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToInitializeOfficeApp, "Word", exc);
                }

                try
                {
                    try
                    {
                        wordDocument = wordApplication.Documents.Open(ref pSourceDocPath);
                    }
                    catch (Exception exc)
                    {
                        return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToOpenOfficeFile, exc.Message, exc);
                    }

                    if (wordDocument != null)
                    {
                        try
                        {
                            wordDocument.ExportAsFixedFormat(
                                                pExportFilePath,
                                                pExportFormat,
                                                pOpenAfterExport,
                                                pExportOptimizeFor,
                                                pExportRange,
                                                pStartPage,
                                                pEndPage,
                                                pExportItem,
                                                pIncludeDocProps,
                                                pKeepIRM,
                                                pCreateBookmarks,
                                                pDocStructureTags,
                                                pBitmapMissingFonts,
                                                pUseISO19005_1
                                            );
                        }
                        catch (Exception exc)
                        {
                            return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToExportToXps, "Word", exc);
                        }
                    }
                    else
                    {
                        return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToOpenOfficeFile);
                    }
                }
                finally
                {
                    // Close and release the Document object.
                    if (wordDocument != null)
                    {
                        wordDocument.Close();
                        wordDocument = null;
                    }

                    // Quit Word and release the ApplicationClass object.
                    if (wordApplication != null)
                    {
                        wordApplication.Quit();
                        wordApplication = null;
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
            catch (Exception exc)
            {
                return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToAccessOfficeInterop, "Word", exc);
            }

            resultFilePath = pExportFilePath;

            return new OfficeToXpsConversionResult(ConversionResult.OK, pExportFilePath);
        }

        private static OfficeToXpsConversionResult ConvertFromPowerPoint(string sourceFilePath, ref string resultFilePath)
        {
            string pSourceDocPath = sourceFilePath;

            string pExportFilePath = string.IsNullOrWhiteSpace(resultFilePath) ? GetTempXpsFilePath() : resultFilePath;

            try
            {
                PowerPoint.Application pptApplication = null;
                PowerPoint.Presentation pptPresentation = null;

                try
                {
                    pptApplication = new PowerPoint.Application();
                }
                catch (Exception exc)
                {
                    return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToInitializeOfficeApp, "PowerPoint", exc);
                }

                try
                {
                    try
                    {
                        pptPresentation = pptApplication.Presentations.Open(pSourceDocPath,
                                                                            Microsoft.Office.Core.MsoTriState.msoTrue,
                                                                            Microsoft.Office.Core.MsoTriState.msoTrue,
                                                                            Microsoft.Office.Core.MsoTriState.msoFalse);
                    }
                    catch (Exception exc)
                    {
                        return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToOpenOfficeFile, exc.Message, exc);
                    }

                    if (pptPresentation != null)
                    {
                        try
                        {
                            pptPresentation.ExportAsFixedFormat(
                                                pExportFilePath,
                                                PowerPoint.PpFixedFormatType.ppFixedFormatTypeXPS,
                                                PowerPoint.PpFixedFormatIntent.ppFixedFormatIntentScreen,
                                                Microsoft.Office.Core.MsoTriState.msoFalse,
                                                PowerPoint.PpPrintHandoutOrder.ppPrintHandoutVerticalFirst,
                                                PowerPoint.PpPrintOutputType.ppPrintOutputSlides,
                                                Microsoft.Office.Core.MsoTriState.msoFalse,
                                                null,
                                                PowerPoint.PpPrintRangeType.ppPrintAll,
                                                string.Empty,
                                                true,
                                                true,
                                                true,
                                                true,
                                                false
                                            );
                        }
                        catch (Exception exc)
                        {
                            return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToExportToXps, "PowerPoint", exc);
                        }
                    }
                    else
                    {
                        return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToOpenOfficeFile);
                    }
                }
                finally
                {
                    // Close and release the Document object.
                    if (pptPresentation != null)
                    {
                        pptPresentation.Close();
                        pptPresentation = null;
                    }

                    // Quit Word and release the ApplicationClass object.
                    if (pptApplication != null)
                    {
                        pptApplication.Quit();
                        pptApplication = null;
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
            catch (Exception exc)
            {
                return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToAccessOfficeInterop, "PowerPoint", exc);
            }

            resultFilePath = pExportFilePath;

            return new OfficeToXpsConversionResult(ConversionResult.OK, pExportFilePath);
        }

        private static OfficeToXpsConversionResult ConvertFromExcel(string sourceFilePath, ref string resultFilePath)
        {
            string pSourceDocPath = sourceFilePath;

            string pExportFilePath = string.IsNullOrWhiteSpace(resultFilePath) ? GetTempXpsFilePath() : resultFilePath;

            try
            {
                var pExportFormat = Excel.XlFixedFormatType.xlTypeXPS;
                var pExportQuality = Excel.XlFixedFormatQuality.xlQualityStandard;
                var pOpenAfterPublish = false;
                var pIncludeDocProps = true;
                var pIgnorePrintAreas = true;


                Excel.Application excelApplication = null;
                Excel.Workbook excelWorkbook = null;

                try
                {
                    excelApplication = new Excel.Application();
                }
                catch (Exception exc)
                {
                    return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToInitializeOfficeApp, "Excel", exc);
                }

                try
                {
                    try
                    {
                        excelWorkbook = excelApplication.Workbooks.Open(pSourceDocPath);
                    }
                    catch (Exception exc)
                    {
                        return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToOpenOfficeFile, exc.Message, exc);
                    }

                    if (excelWorkbook != null)
                    {
                        try
                        {
                            excelWorkbook.ExportAsFixedFormat(
                                                pExportFormat,
                                                pExportFilePath,
                                                pExportQuality,
                                                pIncludeDocProps,
                                                pIgnorePrintAreas,

                                                OpenAfterPublish: pOpenAfterPublish
                                            );
                        }
                        catch (Exception exc)
                        {
                            return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToExportToXps, "Excel", exc);
                        }
                    }
                    else
                    {
                        return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToOpenOfficeFile);
                    }
                }
                finally
                {
                    // Close and release the Document object.
                    if (excelWorkbook != null)
                    {
                        excelWorkbook.Close();
                        excelWorkbook = null;
                    }

                    // Quit Word and release the ApplicationClass object.
                    if (excelApplication != null)
                    {
                        excelApplication.Quit();
                        excelApplication = null;
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
            catch (Exception exc)
            {
                return new OfficeToXpsConversionResult(ConversionResult.ErrorUnableToAccessOfficeInterop, "Excel", exc);
            }

            resultFilePath = pExportFilePath;

            return new OfficeToXpsConversionResult(ConversionResult.OK, pExportFilePath);
        }
        #endregion
    }

    public class OfficeToXpsConversionResult
    {
        #region Properties
        public ConversionResult Result { get; set; }
        public string ResultText { get; set; }
        public Exception ResultError { get; set; }
        #endregion

        #region Constructors
        public OfficeToXpsConversionResult()
        {
            Result = ConversionResult.UnexpectedError;
            ResultText = string.Empty;
        }
        public OfficeToXpsConversionResult(ConversionResult result)
            : this()
        {
            Result = result;
        }
        public OfficeToXpsConversionResult(ConversionResult result, string resultText)
            : this(result)
        {
            ResultText = resultText;
        }
        public OfficeToXpsConversionResult(ConversionResult result, string resultText, Exception exc)
            : this(result, resultText)
        {
            ResultError = exc;
        }
        #endregion
    }

    public enum ConversionResult
    {
        OK = 0,
        InvalidFilePath = 1,
        InvalidFileExtension = 2,
        UnexpectedError = 3,
        ErrorUnableToInitializeOfficeApp = 4,
        ErrorUnableToOpenOfficeFile = 5,
        ErrorUnableToAccessOfficeInterop = 6,
        ErrorUnableToExportToXps = 7
    }
}
